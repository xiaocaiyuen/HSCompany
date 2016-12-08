using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Shu.Model;
using Shu.BLL;
using Shu.Comm;
using System.Text;
using Shu.Utility.Extensions;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// RoleHandler 的摘要说明
    /// </summary>
    public class RoleHandler : IHttpHandler, IRequiresSessionState
    {
        Sys_MenuBLL bll = new Sys_MenuBLL();
        Sys_DepartmentBLL bllDep = new Sys_DepartmentBLL();
        List<Sys_Department> NodeDepList = new List<Sys_Department>();
        Sys_RolePurviewBLL bllRP = new Sys_RolePurviewBLL();
        Sys_SeeChargeBLL bllSee = new Sys_SeeChargeBLL();
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            string method = context.Request.QueryString["method"];
            context.Response.Cache.SetNoStore();

            if (method == "load")
            {
                context.Response.Write(LoadMuenu());
            }
            else if (method == "SeeChecked")
            {
                context.Response.Write(GetChecked(context));
            }
            else if (method == "SaveDep")
            {
                context.Response.Write(SaveDep(context));
            }
            else if (method == "DepTree")
            {
                this.AllDeptData(context);
            }
            else if (method == "CheckSeeCharge")
            {
                context.Response.Write(GetCheckSeeCharge(context));
            }
            else
            {
                context.Response.Write(GetUserRole(context));
            }




        }

        public string GetChecked(HttpContext context)
        {
            string ID = context.Request.QueryString["id"];

            //List<Sys_SeeCharge> list = new BLL.BLLSys_SeeCharge().FindWhere(" SeeCharge_MenuID ='" + ID + "'");
            List<Sys_SeeCharge> list = new BLL.Sys_SeeChargeBLL().GetList(p => p.SeeCharge_MenuID == ID).ToList();
            string roleList = list.ToJson<Sys_SeeCharge>();//JosnHandler.GetJson<List<Sys_SeeCharge>>(list);

            return roleList;
        }

        public string GetUserRole(HttpContext context)
        {
            string roleID = context.Request.QueryString["id"];
            //List<Sys_RolePurview> list = new BLL.BLLSys_RolePurview().FindWhere(" RolePurview_RoleID ='" + roleID + "'");
            List<Sys_RolePurview> list = new BLL.Sys_RolePurviewBLL().GetList(p => p.RolePurview_RoleID == roleID).ToList();
            string roleList = list.ToJson<Sys_RolePurview>();//JosnHandler.GetJson<List<Sys_RolePurview>>(list);
            return roleList;
        }


        public string GetCheckSeeCharge(HttpContext context)
        {
            string roleId = context.Request.QueryString["roleId"];
            string menucodes = context.Request.QueryString["menucodes"];
            string str = "";
            string st1 = "";
            string st2 = "";

            //List<Sys_RolePurview> rpList = bllRP.FindWhere(" RolePurview_RoleID='" + roleId + "' and RolePurview_MenuCode='" + menucodes + "'");
            List<Sys_RolePurview> rpList = bllRP.GetList(p => p.RolePurview_RoleID == roleId && p.RolePurview_MenuCode == menucodes).ToList();


            //List<Sys_SeeCharge> list = bllSee.FindWhere(" SeeCharge_MenuID ='" + menucodes + "' and  SeeCharge_Name not like '%状态%'  order by SeeCharge_Sort asc");
            List<Sys_SeeCharge> list = bllSee.GetList(p => p.SeeCharge_MenuID == menucodes && !p.SeeCharge_Name.Contains("状态")).OrderBy(p => p.SeeCharge_Sort).ToList();

            //List<Sys_SeeCharge> stateList = bllSee.FindWhere(" SeeCharge_MenuID ='" + menucodes + "' and SeeCharge_Name like '%状态%'  order by SeeCharge_Sort asc");
            List<Sys_SeeCharge> stateList = bllSee.GetList(p => p.SeeCharge_MenuID == menucodes && p.SeeCharge_Name.Contains("状态")).OrderBy(p => p.SeeCharge_Sort).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                string checks = "";
                if (rpList.Count > 0)
                {

                    if (!string.IsNullOrEmpty(rpList[0].RolePurview_SeeType))
                    {
                        if (rpList[0].RolePurview_SeeType.Trim() == "or")
                        {
                            st1 = "selected=\"selected\"";
                        }
                        else if (rpList[0].RolePurview_SeeType.Trim() == "and")
                        {
                            st2 = "selected=\"selected\"";
                        }
                    }


                    if (stateList.Count > 0)
                    {
                        str += "&nbsp;" + (i + 1) + "、" + list[i].SeeCharge_Name + "<br/>";
                    }
                    else
                    {
                        if (rpList[0].RolePurview_SeeCharge != null)
                        {
                            var da = rpList[0].RolePurview_SeeCharge.Split(',');
                            for (int r = 0; r < da.Count(); r++)
                            {
                                if (list[i].SeeCharge_Name == da[r])
                                {
                                    checks = "checked=\"checked\"";
                                }

                            }
                        }


                        str += "&nbsp;<input type=\"checkbox\" id=\"See_" + i + " name=\"See_" + i + "\"  " + checks + " value=\"" + list[i].SeeChargeID + "\">" + list[i].SeeCharge_Name + "<br/>";
                    }

                    for (int j = 0; j < stateList.Count; j++)
                    {
                        string st = "";
                        if (!string.IsNullOrEmpty(rpList[0].RolePurview_SeeCharge))
                        {
                            var da = rpList[0].RolePurview_SeeCharge.Split(',');
                            for (int s = 0; s < da.Count(); s++)
                            {
                                if (da[s].Contains("_"))
                                {
                                    if (stateList[j].SeeCharge_Name == da[s].Split('_')[1] && da[s].Split('_')[0] == list[i].SeeCharge_Name)
                                    {
                                        st = "checked=\"checked\"";
                                    }
                                }

                            }
                        }
                        string na = stateList[j].SeeCharge_Name;
                        na = na.Replace("状态", "").Replace("查看", "");
                        str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"checkbox\" id=\"See_" + i + "_" + j + " name=\"See_" + i + "_" + j + "\"  " + st + " value=\"" + list[i].SeeCharge_Name + "_" + stateList[j].SeeChargeID + "\">" + na + "<br/>";
                    }


                }
                else
                {
                    if (stateList.Count > 0)
                    {
                        str += "&nbsp;" + (i + 1) + "、" + list[i].SeeCharge_Name + "<br/>";
                    }
                    else
                    {
                        str += "&nbsp;<input type=\"checkbox\" id=\"See_" + i + " name=\"See_" + i + "\"  " + checks + " value=\"" + list[i].SeeChargeID + "\">" + list[i].SeeCharge_Name + "<br/>";
                    }
                    for (int j = 0; j < stateList.Count; j++)
                    {
                        string st = "";

                        string na = stateList[j].SeeCharge_Name;
                        na = na.Replace("状态", "").Replace("查看", "");
                        str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"checkbox\" id=\"See_" + i + "_" + j + " name=\"See_" + i + "_" + j + "\"  " + st + " value=\"" + list[i].SeeCharge_Name + "_" + stateList[j].SeeChargeID + "\">" + na + "<br/>";
                    }
                }
            }



            string ddl = "#<select name=\"ddlType\" id=\"ddlType\"><option " + st1 + " value=\"or\">或者</option><option " + st2 + " value=\"and\">并且</option></select>";

            str += ddl;
            return str;
        }


        #region 加载菜单树
        /// <summary>
        /// 加载菜单树
        /// </summary>
        /// <returns></returns>
        public string LoadMuenu()
        {
            Sys_SeeChargeBLL bllSee = new Sys_SeeChargeBLL();

            List<Sys_Menu> list = bll.GetAll().OrderBy(p => p.Menu_Sequence).ToList();

            StringBuilder strMenu = new StringBuilder();

            strMenu.Append("{\"total\":" + list.Count + ",\"rows\":[");

            int index = 0;

            foreach (Sys_Menu menu in list)
            {
                index++;

                if (menu.Menu_ParentCode == "0")
                {

                    strMenu.Append("{\"id\":\"" + menu.Menu_Code + "\",\"name\":\"" + menu.Menu_Name + "\",\"url\":\"" + menu.Menu_Url + "\",\"sort\":\"" + menu.Menu_Sequence + "\",\"Opt\":\"" + menu.Menu_Operation + "\"},");
                }
                else
                {
                    //List<Sys_SeeCharge> seeList = bllSee.FindWhere(" SeeCharge_MenuID='" + menu.Menu_Code + "' order by SeeCharge_Sort asc");
                    List<Sys_SeeCharge> seeList = bllSee.GetList(p => p.SeeCharge_MenuID == menu.Menu_Code).OrderBy(p => p.SeeCharge_Sort).ToList();

                    string st = "";
                    for (int i = 0; i < seeList.Count(); i++)
                    {
                        st += seeList[i].SeeCharge_Name + "#";
                    }
                    if (st.Length > 0)
                    {
                        st = st.Substring(0, st.Length - 1);
                    }
                    strMenu.Append("{\"id\":\"" + menu.Menu_Code + "\",\"name\":\"" + menu.Menu_Name + "\",\"url\":\"" + menu.Menu_Url + "\",\"sort\":\"" + menu.Menu_Sequence + "\",\"Opt\":\"" + menu.Menu_Operation + "\",\"_parentId\":\"" + menu.Menu_ParentCode + "\",\"See\":\"" + st + "\"}");

                    if (index != list.Count)
                    {

                        strMenu.Append(",");
                    }
                }

            }

            strMenu.Append("]}");

            return strMenu.ToString();
        }

        #endregion

        #region 加载部门查看范围树
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
            string roleID = context.Request.QueryString["roleID"];
            string MenuCode = context.Request.QueryString["MenuCode"];
            //List<Sys_RolePurview> RPList = bllRP.FindWhere(" RolePurview_RoleID='" + roleID + "' and RolePurview_MenuCode='" + MenuCode + "'");
            List<Sys_RolePurview> RPList = bllRP.GetList(p => p.RolePurview_RoleID == roleID && p.RolePurview_MenuCode == MenuCode).ToList();

            string SeeDepCode = "";
            if (RPList.Count == 1)
            {
                SeeDepCode = RPList[0].RolePurview_SeeDepCode;
                if (!string.IsNullOrEmpty(SeeDepCode))
                {
                    SeeDepCode = SeeDepCode.Replace("'", "");
                }
            }
            if (!parentId.Equals(""))
            {
                sb.Append("[");
                //NodeDepList = bllDep.FindWhere(" 1=1 ");
                NodeDepList = bllDep.GetAll().ToList();
                if (NodeDepList.Count != 0)
                {
                    sb.Append(AllDeptDataBindTree(NodeDepList, parentId, SeeDepCode));
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
        private StringBuilder AllDeptDataBindTree(List<Sys_Department> depList, string PCode, string SeeDepCode)
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

                    string depCheck = "";
                    if (!string.IsNullOrEmpty(SeeDepCode))
                    {
                        var da = SeeDepCode.Split(',');
                        for (int j = 0; j < da.Count(); j++)
                        {
                            if (da[j] == deps[d].Department_Code)
                            {
                                depCheck = ", \"checked\": \"true\"";
                            }
                        }
                    }

                    if (list.Count == 0)
                    { //没有子节点  设置 state 为空
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"" + depCheck);
                    }
                    else
                    {
                        sb.Append("\"id\": \"" + deps[d].Department_Code + "\", \"text\": \"" + deps[d].Department_Name + "\", \"iconCls\": \"\", \"state\": \"\"" + depCheck);
                        sb.Append(",\"children\":[");
                        sb.Append(AllDeptDataBindTree(depList, deps[d].Department_Code, SeeDepCode));
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
        /// 保存部门查看范围
        /// </summary>
        /// <param name="context"></param>
        public string SaveDep(HttpContext context)
        {
            string st = "0";
            string codes = context.Request.QueryString["code"];
            string roleId = context.Request.QueryString["roleId"];
            string menucode = context.Request.QueryString["menucode"];
            string seeCharge = context.Request.QueryString["seeCharge"];
            string seeType = context.Request.QueryString["type"];
            string showType = context.Request.QueryString["showType"];
            string see = "";
            if (!string.IsNullOrEmpty(seeCharge))
            {
                var ds = seeCharge.Split(',');
                for (int i = 0; i < ds.Count(); i++)
                {
                    if (ds[i].Contains("_"))
                    {
                        see += ds[i].Split('_')[0] + "_";

                        //Sys_SeeCharge ses = bllSee.Find(ds[i].Split('_')[1]);
                        string ids = ds[i].Split('_')[1];
                        Sys_SeeCharge ses = bllSee.Get(p => p.SeeChargeID == ids);
                        if (ses != null)
                        {
                            see += ses.SeeCharge_Name + ",";
                        }
                    }
                    else
                    {
                        // see += ds[i].Split('_')[0] + "_";

                        //Sys_SeeCharge ses = bllSee.Find(ds[i]);
                        string ids = ds[i];
                        Sys_SeeCharge ses = bllSee.Get(p => p.SeeChargeID == ids);
                        if (ses != null)
                        {
                            see += ses.SeeCharge_Name + ",";
                        }
                    }



                }
            }
            if (see.Length > 0)
            {
                see = see.Substring(0, see.Length - 1);
                var da = codes.Split(',');
                string str = "";
                for (int i = 0; i < da.Count(); i++)
                {
                    str += "'" + da[i] + "',";
                }
                if (str.Length > 0)
                {
                    string msg = string.Empty;
                    str = str.Substring(0, str.Length - 1);
                    Sys_RolePurviewBLL bllRP = new BLL.Sys_RolePurviewBLL();
                    //List<Sys_RolePurview> list = bllRP.FindWhere(" RolePurview_RoleID ='" + roleId + "' and RolePurview_MenuCode='" + menucode + "'");
                    List<Sys_RolePurview> list = bllRP.GetList(p => p.RolePurview_RoleID == roleId && p.RolePurview_MenuCode == menucode).ToList();

                    if (list.Count == 1)
                    {
                        Sys_RolePurview p = list[0];
                        p.RolePurview_SeeDepCode = str;
                        p.RolePurview_SeeCharge = see;
                        p.RolePurview_SeeType = seeType;
                        if (bllRP.Update(p))
                        {
                            st = "1";
                        }


                    }
                    else
                    {
                        Sys_RolePurview p = new Sys_RolePurview();
                        p.RolePurviewID = Guid.NewGuid().ToString();
                        p.RolePurview_RoleID = roleId;
                        p.RolePurview_MenuCode = menucode;
                        p.RolePurview_SeeDepCode = str;
                        p.RolePurview_SeeCharge = see;
                        p.RolePurview_SeeType = seeType;
                        if (bllRP.Add(p))
                        {
                            st = "1";
                        }


                    }
                }
            }
            else
            {
                st = "0";
            }


            return st;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}