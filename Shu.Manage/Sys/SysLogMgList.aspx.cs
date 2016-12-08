using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;
using System.Text;
using Shu.Utility;

namespace Shu.Manage.Sys
{
    public partial class SysLogMgList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                if (WebUtil.IsPost())
                {
                    UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
                    GetList();
                }
            }
        }

        /// <summary>
        /// 列表绑定
        /// </summary>
        public void BindGrid()
        {
            UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridLogList.xml";
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
                    Result = UCEasyUIDataGrid.DelInfo(id);
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
            if (!string.IsNullOrEmpty(Request["txt_FullName"]))
            {
                strWhere.AppendFormat(" and UserInfo_FullName like '%{0}%'", Request["txt_FullName"]);
            }
            if (!string.IsNullOrEmpty(Request["txt_DepName"]))
            {
                strWhere.AppendFormat(" and Department_Name like '%{0}%'", Request["txt_DepName"]);
            }
            if (!string.IsNullOrEmpty(Request["txt_OperateType"]))
            {
                strWhere.AppendFormat(" and SysLog_OperateType like '%{0}%'", Request["txt_OperateType"]);
            }
            if (!string.IsNullOrEmpty(Request["txt_OperateDateFrom"]))
            {
                strWhere.AppendFormat(" and SysLog_OperateDate >='{0}'", Request["txt_OperateDateFrom"]);
            }
            if (!string.IsNullOrEmpty(Request["txt_OperateDateTo"]))
            {
                strWhere.AppendFormat(" and SysLog_OperateDate <='{0}'", Request["txt_OperateDateTo"] + " 23:59:59.00");
            }
            return strWhere.ToString();
        }
    }
}