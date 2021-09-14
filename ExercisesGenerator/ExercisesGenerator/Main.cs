using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyTools;

namespace ExercisesGenerator
{
    public partial class Main : Form
    {
        int MIN;

        int MAX;

        private HashSet<String> exercises;

        private HashSet<String> postfixNotations;

        private Dictionary<String, int> answers;
        public Main()
        {
            InitializeComponent();
            ShowDefault();
        }
        void ShowDefault()
        {
            textBox1.Text = Settings.Default.ProblemNumber.ToString();
            textBox2.Text = Settings.Default.OperatorNumber.ToString();
            textBox3.Text = Settings.Default.MIN.ToString();
            textBox4.Text = Settings.Default.MAX.ToString();
            radioButton1.Checked = Settings.Default.AllowParentheses;
            radioButton2.Checked = !Settings.Default.AllowParentheses;
        }

        private void SaveSettings()
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("请输入题目个数！");
                return;
            }
            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("请输入运算符个数！");
            }
            Settings.Default.ProblemNumber = int.Parse(textBox1.Text);
            Settings.Default.OperatorNumber = int.Parse(textBox2.Text);
            Settings.Default.MIN = !String.IsNullOrWhiteSpace(textBox3.Text) ?
                int.Parse(textBox3.Text) : int.MinValue + 1;
            Settings.Default.MAX = !String.IsNullOrWhiteSpace(textBox4.Text) ?
                int.Parse(textBox4.Text) : int.MaxValue - 1;
            Settings.Default.AllowParentheses = radioButton1.Checked;
            Settings.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
            MessageBox.Show("成功保存");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*Settings.Default.ProblemNumber = 100;
            Settings.Default.OperatorNumber = 1;
            Settings.Default.MIN = 0;
            Settings.Default.MAX = 100;
            Settings.Default.AllowParentheses = false;
            Settings.Default.Save();*/
            Settings.Default.Reset();
            ShowDefault();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveSettings();
            GetExercisesAndAnswers();
            ShowExercisesAndAnswers();
            try
            {
                SaveExercisesAndAnswers();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", "Exercises.txt");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", "Answers.txt");
        }

        private List<Unit> GetUnits()
        {
            List<Unit> units = new List<Unit>();
            if (Settings.Default.AllowPlus)
            {
                units.Add(new Unit(UnitType.Operator, 2));
            }
            if (Settings.Default.AllowSubscribe)
            {
                units.Add(new Unit(UnitType.Operator, 3));
            }
            if (Settings.Default.AllowMultiply)
            {
                units.Add(new Unit(UnitType.Operator, 4));
            }
            if (Settings.Default.AllowDivide)
            {
                units.Add(new Unit(UnitType.Operator, 5));
            }
            if (Settings.Default.AllowPower)
            {
                units.Add(new Unit(UnitType.Operator, 6));
            }
            return units;
        }

        private void GetExercisesAndAnswers()
        {
            MIN = Settings.Default.MIN;
            MAX = Settings.Default.MAX;
            Random random = new Random();
            List<Unit> operators = GetUnits();
            exercises = new HashSet<string>();
            postfixNotations = new HashSet<string>();
            answers = new Dictionary<string, int>();
            for (int i = 0; i < Settings.Default.ProblemNumber; i++)
            {
                List<Unit> infix = new List<Unit>();
                int left = -1;
                int right = -1;
                if (Settings.Default.AllowParentheses)
                {
                    left = random.Next(0, Settings.Default.OperatorNumber);
                    right = random.Next(left + 1, Settings.Default.OperatorNumber + 1);
                    if (left == 0 && right == Settings.Default.OperatorNumber)
                    {
                        left = right = -1;
                    }
                }
                for (int j = 0; j < Settings.Default.OperatorNumber; j++)
                {
                    if (left == j)
                    {
                        infix.Add(new Unit(UnitType.Operator, 0));
                    }
                    infix.Add(new Unit(UnitType.Number, random.Next(MIN, MAX)));
                    if (right == j)
                    {
                        infix.Add(new Unit(UnitType.Operator, 1));
                    }
                    infix.Add(operators[random.Next(0, operators.Count)]);
                }
                infix.Add(new Unit(UnitType.Number, random.Next(MIN, MAX)));
                if (right == Settings.Default.OperatorNumber)
                {
                    infix.Add(new Unit(UnitType.Operator, 1));
                }
                String infixNotation = Tools.ListToString(infix);
                List<Unit> postfix = Tools.InfixToPostfix(infix);
                String postNotation = Tools.ListToString(postfix);
                if (!postfixNotations.Add(postNotation))
                {
                    i--;
                    continue;
                }
                int? value = Tools.CalculatePostfix(postfix, MIN, MAX);
                if (value == null)
                {
                    i--;
                    continue;
                }
                exercises.Add(infixNotation);
                answers.Add(infixNotation, (int)value);
            }
        }

        private void ShowExercisesAndAnswers()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < exercises.Count; i++)
            {
                stringBuilder.Append((i + 1).ToString() + ": " +
                    exercises.ElementAt(i) + "= " +
                    answers[exercises.ElementAt(i)] + Environment.NewLine);
            }
            textBox5.Text = stringBuilder.ToString();
        }

        private void SaveExercisesAndAnswers()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("Exercises.txt"))
                {
                    for (int i = 0; i < exercises.Count; i++)
                    {
                        writer.WriteLine((i + 1).ToString() + ". " +
                            exercises.ElementAt(i) + "= ");
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("文件保存失败！");
            }
            try
            {
                using (StreamWriter writer = new StreamWriter("Answers.txt"))
                {
                    for (int i = 0; i < exercises.Count; i++)
                    {
                        writer.WriteLine((i + 1).ToString() + ". " +
                            answers[exercises.ElementAt(i)]);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("文件保存失败！");
            }
        }
    }
}
