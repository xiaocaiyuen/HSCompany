using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using System.Text;
using YDT.BLL;
using YDT.Model;

namespace YDT.Web.Manage.Sys
{
    public partial class InsurerInfoList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定列表信息
                InitGrid();
            }
        }

        /// <summary>
        /// 绑定列表信息
        /// </summary>
        public void InitGrid()
        {
            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridInsurerInfoList.xml";
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
                case "DelButton"://删除数据
                    Result = DelInsurer(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 删除一条保险公司数据
        /// </summary>
        /// <param name="id"></param>
        public string DelInsurer(string id)
        {
            string msg = string.Empty;
            Sys_InsurerInfoBLL bllInsurerInfo = new Sys_InsurerInfoBLL();
            Sys_InsurerInfo model = new Sys_InsurerInfo();
            model = bllInsurerInfo.Find(p => p.InsurerInfoId == id);
            model.InsurerInfo_UpdateTime = DateTime.Now;
            model.InsurerInfo_UpdateUserId = base.CurrUserInfo().UserID;
            model.InsurerInfo_IsDelete = true;
            bool bol = bllInsurerInfo.Update(model, out msg);
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
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        public string GetSqlWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1");
            //保险公司名称
            if (!string.IsNullOrEmpty(Request["txtInsurerInfo_Name"]))
            {
                strWhere.AppendFormat(" AND InsurerInfo_Name like '%{0}%'", Request["txtInsurerInfo_Name"]);
            }
            strWhere.Append(" AND InsurerInfo_IsDelete = 0");
            return strWhere.ToString();
        }
    }
}