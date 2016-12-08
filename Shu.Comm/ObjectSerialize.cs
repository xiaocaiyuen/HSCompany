using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Shu.Comm
{
    public class ObjectSerialize
    {
        #region 对象序列化
        /// <summary>   
        /// 序列化对象   
        /// </summary>   
        /// <typeparam name="T">对象类型</typeparam>   
        /// <param name="t">对象</param>   
        /// <returns></returns>   
        public static string Serialize<T>(T t)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xz = new XmlSerializer(t.GetType());
                xz.Serialize(sw, t);
                return sw.ToString();
            }
        }

        /// <summary>   
        /// 反序列化为对象   
        /// </summary>   
        /// <param name="type">对象类型</param>   
        /// <param name="s">对象序列化后的Xml字符串</param>   
        /// <returns></returns>   
        public static object Deserialize(Type type, string s)
        {
            using (StringReader sr = new StringReader(s))
            {
                XmlSerializer xz = new XmlSerializer(type);
                return xz.Deserialize(sr);
            }
        }
        #endregion
    }
}
