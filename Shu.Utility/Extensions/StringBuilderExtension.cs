/*
Author      : 肖亮
Date        : 2011 - 4 - 16
Description : 对 StringBuilder 的扩展
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Utility.Extensions
{
    /// <summary>
    /// 对 StringBuilder 的扩展
    /// </summary>
    public static class StringBuilderExtension
    {
        /// <summary>
        /// 确定此实例的开头是否与指定的字符串匹配
        /// </summary>
        /// <param name="builder">StringBuilder的引用</param>
        /// <param name="value">要比较的 System.String</param>
        /// <returns></returns>
        public unsafe static bool StartsWith(this StringBuilder builder, string value)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (value == null)
                throw new ArgumentNullException("value");

            if (value.Length == 0)
                return true;

            if (value.Length > builder.Length)
                return false;

            return builder.ToString(0, value.Length).Equals(value, StringComparison.Ordinal);

        }
    }
}
