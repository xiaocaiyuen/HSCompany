using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shu.WebApi.Models
{
    public class MenuModels
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string menuid { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menuname { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string menus { get; set; }

        /// <summary>
        /// URL地址
        /// </summary>
        public string url { get; set; }

        public List<MenuModels> child { get; set; }
    }
}