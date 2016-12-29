using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Shu.Model;
using Shu.Comm;
using Shu.BLL;
using System.Text;
using System.Configuration;
using Shu.Utility.Extensions;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// IndexHandler 的摘要说明
    /// </summary>
    public class IndexHandler : IHttpHandler, IRequiresSessionState
    {
        private static readonly string SystemModuleId = ConfigurationManager.AppSettings["SSOAppKey"];//系统模块ID及AppKey唯一标识
        SessionUserModel currentUser = null;
        public Sys_MenuBLL bllMenu = new Sys_MenuBLL();
        public List<View_Sys_RolePurviewAndMenu> menuList = new List<View_Sys_RolePurviewAndMenu>();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");



            string method = context.Request.QueryString["method"];

            try
            {
                currentUser = context.Session["UserInfo"] as SessionUserModel;
            }
            catch
            {
                context.Response.Redirect("~/Login.aspx");
            }

            if (currentUser == null)
            {
                context.Response.Redirect("~/Login.aspx");
            }

            if (method == "modifypwd")
            {
                ModifyPassWord(context);
            }

            if (method == "LoadMenuTree")
            {
                LoadMenuTree(context);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 菜单加载


        /// <summary>
        /// 菜单加载
        /// </summary>
        /// <param name="context"></param>
        public void LoadMenuTree(HttpContext context)
        {
            menuList = new Sys_RolePurviewBLL().GetMenus(currentUser.RoleID);//new Sys_RolePurviewBLL().GetRoleMenus(currentUser.UserID);//加载用户菜单
            string javaScript=string.Empty;
            javaScript += "{ 'menus': [";
            View_Sys_RolePurviewAndMenu RoleMenuInfo = menuList.Find(p => p.Menu_ModuleId == SystemModuleId);
            List<View_Sys_RolePurviewAndMenu> menuListLevel1 = new List<View_Sys_RolePurviewAndMenu>();
            if (RoleMenuInfo.IsNotNull())
            {
                menuListLevel1 = menuList.FindAll(p => p.Menu_ParentCode == RoleMenuInfo.Menu_Code); //menuList.FindAll(p => p.Menu_ParentCode == "10");
            }
            
            foreach (View_Sys_RolePurviewAndMenu menuModel1 in menuListLevel1.OrderBy(p => p.Menu_Sequence))//一级菜单
            {
                javaScript += "{ 'menuid': '" + menuModel1.MenuID + "', 'menuname': '" + menuModel1.Menu_Name + "', 'icon': 'icon-" + menuModel1.Menu_IconName + "', 'menus': [";

                string menuLel2String = "";
                foreach (View_Sys_RolePurviewAndMenu menuModel2 in menuList.FindAll(p => p.Menu_ParentCode == menuModel1.Menu_Code).OrderBy(p => p.Menu_Sequence))//二级菜单
                {
                    menuLel2String += "{";
                    menuLel2String += "'menuid': '" + menuModel2.MenuID + "',";
                    menuLel2String += "'menuname': '" + menuModel2.Menu_Name + "',";
                    menuLel2String += "'icon': 'icon-" + menuModel2.Menu_IconName + "',";
                    List<View_Sys_RolePurviewAndMenu> list3 = menuList.FindAll(p => p.Menu_ParentCode == menuModel2.Menu_Code);
                    string menuLel3String = "";
                    if (list3.Count == 0)
                    {
                        menuLel2String += "'url': '" + menuModel2.Menu_Url + "'";
                    }
                    else
                    {
                        menuLel2String += "'url': '" + menuModel2.Menu_Url + "',";

                        menuLel2String += "'child': [";

                        foreach (View_Sys_RolePurviewAndMenu menuModel3 in list3.OrderBy(p => p.Menu_Sequence))//三级菜单
                        {
                            menuLel3String += "{";
                            menuLel3String += "'menuid': '" + menuModel3.MenuID + "',";
                            menuLel3String += "'menuname': '" + menuModel3.Menu_Name + "',";
                            menuLel3String += "'icon': 'icon-" + menuModel3.Menu_IconName + "',";
                            menuLel3String += "'url': '" + menuModel3.Menu_Url + "'";
                            menuLel3String += "},";
                        }
                        if (!menuLel3String.Equals(""))
                        {
                            menuLel3String = menuLel3String.Substring(0, menuLel3String.Length - 1);
                        }
                        else
                        {
                            menuLel3String = "{}";
                        }
                        menuLel3String += "]";
                    }


                    menuLel2String += menuLel3String + "},";
                }
                if (!menuLel2String.Equals(""))
                {
                    menuLel2String = menuLel2String.Substring(0, menuLel2String.Length - 1);
                }
                else
                {
                    menuLel2String = "";
                }
                javaScript += menuLel2String + "]},";
            }
            javaScript = javaScript.Substring(0, javaScript.Length - 1);
            javaScript += "]}";
            context.Response.Write(javaScript);
            context.Response.End();
        }

        /// <summary>
        /// 判定是否存在子菜单
        /// </summary>
        /// <param name="menuModel"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public string isSubMenuExists(View_Sys_RolePurviewAndMenu menuModel, int level)
        {
            List<Sys_Menu> list = new Sys_MenuBLL().GetList(p => p.Menu_ParentCode == menuModel.Menu_Code).ToList();
            if (list.Count > 0)
            {
                return "icon-nav2";
            }
            else
            {
                return "icon-nav" + menuModel.Menu_Name + "";
            }
        }

        /// <summary>
        /// 获取菜单图标
        /// </summary>
        /// <param name="menuName"></param>
        /// <returns></returns>
        public string getMenusIcon(string menuName)
        {
            string iconString = string.Empty;
            switch (menuName)
            {
                case "业务管理":
                    iconString = "Menu-YWGLXTB";
                    break;
                case "融资原件":
                    iconString = "Menu-RZYJGLXTB";
                    break;
                case "任务中心":
                    iconString = "Menu-SPGLXTB";
                    break;
                case "抵押管理":
                    iconString = "Menu-DYGLXTB";
                    break;
                case "逾期催收":
                    iconString = "Menu-YQCSXTB";
                    break;
                case "合同管理":
                    iconString = "Menu-HTGLXTB";
                    break;
                case "贷后管理":
                    iconString = "Menu-BDGLXTB";
                    break;
                case "产品管理":
                    iconString = "Menu-CPGLXTB";
                    break;
                case "还款管理":
                    iconString = "Menu-HKGLXTB";
                    break;
                case "流程设置":
                    iconString = "Menu-LCSZXTB";
                    break;
                case "统计查询":
                    iconString = "Menu-TJCXXTB";
                    break;
                case "系统管理":
                    iconString = "Menu-XTGLXTB";
                    break;
                case "车型管理":
                    iconString = "Menu-CXGLXTB";
                    break;
                case "金蝶EAS":
                    iconString = "Menu-JDEAS";
                    break;
                case "个人信息":
                    iconString = "Menu-Personal";
                    break;
                case "财务管理":
                    iconString = "Menu-Finance";
                    break;
                case "资产保理":
                    iconString = "Menu-ZiChan";
                    break;
            }
            return iconString;
        }
        #endregion

        #region 修改密码

        public void ModifyPassWord(HttpContext context)
        {
            string ypwd = DESEncrypt.Encrypt(HttpUtility.UrlDecode(context.Request.QueryString["ypwd"].ToString()));

            string pwd = HttpUtility.UrlDecode(context.Request.QueryString["pwd"].ToString());

            SessionUserModel currUserInfo = context.Session["UserInfo"] as SessionUserModel;

            Sys_UserInfoBLL bllUserInfo = new Sys_UserInfoBLL();
            List<Sys_UserInfo> userModelList = bllUserInfo.GetList(p=>p.UserInfo_LoginUserName==currUserInfo.LoginUserName && p.UserInfo_LoginUserPwd==ypwd).ToList();
            if (userModelList.Count > 0)
            {
                userModelList[0].UserInfo_LoginUserPwd = DESEncrypt.Encrypt(pwd);
                string msg = string.Empty;
                if (bllUserInfo.Update(userModelList[0]))
                {
                    context.Response.Write("success");
                }
                else
                {
                    context.Response.Write("error_pwd");
                }
            }
            else
            {
                context.Response.Write("error_pwd");
            }

        }
        #endregion
    }
}