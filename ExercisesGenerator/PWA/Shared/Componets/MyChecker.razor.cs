using PWA.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tools;
using static PWA.Shared.Models.CheckData;

namespace PWA.Shared.Componets
{
    public partial class MyChecker
    {
        [Parameter]
        public CheckData Exercises { get; set; }

        private bool HasChecked;

        public String ProblemsText;
        public String AnswersText;

        //检查结果
        private String Text;

        public String[] Problems;
        public String[] Answers;

        private WebSettings LocalSettings = new WebSettings();

        protected async override Task<Task> OnInitializedAsync()
        {
            await LoadLocalSettings();
            LocalSettings.SetSettings();

            return Task.CompletedTask;
        }

        /// <summary>
        /// 加载习题文件
        /// </summary>
        /// <param name="e"></param>
        private async void LoadProblemsFile(InputFileChangeEventArgs e)
        {
            HasChecked = false;
            var file = e.File;
            ProblemsText = await new StreamReader(file.OpenReadStream(file.Size + 1)).ReadToEndAsync();
        }

        /// <summary>
        /// 加载答案文件
        /// </summary>
        /// <param name="e"></param>
        private async void LoadAnswersFile(InputFileChangeEventArgs e)
        {
            HasChecked = false;
            var file = e.File;
            AnswersText = await new StreamReader(file.OpenReadStream(file.Size + 1)).ReadToEndAsync();
        }

        /// <summary>
        /// 对输入的文件进行检查
        /// </summary>
        private void Check()
        {
            WebSettings.RemoveSettings();
            //修改设置, 防止运算过程中因为超出范围导致异常

            Exercises.Exercises = new List<Checker>();
            Exercises.Expressions = new Dictionary<string, Checker>();
            Exercises.Correct = new List<Checker>();
            Exercises.Wrong = new List<Checker>();
            Exercises.Repeat = new Dictionary<Checker, Checker>();
            //重置对象

            ProblemsText = Regex.Replace(ProblemsText, @"\s+$", "");
            ProblemsText = Regex.Replace(ProblemsText, @"(\n$)+", "");
            AnswersText = Regex.Replace(AnswersText, @"\s+$", "");
            AnswersText = Regex.Replace(AnswersText, @"(\n$)+", "");
            //规格化, 去除行末空白以及空行

            Problems = ProblemsText.Split("\n");
            Answers = AnswersText.Split("\n");

            Exercises.Exercises = new List<Checker>();
            for (int i = 0, j = 0; i < Problems.Length; i++, j++)
            {
                int number = int.Parse(Regex.Match(Problems[i], @"(\d+)\.\s{0,}").Groups[1].Value);
                //题号
                String OriginalProblem = Regex.Replace(Problems[i], @"(\d+)\.\s{0,}", "");
                //题目
                String OriginalAnswer = Regex.Replace(Answers[j], @"(\d+)\.\s{0,}", "");
                //答案
                Exercises.Exercises.Add(new Checker(number, OriginalProblem, OriginalAnswer));
            }
            for (int i = 0; i < Exercises.Exercises.Count; i++)
            {
                List<Unit> postfix = Exercises.Exercises[i].Problem;

                Unit Answer = new Unit();

                try
                {
                    Answer = Expression.CalculatePostfix(postfix);
                }
                catch (Exception e)
                {
                    ShowMessage(e.ToString());
                    ShowMessage(Exercises.Exercises[i].OriginalProblem);
                    ShowMessage(Expression.ExpressionToString(postfix));
                }

                if (Answer.CompareTo(Exercises.Exercises[i].Answer) != 0)
                {
                    Exercises.Wrong.Add(Exercises.Exercises[i]);
                }//答案不匹配
                else
                {
                    Exercises.Correct.Add(Exercises.Exercises[i]);
                }//答案匹配

                //重复判断
                String tree = Expression.PostfixToTree(postfix).ToString();
                if (Exercises.Expressions.ContainsKey(tree))
                {
                    Exercises.Repeat.Add(Exercises.Exercises[i], Exercises.Expressions[tree]);
                }//重复
                else
                {
                    Exercises.Expressions.Add(tree, Exercises.Exercises[i]);
                }//不重复
            }

            WriteToString();
            LocalSettings.SetSettings();
            //恢复设置
            HasChecked = true;
        }

