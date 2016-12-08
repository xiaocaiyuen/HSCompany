using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Shu.Comm;
using Shu.BLL;
using Shu.Model;
using System.Web.SessionState;
using Newtonsoft.Json;
using Shu.Utility.Extensions;
using System.Data;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// AsynDeptTree 的摘要说明
    /// </summary>
    public class AsynDeptTree : IHttpHandler, IRequiresSessionState
    {
        public Sys_UserInfoBLL bllUser = new Sys_UserInfoBLL();
        public Sys_DepartmentBLL bllDep = new Sys_DepartmentBLL();
        private List<Sys_Department> NodeDepList = new List<Sys_Department>();
        public RoleConfig roleConfig = new RoleConfig();
        public string depCodeSelectedStr { get; set; }//部门树默认值
        SessionUserModel currentUser = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
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

            string Option = context.Request.QueryString["Option"];

            switch (Option)
            {
                case "Data":
                    this.getTreeData(context);
                    break;
                case "DepData":
                    this.getDepTreeData(context);
                    break;
                case "User":
                    this.getUserData(context);
                    break;
                case "AllDeptUserCheckBoxData"://全展开部门用户树 带Checkbox
                    this.AllDeptUserCheckBoxData(context);
                    break;
                case "AllDeptData"://全展开部门树
                    depCodeSelectedStr = context.Request.QueryString["DepSelectedStr"] == null ? "" : context.Request.QueryString["DepSelectedStr"];
                    this.AllDeptData(context);
                    break;
                case "AllUserSelected":  //全部展开用户树并将有勾选用户的勾上
                    this.AllDeptDataSelected(context);
                    break;
                case "AllDepSelected":
                    this.AllDepSelected(context);
                    break;
                case "OfficersManagement":
                    this.OfficersManagementDataSelected(context);//士官选取管理
                    break;
                case "CadresManagement":
                    this.CadresManagementDataSelected(context);//干部晋升管理
                    break;
                case "Dep":
                    this.DepSelected(context);// 单位下部门树
                    break;
                case "Menu"://用户菜单
                    this.GetMenu(context);
                    break;
                case "Role"://角色
                    this.GetRole(context);
                    break;

                case "SetServiceObject"://全展开部门树
                    this.SetServiceObject(context);
                    break;
                case "GetTrees"://全展开部门树
                    this.GetTrees(context);
                    break;
                //case "Province"://省
                //    this.Province(context);
                //    break;
                //case "City"://市
                //    this.City(context);
                //    break;
                //case "Area"://区域
                //    this.Area(context);
                //    break;
                case "RoleUser":
                    this.RoleUser(context);
                    break;
                //case "DataMapping":
                //    this.DataMapping(context);
                //    break;
            }
        }

        //private void DataMapping(HttpContext context)
        //{
        //    List<string> UploadDataTypeList = new Sys_UploadDataTypeBLL().FindWhere(p => p.UploadDataType_IsDelete == false).GroupBy(p => p.UploadDataType_ProcessStage).Select(p=>p.Key).ToList();
        //    List<combotree> comtree = new List<combotree>();
        //    foreach (var tree in UploadDataTypeList)
        //    {
        //        comtree.Add(new combotree { id = tree, text = tree });
        //    }
        //    string jsonPerson = JsonConvert.SerializeObject(comtree);
        //    context.Response.Write(jsonPerson);
        //}

        private void RoleUser(HttpContext context)
        {
            //用户ID
            StringBuilder strMenu = new StringBuilder();
            //string RoleID = context.Request.QueryString["roleid"];

            //string RoleID = currentUser.RoleID;
            string TaskPoolID = context.Request.QueryString["TaskPoolID"];
            string TaskPool_ModuleName = context.Request.QueryString["TaskPool_ModuleName"];
            string roleid = context.Request.QueryString["roleid"];

            List<Sys_UserInfo> UserList = new List<Sys_UserInfo>();
            //if (currentUser.RoleID == "1")
            //{
            //    UserList = new Sys_UserInfoBLL().FindWhere(p => p.UserInfo_WorkingState == null || p.UserInfo_WorkingState == "0");
            //}
            //else
            //{
            UserList = new Sys_UserInfoBLL().GetList(p => (p.UserInfo_WorkingState == null || p.UserInfo_WorkingState == "0") && p.UserInfo_RoleID.Contains(roleid)).ToList();
            //}

            List<combotree> comtree = new List<combotree>();
            foreach (var tree in UserList)
            {
                comtree.Add(new combotree { id = tree.UserInfoID, text = tree.UserInfo_FullName, attributes = TaskPoolID, iconCls = TaskPool_ModuleName });
            }
            string jsonPerson = JsonConvert.SerializeObject(comtree);
            strMenu.Append(jsonPerson);

            context.Response.Write(strMenu);
            context.Response.End();
        }

        private void GetRole(HttpContext context)
        {
            List<Sys_Role> entityList = new List<Sys_Role>();
            Sys_RoleBLL bll = new Sys_RoleBLL();
            List<combotree> comtree = new List<combotree>();
            entityList = bll.GetAll().ToList();
            foreach (var tree in entityList)
            {
                comtree.Add(new combotree { id = tree.RoleID, text = tree.Role_Name });
            }
            string jsonPerson = JsonConvert.SerializeObject(comtree);
            context.Response.Write(jsonPerson);
        }


        #region 用户菜单
        private void GetMenu(HttpContext context)
        {
            //用户ID
            StringBuilder strMenu = new StringBuilder();
            string userid = context.Request.QueryString["userid"];
            List<View_Sys_RolePurviewAndMenu> menuList = new Sys_RolePurviewBLL().GetRoleMenus(currentUser.UserID);//加载用户菜单

            List<View_Sys_RolePurviewAndMenu> menuLevel = menuList.FindAll(p => p.Menu_ParentCode == "10").OrderBy(p => p.Menu_Sequence).ToList();//一级菜单

            List<combotree> comtree = new List<combotree>();
            foreach (var tree in menuLevel)
            {
                comtree.Add(new combotree { id = tree.MenuID, text = tree.Menu_Name, children = SetMenu(menuList, tree.Menu_Code) });
            }
            string jsonPerson = JsonConvert.SerializeObject(comtree);
            strMenu.Append(jsonPerson);

            context.Response.Write(strMenu);
            context.Response.End();
        }

        private List<combotree> SetMenu(List<View_Sys_RolePurviewAndMenu> menuList, string Menu_Code)
        {
            StringBuilder strMenu = new StringBuilder();
            List<View_Sys_RolePurviewAndMenu> menuApp = menuList.FindAll(p => p.Menu_ParentCode == Menu_Code).OrderBy(p => p.Menu_Sequence).ToList();
            List<combotree> comtree = new List<combotree>();
            if (menuApp.Count > 0)
            {
                foreach (var tree in menuApp)
                {
                    List<combotree> menuChild = SetMenu(menuList, tree.Menu_Code);
                    comtree.Add(new combotree { id = tree.MenuID, text = tree.Menu_Name, children = menuChild });
                }
            }
            return comtree;

        }

        #endregion
        /// <summary>
        /// 部门树 
        /// </summary>
        /// <param name="context"></param>
        public void DepSelected(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            string parentId = context.Request.QueryString["pid"];
            if (parentId != "")
            {
                sb.Append("[");
                if (currentUser.DepartmentCode != "001")
                {
                    //NodeDepList = bllDep.FindWhere(" Department_Code in(" + coedwhere() + ")");
                    NodeDepList = bllDep.FindWhere("Department_ParentCode like dbo.F_GetUnitForDepCode('" + currentUser.DepartmentCode + "')+'%' or Department_ParentCode='0'");
                }
                else
                {
                    NodeDepList = bllDep.GetAll().ToList();

                }
                sb.Append(BindTree(NodeDepList, parentId));
                sb.Remove(sb.Length - 1, 1);
                sb.Append("}]");
            }
            context.Response.Write(sb.ToString());

        }



        #region 全展开部门用户树 带Checkbox
        /// <summary>
        /// 全展开部门用户树 带Checkbox
        /// </summary>
        /// <param name="context"></param>
        public void AllDeptUserCheckBoxData(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            string parentId = "";

            string seeCharge = "1";
            if (!string.IsNullOrEmpty(context.Request.QueryString["depseecharge"]))
            {
                seeCharge = context.Request.QueryString["depseecharge"];
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pid"]))
            {
                parentId = context.Request.QueryString["pid"];
            }
            if (!parentId.Equals(""))
            {
                sb.Append("[");
                //获得部门条件
                if (currentUser.DepartmentCode != "001")
                {
                    //NodeDepList = bllDep.FindWhere(" Department_Code in(" + coedwhere() + ")");
                    NodeDepList = bllDep.FindWhere("Department_ParentCode like dbo.F_GetUnitForDepCode('" + currentUser.DepartmentCode + "')+'%' or Department_ParentCode='0'");
                }
                else
                {
                    NodeDepList = bllDep.GetAll().ToList();

                }
                if (NodeDepList.Count != 0)
                {
                    sb.Append(AllDeptUserCheckBoxDataBindTree(NodeDepList, parentId, context));
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append("}]");
            }
            context.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 递归树
        /// </summary>
        /// <param name="depList"></param>
        /// <param name="PCode"></param>
        /// <returns></returns>
        private StringBuilder AllDeptUserCheckBoxDataBindTree(List<Sys_Department> depList, string PCode, HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            List<Sys_Department> deps = new List<Sys_Department>();
            deps = NodeDepList.FindAll(p => p.Department_ParentCode == PCode).OrderBy(p => p.Department_Sequence).ToList();
            int depsCount = deps.Count;
            if (depsCount > 0)
            {
                for (int d = 0; d < depsCount; d++)
                {
                    sb.Append("{");
                    List<Sys_Department> list = NodeDepList.FindAll(p => p.Department_ParentCode == deps[d].Department_Code);  //判断当前节点是否还有子节点
                    if (list.Count == 0)
                    { //没有子节点  设置 state 为空
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "_Dep\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                        sb.Append(getUserDataByDepCode(deps[d].Department_Code, context));
                    }
                    else
                    {
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "_Dep\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                        sb.Append(",\"children\":[");
                        string userJason = getUserDataByDepCodeEx(deps[d].Department_Code, context);
                        if (userJason != "")
                        {
                            sb.Append(userJason);
                            sb.Append(",");
                        }
                        sb.Append(AllDeptUserCheckBoxDataBindTree(depList, deps[d].Department_Code, context));
                        sb.Append("]");
                    }
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return sb;
        }

        #endregion

        #region 全部展开部门树
        /// <summary>
        /// 全展开部门用户树 带Checkbox
        /// </summary>
        /// <param name="context"></param>
        public void AllDeptData(HttpContext context)
        {

            StringBuilder sb = new StringBuilder();
            string parentId = "";
            if (!string.IsNullOrEmpty(context.Request.QueryString["pid"]))
            {
                parentId = context.Request.QueryString["pid"];
            }

            string seeCharge = "4";

            //if (!string.IsNullOrEmpty(context.Request.QueryString["depseecharge"]))
            //{
            //    seeCharge = context.Request.QueryString["depseecharge"];
            //}

            if (!parentId.Equals(""))
            {
                sb.Append("[");
                //获得部门条件
                //string depWhere = roleConfig.GetDepTreeViewRange(seeCharge);
                if (currentUser.DepartmentCode != "001")
                {
                    //NodeDepList = bllDep.FindWhere("Department_Code in(" + coedwhere() + ")");

                    //查看本单位的组织机构暂时屏蔽
                    //NodeDepList = bllDep.FindWhere("Department_Code like dbo.F_GetUnitForDepCode('" + currentUser.DepartmentCode + "')+'%' or Department_ParentCode='0'");
                    NodeDepList = bllDep.GetList(p => p.Department_IsDel == "0").ToList();
                }
                else { NodeDepList = bllDep.GetList(p => p.Department_IsDel == "0").ToList(); }
                if (NodeDepList.Count != 0)
                {
                    sb.Append(AllDeptDataBindTree(NodeDepList, parentId));
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append("}]");
            }
            context.Response.Write(sb.ToString());
        }


        /// <summary>
        /// 判定一个部门代码是否一个集合中
        /// </summary>
        /// <param name="depCode"></param>
        /// <returns></returns>
        public bool DepCodeIfExist(string depCode)
        {
            bool bRtn = false;
            string[] list = this.depCodeSelectedStr.Split(',');
            foreach (string s in list)
            {
                if (s == depCode)
                {
                    bRtn = true;
                    break;
                }
            }
            return bRtn;
        }



        /// <summary>
        /// 递归树
        /// </summary>
        /// <param name="depList"></param>
        /// <param name="PCode"></param>
        /// <returns></returns>
        private StringBuilder AllDeptDataBindTree(List<Sys_Department> depList, string PCode)
        {
            StringBuilder sb = new StringBuilder();
            List<Sys_Department> deps = new List<Sys_Department>();
            deps = NodeDepList.FindAll(p => p.Department_ParentCode == PCode).OrderBy(p => p.Department_Sequence).ToList();
            int depsCount = deps.Count;
            if (depsCount > 0)
            {
                for (int d = 0; d < depsCount; d++)
                {
                    sb.Append("{");
                    List<Sys_Department> list = NodeDepList.FindAll(p => p.Department_ParentCode == deps[d].Department_Code);  //判断当前节点是否还有子节点
                    if (list.Count == 0)
                    { //没有子节点  设置 state 为空
                        if (this.DepCodeIfExist(deps[d].Department_Code))
                        {
                            sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"checked\":true,\"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                        }
                        else
                        {
                            sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                        }
                    }
                    else
                    {
                        if (this.DepCodeIfExist(deps[d].Department_Code))
                        {
                            sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"checked\":true,\"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                        }
                        else
                        {
                            sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                        }
                        sb.Append(",\"children\":[");
                        sb.Append(AllDeptDataBindTree(depList, deps[d].Department_Code));
                        sb.Append("]");
                    }
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return sb;
        }
        #endregion

        /// <summary>
        /// 初始化组织结构
        /// </summary>
        /// <param name="context"></param>
        public void getTreeData(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            string parentId = "";
            if (!string.IsNullOrEmpty(context.Request.QueryString["pid"]))
            {
                parentId = context.Request.QueryString["pid"];
            }

            string seeCharge = "部门";

            if (!string.IsNullOrEmpty(context.Request.QueryString["depseecharge"]))
            {
                seeCharge = "部门树";
            }

            if (!parentId.Equals(""))
            {
                sb.Append("[");
                //获得部门条件
                if (currentUser.DepartmentCode != "001")
                {
                    //NodeDepList = bllDep.FindWhere(" Department_Code in(" + coedwhere() + ")");
                    NodeDepList = bllDep.FindWhere("Department_Code like dbo.F_GetUnitForDepCode('" + currentUser.DepartmentCode + "')+'%' or Department_ParentCode='0'");
                }
                else
                {
                    NodeDepList = bllDep.GetAll().ToList();

                }
                if (NodeDepList.Count != 0)
                {
                    sb.Append(BindTree(NodeDepList, parentId));
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append("}]");
            }
            context.Response.Write(sb.ToString());
        }

        private StringBuilder BindTree(List<Sys_Department> depList, string PCode)
        {
            StringBuilder sb = new StringBuilder();
            List<Sys_Department> deps = new List<Sys_Department>();
            deps = NodeDepList.FindAll(p => p.Department_ParentCode == PCode).OrderBy(p => p.Department_Sequence).ToList();
            int depsCount = deps.Count;
            if (depsCount > 0)
            {
                for (int d = 0; d < depsCount; d++)
                {
                    sb.Append("{");
                    List<Sys_Department> list = NodeDepList.FindAll(p => p.Department_ParentCode == deps[d].Department_Code);  //判断当前节点是否还有子节点
                    if (list.Count == 0)
                    { //没有子节点  设置 state 为空
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                        //sb.Append( getUserDataByDepCode(deps[d].Department_Code));
                    }
                    else
                    {
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                        sb.Append(",\"children\":[");
                        sb.Append(BindTree(depList, deps[d].Department_Code));
                        sb.Append("]");
                    }
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return sb;
        }


        /// <summary>
        /// 根据部门代码获取改部门下面的所有人
        /// </summary>
        /// <param name="context"></param>
        public void getUserData(HttpContext context)
        {
            string depCode = string.Empty;
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(context.Request.QueryString["pid"]))
            {
                depCode = context.Request.QueryString["pid"];
            }

            string seeCharge = "个人";

            if (!string.IsNullOrEmpty(context.Request.QueryString["perseecharge"]))
            {
                seeCharge = "个人树";
            }

            if (!depCode.Equals(""))
            {
                List<Sys_UserInfo> users = bllUser.GetList(p=>p.UserInfo_DepCode== depCode).OrderBy(p => p.UserInfo_Sequence).ToList();//"UserInfo_DepCode='" + depCode + "'"
                int usersCount = users.Count;
                if (usersCount > 0)
                {
                    sb.Append("[");
                    for (int i = 0; i < usersCount; i++)
                    {
                        sb.Append("{");
                        sb.Append("\"id\": \"" + users[i].UserInfoID + "\", \"text\": \"" + users[i].UserInfo_FullName + "\", \"iconCls\": \"\", \"state\": \"\"");
                        sb.Append("},");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");
                }
            }
            context.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="depCode"></param>
        /// <returns></returns>
        public string getUserDataByDepCode(string depCode, HttpContext context)
        {
            StringBuilder sb = new StringBuilder();

            string seeCharge = "1";

            if (!string.IsNullOrEmpty(context.Request.QueryString["perseecharge"]))
            {
                seeCharge = context.Request.QueryString["perseecharge"];
            }

            if (!depCode.Equals(""))
            {

                //string userWhere = new RoleConfig().FindRange(seeCharge, "UserInfoID");
                //string userWhere = new RoleConfig().GetUserTreeViewRange(seeCharge);

                //List<View_Sys_UserInfo> users = bllUser.FindViewUserList(userWhere + " and UserInfo_DepCode='" + depCode + "'").OrderBy(p => p.UserInfo_Sequence).ToList();
                View_Sys_UserInfoBLL bllUser = new View_Sys_UserInfoBLL();

                List<View_Sys_UserInfo> users = bllUser.GetList(p => p.UserInfo_DepCode == depCode).OrderBy(p => p.UserInfo_Sequence).ToList();

                int usersCount = users.Count;
                if (usersCount > 0)
                {
                    sb.Append(",\"children\":[");
                    for (int i = 0; i < usersCount; i++)
                    {
                        sb.Append("{");
                        sb.Append("\"id\": \"" + users[i].UserInfoID + "\", \"text\": \"" + users[i].UserInfo_FullName + "\", \"iconCls\": \"icon-add\", \"state\": \"\"");
                        sb.Append("},");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");
                }
            }
            return sb.ToString();
        }


        public string getUserDataByDepCodeEx(string depCode, HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            string seeCharge = "1";
            if (!string.IsNullOrEmpty(context.Request.QueryString["perseecharge"]))
            {
                seeCharge = context.Request.QueryString["perseecharge"];
            }
            if (!depCode.Equals(""))
            {
                //string userWhere = new RoleConfig().FindRange(seeCharge, "UserInfoID");
                //string userWhere = new RoleConfig().GetUserTreeViewRange(seeCharge);
                //List<View_Sys_UserInfo> users = bllUser.FindViewUserList(userWhere + " and UserInfo_DepCode='" + depCode + "'").OrderBy(p => p.UserInfo_Sequence).ToList();
                View_Sys_UserInfoBLL bllUser = new View_Sys_UserInfoBLL();
                List<View_Sys_UserInfo> users = bllUser.GetList(p => p.UserInfo_DepCode == depCode).OrderBy(p => p.UserInfo_Sequence).ToList();
                int usersCount = users.Count;
                if (usersCount > 0)
                {
                    for (int i = 0; i < usersCount; i++)
                    {
                        sb.Append("{");
                        sb.Append("\"id\": \"" + users[i].UserInfoID + "\", \"text\": \"" + users[i].UserInfo_FullName + "\", \"iconCls\": \"icon-add\", \"state\": \"\"");
                        sb.Append("},");
                    }
                    sb.Remove(sb.Length - 1, 1);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 初始化组织结构
        /// </summary>
        /// <param name="context"></param>
        public void getDepTreeData(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();

            string seeCharge = "部门";

            if (!string.IsNullOrEmpty(context.Request.QueryString["depseecharge"]))
            {
                seeCharge = "部门树";
            }

            string parentId = "";
            if (!string.IsNullOrEmpty(context.Request.QueryString["pid"]))
            {
                parentId = context.Request.QueryString["pid"];
            }
            if (!parentId.Equals(""))
            {
                sb.Append("[");
                //获得部门条件
                if (currentUser.DepartmentCode != "001")
                {
                    //NodeDepList = bllDep.FindWhere(" Department_Code in(" + coedwhere() + ")");
                    NodeDepList = bllDep.FindWhere("Department_Code like dbo.F_GetUnitForDepCode('" + currentUser.DepartmentCode + "')+'%' or Department_ParentCode='0'");
                }
                else
                {
                    NodeDepList = bllDep.GetAll().ToList();

                }
                if (NodeDepList.Count != 0)
                {
                    sb.Append(BindDepTree(NodeDepList, parentId));
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append("}]");
            }
            context.Response.Write(sb.ToString());
        }


        private StringBuilder BindDepTree(List<Sys_Department> depList, string PCode)
        {
            StringBuilder sb = new StringBuilder();
            List<Sys_Department> deps = new List<Sys_Department>();
            deps = NodeDepList.FindAll(p => p.Department_ParentCode == PCode).OrderBy(p => p.Department_Sequence).ToList();
            int depsCount = deps.Count;
            if (depsCount > 0)
            {
                for (int d = 0; d < depsCount; d++)
                {

                    sb.Append("{");
                    List<Sys_Department> list = NodeDepList.FindAll(p => p.Department_ParentCode == deps[d].Department_Code);  //判断当前节点是否还有子节点
                    if (list.Count == 0)
                    { //没有子节点  设置 state 为空
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                    }
                    else
                    {
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                        sb.Append(",\"children\":[");
                        sb.Append(BindTree(depList, deps[d].Department_Code));
                        sb.Append("]");
                    }
                    sb.Append("},");

                }
                sb.Remove(sb.Length - 1, 1);
            }
            return sb;
        }

        #region 组织结构树
        private void GetTrees(HttpContext context)
        {

            StringBuilder strMenu = new StringBuilder();
            string depcode = context.Request.QueryString["depcode"];
            if (depcode == "001")
            {
                NodeDepList = bllDep.GetAll().ToList();
            }
            else
            {
                NodeDepList = bllDep.FindWhere("Department_ParentCode like dbo.F_GetUnitForDepCode('" + currentUser.DepartmentCode + "')+'%' or Department_Code=dbo.F_GetUnitForDepCode('" + currentUser.DepartmentCode + "')");
            }
            List<combotree> comtree = new List<combotree>();
            comtree.Add(new combotree { id = "001", text = "广西公安消防总队", children = ChildrenMenu(NodeDepList, "001") });
            string jsonPerson = JsonConvert.SerializeObject(comtree);
            strMenu.Append(jsonPerson);
            context.Response.Write(strMenu);
            context.Response.End();
        }

        private List<combotree> ChildrenMenu(List<Sys_Department> menuList, string Menu_Code)
        {
            StringBuilder strMenu = new StringBuilder();
            List<Sys_Department> menuApp = menuList.FindAll(p => p.Department_ParentCode == Menu_Code).OrderBy(p => p.Department_Sequence).ToList();
            List<combotree> comtree = new List<combotree>();
            if (menuApp.Count > 0)
            {
                foreach (var tree in menuApp)
                {
                    List<combotree> menuChild = ChildrenMenu(menuList, tree.Department_Code);
                    comtree.Add(new combotree
                    {
                        id = tree.Department_Code,
                        text = tree.Department_Name,
                        children = menuChild
                    });

                }
            }
            return comtree;

        }

        #endregion

















        public bool IsReusable
        {
            get
            {
                return false;
            }
        }



        #region 全部展开用户树并将有勾选用户的勾上
        public void AllDeptDataSelected(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            string parentId = "";

            //string seeCharge = "部门";
            //if (!string.IsNullOrEmpty(context.Request.QueryString["depseecharge"]))
            //{
            //    seeCharge = "部门树";
            //}

            if (!string.IsNullOrEmpty(context.Request.QueryString["pid"]))
            {
                parentId = context.Request.QueryString["pid"];
            }
            if (!parentId.Equals(""))
            {
                sb.Append("[");
                if (currentUser.DepartmentCode != "001")
                {
                    //NodeDepList = bllDep.FindWhere(" Department_Code in(" + coedwhere() + ")");
                    NodeDepList = bllDep.FindWhere("Department_Code like dbo.F_GetUnitForDepCode('" + currentUser.DepartmentCode + "')+'%' or Department_ParentCode='0'");
                }
                else
                {
                    NodeDepList = bllDep.GetAll().ToList();

                }
                if (NodeDepList.Count != 0)
                {
                    sb.Append(AllDeptUserCheckBoxDataBindTreeSelected(NodeDepList, parentId, context));
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append("}]");
            }
            context.Response.Write(sb.ToString());
        }


        /// <summary>
        /// 递归树
        /// </summary>
        /// <param name="depList"></param>
        /// <param name="PCode"></param>
        /// <returns></returns>
        private StringBuilder AllDeptUserCheckBoxDataBindTreeSelected(List<Sys_Department> depList, string PCode, HttpContext context)
        {

            StringBuilder sb = new StringBuilder();
            List<Sys_Department> deps = new List<Sys_Department>();
            deps = NodeDepList.FindAll(p => p.Department_ParentCode == PCode).OrderBy(p => p.Department_Sequence).ToList();
            int depsCount = deps.Count;
            if (depsCount > 0)
            {
                for (int d = 0; d < depsCount; d++)
                {

                    sb.Append("{");
                    List<Sys_Department> list = NodeDepList.FindAll(p => p.Department_ParentCode == deps[d].Department_Code);  //判断当前节点是否还有子节点
                    if (list.Count == 0)
                    { //没有子节点  设置 state 为空
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "_Dep\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                        sb.Append(getUserDataByDepCodeSelected(deps[d].Department_Code, context));
                    }
                    else
                    {
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "_Dep\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
                        sb.Append(",\"children\":[");
                        sb.Append(AllDeptUserCheckBoxDataBindTreeSelected(depList, deps[d].Department_Code, context));
                        sb.Append("]");
                    }
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return sb;
        }

        public string getUserDataByDepCodeSelected(string depCode, HttpContext context)
        {

            //string id = context.Request.QueryString["id"];
            //List<Supervise_NetworkPer> preList = bllPer.FindWhere(" NetworkPer_ManagerID='" + id + "'");

            //StringBuilder sb = new StringBuilder();

            ////string seeCharge = "个人";

            ////if (!string.IsNullOrEmpty(context.Request.QueryString["perseecharge"]))
            ////{
            ////    seeCharge = "个人树";
            ////}

            //if (!depCode.Equals(""))
            //{

            //    string userWhere = " 1=1 ";
            //    //userWhere += " and ( UserInfo_RoleID like '%02f13817-9f74-4169-b279-4b00cc741a91%'"; //普通领导
            //    //userWhere += " or UserInfo_RoleID  like '%047d082b-4d05-40c8-ae05-38b14e5ea89d%'"; //纪检监察负责人
            //    //userWhere += "  or UserInfo_RoleID  like '%0e3e6bb4-b075-4231-b368-38929edb3f17%'"; //主管领导
            //    //userWhere += "  or UserInfo_RoleID  like '%1f8f19da-6b68-4eb8-84a2-31987023c690%'"; //部门负责人
            //    //userWhere += "  or UserInfo_RoleID  like '%2d3a42e3-8123-4caa-ac81-f442596d4889%'"; //纪检监察负责人
            //    //userWhere += "  or UserInfo_RoleID  like '%50596676-09bf-49a2-b892-e9244fea82e3%'"; //教职工
            //    //userWhere += "  or UserInfo_RoleID  like '%5fa41d62-67f3-43d1-bb51-cc9e21afcfd5%'"; //安全管理负责人
            //    //userWhere += "  or UserInfo_RoleID  like '%a47dd93d-9598-45ed-9de4-252310f66324%'"; //纪委领导
            //    //userWhere += "  or UserInfo_RoleID  like '%ce5e3e4c-c445-4845-9e97-3f5044fc3de6%'"; //学籍管理负责人
            //    //userWhere += "  or UserInfo_RoleID  like '%d075a1e3-9d67-41dd-96d1-08d2a4c44615%'"; //教师管理负责人
            //    //userWhere += "  or UserInfo_RoleID  like '%5fa41d62-67f3-43d1-bb51-cc9e21afcfd5%'"; //安全管理负责人
            //    //userWhere += "  or UserInfo_RoleID  like '%dc470a6d-f901-4a8a-8233-0d7381e1a522%')"; //分管领导
            //    userWhere += "  and UserInfo_Status='0301'";
            //    List<Sys_UserInfo> users = bllUser.FindWhere(userWhere + "and UserInfo_DepCode='" + depCode + "'").OrderBy(p => p.UserInfo_Sequence).ToList();
            //    int usersCount = users.Count;
            //    if (usersCount > 0)
            //    {
            //        sb.Append(",\"children\":[");
            //        for (int i = 0; i < usersCount; i++)
            //        {
            //            string check = "";
            //            for (int j = 0; j < preList.Count; j++)
            //            {
            //                if (preList[j].NetworkPer_PersonID == users[i].UserInfoID)
            //                {
            //                    check = ", \"checked\": \"true\"";
            //                }
            //            }
            //            sb.Append("{");
            //            sb.Append("\"id\": \"" + users[i].UserInfoID + "\", \"text\": \"" + users[i].UserInfo_FullName + "\", \"iconCls\": \"\", \"state\": \"\"" + check);
            //            sb.Append("},");
            //        }
            //        sb.Remove(sb.Length - 1, 1);
            //        sb.Append("]");
            //    }
            //}
            //return sb.ToString();
            return "";
        }
        #endregion


        /// <summary>
        /// 全展开部门用户树 带Checkbox
        /// </summary>
        /// <param name="context"></param>
        public void AllDepSelected(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            NodeDepList = bllDep.GetAll().ToList();
            if (NodeDepList.Count != 0)
            {
                string depcode = GetSelectedDepCode(context.Request.QueryString["id"]);
                var da = depcode.Split(',');
                sb.Append(AllDepBindTree(NodeDepList, "0", da));
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("}]");
            context.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 网络评廉部门
        /// </summary>
        /// <returns></returns>
        private string GetSelectedDepCode(string id)
        {
            string str = "";
            //List<Supervise_NetworkDep> lis = bllNetDep.FindWhere(" NetworkDep_ManagerID='" + id + "'");
            //for (int i = 0; i < lis.Count; i++)
            //{
            //    str += lis[i].NetworkDep_DepCode + ",";
            //}
            //if (str.Length > 0)
            //{
            //    str = str.Substring(0, str.Length - 1);
            //}
            return str;
        }

        /// <summary>
        /// 递归树
        /// </summary>
        /// <param name="depList"></param>
        /// <param name="PCode"></param>
        /// <returns></returns>
        private StringBuilder AllDepBindTree(List<Sys_Department> depList, string PCode, string[] da)
        {


            StringBuilder sb = new StringBuilder();
            List<Sys_Department> deps = new List<Sys_Department>();
            deps = NodeDepList.FindAll(p => p.Department_ParentCode == PCode).OrderBy(p => p.Department_Sequence).ToList();
            int depsCount = deps.Count;
            if (depsCount > 0)
            {
                for (int d = 0; d < depsCount; d++)
                {
                    string check = "";

                    for (int s = 0; s < da.Count(); s++)
                    {
                        if (deps[d].Department_Code == da[s])
                        {
                            check = ", \"checked\": \"true\"";

                        }

                    }


                    sb.Append("{");
                    List<Sys_Department> list = NodeDepList.FindAll(p => p.Department_ParentCode == deps[d].Department_Code);  //判断当前节点是否还有子节点
                    if (list.Count == 0)
                    { //没有子节点  设置 state 为空
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"" + check);
                    }
                    else
                    {
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"" + check);
                        sb.Append(",\"children\":[");
                        sb.Append(AllDepBindTree(depList, deps[d].Department_Code, da));
                        sb.Append("]");
                    }
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return sb;
        }






        #region 批次管理---------------------------------------------------------------------------------------
        //
        /// <summary>
        /// 士官选取条件管理
        /// </summary>
        /// <param name="context"></param>
        public void OfficersManagementDataSelected(HttpContext context)
        {
            //string code=context.Request["code"];
            //string Option = context.Request.QueryString["Option"];
            //string[] a = new string[2]; ;
            //if (string.IsNullOrEmpty(code))
            //{
            //    code = "1"; a[0] = "1"; a[1] = "101";

            //}
            //else
            //{
            //    a[0] = "1"; a[1] = "101";


            //}
            //StringBuilder sb = new StringBuilder();
            //sb.Append("[");
            //NodeBBList = bllbb.FindWhere(p => p.BatchManagement_Dep.StartsWith(currentUser.DepartmentCode)||a.Contains(p.BatchManagement_ParentCode));
            //sb.Append(BatchBindTrees(NodeBBList, code));
            //sb.Remove(sb.Length - 1, 1);
            //sb.Append("}]");
            //context.Response.Write(sb.ToString());
        }


        //
        /// <summary>
        /// 干部选取条件管理
        /// </summary>
        /// <param name="context"></param>
        public void CadresManagementDataSelected(HttpContext context)
        {
            //string code = context.Request["code"];
            //string Option = context.Request.QueryString["Option"];
            //string[] a = new string[2]; ;
            //if (string.IsNullOrEmpty(code))
            //{
            //  code = "1"; a[0] = "1"; a[1] = "101"; 

            //}
            //else
            //{
            //    a[0] = "1"; a[1] = "101"; 


            //}
            //StringBuilder sb = new StringBuilder();
            //sb.Append("[");
            //NodeBBListCadres = bllbbc.FindWhere(p => p.BatchManagement_Dep.StartsWith(currentUser.DepartmentCode) || a.Contains(p.BatchManagement_ParentCode));
            //sb.Append(BatchBindTreesCadres(NodeBBListCadres, code));
            //sb.Remove(sb.Length - 1, 1);
            //sb.Append("}]");
            //context.Response.Write(sb.ToString());
        }
        /// <summary>
        /// 递归树
        /// </summary>
        /// <param name="depList"></param>
        /// <param name="PCode"></param>
        /// <returns></returns>
        //private StringBuilder BatchBindTrees(List<Batch_BatchManagement> depList, string PCode)
        //{


        //    StringBuilder sb = new StringBuilder();
        //    List<Batch_BatchManagement> deps = new List<Batch_BatchManagement>();
        //    deps = NodeBBList.FindAll(p => p.BatchManagement_ParentCode == PCode).OrderBy(p => p.BatchManagement_Sort).ToList();
        //    int depsCount = deps.Count;
        //    if (depsCount > 0)
        //    {
        //        for (int d = 0; d < depsCount; d++)
        //        {

        //            sb.Append("{");
        //            List<Batch_BatchManagement> list = NodeBBList.FindAll(p => p.BatchManagement_ParentCode == deps[d].BatchManagement_Code);  //判断当前节点是否还有子节点
        //            if (list.Count == 0)
        //            { //没有子节点  设置 state 为空
        //                sb.Append("\"id\": \"" + deps[d].BatchManagement_Code + "\", \"text\": \"" + deps[d].BatchManagement_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
        //            }
        //            else
        //            {
        //                sb.Append("\"id\": \"" + deps[d].BatchManagement_Code + "\", \"text\": \"" + deps[d].BatchManagement_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
        //                sb.Append(",\"children\":[");
        //                sb.Append(BatchBindTrees(NodeBBList, deps[d].BatchManagement_Code));
        //                sb.Append("]");
        //            }
        //            sb.Append("},");

        //        }
        //        sb.Remove(sb.Length - 1, 1);
        //    }
        //    return sb;
        //}
        /// <summary>
        /// 递归树
        /// </summary>
        /// <param name="depList"></param>
        /// <param name="PCode"></param>
        /// <returns></returns>
        //private StringBuilder BatchBindTreesCadres(List<Cadres_BatchManagement> depList, string PCode)
        //{


        //    StringBuilder sb = new StringBuilder();
        //    List<Cadres_BatchManagement> deps = new List<Cadres_BatchManagement>();
        //    deps = NodeBBListCadres.FindAll(p => p.BatchManagement_ParentCode == PCode).OrderBy(p => p.BatchManagement_Sort).ToList();
        //    int depsCount = deps.Count;
        //    if (depsCount > 0)
        //    {
        //        for (int d = 0; d < depsCount; d++)
        //        {

        //            sb.Append("{");
        //            List<Cadres_BatchManagement> list = NodeBBListCadres.FindAll(p => p.BatchManagement_ParentCode == deps[d].BatchManagement_Code);  //判断当前节点是否还有子节点
        //            if (list.Count == 0)
        //            { //没有子节点  设置 state 为空
        //                sb.Append("\"id\": \"" + deps[d].BatchManagement_Code + "\", \"text\": \"" + deps[d].BatchManagement_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
        //            }
        //            else
        //            {
        //                sb.Append("\"id\": \"" + deps[d].BatchManagement_Code + "\", \"text\": \"" + deps[d].BatchManagement_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
        //                sb.Append(",\"children\":[");
        //                sb.Append(BatchBindTreesCadres(NodeBBListCadres, deps[d].BatchManagement_Code));
        //                sb.Append("]");
        //            }
        //            sb.Append("},");

        //        }
        //        sb.Remove(sb.Length - 1, 1);
        //    }
        //    return sb;
        //}
        #endregion




        public string coedwhere()
        {
            string Code = currentUser.DepartmentCode;
            string Codes = "";
            NodeDepList = bllDep.FindWhere("Department_ParentCode like '" + Code + "%'");
            int a = Code.Length;
            int t = a / 3;
            int s = 0;
            for (int i = 0; i < t; i++)
            {
                Codes += "'" + Code.Substring(0, s += 3) + "',";
            }

            if (NodeDepList.Count > 0)
            {
                Codes += "'" + Code + "',";
                foreach (var item in NodeDepList)
                {
                    Codes += "'" + item.Department_Code + "',";
                }

            }
            return Codes.Substring(0, Codes.Length - 1);
        }


        #region 服务对象
        /// <summary>
        /// 全展开服务对象树 带Checkbox
        /// </summary>
        /// <param name="context"></param>
        public void SetServiceObject(HttpContext context)
        {
            //View_LawEnfoReturn_ServiceObjectBLL view_bll = new View_LawEnfoReturn_ServiceObjectBLL();
            //List<View_LawEnfoReturn_ServiceObject> NodeObjectList = new List<View_LawEnfoReturn_ServiceObject>();
            //StringBuilder sb = new StringBuilder();
            //string parentId = "";
            //if (!string.IsNullOrEmpty(context.Request.QueryString["pid"]))
            //{
            //    parentId = context.Request.QueryString["pid"];
            //}

            //string seeCharge = "1";

            //if (!string.IsNullOrEmpty(context.Request.QueryString["depseecharge"]))
            //{
            //    seeCharge = context.Request.QueryString["depseecharge"];
            //}

            //if (!parentId.Equals(""))
            //{
            //    sb.Append("[");
            //    //获得部门条件
            //    NodeObjectList = view_bll.FindWhere(p => p.UserInfo_DepCode.Contains(parentId));
            //    if (NodeObjectList.Count != 0)
            //    {
            //        sb.Append(ServiceObjectDataBindTree(NodeObjectList));
            //    }

            //    sb.Remove(sb.Length - 1, 1);
            //    sb.Append("}]");
            //}
            //context.Response.Write(sb.ToString());
        }


        /// <summary>
        /// 递归树
        /// </summary>
        /// <param name="depList"></param>
        /// <param name="PCode"></param>
        /// <returns></returns>
        //private StringBuilder ServiceObjectDataBindTree(List<View_LawEnfoReturn_ServiceObject> depList)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    int depsCount = depList.Count;
        //    if (depsCount > 0)
        //    {
        //        for (int d = 0; d < depsCount; d++)
        //        {
        //            sb.Append("{");
        //            //if (this.DepCodeIfExist(deps[d].Department_Code))
        //            //{
        //            //    sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"checked\":true,\"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"");
        //            //}
        //            //else
        //            //{
        //            sb.Append("\"id\": \"" + depList[d].ServiceObjectID + "\", \"text\": \"" + depList[d].ServiceObject_Unit + "\", \"iconCls\": \"\", \"state\": \"\"");
        //            //}
        //            sb.Append("},");
        //        }
        //        sb.Remove(sb.Length - 1, 1);
        //    }
        //    return sb;
        //}
        #endregion
    }
}