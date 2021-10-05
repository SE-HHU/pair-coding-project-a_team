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

namespace PWA.Shared.Componets
{
    public partial class MyGenerator
    {
        [Parameter]
        public TableData Exercises { get; set; }

        public static bool ShowAnswer = true;

        public String DisplayStatue = "隐藏答案";

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
            await JS.InvokeVoidAsync("ChangeDisplay", "showexercises", 2, (ShowAnswer ? "" : "none"));
            DisplayStatue = (ShowAnswer ? "显示答案" : "隐藏答案");
            ShowAnswer = !ShowAnswer;
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
    }
}
