/*
Author      : 肖亮
Date        : 2011-3-7
Description : 对 System.Object的扩展
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;

namespace Shu.Utility.Extensions
{
    /// <summary>
    /// 对 System.Object的扩展
    /// </summary>
    public static class ObjectExtension
    {

        /// <summary>
        /// 将对象进行JSON格式序列化
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public static string JSONSerialize(this object obj)
        {
            //var jss = new JavaScriptSerializer();
            //jss.RegisterConverters(new JavaScriptConverter[]{ new DateTimeConverter()});
            //return jss.Serialize(obj);
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        ///  将对象进行二进制序列化
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public static byte[] BinarySerialize(this object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                ms.Flush();
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 尝试将对象转换为指定的类型 转换失败则返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static T ToType<T>(this object obj, T defaultValue)
        {
            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            var type = typeof(T);
            try
            {
                return (T)Convert.ChangeType(obj, type);
            }
            catch
            {
                if (obj.GetType() != type && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var valType = type.GetGenericArguments()[0];
                    try
                    {
                        return (T)Convert.ChangeType(obj, valType);
                    }
                    catch
                    {
                        return defaultValue;
                    }
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// 尝试将对象转换为指定的类型 转换失败则返回default(T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToType<T>(this object obj)
        {
            return ToType<T>(obj, default(T));
        }

       
        //public static string TemplateFormat()
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
