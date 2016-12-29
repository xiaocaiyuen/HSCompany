using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shu.Model;
using Shu.Comm;

namespace Shu.BLL
{
    public class RoleConfig
    {
        #region 增加待办事项

        /// <summary>
        /// 增加待办事项
        /// </summary>
        /// <param name="moduleID">业务模块ID</param>
        /// <param name="moduleName">业务模块名称</param>
        /// <param name="SessionUserModel">用户信息</param>
        /// <param name="tasksInstanceID">工作流程ID</param>
        /// <param name="step">工作流程步骤（这里指当前步骤）</param>
        /// <param name="title">待办事项标题</param>
        /// <param name="url">待办事项地址</param>
        /// <returns></returns>
        public bool AddMatterTasks(string moduleID, string moduleName, SessionUserModel userInfo, string tasksInstanceID, int step, string title, string url)
        {
            try
            {
                string toRoleName = GetWorkFlowAuditObject(tasksInstanceID, step);
                if (!toRoleName.Equals(""))
                {
                    string[] roleArray = toRoleName.Split(',');
                    List<Sys_PendingMatter> pendingMatterList = new List<Sys_PendingMatter>();
                    foreach (string role in roleArray)//多角色
                    {
                        if (role != "")
                        {
                            Sys_PendingMatter pendingMatter = new Sys_PendingMatter();
                            pendingMatter.PendingMatterID = Guid.NewGuid().ToString();
                            pendingMatter.PendingMatter_ModuleID = moduleID;
                            pendingMatter.PendingMatter_Title = title;
                            pendingMatter.PendingMatter_ModuleName = moduleName;
                            pendingMatter.PendingMatter_ToRoleName = role;
                            pendingMatter.PendingMatter_URL = url;
                            pendingMatter.PendingMatter_AddTime = DateTime.Now;
                            pendingMatter.PendingMatter_AddUserID = userInfo.UserID;
                            pendingMatterList.Add(pendingMatter);
                        }
                    }
                    if (pendingMatterList.Count > 0)
                    {
                        this.DeleteMatterTasks(moduleID);//先删除
                        string msg = string.Empty;
                        new Sys_PendingMatterBLL().Add(pendingMatterList);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取流程的审核对象信息
        /// </summary>
        /// <param name="tasksInstanceID">工作流程ID</param>
        /// <param name="step">工作流程步骤</param>
        /// <returns></returns>
        public string GetWorkFlowAuditObject(string tasksInstanceID, int step)
        {
            string sRtn = string.Empty;
            //Sys_WorkflowNodeConfig workflowNodeConfig = new Sys_WorkflowNodeConfigBLL().Find(p => p.WorkflowNodeConfig_TasksInstanceID == tasksInstanceID && p.WorkflowNodeConfig_Setp == step);
            //if (workflowNodeConfig != null)
            //{
            //    if (workflowNodeConfig.WorkflowNodeConfig_AuditMode == "0")
            //    {
            //        sRtn = workflowNodeConfig.WorkflowNodeConfig_AuditObjectID;
            //    }
            //    if (workflowNodeConfig.WorkflowNodeConfig_AuditMode == "1")
            //    {
            //        sRtn = workflowNodeConfig.WorkflowNodeConfig_AuditObjectID;
            //    }
            //}
            return sRtn;
        }

        #region 通过模块菜单名称获取 审核按钮权限对应的角色
        /// <summary>
        /// 通过模块菜单名称获取 有审核按钮权限的角色
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public string GetAuditRoleByMouleName(string moduleName)
        {
            string rtnRoleStr = string.Empty;
            string auditButtonName = "审核";
            if (!moduleName.Equals(""))
            {
                List<View_PendingMatterToRolePurview> roleList = new View_PendingMatterToRolePurviewBLL().GetList(p => p.Menu_Name == moduleName && p.RolePurview_OperatePurview.Contains(auditButtonName)).ToList(); //new BLLSys_RolePurview().FindView_PendingMatterToRolePurview(" Menu_Name='" + moduleName + "' and  RolePurview_OperatePurview like '%" + auditButtonName + "%'");
                foreach (View_PendingMatterToRolePurview role in roleList)
                {
                    rtnRoleStr += role.Role_Name + ",";
                }
            }
            if (rtnRoleStr != "")
            {
                rtnRoleStr = rtnRoleStr.Substring(0, rtnRoleStr.Length - 1);
                //if (moduleName != Constant.SelfMonitorName && moduleName != Constant.PostRiskwh && moduleName != Constant.AskName)
                //{
                //    BasePage basePage = new BasePage();
                //    SessionUserModel CurrUser = basePage.CurrUserInfo();
                //    if (!string.IsNullOrEmpty(CurrUser.RoleName))
                //    {
                //        string[] currUserRoleArray = CurrUser.RoleName.Split(',');
                //        foreach (string currRole in currUserRoleArray)
                //        {
                //            if (rtnRoleStr.Contains(currRole))
                //            {
                //                rtnRoleStr = string.Empty;//当前角色已经是 待审核角色
                //                break;
                //            }
                //        }
                //    }
                //}

            }
            return rtnRoleStr;
        }

        ///// <summary>
        ///// 通过模块菜单名称获取 有审核按钮权限的角色
        ///// </summary>
        ///// <param name="moduleName">功能菜单名称</param>
        ///// <param name="buttonName">操作按钮名称</param>
        ///// <returns></returns>
        //public string GetAuditRoleByMouleName(string moduleName, string buttonName)
        //{
        //    string rtnRoleStr = string.Empty;
        //    if (!moduleName.Equals(""))
        //    {
        //        List<View_PendingMatterToRolePurview> roleList = new View_PendingMatterToRolePurviewBLL().GetList(p => p.Menu_Name == moduleName && p.RolePurview_OperatePurview.Contains(buttonName)).ToList();//" Menu_Name='" + moduleName + "' and  RolePurview_OperatePurview like '%" + buttonName + "%'");
        //        foreach (View_PendingMatterToRolePurview role in roleList)
        //        {
        //            rtnRoleStr += role.Role_Name + ",";
        //        }
        //    }
        //    if (rtnRoleStr != "")
        //    {
        //        rtnRoleStr = rtnRoleStr.Substring(0, rtnRoleStr.Length - 1);

        //        BasePage basePage = new BasePage();
        //        SessionUserModel CurrUser = basePage.CurrUserInfo();
        //        if (!string.IsNullOrEmpty(CurrUser.RoleName))
        //        {
        //            string[] currUserRoleArray = CurrUser.RoleName.Split(',');
        //            foreach (string currRole in currUserRoleArray)
        //            {
        //                if (rtnRoleStr.Contains(currRole))
        //                {
        //                    rtnRoleStr = string.Empty;//当前角色已经是 待审核角色
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return rtnRoleStr;
        //}
        #endregion

        /// <summary>
        /// 增加待办事项（审核退回）
        /// </summary>
        /// <param name="moduleID">业务模块ID</param>
        /// <param name="moduleName">功能菜单名称</param>
        /// <param name="SessionUserModel">用户信息</param>
        /// <param name="title">待办事项标题</param>
        /// <param name="url">待办事项地址</param>
        /// <param name="toUserId">业务的操作用户</param>
        /// <returns></returns>
        public bool AddMatterTasks(string moduleID, string moduleName, SessionUserModel userInfo, string title, string url, string toUserId)
        {
            try
            {
                Sys_PendingMatter pendingMatter = new Sys_PendingMatter();
                pendingMatter.PendingMatterID = Guid.NewGuid().ToString();
                pendingMatter.PendingMatter_ModuleID = moduleID;
                pendingMatter.PendingMatter_Title = title;
                pendingMatter.PendingMatter_ModuleName = moduleName;
                pendingMatter.PendingMatter_ToRoleName = toUserId;
                pendingMatter.PendingMatter_URL = url;
                pendingMatter.PendingMatter_AddTime = DateTime.Now;
                pendingMatter.PendingMatter_AddUserID = userInfo.UserID;
                new Sys_PendingMatterBLL().Add(pendingMatter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 删除待办事项
        /// <summary>
        /// 删除待办事项
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <returns></returns>
        public bool DeleteMatterTasks(string moduleID)
        {
            try
            {
                new Sys_PendingMatterBLL().Delete(p=>p.PendingMatter_ModuleID== moduleID);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除待办事项
        /// </summary>
        /// <param name="moduleIDStr">模块ID字符串</param>
        /// <returns></returns>
        public bool DeleteBatchMatterTasks(string moduleIDStr)
        {
            try
            {
                string[] arrywhere = moduleIDStr.Split(',');
                new Sys_PendingMatterBLL().Delete(p=> arrywhere.Contains( p.PendingMatter_ModuleID));// " PendingMatter_ModuleID in(" + moduleIDStr + ")"
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据角色、模块名称获取查看范围sql
        /// <summary>
        /// 根据角色、模块名称获取查看范围sql
        /// </summary>
        /// <param name="menu">模块路径</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="Dep">表中用来确定部门查看范围的字段名（可为空）</param>
        /// <returns></returns>
        public string GetSQlWhere(string menu, string roleId, string Dep)
        {
            string str = "(";
            Sys_MenuBLL dalMenu = new Sys_MenuBLL();
            Sys_RolePurviewBLL dalRP = new Sys_RolePurviewBLL();
            Sys_SeeChargeBLL dalSee = new Sys_SeeChargeBLL();
            // List<Sys_Menu> meList = dalMenu.FindWhere(" Menu_Url='" + menu + "'");  //获取当前菜单实体
            Sys_Menu me = dalMenu.FindByURL(menu);
            if (me != null && roleId != "1")  //判断菜单是否存在
            {
                var roleList = roleId.Split(',');
                for (int i = 0; i < roleList.Count(); i++)  //循环当前用户的角色
                {
                    if (i != 0)
                    {
                        str += " or ";
                    }
                    //根据当前角色和菜单获取查看权限
                    string roleIds = roleList[i];
                    List<Sys_RolePurview> rpLsit = dalRP.GetList(p => p.RolePurview_MenuCode == me.Menu_Code && p.RolePurview_RoleID == roleIds).ToList();// (" RolePurview_MenuCode='" + me.Menu_Code + "' and RolePurview_RoleID='" + roleList[i] + "'");
                    if (rpLsit.Count() == 1)
                    {
                        str += " (  ";
                        if (!string.IsNullOrEmpty(rpLsit[0].RolePurview_SeeCharge))   //判断是否有自定义查看规则
                        {
                            str += " ( ";
                            string SeeName = "";
                            var ds = rpLsit[0].RolePurview_SeeCharge.Split(',');
                            for (int j = 0; j < ds.Count(); j++)
                            {
                                var st = ds[j].Split('_');

                                if (!SeeName.Contains(st[0] + "',"))
                                {
                                    SeeName += "'" + st[0] + "',";
                                }
                                //if (!SeeName.Contains(st[1] + "',"))
                                //{
                                //    SeeName += "'" + st[1] + "',";
                                //}

                            }
                            SeeName = SeeName.Substring(0, SeeName.Length - 1);

                            //获取自定义查看规则列表
                            List<Sys_SeeCharge> seeList = dalSee.GetList(p => p.SeeCharge_MenuID == me.Menu_Code && SeeName.Contains(p.SeeCharge_Name)).ToList();// (" SeeCharge_MenuID='" + me.Menu_Code + "' and SeeCharge_Name in (" + SeeName + ")");
                            List<Sys_SeeCharge> seeStateList = dalSee.GetList(p => p.SeeCharge_MenuID == me.Menu_Code && p.SeeCharge_Name.Contains("状态")).ToList();// (" SeeCharge_MenuID='" + me.Menu_Code + "' and SeeCharge_Name like '%状态%'");
                            int seeListCount = seeList.Count;
                            for (int r = 0; r < seeListCount; r++)  //得到本角色自定义查看规则
                            {
                                str += " ( ";
                                str += seeList[r].SeeCharge_Code;
                                str += " and (";



                                for (int j = 0; j < ds.Count(); j++)
                                {
                                    if (seeStateList.Count > 0)
                                    {
                                        for (int s = 0; s < seeStateList.Count; s++)
                                        {
                                            string ss = ds[j];
                                            if (ss.Contains("_"))
                                            {
                                                if (ss.Split('_')[1] == seeStateList[s].SeeCharge_Name)
                                                {
                                                    str += seeStateList[s].SeeCharge_Code + " or ";
                                                }

                                            }
                                            else
                                            {
                                                if (ss == seeStateList[s].SeeCharge_Name)
                                                {
                                                    str += seeStateList[s].SeeCharge_Code + " or ";
                                                }

                                            }

                                        }
                                    }
                                    else
                                    {
                                        str += "1=1 or ";
                                    }


                                }

                                str += "1!=1";

                                str += ")";
                                if (r == seeListCount - 1)
                                {
                                    str += " ) ";
                                }
                                else
                                {
                                    str += ") " + rpLsit[0].RolePurview_SeeType + " ";
                                }

                            }
                            str += " ) ";
                        }
                        else
                        {
                            str += "  (1!=1) ";
                        }
                        string seed = rpLsit[0].RolePurview_SeeDepCode;
                        if (!string.IsNullOrEmpty(rpLsit[0].RolePurview_SeeDepCode))
                        {
                            seed = seed.Replace("''", "");
                        }
                        if (!string.IsNullOrEmpty(seed) && !string.IsNullOrEmpty(Dep))  //判断是否使用部门树分管查看规则
                        {
                            str += " " + rpLsit[0].RolePurview_SeeType;
                            str += "  (" + Dep + " in ( ";
                            str += rpLsit[0];
                            str += " )) ";
                        }
                        else
                        {
                            str += " or (1!=1) ";
                        }
                    }
                    else
                    {
                        str += " (1!=1 ";
                    }
                    str += " ) ";
                }

            }
            else
            {
                str += " (1=1) ";
            }

            str += " ) ";
            return str;
        }
        #endregion

    }
}
