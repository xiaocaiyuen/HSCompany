/*
Author      : 肖亮
Date        : 2011-3-7
Description : 对 System.String 的扩展
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using System.IO;
using System.ComponentModel;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Collections;
namespace Shu.Utility.Extensions
{
    /// <summary>
    /// 对 System.String 的扩展
    /// </summary>
    /// 
    public static class StringExtension
    {
        #region 私有成员

        /// <summary>
        /// 需要转义的字符正则
        /// </summary>
        static readonly Regex _transferredRule = new Regex(@"(""|\\)", RegexOptions.Compiled);

        /// <summary>
        /// 空白字符正则
        /// </summary>
        static readonly Regex _spaceRule = new Regex(@"\s", RegexOptions.Compiled);

        /// <summary>
        /// HTML标签正则
        /// </summary>
        static readonly Regex _htmlTagRule = new Regex(@"<[^>]*>", RegexOptions.Compiled);

        /// <summary>
        /// 空字节数组
        /// </summary>
        static readonly Byte[] _emptyByteArray = new Byte[0];

        /// <summary>
        /// 空字符数组
        /// </summary>
        static readonly string[] _emptyStringArray = new string[0];
        #endregion

        #region 基本功能
        /// <summary>
        /// 指示 System.string 对象是 null 或者 System.String.Empty。
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 返回如果 System.string 对象是 null 则返回  System.String.Empty 否则返回他本身。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IfNullReturnEmpty(this string str)
        {
            if (str == null)
                return string.Empty;
            return str;
        }

        /// <summary>
        /// 返回如果 System.string 对象是 null或者为空字符串 则返回 指定值。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IfNullOrEmpty(this string str, string value)
        {
            if (string.IsNullOrEmpty(str))
                return value;

            return str;
        }

        /// <summary>
        /// 返回如果 System.string 对象是 null或者为空字符串 则返回 指定值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IfNullOrEmpty<T>(this string str, Func<string> value)
        {
            if (string.IsNullOrEmpty(str))
                return value();

            return str;
        }

        /// <summary>
        /// 返回如果 System.string 对象是 null或者为空字符串 则返回yesValue 执行后的值 否则返回noValue执行后的值。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="yesValue"></param>
        /// <param name="noValue"></param>
        /// <returns></returns>
        public static string IfNullOrEmpty(this string str, Func<string, string> yesValue, Func<string, string> noValue)
        {
            if (string.IsNullOrEmpty(str))
                return yesValue(str);

            return noValue(str);
        }


        //
        /// <summary>
        /// 返回如果 System.string 对象是 不为null并且不为空字符串 则返回 指定值。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IfNotNullAndEmpty(this string str, string value)
        {
            if (!string.IsNullOrEmpty(str))
                return value;

            return str;
        }

        /// <summary>
        /// 返回如果 System.string 对象是 不为null并且不为空字符串 则返回 指定值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IfNotNullAndEmpty<T>(this string str, Func<string> value)
        {
            if (!string.IsNullOrEmpty(str))
                return value();

            return str;
        }

        /// <summary>
        /// 返回如果 System.string 对象是 不为null并且不为空字符串 则返回yesValue 执行后的值 否则返回noValue执行后的值。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="yesValue"></param>
        /// <param name="noValue"></param>
        /// <returns></returns>
        public static string IfNotNullAndEmpty(this string str, Func<string, string> yesValue, Func<string, string> noValue)
        {
            if (!string.IsNullOrEmpty(str))
                return yesValue(str);

            return noValue(str);
        }

        //





        /// <summary>
        /// 当System.String 不为Null 不为空字符 不是仅仅包含空白字符的时候在其左右附加边框
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="leftBorder">左边框</param>
        /// <param name="rightBorder">右边框</param>
        /// <returns></returns>
        public static string IfNotNullAndEmptyAppendBorder(this string str, string leftBorder, string rightBorder)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length == 0)
                return str;

            return string.Concat(leftBorder, str, rightBorder);
        }

        /// <summary>
        /// 返回 System.string 对象中从左边开始指定个数的字符。
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="length">正整数，将返回的字符数</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        ///  String str="abcdefg";
        ///  Console.WriteLine(str.Left(3));
        /// </code>
        /// //abc
        /// </example>
        public static string Left(this string str, int length)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (length < 0)
                throw new ArgumentOutOfRangeException("length");

            if (str.Length == 0 || str.Length <= length)
                return str;

            return str.Substring(0, length);
        }

        /// <summary>
        /// 返回 System.string 对象中从右边开始指定个数的字符。
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="length">正整数，将返回的字符数</param>
        /// <returns></returns>
        /// <example>
        /// 	<code>
        ///  String str="abcdefg";
        ///  Console.WriteLine(str.Right(3));
        ///  </code>
        ///  //efg
        ///  </example>
        public static string Right(this string str, int length)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (length < 0)
                throw new ArgumentOutOfRangeException("length");

            if (str.Length == 0 || str.Length <= length)
                return str;

            var index = str.Length - length;
            return str.Substring(index);
        }

        /// <summary>
        /// 返回 System.String 对象从左边劈掉1个字符后的字符串
        /// </summary>
        /// <example>
        /// <code>
        /// String str="abcdef";
        /// Console.WriteLine(str.ChopLeft())
        /// </code>
        /// //bcdef
        /// </example>
        /// <param name="str">一个 System.String 引用</param>
        public static string ChopLeft(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            return internalChopLeft(str, 1);
        }

        /// <summary>
        /// 返回 System.String 对象从左边劈掉指定个字符后的字符串
        /// </summary>
        /// <example>
        /// <code>
        /// String str="abcdef";
        /// Console.WriteLine(str.ChopLeft(2))
        /// </code>
        /// //cdef
        /// </example>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="count">劈掉的字符个数</param>
        public static string ChopLeft(this string str, int count)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (count < 0)
                throw new ArgumentOutOfRangeException("length");

            return internalChopLeft(str, count);
        }

        private static string internalChopLeft(string str, int count)
        {
            if (str.Length == 0 || str.Length <= count)
                return string.Empty;

            return str.Substring(count);
        }


        /// <summary>
        /// 返回 System.String 对象从右边劈掉1个字符后的字符串
        /// </summary>
        /// <example>
        /// <code>
        /// String str="abcdef";
        /// Console.WriteLine(str.ChopRight())
        /// </code>
        /// //abcde
        /// </example>
        /// <param name="str">一个 System.String 引用</param>
        public static string ChopRight(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            return internalChopRight(str, 1);
        }

        /// <summary>
        /// 返回 System.String 对象从右边劈掉指定个字符后的字符串
        /// </summary>
        /// <example>
        /// 
        /// 
        /// </example>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="count">劈掉的字符个数</param>
        public static string ChopRight(this string str, int count)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (count < 0)
                throw new ArgumentOutOfRangeException("length");

            return internalChopRight(str, count);
        }

        private static string internalChopRight(string str, int count)
        {
            if (str.Length <= count)
                return String.Empty;

            return str.Substring(0, str.Length - count);
        }


        /// <summary>
        /// 返回 从 System.String 对象中删除了所有指定字符串之后的字符串
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="deleteStr">将被删除的字符串</param>
        public static string Remove(this string str, string deleteStr)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (deleteStr == null
                || deleteStr.Length == 0
                || str.Length == 0
                || str.Length < deleteStr.Length)
                return str;

            StringBuilder sb = new StringBuilder(str);
            return sb.Replace(deleteStr, string.Empty).ToString();

        }

        /// <summary>
        /// 返回 从 System.String 对象中删除了所有指定的一系列字符串之后的字符串
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="deleteStrs">将被删除的字符串</param>
        public static string Remove(string str, params string[] deleteStrs)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (str.Length == 0
                || deleteStrs == null
                || deleteStrs.Length == 0)
                return str;

            StringBuilder sb = new StringBuilder(str);
            foreach (var i in deleteStrs)
            {
                if (i == null
                    || i.Length == 0
                    || i.Length > str.Length)
                    continue;

                sb.Replace(i, string.Empty);
            }

            return sb.ToString();
        }
        /// <summary>
        /// 返回 System.String 对象重复拷贝指定次数之后的字符串
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="count">拷贝的次数</param>
        /// <returns></returns>
        unsafe public static string Replicate(this string str, int count)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            if (count == 1)
                return str;

            StringBuilder sb = new StringBuilder(count * str.Length);
            for (int i = 0; i < count; i++)
            {
                sb.Append(str);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 返回 System.String 以确保对象是以指定的字符串开始的 
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="start">开始字符串</param>
        /// <returns>
        /// 如果 str 是以starts 字符串开始则返回本身 否则在str开始处加上starts
        /// </returns>
        public static string InsureStartsWith(this string str, string starts, StringComparison comparisonType)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (starts == null)
                throw new ArgumentNullException("starts");

            if (str.StartsWith(starts, comparisonType))
                return str;

            return starts + str;
        }

        /// <summary>
        /// 返回 System.String 以确保对象是以指定的字符串开始的 
        /// 使用区域敏感排序规则和当前区域比较字符串。
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="start">开始字符串</param>
        /// <returns>
        /// 如果 str 是以starts 字符串开始则返回本身 否则在str开始处加上starts
        /// </returns>
        public static string InsureStartsWith(this string str, string starts)
        {
            return InsureStartsWith(str, starts, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// 返回 从 System.String 对象中删除了一系列空白字符 如：空白字符，包括空格、制表符、换页符等等  之后的字符串
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <returns></returns>
        public static string RemoveSpaceString(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            return _spaceRule.Replace(str, string.Empty);
        }

        /// <summary>
        /// 断言 System.String 对象中字符个数是否在指定的范围之内
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="minLength">字符个数最少不能小于的个数</param>
        /// <param name="maxLength">字符个数最多不能大于的个数</param>
        /// <returns></returns>
        public static bool LengthRange(this string str, int? minLength, int? maxLength)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (minLength.HasValue && minLength < 0)
                throw new ArgumentOutOfRangeException("minLength");

            if (maxLength.HasValue && (maxLength < 0 || (minLength.HasValue && maxLength < minLength)))
                throw new ArgumentOutOfRangeException("maxLength");

            int l = str.Length;
            return (!minLength.HasValue || l >= minLength) && (!maxLength.HasValue || l <= maxLength);
        }

        /// <summary>
        /// 断言 System.String 对象中字符个数是否在指定的范围之内
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="minLength">字符个数最少不能小于的个数</param>
        /// <param name="maxLength">字符个数最多不能大于的个数</param>
        /// <returns></returns>
        public static bool LengthRange(this string str, int minLength, int maxLength)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (minLength < 0)
                throw new ArgumentOutOfRangeException("minLength");

            if (maxLength < minLength || maxLength < 0)
                throw new ArgumentOutOfRangeException("maxLength");

            return str.Length >= minLength && str.Length <= maxLength;
        }

        /// <summary>
        /// 将指定的 JSON 字符串转换为 T 类型的对象
        /// </summary>
        /// <typeparam name="T">所生成对象的类型</typeparam>
        /// <param name="str">要进行反序列化的 JSON 字符串</param>
        /// <returns></returns>
        public static T JSONDeserialize<T>(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(str);
        }

        /// <summary>
        /// 将指定的 JSON 字符串转换为对象图
        /// </summary>
        /// <param name="str">要进行反序列化的 JSON 字符串</param>
        /// <returns></returns>
        public static object JSONDeserialize(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            var jss = new JavaScriptSerializer();
            return jss.DeserializeObject(str);
        }

        /// <summary>
        /// 将字符串转换为简体中文
        /// </summary>
        public static string ToSChinese(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }

        /// <summary>
        /// 将字符串转换为繁体中文
        /// </summary>
        public static string ToTChinese(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        }


        static int[] intSplit(string str, string separator, int? defaultValue)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (separator == null)
                throw new ArgumentNullException("separator");

            bool def = defaultValue.HasValue;
            string[] numbersStringArray = str.Split(new string[] { separator },
                                                        StringSplitOptions.RemoveEmptyEntries);
            List<int> numbers = new List<int>();

            foreach (var item in numbersStringArray)
            {
                int i;
                if (int.TryParse(item, out i))
                {
                    numbers.Add(i);
                }
                else if (def)
                {
                    numbers.Add(defaultValue.Value);
                }
            }

            return numbers.ToArray();
        }


        /// <summary>
        /// 将字符串以指定间隔字符拆分为Int32 数组 如果遇到无法转换为Int32的字符元素则用默认值代替
        /// </summary>
        /// <param name="str">要拆分的字符</param>
        /// <param name="separator">分隔符</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int[] IntSplit(this string str, string separator, int defaultValue)
        {

            return intSplit(str, separator, defaultValue);
        }

        /// <summary>
        /// 返回的字符串数组包含此字符串中的子字符串（由指定字符串数组的元素分隔）。并且不返回空数组元素。
        /// </summary>
        /// <param name="str">要拆分的字符</param>
        /// <param name="separator">分隔符</param>
        /// <returns>如果 str为null或者是空字符串则返回空字符数组</returns>
        public static string[] SplitRemoveEmpty(this string str, string separator)
        {
            if (string.IsNullOrEmpty(str))
                return _emptyStringArray;

            return str.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 将字符串以指定间隔字符拆分为Int32 数组
        /// </summary>
        /// <param name="str">要拆分的字符</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static int[] IntSplit(this string str, string separator)
        {
            return intSplit(str, separator, null);
        }

        //public static T[] Split<T>()
        //{
        //    throw new System.NotImplementedException();
        //}

        /// <summary>
        /// 截取字符串使其总字符个数不超过指定值，超出的部分用填充字符串替代
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="count">字符个数</param>
        /// <param name="fill">填充字符串</param>
        /// <returns>如果str为null 则返回string.Empty</returns>
        public static string SubstringByChar(this string str, int count, string fill)
        {
            if (string.IsNullOrEmpty(str) || count < 1)
                return string.Empty;

            if (str.Length <= count)
                return str;

            if (string.IsNullOrEmpty(fill) || fill.Length >= count)
                return str.Substring(0, count);


            return str.Substring(0, count - fill.Length) + fill;
        }

        /// <summary>
        /// 截取字符串使其总字符个数不超过指定值
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="count">字符个数</param>
        /// <returns></returns>
        public static string SubstringByChar(this string str, int count)
        {
            return SubstringByChar(str, count, null);
        }


        /// <summary>
        /// 按照字节数截取字符串使其总字节数不超过指定值
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="length">字节长度</param>
        /// <returns></returns>
        public static string SubstringByByte(this string str, int length)
        {
            return SubstringByByte(str, length, null);
        }


        /// <summary>
        /// 按照拉丁字符一个位置，非拉丁字符两个位置的标准 截取指定长度内合适的字符串
        /// 并且用指定字符串替代超过截取长度部分的字符串
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="length">字节长度</param>
        /// <param name="fill">填充字符串</param>
        /// <returns></returns>
        unsafe public static string SubstringByByte(this string str, int length, string fill)
        {
            if (string.IsNullOrEmpty(str) || length < 1)
                return string.Empty;


            int strLen = str.Length;
            if ((strLen << 1) <= length) //截取不可能发生的
                return str;

            int expectLength = length;
            fixed (char* p = str)
            {
                char* p_char = p;
                int templ = strLen;
                char ch;

                do
                {
                    ch = *p_char++;

                    /*
                     * 拉丁文按照1个宽度来算
                     * 其他都按照2个宽度计算
                     */
                    if (ch > '\x00ff')
                    {
                        length -= 2;
                    }
                    else
                    {
                        length--;
                    }

                } while (--templ > 0 && length > 0);


                /* 
                 * 如果原字符超出预期的长度将最多不超过 length
                 * 长度的字符串原样拷贝 
                 */
                if (templ > 0 || length < 0)
                {

                    if (String.IsNullOrEmpty(fill))
                    {
                        templ = strLen - templ + length;
                        return new String(p, 0, templ);
                    }

                    /*
                     * 处理填充字符 对字符指针进行回退
                     * 直到找到适合容纳填充字符的位置
                     */
                    fixed (char* pFill = fill)
                    {
                        char* p_fill = pFill;
                        int fillLen = fill.Length;

                        p_char += length;
                        length = 0;

                        do
                        {

                            if (length < 0)
                            {
                                if (*p_fill++ > '\x00ff')
                                {
                                    length += 2;
                                }
                                else
                                {
                                    length++;
                                }
                                fillLen--;
                            }
                            else
                            {
                                if (*--p_char > '\x00ff')
                                {
                                    length -= 2;
                                }
                                else
                                {
                                    length--;
                                }

                                templ++;
                            }

                        } while (fillLen > 0);

                        if (templ > strLen)
                            throw new ArgumentException("填充字符串的长度超过了Length", "fill");

                        templ = strLen - templ;             //实际应该拷贝的字符数
                        var retStr = new String(p, 0, templ + fill.Length);  //用截取的子串初始化字符串
                        fixed (char* p_retStr = retStr)
                        {
                            wstrcpyPtrAligned(p_retStr + templ, pFill, fill.Length); //补上填充字符
                            return retStr;
                        }
                    }
                }
            }

            return str;
        }

        private static unsafe void wstrcpyPtrAligned(char* dmem, char* smem, int charCount)
        {
            while (charCount >= 8)
            {
                *((int*)dmem) = *((int*)smem);
                *((int*)(dmem + 2)) = *((int*)(smem + 2));
                *((int*)(dmem + 4)) = *((int*)(smem + 4));
                *((int*)(dmem + 6)) = *((int*)(smem + 6));
                dmem += 8;
                smem += 8;
                charCount -= 8;
            }
            if ((charCount & 4) != 0)
            {
                *((int*)dmem) = *((int*)smem);
                *((int*)(dmem + 2)) = *((int*)(smem + 2));
                dmem += 4;
                smem += 4;
            }
            if ((charCount & 2) != 0)
            {
                *((int*)dmem) = *((int*)smem);
                dmem += 2;
                smem += 2;
            }
            if ((charCount & 1) != 0)
            {
                dmem[0] = smem[0];
            }
        }

        //public static string Choose()
        //{
        //    throw new System.NotImplementedException();
        //}



        //public static string Stuff()
        //{
        //    throw new System.NotImplementedException();
        //}



        #endregion

        #region 正则表达式

        /// <summary>
        /// 指示正则表达式使用 pattern 参数中指定的正则表达式是否在输入字符串中找到匹配项。
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="pattern"> 要匹配的正则表达式模式</param>
        /// <returns></returns>
        public static bool IsMatch(this string str, string pattern)
        {
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 指示正则表达式使用 pattern 参数中指定的正则表达式和 options 参数中提供的匹配选项是否在输入字符串中找到匹配项。
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">System.Text.RegularExpressions.RegexOptions 枚举值的按位“或”组合</param>
        /// <returns></returns>
        public static bool IsMatch(this string str, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(str, pattern, options);
        }

        /// <summary>
        /// 在指定的输入字符串中搜索 pattern 参数中提供的正则表达式的匹配项。
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns></returns>
        public static Match Match(this string str, string pattern)
        {
            return Regex.Match(str, pattern);
        }

        /// <summary>
        /// 在输入字符串中搜索 pattern 参数中提供的正则表达式的匹配项（匹配选项在 options 参数中提供）。
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">System.Text.RegularExpressions.RegexOptions 枚举值的按位“或”组合</param>
        /// <returns></returns>
        public static Match Match(this string str, string pattern, RegexOptions options)
        {
            return Regex.Match(str, pattern, options);
        }

        /// <summary>
        /// 在指定的输入字符串中搜索 pattern 参数中指定的正则表达式的所有匹配项。
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns></returns>
        public static MatchCollection Matchs(this string str, string pattern)
        {
            return Regex.Matches(str, pattern);
        }

        /// <summary>
        /// 在指定的输入字符串中搜索 pattern 参数中提供的正则表达式的所有匹配项（匹配选项在 options 参数中提供）。
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">System.Text.RegularExpressions.RegexOptions 枚举值的按位“或”组合</param>
        /// <returns></returns>
        public static MatchCollection Matchs(this string str, string pattern, RegexOptions options)
        {
            return Regex.Matches(str, pattern, options);
        }

        /// <summary>
        /// 在指定的输入字符串内，使用指定的替换字符串替换与指定正则表达式匹配的所有字符串。
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <returns></returns>
        public static string ReplaceX(this string str, string pattern, string replacement)
        {
            return Regex.Replace(str, pattern, replacement);
        }

        /// <summary>
        /// 在指定的输入字符串内，使用指定的替换字符串替换与指定正则表达式匹配的所有字符串。指定的选项将修改匹配操作。
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="options">System.Text.RegularExpressions.RegexOptions 枚举值的按位“或”组合</param>
        /// <returns></returns>
        public static string ReplaceX(this string str, string pattern, string replacement, RegexOptions options)
        {
            return Regex.Replace(str, pattern, replacement, options);
        }

        /// <summary>
        /// 在指定的输入字符串内，使用 System.Text.RegularExpressions.MatchEvaluator 委托返回的字符串替换与指定正则表达式匹配的所有字符串。
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="evaluator">一个自定义方法，它检查每个匹配项，并返回原始匹配字符串或替换字符串</param>
        /// <returns></returns>
        public static string ReplaceX(this string str, string pattern, MatchEvaluator evaluator)
        {
            return Regex.Replace(str, pattern, evaluator);
        }

        /// <summary>
        /// 在指定的输入字符串内，使用 System.Text.RegularExpressions.MatchEvaluator 委托返回的字符串替换与指定正则表达式匹配的所有字符串。指定的选项将修改匹配操作。
        /// </summary>
        /// <param name="str">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">System.Text.RegularExpressions.RegexOptions 枚举值的按位“或”组合</param>
        /// <param name="evaluator">一个自定义方法，它检查每个匹配项，并返回原始匹配字符串或替换字符串</param>
        /// <returns></returns>
        public static string ReplaceX(this string str, string pattern, MatchEvaluator evaluator, RegexOptions options)
        {
            return Regex.Replace(str, pattern, evaluator, options);
        }

        #endregion

        #region WEB开发

        /// <summary>
        /// 返回 System.String 对象转义为Javascript脚本字符串常量之后的字符串
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <returns></returns>
        public static string ToJavascriptString(this string str)
        {
            return ToJavascriptString(str, false);
        }

        /// <summary>
        /// 返回 System.String 对象转义为Javascript脚本字符串常量之后的字符串
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="addDoubleQuotes">是否添加双引号</param>
        /// <returns></returns>
        public static string ToJavascriptString(this string str, bool addDoubleQuotes)
        {
            if (str == null)
                str = string.Empty;

            const string QUETE = "\"";

            if (str.Length == 0)
            {
                if (!addDoubleQuotes)
                    return str;
                else
                    return QUETE + QUETE;
            }

            str = _transferredRule.Replace(str, "\\$1");
            StringBuilder sb = new StringBuilder(str);
            sb.Replace("\r", "\\r");
            sb.Replace("\n", "\\n");

            if (addDoubleQuotes)
            {
                sb.Insert(0, QUETE);
                sb.Append(QUETE);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 返回 System.String 对象中将HTML标签删除之后的字符串
        /// </summary>
        /// <param name="str">HTML字符串</param>
        /// <returns></returns>
        public static string RemoveHtml(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (str.Length == 0)
                return str;

            return _htmlTagRule.Replace(str, string.Empty);
        }
        /// <summary>
        /// 将字符串进行HTML属性编码
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <returns></returns>
        public static string HtmlAttributeEncode(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (str.Length == 0)
                return str;

            return HttpUtility.HtmlAttributeEncode(str);
        }

        /// <summary>
        /// 将字符串转换为 HTML 编码的字符串。
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <returns></returns>
        public static string HtmlEncode(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (str.Length == 0)
                return str;

            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 将字符串转换为 HTML 编码的字符串并将输出作为 System.IO.TextWriter 输出流返回。
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <param name="output">System.IO.TextWriter 输出流</param>
        /// <returns></returns>
        public static void HtmlEncode(this string str, TextWriter output)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (output == null)
                throw new ArgumentNullException("output");

            HttpUtility.HtmlEncode(str, output);
        }

        /// <summary>
        /// 已经为 HTTP 传输进行过 HTML 编码的字符串转换为已解码的字符串。
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <returns></returns>
        public static string HtmlDecode(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (str.Length == 0)
                return str;

            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 将已经过 HTML 编码的字符串转换为已解码的字符串并将其发送给 System.IO.TextWriter 输出流。
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <param name="output">System.IO.TextWriter 输出流</param>
        /// <returns></returns>
        public static void HtmlDecode(this string str, TextWriter output)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (output == null)
                throw new ArgumentNullException("output");

            HttpUtility.HtmlDecode(str, output);
        }

        /// <summary>
        /// 对 URL 字符串进行编码。
        /// </summary>
        /// <param name="str">要编码的文本</param>
        /// <returns></returns>
        public static string UrlEncode(this string str)
        {

            if (str == null)
                throw new ArgumentNullException("str");

            if (str.Length == 0)
                return str;

            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                return HttpUtility.UrlEncode(str, httpContext.Response.ContentEncoding);
            }
            else
            {
                return HttpUtility.UrlEncode(str);
            }
        }

        /// <summary>
        /// 使用指定的编码对象对 URL 字符串进行编码。
        /// </summary>
        /// <param name="str">要编码的文本</param>
        /// <param name="encoding">指定编码方案的 System.Text.Encoding 对象</param>
        /// <returns></returns>
        public static string UrlEncode(this string str, Encoding encoding)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (encoding == null)
                throw new ArgumentNullException("encoding");

            if (str.Length == 0)
                return str;

            return HttpUtility.UrlEncode(str, encoding);
        }

        /// <summary>
        /// 将已经为在 URL 中传输而编码的字符串转换为解码的字符串。
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <returns></returns>
        public static string UrlDecode(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                return HttpUtility.UrlDecode(str, httpContext.Request.ContentEncoding);
            }
            else
            {
                return HttpUtility.UrlDecode(str);
            }
        }

        /// <summary>
        /// 使用指定的编码对象将 URL 编码的字符串转换为已解码的字符串。
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <param name="encoding">指定解码方法的 System.Text.Encoding</param>
        /// <returns></returns>
        public static string UrlDecode(this string str, Encoding encoding)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (encoding == null)
                throw new ArgumentNullException("encoding");

            if (str.Length == 0)
                return str;

            return HttpUtility.UrlDecode(str, encoding);
        }

        #endregion

        #region 加密解密

        #region MD5

        /// <summary>
        /// 返回 System.String 对象进行MD5加密后的32字符十六进制格式字符串
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            byte[] bytes = SecurityUtil.MD5Encrypt(Encoding.GetEncoding("GB2312").GetBytes(str));
            StringBuilder sb = new StringBuilder();
            foreach (var i in bytes)
            {
                sb.Append(i.ToString("x2"));
            }
            return sb.ToString();
        }

        #endregion

        #region DES

        /// <summary>
        /// 返回 System.String 对象使用指定向量来进行DES对称加密后的Base64字符串
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="rgbIV">64位的初始化向量</param>
        /// <param name="key">进行加密的64位密钥</param>
        /// <returns></returns>
        public static string DESEncrypt(this string str, byte[] rgbIV, string key)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (rgbIV == null)
                throw new ArgumentNullException("rgbIV");

            if (key == null)
                throw new ArgumentNullException("key");

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(str);
            byte[] outputBytes = SecurityUtil.DESEncrypt(inputBytes, rgbIV, keyBytes);
            return Convert.ToBase64String(outputBytes);
        }

        /// <summary>
        ///  返回 System.String 对象使用固定向量来进行DES对称加密后的Base64字符串
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="key">进行加密的64位密钥</param>
        /// <returns></returns>
        public static string DESEncrypt(this string str, string key)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (key == null)
                throw new ArgumentNullException("key");

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(str);
            byte[] outputBytes = SecurityUtil.DESEncrypt(inputBytes, keyBytes);
            return Convert.ToBase64String(outputBytes);
        }


        /// <summary>
        /// 对已进行DES加密的Base64字符串进行解密
        /// </summary>
        /// <param name="str">要解密的字符串</param>
        /// <param name="rgbIV">64位的初始化向量</param>
        /// <param name="key">进行解密的64位密钥</param>
        /// <returns></returns>
        public static string DESDecrypt(this string str, byte[] rgbIV, string key)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (rgbIV == null)
                throw new ArgumentNullException("rgbIV");

            if (key == null)
                throw new ArgumentNullException("key");

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Convert.FromBase64String(str);
            byte[] outputBytes = SecurityUtil.DESDecrypt(inputBytes, rgbIV, keyBytes);
            return Encoding.UTF8.GetString(outputBytes);
        }

        /// <summary>
        ///  对已使用固定向量进行DES加密的Base64字符串进行解密
        /// </summary>
        /// <param name="str">要解密的字符串</param>
        /// <param name="key">进行解密的64位密钥</param>
        /// <returns></returns>
        public static string DESDecrypt(this string str, string key)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (key == null)
                throw new ArgumentNullException("key");

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Convert.FromBase64String(str);
            byte[] outputBytes = SecurityUtil.DESDecrypt(inputBytes, keyBytes);
            return Encoding.UTF8.GetString(outputBytes);
        }

        #endregion

        #region AES

        /// <summary>
        /// 返回 System.String 对象使用指定向量来进行128位 AES对称加密后的Base64字符串
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="rgbIV">128位的初始化向量</param>
        /// <param name="key">进行加密的128位密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(this string str, byte[] rgbIV, string key)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (rgbIV == null)
                throw new ArgumentNullException("rgbIV");

            if (key == null)
                throw new ArgumentNullException("key");

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(str);
            byte[] outputBytes = SecurityUtil.AESEncrypt(inputBytes, rgbIV, keyBytes);
            return Convert.ToBase64String(outputBytes);
        }

        /// <summary>
        ///  返回 System.String 对象使用固定向量来进行128位 AES对称加密后的Base64字符串
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="key">进行加密的128位密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(this string str, string key)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (key == null)
                throw new ArgumentNullException("key");

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(str);
            byte[] outputBytes = SecurityUtil.AESEncrypt(inputBytes, keyBytes);
            return Convert.ToBase64String(outputBytes);
        }

        /// <summary>
        /// 对已进行128位 AES加密的Base64字符串进行解密
        /// </summary>
        /// <param name="str">要解密的字符串</param>
        /// <param name="rgbIV">128位的初始化向量</param>
        /// <param name="key">进行解密的128位密钥</param>
        /// <returns></returns>
        public static string AESDecrypt(this string str, byte[] rgbIV, string key)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (rgbIV == null)
                throw new ArgumentNullException("rgbIV");

            if (key == null)
                throw new ArgumentNullException("key");

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Convert.FromBase64String(str);

            byte[] outputBytes = SecurityUtil.AESDecrypt(inputBytes, rgbIV, keyBytes);
            return Encoding.UTF8.GetString(outputBytes);
        }

        /// <summary>
        ///  对已使用固定向量进行128位 AES加密的Base64字符串进行解密
        /// </summary>
        /// <param name="str">要解密的字符串</param>
        /// <param name="key">进行解密的128位密钥</param>
        /// <returns></returns>
        public static string AESDecrypt(this string str, string key)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (key == null)
                throw new ArgumentNullException("key");

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Convert.FromBase64String(str);
            byte[] outputBytes = SecurityUtil.AESDecrypt(inputBytes, keyBytes);
            return Encoding.UTF8.GetString(outputBytes);
        }
        #endregion

        #endregion

        #region 类型转换

        /// <summary>
        /// 返回将字符串转换为它等效的 32 位有符号整数。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含要转换的数字的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static int ToInt(this string str, int defaultValue)
        {
            int value;
            if (Int32.TryParse(str, out value))
                return value;
            return defaultValue;

        }

        /// <summary>
        /// 返回将字符串转换为它等效的 64 位有符号整数。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含要转换的数字的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static long ToLong(this string str, long defaultValue)
        {
            long value;
            if (Int64.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// 返回将字符串转换为它等效的 16 位有符号整数。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含要转换的数字的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static short ToShort(this string str, short defaultValue)
        {
            short value;
            if (Int16.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// 返回将字符串转换为它等效的 System.Byte。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含要转换的数字的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static byte ToByte(this string str, byte defaultValue)
        {
            byte value;
            if (Byte.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// 返回将字符串转换为它等效的单精度浮点数字。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含要转换的数字的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static float ToFloat(this string str, float defaultValue)
        {
            Single value;
            if (Single.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// 返回将字符串转换为它等效的双精度浮点数字。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含要转换的数字的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defaultValue)
        {
            Double value;
            if (Double.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// 返回将字符串转换为它等效的System.Boolean 值。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含要转换的值的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static bool ToBoolean(this string str, bool defaultValue)
        {
            Boolean value;
            if (Boolean.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// 返回将字符串转换为它等效的Unicode 字符。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含单个字符或 null 的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static char ToChar(this string str, char defaultValue)
        {
            Char value;
            if (Char.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// 返回将字符串转换为它等效的 System.Decimal 表示形式。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">要转换的数字的字符串表示形式</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, Decimal defaultValue)
        {
            Decimal value;
            if (Decimal.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// 返回将日期和时间的指定字符串表示形式转换为其 System.DateTime 等效项。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含要转换的日期和时间的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, DateTime defaultValue)
        {
            DateTime value;
            if (DateTime.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// 返回将字符串转换为它等效的 32 位无符号整数。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含要转换的数字的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static uint ToUInt(this string str, uint defaultValue)
        {
            UInt32 value;
            if (UInt32.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// 返回将字符串转换为它等效的 8 位有符号整数。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含要转换的数字的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static sbyte ToSByte(this string str, sbyte defaultValue)
        {
            SByte value;
            if (SByte.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        /// <summary>
        /// 返回将字符串转换为它等效的 64 位无符号整数。如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">包含要转换的数字的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static ulong ToULong(this string str, ulong defaultValue)
        {
            UInt64 value;
            if (UInt64.TryParse(str, out value))
                return value;
            return defaultValue;
        }

        #endregion

        #region 验证

        /// <summary>
        /// 是否为电话号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhone(this string str)
        {
            return Regex.IsMatch(str, @"(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$");
            //(^\d{2,5}[+|-]\d{6,10}$)|(^\d{3}[+|-]\d{4}[+|-]\d{3}$)|(^\d{3}[+|-]\d{3}[+|-]\d{4}$)
        }
        /// <summary>
        /// 是否为日期格式 不含时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDate(this string str)
        {
            //DateTime dt;
            //return DateTime.TryParse(str, out dt);
            return Regex.IsMatch(str, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        }
        /// <summary>
        /// 是否是否为日期格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string str)
        {
            DateTime dt;
            return DateTime.TryParse(str, out dt);
        }
        /// <summary>
        /// 是否为时间格式,时:分:秒
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(this string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }
        /// <summary>
        /// 是否都为中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChinese(this string str)
        {
            return Regex.IsMatch(str, "^[\u4e00-\u9fa5]*$");
        }

        /// <summary>
        /// 是否为正确的Url(http/https/ftp)
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(this string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https|ftp)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        /// 是否为ip字符串
        /// </summary>
        /// <param name="ip">ip字符串</param>
        /// <returns></returns>
        public static bool IsIP(this string ip)
        {
            string pattrn = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
            if (System.Text.RegularExpressions.Regex.IsMatch(ip, pattrn))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否为身份证号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool IsIDCardNumber(this string Id)
        {
            #region
            int intLen = Id.Length;
            long n = 0;

            if (intLen == 18)
            {
                if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
                {
                    return false;//数字验证
                }
                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(Id.Remove(2)) == -1)
                {
                    return false;//省份验证
                }
                string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证
                }
                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                char[] Ai = Id.Remove(17).ToCharArray();
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                }
                int y = -1;
                Math.DivRem(sum, 11, out y);
                if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
                {
                    return false;//校验码验证
                }
                return true;//符合GB11643-1999标准
            }
            else if (intLen == 15)
            {
                if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
                {
                    return false;//数字验证
                }
                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(Id.Remove(2)) == -1)
                {
                    return false;//省份验证
                }
                string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证
                }
                return true;//符合15位身份证标准
            }
            else
            {
                return false;//位数不对
            }
            #endregion
        }

        /// <summary>
        /// 是否为符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(this string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            // return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        }

        /// <summary>
        /// 是否安全(是否存在SQL注入) 存在返回true 不存在返回false
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUnSafeSql(this string str)
        {
            return Regex.IsMatch(str, @"[-|;|,|\/|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 是否为Int16格式
        /// </summary>
        /// <param name="Int"></param>
        /// <returns></returns>
        public static bool IsInt16(this string Int)
        {
            Int16 strint16;
            return Int16.TryParse(Int,out strint16);
        }

        /// <summary>
        /// 是否为Int32格式
        /// </summary>
        /// <param name="Int"></param>
        /// <returns></returns>
        public static bool IsInt(this string Int)
        {
            int strint;
            return int.TryParse(Int, out strint);
           
        }

        /// <summary>
        /// 是否为Int64(long)格式
        /// </summary>
        /// <param name="Int"></param>
        /// <returns></returns>
        public static bool IsInt64(this string Int)
        {
            Int64 strint;
            return Int64.TryParse(Int, out strint);
        }

        /// <summary>
        /// 是否为decimal格式
        /// </summary>
        /// <param name="Decimal"></param>
        /// <returns></returns>
        public static bool IsDecimal(this string Decimal)
        {
            decimal strDecimal;
            return decimal.TryParse(Decimal, out strDecimal);
        }

        /// <summary>
        /// 是否为float格式
        /// </summary>
        /// <param name="Float"></param>
        /// <returns></returns>
        public static bool IsFloat(this string Float)
        {
            float strFloat;
            return float.TryParse(Float, out strFloat);
        }

        /// <summary>
        /// 是否为Double格式
        /// </summary>
        /// <param name="Double"></param>
        /// <returns></returns>
        public static bool IsDouble(this string Double)
        {
            double strDouble;
            return double.TryParse(Double,out strDouble);
        }

        /// <summary>
        /// 是否为Money格式
        /// </summary>
        /// <param name="Money"></param>
        /// <returns></returns>
        public static bool IsMoney(this string Money)
        {
            return Regex.IsMatch(Money, @"^\d{1,12}(?:\.\d{1,2})?$");
        }

        /// <summary>
        /// 是否为长数字格式
        /// </summary>
        /// <param name="Long"></param>
        /// <returns></returns>
        public static bool IsLong(this string Long)
        {
            long strLong;
            return long.TryParse(Long, out strLong);
        }

        /// <summary>
        /// 是否为短数字格式
        /// </summary>
        /// <param name="Short"></param>
        /// <returns></returns>
        public static bool IsShort(this string Short)
        {
            short strShort;
            return short.TryParse(Short,out strShort);
        }

        /// <summary>
        /// 是否为Guid格式
        /// </summary>
        /// <param name="GuidId"></param>
        /// <returns></returns>
        public static bool IsGuid(this string GuidId)
        {
            Guid strGuid;
            return Guid.TryParse(GuidId, out strGuid);
        }

        /// <summary>
        /// 是否为Byte格式
        /// </summary>
        /// <param name="Byte"></param>
        /// <returns></returns>
        public static bool IsByte(this string Byte)
        {
            byte strByte;
            return byte.TryParse(Byte,out strByte);
        }

        /// <summary>
        /// 是否为Bool格式
        /// </summary>
        /// <param name="Bool"></param>
        /// <returns></returns>
        public static bool IsBool(this string Bool)
        {
            bool strBool;
            return bool.TryParse(Bool,out strBool);
        }

        #endregion

        /// <summary>
        /// 返回字符串的第一个大写字母或第一个数字（返回中文第一个拼音的第一个大写字母，英文的第一个大写字母，数字的第一个数字，其他的均返回null）
        /// </summary>
        /// <param name="input"></param>
        /// <returns>返回中文第一个拼音的第一个大写字母，英文的第一个大写字母，数字的第一个数字，其他的均返回null</returns>
        public static char? GetFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return (char?)null;
            }
            //将汉字转化为ASNI码,二进制序列
            input = input.ToUpper();
            byte[] arrCN = Encoding.Default.GetBytes(input);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                //字母和数字
                int[] intscode = { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90 };
                if (intscode.Contains(area))
                {
                    return Encoding.Default.GetChars(new byte[] { (byte)(area) })[0];
                }
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = {45217,45253,45761,46318,46826,47010,47297,47614,48119,48119,49062,
            49324,49896,50371,50614,50622,50906,51387,51446,52218,52698,52698,52698,52980,53689,
            54481};
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetChars(new byte[] { (byte)(65 + i) })[0];
                    }
                }
            }
            return (char?)null;
        }
    }
}
