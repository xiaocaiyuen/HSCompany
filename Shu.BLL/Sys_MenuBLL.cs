using Shu.Model;
using Shu.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shu.Utility.Extensions;

namespace Shu.BLL
{
    public partial class Sys_MenuBLL : BaseBLL<Sys_Menu>
    {
        /// <summary>
        /// 通过菜单地址获取菜单Model
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Sys_Menu FindByURL(string url)
        {
            List<Sys_Menu> list = GetList(p => p.Menu_Url.Contains(url)).ToList();
            if (list.Count > 0)
                return list[0];
            else
                return null;
        }
        public string GetMaxNum(string pcode, string type)
        {
            if (string.IsNullOrEmpty(pcode))
            {
                return "0";
            }
            else
            {
                //string str = string.Format(" Menu_ParentCode='{0}'", pcode);
                //List<Sys_Menu> list = FindWhere(str);
                List<Sys_Menu> list = GetList(p => p.Menu_ParentCode == pcode).ToList();
                if (type == "bh")
                {
                    if (list.Count == 0)
                    {
                        return (pcode + "001");
                    }
                    else
                    {
                        string num = "0";
                        foreach (Sys_Menu men in list)
                        {
                            num = long.Parse(men.Menu_Code) > long.Parse(num) ? men.Menu_Code : num;
                        }
                        return (num == "0" ? "0" : (long.Parse(num) + 1).ToString());
                    }
                }
                else if (type == "xh")
                {
                    int maxOrder = 0;
                    foreach (Sys_Menu menu in list)
                    {
                        maxOrder = maxOrder > int.Parse(menu.Menu_Sequence.ToString()) ? maxOrder : int.Parse(menu.Menu_Sequence.ToString());
                    }
                    return maxOrder.ToString();
                }
            }
            return "0";
        }

        public bool IconMenu(string Path)
        {
            //var query = from t in DBSession.Db.Sys_Menu
            //            where !string.IsNullOrEmpty(t.Menu_IconName)
            //            group t by t.Menu_IconName into g
            //            select g.ToList();

            List<Sys_Menu> MenuList = GetList(p => !string.IsNullOrEmpty(p.Menu_IconName)).Distinct(p => p.Menu_IconName).ToList();

            StringBuilder IconClass = new StringBuilder();

            foreach (var t in MenuList)
            {
                IconClass.Append(".icon-" + t.Menu_IconName + "{background: url('" + t.Menu_IconPath + "') no-repeat center center;}\r\n");
                //Console.WriteLine("t图标名称：{0} 图标地址：{1}", t.Menu_IconName, t.Menu_IconPath);
            }
            try
            {
                //Path.DeleteFile();
                //Path.CreateFile();
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
