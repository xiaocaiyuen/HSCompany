/*
Author      : 肖亮
Date        : 2016-12-2
Description : 对 Json序列化的扩展
*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shu.Utility.Extensions
{
    /// <summary>
    ///  对 Json序列化的扩展
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 对Model数据进行序列化 如果对象为null 则返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultString">当对象为null时返回的默认{}</param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj, string defaultString = "{}") where T : class
        {
            if (obj == null)
                return defaultString;
            else
                return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 对List数据进行序列化 如果对象为null 则返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultString">当对象为null时返回的默认{}</param>
        /// <returns></returns>
        public static string ToJson<T>(this List<T> obj, string defaultString = "{}") where T : class
        {
            if (obj == null)
                return defaultString;
            else
                return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 反序列化操作 如果对象为null 则返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="defaultT"></param>
        /// <returns></returns>
        public static T DeserializeToObject<T>(string str, T defaultT)
        {
            if (str == null)
                return defaultT;
            else
                return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
