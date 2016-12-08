/*
Author      : 肖亮
Date        : 2011-4-12
Description : 对 System.Array的扩展
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Utility.Extensions
{
    /// <summary>
    ///  对 System.Array的扩展
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        ///  数组是否为Null 或者 为空数组
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this Array array)
        {
            return array == null || array.Length == 0;
        }
    }
}
