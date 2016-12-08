using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Shu.Model
{
    /// <summary>
    /// 通用存储过程Model
    /// </summary>
    public partial class StoredProcModel
    {
        /// <summary>
        /// 存储过程名
        /// </summary>
        public string ProcName { get; set; }

        /// <summary>
        /// 存储过程参数列表
        /// </summary>
        public List<ParameterModel> ParameterList { get; set; }
    }

    /// <summary>
    /// 存储过程参数
    /// </summary>
    public partial class ParameterModel
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public DbType ParamType { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object ParamValue { get; set; }

        /// <summary>
        /// 输入输出
        /// </summary>
        public ParamEnumMode ParamMode { get; set; }

        /// <summary>
        /// 参数返回结果
        /// </summary>
        public object ParamResults { get; set; }
    }


    public enum ParamEnumMode
    {
        InMode = 1,
        OutMode = 2
    }
}
