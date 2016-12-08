using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shu.Manage
{
    public partial class Toolbar
    {
        /// <summary>
        /// 控件ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 控件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图标名称
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 控件类型(1.链接2.事件,3.自定义事件,4.新页面,5.自定义)
        /// </summary>
        public int Type { get; set; }
    }
}