/*
Author      : 肖亮
Date        : 2016-12-03
Description : 对 System.Linq.Enumerable 的扩展
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shu.Utility.Extensions
{
    /// <summary>
    /// Distinct去重扩展方法
    /// </summary>
    public static class DistinctExtensions
    {
        /// <summary>
        /// Distinct扩展方法(Linq表达式进行自定义类的任意字段进行Distinct的处理)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new EqualityComparer<T, V>(keySelector));
        }
    }

    /// <summary>
    /// 通用比较的类 实现IEqualityComparer<T>接口：
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class EqualityComparer<T, V> : IEqualityComparer<T>
    {
        private Func<T, V> keySelector;

        public EqualityComparer(Func<T, V> keySelector)
        {
            this.keySelector = keySelector;
        }

        public bool Equals(T x, T y)
        {
            return EqualityComparer<V>.Default.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return EqualityComparer<V>.Default.GetHashCode(keySelector(obj));
        }
    }
}
