using Shu.Model;
using Shu.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shu.BLL
{
    public partial class Sys_OperatingButtonBLL : BaseBLL<Sys_OperatingButton>
    {
        public bool IconMenu(string Path)
        {
            List<Sys_OperatingButton> MenuList = GetList(p => !string.IsNullOrEmpty(p.IconName)).Distinct(p=>p.IconName).ToList();

            StringBuilder IconClass = new StringBuilder();

            foreach (var t in MenuList)
            {
                IconClass.Append(".icon-" + t.IconName + "{background: url('" + t.Icon + "') no-repeat center center;}\r\n");
            }
            try
            {
                Path.WriteAllText(IconClass.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
    }
}
