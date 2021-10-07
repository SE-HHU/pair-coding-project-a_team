using PWA.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools;
using static PWA.Shared.Models.TableData;
using System.Text;
using Newtonsoft.Json;

namespace PWA.Shared.Componets
{
    public partial class MyGenerator
    {
        [Parameter]
        public TableData Exercises { get; set; }
        
        [Parameter]
        public WebSettings LocalSettings { get; set; }

        public static bool ShowAnswer = true;

        public String DisplayStatue = "隐藏答案";

        protected async override Task<Task> OnInitializedAsync()
        {
            await LoadLocalSettings();
            //加载设置, 防止在未设置的情况下生成习题出错
            LocalSettings.SetSettings();

            return Task.CompletedTask;
        }

        /// <summary>
        /// 生成题目及答案
        /// </summary>
        private void Generate()
        {
            List<Exercise> exercises = new List<Exercise>();
            //习题
            HashSet<String> expressions = new HashSet<string>();
            //用于去重的HashSet

            Expression.FillCanUseOperators();

            for (int i = 1; i <= Settings.ProblemsNumber; i++)
            {
                List<Unit> infix = Expression.GetRandomExpression();
                List<Unit> postfix = Expression.InfixToPostfix(infix);

                Unit answer;
                try
                {
                    answer = Expression.CalculatePostfix(postfix);
                }
                catch
                {
                    i--;
                    continue;
                }

                Node root = Expression.PostfixToTree(postfix);
                String tree = root.ToString();
                if (expressions.Add(tree))
                {
                    exercises.Add(new TableData.Exercise(i, infix, answer));
                }//没有重复
                else
                {
                    i--;
                }//存在重复
            }
            Exercises.Exercises = exercises;
            Exercises.Expressions = expressions;
        }

        /// <summary>
        /// 打印习题
        /// </summary>
        private async void Print()
        {
            await JS.InvokeVoidAsync("PrintDiv", "showdiv");
        }

        /// <summary>
        /// 隐藏或显示答案
        /// </summary>
        private async void ChangeAnswersDisplay()
        {
            ShowAnswer = !ShowAnswer;
            DisplayStatue = (ShowAnswer ? "隐藏答案" : "显示答案");
            await JS.InvokeVoidAsync("ChangeDisplay", "showexercises", 2, (ShowAnswer ? "" : "none"));
        }

        /// <summary>
        /// 保存习题和答案到文件
        /// </summary>
        private async void SaveFiles()
        {
            StringBuilder Problems = new StringBuilder();
            StringBuilder Answers = new StringBuilder();
            foreach (Exercise exercise in Exercises.Exercises)
            {
                Problems.Append(exercise.Number);
                Problems.Append(". ");
                Problems.Append(Tools.Expression.ExpressionToString(exercise.Problem));
                Problems.Append('\n');
                Answers.Append(exercise.Number);
                Answers.Append(". ");
                Answers.Append(exercise.Answer.ToString());
                Answers.Append('\n');
            }
            await JS.InvokeVoidAsync("Save", Problems.ToString(), "Exercises.txt");
            await JS.InvokeVoidAsync("Save", Answers.ToString(), "Answers.txt");
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
    }
}
