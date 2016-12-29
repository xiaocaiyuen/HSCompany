using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shu.Utility.WebApi
{
    /// <summary>
    /// Web Api 返回分页结果
    /// </summary>
    /// <typeparam name="T">返回数据的类型</typeparam>
    public class WebApiPagingResult<T> : WebApiResult<T>
    {
        /// <summary>
        /// 获取或设置所有记录数
        /// </summary>
        public virtual int RowsCount { get; set; }
    }
}