        /// <summary>
        /// 生成检查结果, 以字符串形式保存到 Text 属性中
        /// </summary>
        private void WriteToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            //正确部分
            stringBuilder.Append("Correct: ");
            stringBuilder.Append(Exercises.Correct.Count);
            stringBuilder.Append(" (");
            foreach (Checker checker in Exercises.Correct)
            {
                stringBuilder.Append(checker.Number);
                stringBuilder.Append(", ");
            }
            if (Exercises.Correct.Count != 0)
            {
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                //去除尾部多余字符
            }//不空才需要去除
            stringBuilder.Append(")\n");

            //错误部分
            stringBuilder.Append("Wrong: ");
            stringBuilder.Append(Exercises.Wrong.Count);
            stringBuilder.Append(" (");
            foreach (Checker checker in Exercises.Wrong)
            {
                stringBuilder.Append(checker.Number);
                stringBuilder.Append(", ");
            }
            if (Exercises.Wrong.Count != 0)
            {
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                //去除尾部多余字符
            }
            stringBuilder.Append(")\n");

            //重复部分
            List<KeyValuePair<Checker, Checker>> Repeat = Exercises.Repeat.ToList();
            //对Dictionary进行排序
            Repeat.Sort((pair1, pair2) => pair1.Value.Number.CompareTo(pair2.Value.Number));
            stringBuilder.Append("Repeat: ");
            stringBuilder.Append(Repeat.Count);
            stringBuilder.Append("\nRepeatDetail: \n");
            for (int i = 0; i < Repeat.Count; i++)
            {
                stringBuilder.Append('(');
                stringBuilder.Append(i + 1);
                stringBuilder.Append(")\t");
                stringBuilder.Append(Repeat[i].Value.Number);
                stringBuilder.Append(',');
                stringBuilder.Append(Repeat[i].Value.OriginalProblem);
                stringBuilder.Append("\tRepeat\t");
                stringBuilder.Append(Repeat[i].Key.Number);
                stringBuilder.Append(',');
                stringBuilder.Append(Repeat[i].Key.OriginalProblem);
                stringBuilder.Append('\n');
            }

            Text = stringBuilder.ToString();
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        private async void SaveFiles()
        {
            if (!HasChecked)
            {
                Check();
            }
            await JS.InvokeVoidAsync("Save", Text, "Grade.txt");
        }
        
        /// <summary>
        /// 重置为默认设置
        /// </summary>
        private void SetDefault()
        {
            LocalSettings.SetDefault();
            SaveLocalSettings();
        }

        /// <summary>
        /// 弹窗显示消息
        /// </summary>
        /// <param name="text">需要显示的消息</param>
        private async void ShowMessage(String text)
        {
            await JS.InvokeVoidAsync("ShowMessage", text);
        }

        /// <summary>
        /// 加载 Local Storage
        /// </summary>
        /// <returns></returns>
        private async Task<Task> LoadLocalSettings()
        {
            String json = await JS.InvokeAsync<String>("BlazorGetLocalStorage", "LocalSettings");
            WebSettings newSettings;
            try
            {
                newSettings = JsonConvert.DeserializeObject<WebSettings>(json);
                if (newSettings != null)
                {
                    LocalSettings.CopyFrom(newSettings);
                }
            }//应对首次使用时不存在 Local Storage 的情况
            catch
            {
                ShowMessage("未检测到本地设置, 已为您加载默认配置");
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 保存设置到 Local Storage
        /// </summary>
        private async void SaveLocalSettings()
        {
            await JS.InvokeVoidAsync("BlazorSetLocalStorage", "LocalSettings", JsonConvert.SerializeObject(LocalSettings));
            LocalSettings.SetSettings();
        }
    }
}
