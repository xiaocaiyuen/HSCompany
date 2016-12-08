/*
Author      : 肖亮
Date        : 2011-3-22
Description : 对 System.Web.UI.Control 的扩展
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;

namespace Shu.Utility.Extensions
{
    /// <summary>
    /// 对 System.Web.UI.Control 的扩展
    /// </summary>
    public static class ControlExtension
    {
        /// <summary>
        /// 获得服务器控件的html
        /// </summary>
        /// <param name="control">控件实例</param>
        /// <returns></returns>
        public static string RenderAsString(this Control control)
        {
            return WebUtil.GetPartial(control); 
        }
    }
}
