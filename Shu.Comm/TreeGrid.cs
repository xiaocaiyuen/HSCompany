using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shu.Comm
{
    /// <summary>
    /// tree树结构
    /// </summary>
    public partial class TreeGrid
    {
        /// <summary>
        /// Id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int sort { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string iconCls { get; set; }

        /// <summary>
        /// open(展开),closed(闭合)
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 扩展属性{"p1":"Custom Attribute1","p2":"Custom Attribute2"}
        /// </summary>
        public string attributes { get; set; }

        /// <summary>
        /// 下级分类编号
        /// </summary>
        public string _parentId { get; set; }
    }
}