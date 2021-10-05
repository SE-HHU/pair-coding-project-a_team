using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Shared.Models
{
    public class WebSettings
    {
        [Range(1, 10000, ErrorMessage = "请输入 1 ~ 10000 之间的整数")]
        public int ProblemsNumber { get; set; }
        [Range(1, 3, ErrorMessage = "请输入 1 ~ 3 之间的整数")]
        public int OperatorsNumber { get; set; }
        [Range(-100000, 100000, ErrorMessage = "请输入 -100000 ~ 100000 之间的整数")]
        public int IntegerMinimize { get; set; }
        [Range(-100000, 100000, ErrorMessage = "请输入 -100000 ~ 100000 之间的整数")]
        public int IntegerMaximum { get; set; }
        [Range(-100000, 100000, ErrorMessage = "请输入 -100000 ~ 100000 之间的整数")]
        public int DenominationMaximum { get; set; }
        public bool AllowParentheses { get; set; }
        public bool AllowPlus { get; set; }
        public bool AllowSubscribe { get; set; }
        public bool AllowMultiply { get; set; }
        public bool AllowDivide { get; set; }
        public bool AllowFraction { get; set; }

        public WebSettings()
        {
            ProblemsNumber = 100;
            OperatorsNumber = 2;
            IntegerMinimize = 0;
            IntegerMaximum = 100;
            DenominationMaximum = 100;
            AllowPlus = true;
            AllowSubscribe = true;
            AllowMultiply = false;
            AllowDivide = false;
            AllowFraction = false;
            AllowParentheses = false;
        }

        /// <summary>
        /// 恢复默认设置
        /// </summary>
        public void SetDefault()
        {
            ProblemsNumber = 100;
            OperatorsNumber = 2;
            IntegerMinimize = 0;
            IntegerMaximum = 100;
            DenominationMaximum = 100;
            AllowPlus = true;
            AllowSubscribe = true;
            AllowMultiply = false;
            AllowDivide = false;
            AllowFraction = false;
            AllowParentheses = false;
        }

        /// <summary>
        /// 去除设置中的范围限制, 不改变对象值, 仅改变 Tools.Srttings 中的值
        /// </summary>
        public static void RemoveSettings()
        {
            Tools.Settings.IntegerMinimize = int.MinValue;
            Tools.Settings.IntegerMaximum = int.MaxValue;
            Tools.Settings.DenominationMaximum = int.MaxValue;
        }

        /// <summary>
        /// 改变 Tools.Srttings 中的值, 使之与对象的值一致
        /// </summary>
        public void SetSettings()
        {
            Tools.Settings.ProblemsNumber = ProblemsNumber;
            Tools.Settings.OperatorsNumber = OperatorsNumber;
            Tools.Settings.IntegerMinimize = IntegerMinimize;
            Tools.Settings.IntegerMaximum = IntegerMaximum;
            Tools.Settings.DenominationMaximum = DenominationMaximum;
            Tools.Settings.AllowParentheses = AllowParentheses;
            Tools.Settings.AllowPlus = AllowPlus;
            Tools.Settings.AllowSubscribe = AllowSubscribe;
            Tools.Settings.AllowMultiply = AllowMultiply;
            Tools.Settings.AllowDivide = AllowDivide;
            Tools.Settings.AllowFraction = AllowFraction;
        }

        /// <summary>
        /// 复制另一个对象的值, 若另一个对象为空则不做处理
        /// </summary>
        /// <param name="webSettings"></param>
        public void CopyFrom(WebSettings webSettings)
        {
            if (webSettings == null)
            {
                return;
            }
            ProblemsNumber = webSettings.ProblemsNumber;
            OperatorsNumber = webSettings.OperatorsNumber;
            IntegerMinimize = webSettings.IntegerMinimize;
            IntegerMaximum = webSettings.IntegerMaximum;
            DenominationMaximum = webSettings.DenominationMaximum;
            AllowParentheses = webSettings.AllowParentheses;
            AllowPlus = webSettings.AllowPlus;
            AllowSubscribe = webSettings.AllowSubscribe;
            AllowMultiply = webSettings.AllowMultiply;
            AllowDivide = webSettings.AllowDivide;
            AllowFraction = webSettings.AllowFraction;
        }
    }
}
