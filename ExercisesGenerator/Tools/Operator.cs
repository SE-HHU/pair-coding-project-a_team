using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class Operator
    {
        public char Value { get; set; }

        public int Number { get; set; }

        public int Propriety { get; set; }

        public Operator()
        {
        }

        public Operator(char value, int number, int propriety)
        {
            Value = value;
            Number = number;
            Propriety = propriety;
        }

    }
}
