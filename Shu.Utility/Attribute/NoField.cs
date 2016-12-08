using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Utility
{
    /// <summary>
    /// 设置为非映射数据库字段
    /// </summary>
    public class NoFieldAttribute : Attribute
    {
    }

    /// <summary>
    /// 映射为主键
    /// </summary>
    public class KeyFieldAttribute : Attribute {
        /// <summary>
        /// 主键字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public Type FieldType { get; set; }
    }
}
