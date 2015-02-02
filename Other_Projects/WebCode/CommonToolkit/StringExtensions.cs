using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonToolkit
{
    /// <summary>
    /// 提供String类型的一些扩展方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 将字符串解析为时间,空字符串返回null
        /// </summary>
        /// <param name="srcStr"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string srcStr)
        {
            if (string.IsNullOrEmpty(srcStr))
            {
                return null;
            }
            DateTime result;
            DateTime.TryParse(srcStr, out result);
            return result;
        }
    }
}
