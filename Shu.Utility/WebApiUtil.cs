using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shu.Utility
{
    /// <summary>
    /// 提供WEBAPI开发常用功能
    /// </summary>
    public class WebApiUtil
    {
        public static IEnumerable<T> Get<T>(string url)
        {
            IEnumerable<T> model = null;

            var client = new HttpClient();

            var resp = client.GetAsync(url).Result;
            resp.EnsureSuccessStatusCode();
            var jsonString = resp.Content.ReadAsStringAsync();
            jsonString.Wait();

            var json = jsonString
                        .Result
                        .TrimStart('\"')
                        .TrimEnd('\"')
                        .Replace("\\", "");

            model = json.ToObject<IEnumerable<T>>();

            //var task = client.GetAsync(url)
            //  .ContinueWith((taskwithresponse) =>
            //  {
            //      var response = taskwithresponse.Result;
            //      var jsonString = response.Content.ReadAsStringAsync();
            //      jsonString.Wait();

            //      var json = jsonString
            //                  .Result
            //                  .TrimStart('\"')
            //                  .TrimEnd('\"')
            //                  .Replace("\\", "");

            //      model = json.ToObject<IEnumerable<T>>();

            //  });
            task.Wait();
            return model;
        }
    }
}
