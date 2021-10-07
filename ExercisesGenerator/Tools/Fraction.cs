using System;

namespace Tools
{
    public class Fraction
    {
        public long Numerator;

        public long Denomination;

        private static readonly Random random = new Random();

        public Fraction()
        {
            Numerator = 1;
            Denomination = 1;
        }

        public Fraction(long numerator, long denomination)
        {
            Numerator = numerator;
            Denomination = denomination;
        }

        /// <summary>
        /// 计算两数的最大公约数
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public static long GCD(long number1, long number2)
        {
            return (number2 == 0) ? number1
                : GCD(number2, number1 % number2);
        }

        /// <summary>
        /// 分数约分
        /// </summary>
        public void Reduce()
        {
            if (Numerator == 0)
            {
                Denomination = 1;

                return;
            }
            long gcd = GCD(Numerator, Denomination);
            Numerator /= gcd;
            Denomination /= gcd;
            if (Denomination < 0)
            {
                Numerator *= -1;
                Denomination *= -1;
            }
        }

        public static Fraction operator + (Fraction fraction1, Fraction fraction2)
        {
            Fraction result = new Fraction(
                fraction1.Numerator * fraction2.Denomination
                + fraction1.Denomination * fraction2.Numerator,
                fraction1.Denomination * fraction2.Denomination);
            result.Reduce();

            return result;
        }

        public static Fraction operator - (Fraction fraction1, Fraction fraction2)
        {
            Fraction result = new Fraction(
                fraction1.Numerator * fraction2.Denomination
                - fraction1.Denomination * fraction2.Numerator,
                fraction1.Denomination * fraction2.Denomination);
            result.Reduce();

            return result;
        }

        public static Fraction operator * (Fraction fraction1, Fraction fraction2)
        {
            Fraction result = new Fraction(
                fraction1.Numerator * fraction2.Numerator,
                fraction1.Denomination * fraction2.Denomination);
            result.Reduce();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fraction1"></param>
        /// <param name="fraction2"></param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException">除零错误</exception>
        public static Fraction operator / (Fraction fraction1, Fraction fraction2)
        {
            if (fraction2.Numerator == 0)
            {
                throw new DivideByZeroException();
            }
            Fraction result = new Fraction(
                fraction1.Numerator * fraction2.Denomination,
                fraction1.Denomination * fraction2.Numerator);
            result.Reduce();

            return result;
        }

        /// <summary>
        /// 获取随机真分数
        /// </summary>
        /// <returns>随机生成的真分数</returns>
        public static Fraction GetRandomFraction()
        {
            int denomination = random.Next(1, Settings.DenominationMaximum + 1);
            int numerator = random.Next(0, denomination);
            Fraction fraction = new Fraction(numerator, denomination);

            return fraction;
        }

        public override string ToString()
        {
            this.Reduce();
            if (Math.Abs(Numerator) > Math.Abs(Denomination))
            {
                long numerator = Numerator % Denomination;
                long integer = Numerator / Denomination;
                if (numerator < 0)
                {
                    numerator *= -1;
                }

                return integer + "U_" + numerator + "~" + Denomination;
            }//分子大于分母
            else if (Math.Abs(Numerator) < Math.Abs(Denomination))
            {
                return "_" + Numerator + "~" + Denomination;
            }//分子小于分母
            else
            {
                return "1";
            }//分子等于分母
        }

        /// <summary>
        /// 输出Fraction的HTML形式
        /// </summary>
        /// <returns>Fraction的HTML形式</returns>
        public string ToHTML()
        {
            this.Reduce();
            if (Math.Abs(Numerator) > Math.Abs(Denomination))
            {
                long numerator = Numerator % Denomination;
                long integer = Numerator / Denomination;
                if (numerator < 0)
                {
                    numerator *= -1;
                }

                return integer + "<span class=\"texthidden\">U_</span><sup>"
                    + numerator + "</sup>~<sub>" + Denomination + "</sub>";
            }//分子大于分母
            else if (Math.Abs(Numerator) < Math.Abs(Denomination))
            {
                return "<span class=\"texthidden\">_</span><sup>"
                    + Numerator + "</sup>~<sub>" + Denomination + "</sub>";
            }//分子小于分母
            else
            {
                return "1";
            }//分子等于分母
        }
    }
}
