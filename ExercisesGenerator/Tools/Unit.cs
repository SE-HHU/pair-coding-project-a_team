using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public enum UnitType
    {
        [Description("整数")]
        Integer,
        [Description("分数")]
        Fraction,
        [Description("运算符")]
        Operator,
    }

    public class Unit
    {
        public UnitType UnitType;

        public Fraction Fraction;

        public Operator Operator;

        private static readonly Random random = new Random();

        public Unit()
        {
        }

        public Unit(UnitType unitType, Fraction fraction, Operator @operator)
        {
            UnitType = unitType;
            Fraction = fraction;
            Operator = @operator;
        }

        public override string ToString()
        {
            ChangeType();

            switch (UnitType)
            {
                case UnitType.Integer:
                    return Fraction.Numerator.ToString();
                case UnitType.Fraction:
                    return Fraction.ToString();
                case UnitType.Operator:
                    return Operator.Value.ToString();
                default:
                    return null;
            }
        }

        /// <summary>
        /// 输出Unit的HTML形式
        /// </summary>
        /// <returns>Unit的HTML形式</returns>
        public string ToHTML()
        {
            ChangeType();

            switch (UnitType)
            {
                case UnitType.Integer:
                    return Fraction.Numerator.ToString();
                case UnitType.Fraction:
                    return Fraction.ToHTML();
                case UnitType.Operator:
                    return Operator.Value.ToString();
                default:
                    return null;
            }
        }

        /// <summary>
        /// 判断Unit是否在范围内
        /// </summary>
        /// <returns>true=>在范围内; false=>不在范围内或Unit是运算符</returns>
        public bool InRange()
        {
            switch (UnitType)
            {
                case UnitType.Integer:
                    return (Fraction.Numerator <= Settings.IntegerMaximum)
                        && (Fraction.Numerator >= Settings.IntegerMinimize);
                case UnitType.Fraction:
                    Fraction.Reduce();
                    long Numerator = Fraction.Numerator % Fraction.Denomination;
                    long Integer = Fraction.Numerator / Fraction.Denomination;
                    if (Numerator < 0)
                    {
                        Integer -= 1;
                    }
                    return (Integer <= Settings.IntegerMaximum
                        && Integer >= Settings.IntegerMinimize
                        && Fraction.Denomination <= Settings.DenominationMaximum);
                default:
                    return false;
            }
        }

        /// <summary>
        /// 获取随机操作数
        /// </summary>
        /// <returns></returns>
        public static Unit GetRandomOperand()
        {
            Unit unit = new Unit();
            unit.UnitType = (!Settings.AllowFraction) ? UnitType.Integer : (UnitType)(random.Next(0, 2));

            if (unit.UnitType == UnitType.Integer)
            {
                unit.Fraction = new Fraction(
                    random.Next(Settings.IntegerMinimize, Settings.IntegerMaximum + 1), 1);
            }
            else
            {
                unit.Fraction = Fraction.GetRandomFraction();
            }

            return unit;
        }

        /// <summary>
        /// 检查Unit的类型, 若应当为整数, 则将类型改为整数; 否则不变
        /// </summary>
        public void ChangeType()
        {
            if (UnitType == UnitType.Operator)
            {
                return;
            }

            Fraction.Reduce();

            if (Fraction.Denomination == 1)
            {
                UnitType = UnitType.Integer;
            }
            if (Fraction.Numerator == 0)
            {
                UnitType = UnitType.Integer;
            }
        }
        public static Unit operator + (Unit unit1, Unit unit2)
        {
            Unit unit = new Unit(UnitType.Fraction, unit1.Fraction + unit2.Fraction, new Operator());
            unit.ChangeType();

            return unit;
        }
        public static Unit operator - (Unit unit1, Unit unit2)
        {
            Unit unit = new Unit(UnitType.Fraction, unit1.Fraction - unit2.Fraction, new Operator());
            unit.ChangeType();

            return unit;
        }
        public static Unit operator * (Unit unit1, Unit unit2)
        {
            Unit unit = new Unit(UnitType.Fraction, unit1.Fraction * unit2.Fraction, new Operator());
            unit.ChangeType();

            return unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit1"></param>
        /// <param name="unit2"></param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException"></exception>
        public static Unit operator / (Unit unit1, Unit unit2)
        {
            Unit unit;

            try
            {
                unit = new Unit(UnitType.Fraction, unit1.Fraction / unit2.Fraction, new Operator());
            }
            catch
            {
                throw;
            }

            unit.ChangeType();

            return unit;
        }
        /// <summary>
        /// 与Unit2比较
        /// </summary>
        /// <param name="unit2">若为null, 将返回正值</param>
        /// <returns>负值 => 小于; 0 => 等于; 正值 => 大于</returns>
        public int CompareTo(Unit unit2)
        {
            if (unit2 == null)
            {
                return 1;
            }
            if (UnitType < unit2.UnitType)
            {
                return -1;
            }//类型小于unit2
            else if (UnitType > unit2.UnitType)
            {
                return 1;
            }//类型大于unit2
            else
            {
                switch (UnitType)
                {
                    case UnitType.Integer:
                        return Fraction.Numerator.CompareTo(unit2.Fraction.Numerator);
                    case UnitType.Fraction:
                        if (Fraction.Denomination.CompareTo(unit2.Fraction.Denomination) != 0)
                        {
                            return Fraction.Denomination.CompareTo(unit2.Fraction.Denomination);
                        }//分母不相等
                        else
                        {
                            return Fraction.Numerator.CompareTo(unit2.Fraction.Numerator);
                        }//分母相等
                    default:
                        return Operator.Number.CompareTo(unit2.Operator.Number);
                }
            }//类型等于unit2
        }
    }
}
