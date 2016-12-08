using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using YDT.Utility;

namespace YDT.Web.Manage.Sys
{
    public partial class SyS_XianZhiIP : BasePage
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
            UCEasyUIDataGrid.DataSource = "~/XML/Sys/Sys_XianZhiIP.xml";
            this.UCEasyUIDataGrid.EditType = 3;
            this.UCEasyUIDataGrid.AddType = 3;
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
                default:
                    break;
            }
        }

        protected string GetSqlWhere()
        {
            //查询的参数
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(Request["txt_kaishi"]))
            {
                strWhere.AppendFormat(" and XianZhiIP_StartIP like '%{0}%'", Request["txt_kaishi"]);
            }
            if (!string.IsNullOrEmpty(Request["txt_jieshu"]))
            {
                strWhere.AppendFormat(" and XianZhiIP_EndIP like '%{0}%'", Request["txt_jieshu"]);
            }
           
            return strWhere.ToString();
        }
    }
}