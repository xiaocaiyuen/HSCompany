using Shu.Comm;
using Shu.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shu.Manage.Popup
{
    public partial class IconMenuButton : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            InitGrid();
        }


        /// <summary>
        /// 初始化表格列表
        /// </summary>
        public void InitGrid()
        {
            string codeWhere = string.Empty;
            this.UCEasyUIDataGrid.DataSource = "~/XML/Windows/Win_IconMenuButton.xml";


            string uri = this.Page.Request.Url.AbsoluteUri;
            //获得页面的URL
            uri = uri.Substring(uri.LastIndexOf('/') + 1);
            this.UCEasyUIDataGrid.MenuURL = uri;
            this.UCEasyUIDataGrid.SQLWhere = GetSqlWhere(codeWhere);
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
                    Response.Write(UCEasyUIDataGrid.jsonPerson());
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
        public string GetSqlWhere(string codeWhere)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1");
            return strWhere.ToString();
        }
    }
}