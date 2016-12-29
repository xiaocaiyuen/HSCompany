using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shu.Utility.WebApi
{
    /// <summary>
    /// Web Api 返回结果
    /// </summary>
    /// <typeparam name="T">返回数据的类型</typeparam>
    public class WebApiResult<T>
    {
        #region 属性
        /// <summary>
        /// 获取或设置返回的数据（集）
        /// </summary>
        public virtual T Data { get; set; }
        /// <summary>
        /// 获取是否错误
        /// </summary>
        public virtual bool HasErrors
        {
            get
            {
                return Errors.Count > 0;
            }
            set { }
        }
        /// <summary>
        /// 获取是否成功
        /// </summary>
        public virtual bool Success
        {
            get { return !HasErrors; }
            set { }
        }
        /// <summary>
        /// 获取所有错误信息
        /// </summary>
        public virtual string AllMessages
        {
            get
            {
                return string.Join(System.Environment.NewLine, Messages.ToArray());
            }
            set { }
        }
        /// <summary>
        /// 获取Exception集合
        /// </summary>
        public virtual List<Exception> Errors { get; set; }
        /// <summary>
        /// 获取错误信息集合
        /// </summary>
        public virtual List<string> Messages { get; set; }
        #endregion

        #region 构造函数
        public WebApiResult()
        {
            Errors = new List<Exception>();
            Messages = new List<string>();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 添加错误信息。
        /// </summary>
        /// <param name="error">错误信息</param>
        public virtual void AddError(string error)
        {
            AddError(new Exception(error));
        }
        /// <summary>
        /// 添加错误信息。
        /// </summary>
        /// <param name="error">错误信息</param>
        public virtual void AddError(Exception error)
        {
            Errors.Add(error);
            Messages.Add(error.Message);
        }
        #endregion
    }

    /// <summary>
    /// Web Api 返回结果
    /// </summary>
    public class WebApiResult : WebApiResult<bool>
    {
        /// <summary>
        /// 获取或设置返回的数据（是否成功）
        /// </summary>
        public override bool Data
        {
            get
            {
                return Success;
            }
            set
            {
            }
        }
    }
}
