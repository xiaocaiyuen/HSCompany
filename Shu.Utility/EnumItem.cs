using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Utility
{
    /// <summary>
    /// 枚举项
    /// </summary>
    public class EnumItem
    {
        /// <summary>
        /// 枚举值
        /// </summary>
        public long Value {  get; internal set; }
        /// <summary>
        /// 枚举的描述
        /// </summary>
        public string Description {  get; internal set; }
    }
}
