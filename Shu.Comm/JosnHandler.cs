using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;

namespace Shu.Comm
{
    /// <summary>
    /// json帮助类
    /// </summary>
    public class JosnHandler
    {
        /// <summary>
        /// 生成Json格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray());

                //替换Json的Date字符串   
                string p = @"\\/Date\((\d+)\+\d+\)\\/";
                MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
                Regex reg = new Regex(p);
                szJson = reg.Replace(szJson, matchEvaluator);
                return szJson;


            }


          
        }
        /// <summary>
        /// 获取Json的Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="szJson"></param>
        /// <returns></returns>
        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
          

            string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            szJson = reg.Replace(szJson, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        /// <summary>     
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串   
        /// </summary>   
        private static string ConvertJsonDateToDateString(Match m)  
        {   
            string result = string.Empty;  
            DateTime dt = new DateTime(1970,1,1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));  
            dt = dt.ToLocalTime();  
            result = dt.ToString("yyyy-MM-dd");   
            return result;   
        }  
        /// <summary>   
       /// 将时间字符串转为Json时间 
       /// </summary> 
       private static string ConvertDateStringToJsonDate(Match m)  
       {  
           string result = string.Empty;  
           DateTime dt = DateTime.Parse(m.Groups[0].Value);  
           dt = dt.ToUniversalTime();  
           TimeSpan ts = dt - DateTime.Parse("1970-01-01");   
           result = string.Format("\\/Date({0}+0800)\\/",ts.TotalMilliseconds);  
           return result;
       }
    }
}
