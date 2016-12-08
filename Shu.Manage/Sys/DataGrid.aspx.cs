using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using YDT.Utility;
using YDT.Utility.Extensions;
using YDT.Comm;
using System.Text;
using YDT.BLL;
using YDT.Model;

namespace YDT.Web.Manage
{
    public partial class DataGrid : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridRole.xml";
                UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
                UCEasyUIDataGrid.EditType = 3;
                UCEasyUIDataGrid.AddType = 3;
                GetList();
            }
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
                    Result=DelInfo(id);
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
            string txtRoleName = Request["txtRoleName"];
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            if (txtRoleName != "")
            {
                strWhere.AppendFormat("  and  Role_Name like '%{0}%'", txtRoleName);
            }
            return strWhere.ToString();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        protected string DelInfo(string id)
        {
            if (id.Length > 0)
            {
                Common_BLL pg = new Common_BLL();
                Sys_MenuBLL bllMenu = new Sys_MenuBLL();
                BasePage page = new BasePage();
                SessionUserModel userInfo = page.CurrUserInfo();
                if (pg.Delete(UCEasyUIDataGrid.TableName, UCEasyUIDataGrid.TableKey, "'"+id+"'", ""))
                {
                    //删除日志
                    //DeleteLog();


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
    }
}