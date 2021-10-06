using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class Operator
    {
        public char Value;

        public int Number;

        public int Propriety;

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
