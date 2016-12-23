using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.BLL;
using Shu.Comm;
using Shu.Model;

namespace Shu.Manage.Workflow
{
    public partial class WorkflowTasksExList : BasePage
    {
        protected BasePage bg = new BasePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        /// <summary>
        /// 列表绑定
        /// </summary>
        public void BindGrid()
        {
            UCEasyUIDataGrid.DataSource = "~/XML/Workflow/GridWorkflowTasksEx.xml";
            UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            UCEasyUIDataGrid.AddType = 3;
            UCEasyUIDataGrid.EditType = 3;
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
                case "DelButton"://删除数据
                    Result = DelInfo(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                case "DeltBatchButton"://批量删除
                    Result = UCEasyUIDataGrid.BatchDelInfo(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                default:
                    break;
            }
        }

        protected string GetSqlWhere()
        {
            //查询的参数
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(Request["txtName"]))
            {
                strWhere.AppendFormat(" and TasksEx_Name like '%{0}%'", Request["txtName"]);
            }
            return strWhere.ToString();
        }

        #region 单表删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public string DelInfo(string id)
        {
            if (id.Length > 0)
            {
                Common_BLL pg = new Common_BLL();
                Sys_MenuBLL bllMenu = new Sys_MenuBLL();
                SessionUserModel userInfo = CurrUserInfo();
                Workflow_TasksExBLL bll = new Workflow_TasksExBLL();
                if (pg.Delete(UCEasyUIDataGrid.TableName, UCEasyUIDataGrid.TableKey, "'" + id + "'", ""))
                {
                    //删除待办事项
                    new RoleConfig().DeleteMatterTasks(id.Replace("'", ""));
                    string rtnUrl = Request.RawUrl;
                    Sys_Menu MenuModel = bllMenu.FindByURL(rtnUrl);
                    if (MenuModel != null)
                    {
                        if (!string.IsNullOrEmpty(MenuModel.Menu_Name))
                        {
                            pg.AddLog(MenuModel.Menu_Name, id, "删除", "ID=" + id + "", userInfo.UserID, userInfo.DepartmentCode);
                        }
                    }
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
        #endregion
    }
}