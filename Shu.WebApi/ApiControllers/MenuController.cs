using Shu.Utility.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shu.Model;
using Shu.WebApi.Models;
using Shu.BLL;
using System.Web.Http.Controllers;
using Shu.Utility.Extensions;

namespace Shu.WebApi.ApiControllers
{
    /// <summary>
    /// 菜单功能
    /// </summary>
    public class MenuController : BaseController
    {
        /// <summary>
        /// 获取菜单权限列表
        /// </summary>
        /// <param name="keyId">Key</param>
        /// <param name="userId">用户ID</param>
        /// <returns>数据实体</returns>
        [ActionName("MenuList")]
        public List<MenuModels> GetMenuList(string RoleId,string appKey)
        {
            List<View_Sys_RolePurviewAndMenu> menuList = new Sys_RolePurviewBLL().GetMenus(RoleId);
            string javaScript = string.Empty;
            View_Sys_RolePurviewAndMenu RoleMenuInfo = menuList.Find(p => p.Menu_ModuleId == appKey);
            List<View_Sys_RolePurviewAndMenu> menuListLevel1 = new List<View_Sys_RolePurviewAndMenu>();
            if (RoleMenuInfo.IsNotNull())
            {
                menuListLevel1 = menuList.FindAll(p => p.Menu_ParentCode == RoleMenuInfo.Menu_Code).OrderBy(p => p.Menu_Sequence).ToList();
            }
            List<MenuModels> tree = new List<MenuModels>();

            menuListLevel1.ForEach(item =>
            {
                tree.Add(new MenuModels
                {
                    menuid = item.MenuID,
                    menuname = item.Menu_Name,
                    icon = "icon-" + item.Menu_IconName,
                    url = item.Menu_Url,
                    child = children(item.Menu_Code, menuList)
                });
            });

            return tree;
        }

        private List<MenuModels> children(string code, List<View_Sys_RolePurviewAndMenu> menuList)
        {
            List<MenuModels> tree = new List<MenuModels>();
            List<View_Sys_RolePurviewAndMenu> list = new List<View_Sys_RolePurviewAndMenu>();

            list = menuList.Where(p => p.Menu_ParentCode == code).OrderBy(p => p.Menu_Sequence).ToList();
            list.ForEach(item =>
            {
                tree.Add(new MenuModels
                {
                    menuid = item.MenuID,
                    menuname = item.Menu_Name,
                    icon = "icon-" + item.Menu_IconName,
                    url = item.Menu_Url,
                    child = children(item.Menu_Code, menuList)
                });
            });
            return tree;
        }
    }
}