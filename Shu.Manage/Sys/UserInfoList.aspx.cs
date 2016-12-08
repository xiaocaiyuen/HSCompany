using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;
using System.Text;
using Shu.BLL;
using Shu.Model;

namespace Shu.Manage.Sys
{
    public partial class UserInfoList : BasePage
    {
        private static string HidDepCode="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.UCGrid.DeleteClick += new UserControls.UCGrid.DeleteClickEventHandler(DelInfo);
                if (Request.QueryString["depCode"] != null)
                {
                    HidDepCode = Request.QueryString["depCode"].ToString();
                }
                else
                {
                    HidDepCode = "001";
                }

                

                //BindGrid();
            }
            InitGrid();
        }

        public void InitGrid()
        {

            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridUserList.xml";
            this.UCEasyUIDataGrid.AddURL = "UserInfoAddOr.aspx?DepCode=" + HidDepCode + "";
            this.UCEasyUIDataGrid.ModifyURL = "UserInfoAddOr.aspx?DepCode=" + HidDepCode + "";
            this.UCEasyUIDataGrid.RedoType = 3;
            this.UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            GetList();
        }

        public void GetList()
        {
            string active = HttpContext.Current.Request["active"];
            string id = HttpContext.Current.Request["id"];
            string Result = "";
            switch (active)
            {
                case "List"://加载列表
                    Response.Write(UCEasyUIDataGrid.jsonPerson());
                    Response.End();
                    break;
                case "DelButton"://删除数据(删除用户同时把客户也一起删除了)
                    Result = DelBank(id);
                    Response.Write(Result.ToString());

                    Response.End();
                    break;
                case "DeltBatchButton"://批量删除
                    Result = BatchDelInfo(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        public string DelBank(string id)
        {
            string msg = string.Empty;
            bool bol = false;
            Sys_UserInfoBLL userBll = new Sys_UserInfoBLL();
            Sys_UserInfo user = userBll.Get(p => p.UserInfoID == id);
            if (user != null)
            {
                string s = "";
                user.UserInfo_IsDelete = true;
                bol =userBll.Update(user);
            }
            if (bol)
            {
                //删除成功
                msg = "1";
            }
            else
            {
                //删除失败
                msg = "0";
            }
            return msg;
        }


        /// <summary>
        /// 批量删除数据
        /// </summary>
        public string BatchDelInfo(string id)
        {
            if (id.Length > 0)
            {
                
                string sql = "";
                List<string> listId = new List<string>();
                listId = id.Split(',').ToList();
                //foreach (var item in listId)
                //{
                //    sql += "'" + item.ToString()+ "',";
                //}
                //if(sql.Length>0)
                //{
                //    sql = sql.Substring(0,sql.Length-1);
                //}
                Sys_UserInfoBLL userBll = new Sys_UserInfoBLL();
                List<Sys_UserInfo> userList = userBll.GetList(p => listId.Contains(p.UserInfoID)).ToList();//(" UserInfoID in (" + sql + ") ");
                List<Sys_UserInfo> newList = new List<Sys_UserInfo>();
                foreach(var users in userList)
                {
                    users.UserInfo_IsDelete = true;
                    newList.Add(users);
                }
                bool b = userBll.Update(newList);
                if (b)
                {
                    //删除待办事项
                    //new RoleConfig().DeleteBatchMatterTasks(sql);
                    //string rtnUrl = Request.RawUrl;
                    //Sys_Menu MenuModel = bllMenu.FindByURL(rtnUrl);
                    //if (MenuModel != null)
                    //{
                    //    if (!string.IsNullOrEmpty(MenuModel.Menu_Name))
                    //    {
                    //        pg.AddLog(MenuModel.Menu_Name, "", "批量删除成功", "批量删除通过：" + listId, bg.CurrUserInfo().UserID, bg.CurrUserInfo().DepartmentCode);


                    //    }
                    //}
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }

        public string GetSqlWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 and UserInfo_IsDelete =0 ");
            if (!base.CurrUserInfo().RoleName.Contains(Constant.SuperAdminRoleName))
            {
                strWhere.Append(" and UserInfoID not in('5E0F2DA5-D803-4146-BD6B-A450B6B05E7A','a3d8cbb0-a455-48eb-a435-aa9b47c45e98') ");
            }
            if (!string.IsNullOrEmpty(Request["txtDepName"]))
            {
                strWhere.AppendFormat(" and Department_Name like '%{0}%'", Request["txtDepName"]);
            }
            if (!string.IsNullOrEmpty(Request["txtPostName"]))
            {
                strWhere.AppendFormat(" and UserInfo_PostName like '%{0}%'", Request["txtPostName"]);
            }
            if (!string.IsNullOrEmpty(Request["txtName"]))
            {
                strWhere.AppendFormat(" and UserInfo_FullName like '%{0}%'", Request["txtName"]);
            }
            if (!string.IsNullOrEmpty(Request["txtRole"]))
            {
                strWhere.AppendFormat(" and UserInfo_RoleName like '%{0}%'", Request["txtRole"]);
            }
            if (HidDepCode != "")
            {
                strWhere.Append(" and UserInfo_DepCode like '" + HidDepCode + "%'");
            }
            return strWhere.ToString();

        }
    }
}