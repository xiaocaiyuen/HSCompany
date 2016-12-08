using Shu.Comm;
using Shu.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shu.Manage.Sys
{
    public partial class OperatingButtonList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitGrid();
            if (!IsPostBack)
            {
                //BindDDL();
            }
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public void BindDDL()
        {
           
        }

        /// <summary>
        /// 初始化表格列表
        /// </summary>
        public void InitGrid()
        {
            this.EasyUIGrid.DataSource = "~/XML/Sys/GridOperatingButton.xml";
            this.EasyUIGrid.SQLWhere = GetSqlWhere();
            this.EasyUIGrid.AddType = 3;
            this.EasyUIGrid.EditType = 3;
            GetList();
        }

        /// <summary>
        /// 方法
        /// </summary>
        public void GetList()
        {
            string active = HttpContext.Current.Request["active"];
            string id = HttpContext.Current.Request["id"];
            string Result = "";
            switch (active)
            {
                case "List"://加载列表
                    Response.Write(EasyUIGrid.jsonPerson());
                    Response.End();
                    break;
                case "DelButton"://删除数据
                    Result = EasyUIGrid.DelInfo(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                default:
                    break;
            }

        }



        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        public string GetSqlWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(Request["txtSQR_NAME"]))
            {
                strWhere.AppendFormat(" and SQR_NAME like '%{0}%'", Request["txtSQR_NAME"]);
            }
            return strWhere.ToString();
        }
    }
}