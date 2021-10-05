using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools;

namespace PWA.Shared.Models
{
    public class TableData
    {
        public class Exercise{
            public int Number;
            //题号
            public List<Unit> Problem;
            //题目
            public Unit Answer;
            //答案

            public Exercise()
            {
            }

            public Exercise(int number, List<Unit> problem, Unit answer)
            {
                Number = number;
                Problem = problem;
                Answer = answer;
            }
        }

        public List<Exercise> Exercises = new List<Exercise>();
        //习题集
        public HashSet<String> Expressions = new HashSet<string>();
        //查重
    }
}
