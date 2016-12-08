/*
 * Author:  肖亮
 * Date:    2011-7-14
 * Description: 返回的Json页面对象
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shu.Utility;
using Shu.Utility.Extensions;
namespace Shu.Comm
{
    /// <summary>
    /// 返回的Json页面对象
    /// </summary>
    public class JsonResult
    {
        /// <summary>
        /// 是否有错误
        /// </summary>
        public bool error { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 其它数据信息
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 返回Json字符串
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return this.JSONSerialize();
        }

        /// <summary>
        /// override tostring(); 返回json字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToJson();
            //return base.ToString();
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        /// <param name="end">是否 Response.End()</param>
        public void Output(bool end) { 
            string text=this.ToJson();
            if (!string.IsNullOrEmpty(text))
                System.Web.HttpContext.Current.Response.Write(text);

            if (end) {
                System.Web.HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 输出结果 并执行 Response.End()
        /// </summary>
        public void Output() {
            Output(true);
        }

        /// <summary>
        /// 设置结果
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public JsonResult SetError(bool error) {
            this.error = error;
            return this;
        }

        /// <summary>
        /// 设置消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public JsonResult SetMsg(string msg) {
            this.msg = msg;
            return this;
        }
        /// <summary>
        /// 设置其它信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult SetData(object data) {
            this.Data = data;
            return this;
        }
    }
}
