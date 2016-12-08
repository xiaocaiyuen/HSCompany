#region
/* 
 *作者：shenjk http://www.shenjk.com
 *时间：2009-10-26
 *描述：字符串验证
 */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shu.Utility.Extensions;
namespace Shu.Utility
{


    /// <summary>
    /// 
    /// </summary>
    public static class StringValidationHelper
    {
        /// <summary>    
        /// 验证<see cref="System.String"/>类型的参数不为空.    
        /// </summary>    
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>    
        /// <returns><paramref name="current"/>的引用以方便链式调用.</returns>    
        public static ValidationHelper<string> NotEmpty(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            //current.NotDefault();
            if (string.IsNullOrEmpty(current.Value))
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_NOT_EMPTY, current.Lang) /*"{0}不可为空字符串"*/, current.Name);
                current.Passed = false;
                // throw new ArgumentException(String.Format("{0}不可为空字符串", current.Name), current.Name);
            }

            return current;
        }
        /// <summary>    
        /// 验证<see cref="System.String"/>类型的参数的长度小于一定值.    
        /// </summary>    
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>    
        /// <param name="length">可行的最大长度(包括此值).</param>    
        /// <returns><paramref name="current"/>的引用以方便链式调用.</returns>    
        public static ValidationHelper<string> MaxLength(this ValidationHelper<string> current, int length)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;

            if (current.Value.Length > length)
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_OVER_LENGTH, current.Lang)/*"{0}的长度不可超过{1}"*/, current.Name, length);
                current.Passed = false;
                //throw new ArgumentException(String.Format("{0}的长度不可超过{1}", current.Name, length), current.Name);
            }
            return current;
        }
        /// <summary>    
        /// 验证<see cref="System.String"/>类型的参数的长度大于一定值.    
        /// </summary>    
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>    
        /// <param name="length">可行的最小长度(包括此值).</param>    
        /// <returns><paramref name="current"/>的引用以方便链式调用.</returns>    
        public static ValidationHelper<string> MinLength(this ValidationHelper<string> current, int length)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            //current.NotDefault();
            if (current.Value.Length < length)
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_LESS_LENGTH, current.Lang)/*"{0}的长度不可小于{1}"*/, current.Name, length);
                current.Passed = false;
                //throw new ArgumentException(String.Format("{0}的长度不可小于{1}", current.Name, length), current.Name);
            }
            return current;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="m_equalto"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ValidationHelper<string> EqualTo(this ValidationHelper<string> current, string m_equalto, string name)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (current.Value != m_equalto)
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_NOT_EQUAL, current.Lang)/*"{0}与{1}不一致"*/, current.Name, name);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>    
        /// 验证<see cref="System.String"/>类型的参数的长度在一定值之间.    
        /// </summary>    
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>    
        /// <param name="minLength">可行的最小长度(包括此值).</param>    
        /// <param name="maxLength">可行的最大长度(包括此值).</param>    
        /// <returns><paramref name="current"/>的引用以方便链式调用.</returns>    
        public static ValidationHelper<string> LengthRange(this ValidationHelper<string> current, int minLength, int maxLength)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            //current.NotDefault();
            if (current.Value.Length < minLength || current.Value.Length > maxLength)
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_LENGTH_RANGE, current.Lang)/*"{0}的长度必须在{1}和{2}之间"*/, current.Name, minLength, maxLength);
                current.Passed = false;
                //throw new ArgumentException(String.Format("{0}的长度必须在{1}和{2}之间", current.Name, minLength, maxLength), current.Name);
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.String"/>存在SQL注入.    
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> ExistSqlIn(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            //current.NotDefault();
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (current.Value.IsUnSafeSql())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_NOT_SAFE, current.Lang)/*"{0}存在非法字符"*/, current.Name);
                current.Passed = false;
                //throw new ArgumentException(String.Format("{0}存在非法字符", current.Name), current.Name);
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.String"/>是否是IP地址.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsIP(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsIP())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISIP, current.Lang)/*"{0}不是合法的IP地址"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.String"/>是否是合法的Email.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsEmail(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsValidEmail())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISEMAIL, current.Lang)/*"{0}不是合法的Email"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.String"/>是否是合法的Url.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsUrl(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!string.IsNullOrEmpty(current.Value) && !current.Value.IsURL())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISURL, current.Lang)/*"{0}不是有效的链接(URL)地址"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的日期格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsDateTime(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsDateTime())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的日期数据"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的身份证号码.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsIDCardNumber(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsIDCardNumber())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISIDCARD, current.Lang)/*"{0}不是有效的身份证号"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.String"/>是否是中文.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsChinese(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsChinese())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISCHINESE, current.Lang)/*"{0}含有非中文字符"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否为电话号码    
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsPhone(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsPhone())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISPHONE, current.Lang)/*"{0}不是有效的电话号码"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Int16格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsInt16(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsInt16())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的smallint类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Int格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsInt(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsInt())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的数字类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Int64格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsInt64(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsInt64())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的Int64类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Decimal格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsDecimal(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsDecimal())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的Decimal类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Float格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsFloat(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsFloat())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的Float类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Double格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsDouble(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsDouble())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的Double类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Money格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsMoney(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsMoney())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的货币类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Long格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsLong(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsLong())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的长数字类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Short格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsShort(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsShort())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的短数字类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }


        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Guid格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsGuid(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsGuid())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的Guid类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Byte格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsByte(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsByte())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的Byte类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的Bool格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static ValidationHelper<string> IsBool(this ValidationHelper<string> current)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsBool())
            {
                current.Msg = String.Format(GetTipLanguage.Get(TipInfo.STR_ISDATETIME, current.Lang)/*"{0}不是有效的Bool类型"*/, current.Value);
                current.Passed = false;
            }
            return current;
        }

    }
}
