using Shu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shu.Utility.Extensions;

namespace Shu.BLL
{
    public partial class Sys_RolePurviewBLL : BaseBLL<Sys_RolePurview>
    {
        public List<View_Sys_RolePurviewAndMenu> GetRoleMenus(string userId)
        {
            Sys_UserInfo userModel = new Sys_UserInfoBLL().Get(p => p.UserInfoID == userId);
            string roleIDs = userModel.UserInfo_RoleID;
            List<View_Sys_RolePurviewAndMenu> list = new List<View_Sys_RolePurviewAndMenu>();
            List<View_Sys_RolePurviewAndMenu> Returnlist = new List<View_Sys_RolePurviewAndMenu>();
            if (roleIDs != "")
            {
                string roleStr = string.Empty;
                string[] roleArr = roleIDs.Split(',');
                //foreach (string s in roleArr)
                //{
                //    roleStr = roleStr + "'" + s + "',";
                //}
                //if (roleStr != "") roleStr = roleStr.Substring(0, roleStr.Length - 1);
                //list = new View_Sys_RolePurviewAndMenuBLL().GetList(" RolePurview_RoleID in(" + roleStr + ")");
                list = new View_Sys_RolePurviewAndMenuBLL().GetList(p => roleArr.Contains(p.RolePurview_RoleID)).ToList();

                foreach (View_Sys_RolePurviewAndMenu menuModel in list)
                {
                    if (Returnlist.Where(p => p.Menu_Code == menuModel.Menu_Code).Count() <= 0)
                    {
                        Returnlist.Add(menuModel);
                    }
                }
            }
            return Returnlist;
        }


        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        public List<View_Sys_RolePurviewAndMenu> GetMenus(string RoleId)
        {
            List<View_Sys_RolePurviewAndMenu> list = new List<View_Sys_RolePurviewAndMenu>();
            List<View_Sys_RolePurviewAndMenu> Returnlist = new List<View_Sys_RolePurviewAndMenu>();
            if (RoleId != "")
            {
                string roleStr = string.Empty;
                string[] roleArr = RoleId.Split(',');
                list = new View_Sys_RolePurviewAndMenuBLL().GetList(p => roleArr.Contains(p.RolePurview_RoleID)).ToList();

                //foreach (View_Sys_RolePurviewAndMenu menuModel in list)
                //{
                //    if (Returnlist.Where(p => p.Menu_Code == menuModel.Menu_Code).Count() <= 0)
                //    {
                //        Returnlist.Add(menuModel);
                //    }
                //}
                Returnlist = list.Distinct(p=>p.MenuID).ToList();

            }
            return Returnlist;
        }

        public List<Sys_RolePurview> FindByRoleAndMenu(string roleIDs, string menuCode)
        {
            List<Sys_RolePurview> list = new List<Sys_RolePurview>();
            if (!string.IsNullOrEmpty(roleIDs))
            {
                string roleStr = string.Empty;
                string[] roleArr = roleIDs.Split(',');
                //foreach (string s in roleArr)
                //{
                //    roleStr = roleStr + "'" + s + "',";
                //}
                //if (roleStr != "") roleStr = roleStr.Substring(0, roleStr.Length - 1);
                //list = this.FindWhere(" RolePurview_MenuCode='" + menuCode + "' and  RolePurview_RoleID in(" + roleStr + ")");
                list = this.GetList(p => p.RolePurview_MenuCode == menuCode && roleArr.Contains(p.RolePurview_RoleID)).ToList();
            }
            return list;
        }
    }
}
