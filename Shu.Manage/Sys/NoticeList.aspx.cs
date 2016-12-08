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
    public partial class NoticeList : BasePage
    {
        protected List<Sys_DataDict> listDataDict;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            BindDDL();
            InitGrid();
            //}
        }


        public void BindDDL()
        {

            Sys_DataDictBLL balDataDict = new Sys_DataDictBLL();
            listDataDict = balDataDict.GetList(p => p.DataDict_ParentCode == "26").OrderBy(p => p.DataDict_Sequence).ToList();
            listDataDict.Insert(0, new Sys_DataDict { DataDict_Name = "--全部--", DataDict_Code = "" });
            //this.ddlNotice_Type.DataTextField = "DataDict_Name";
            //this.ddlNotice_Type.DataValueField = "DataDict_Code";
            //ListItem item = new ListItem();
            //item.Text = "请选择";
            //item.Value = "";
            //ddlNotice_Type.Items.Insert(0, item);
            //this.ddlNotice_Type.SelectedValue = base.RequstStr("type");
        }

        public void InitGrid()
        {

            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridNoticeList.xml";
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
                    Result = UCEasyUIDataGrid.DelInfo(id);
                    Response.Write(Result.ToString());
                    DelFile(id);
                    Response.End();

                    break;
                case "DeltBatchButton"://批量删除
                    Result = UCEasyUIDataGrid.BatchDelInfo(id);
                    Response.Write(Result.ToString());
                    DelFiles();
                    Response.End();

                    break;
                default:
                    break;
            }

        }

        public string GetSqlWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1");
            if (!string.IsNullOrEmpty(Request["ddlNotice_Type"]))
            {
                strWhere.AppendFormat(" and Notice_Type = '{0}'", Request["ddlNotice_Type"]);
            }

            if (base.RequstStr("type").Equals("Two"))
            {
                strWhere.Append(" and Notice_Type in ('1002','1003')");
            }
            if (!string.IsNullOrEmpty(Request["Notice_Title"]))
            {
                strWhere.AppendFormat(" and Notice_Title like '%{0}%'", Request["Notice_Title"]);
            }

            if (!string.IsNullOrEmpty(Request["txt_TimeFrom"]))
            {
                strWhere.AppendFormat(" and Notice_AddTime >='{0}'", Request["txt_TimeFrom"]);
            }
            if (!string.IsNullOrEmpty(Request["txt_TimeTo"]))
            {
                strWhere.AppendFormat(" and Notice_AddTime <='{0}'", Request["txt_TimeTo"] + " 23:59:59.00");
            }
            return strWhere.ToString();

        }
        public void DelFile(string id)
        {

            Sys_ModelFileBLL filebll = new Sys_ModelFileBLL();
            bool file = filebll.Delete(p => p.File_OperationID == id);
        }
        public void DelFiles()
        {
            Sys_ModelFileBLL filebll = new Sys_ModelFileBLL();
             string id = HttpContext.Current.Request["id"];
             List<string> listId = new List<string>();
             listId = id.Split(',').ToList();
             foreach (var item in listId)
             {
                 bool file = filebll.Delete(p => p.File_OperationID == item);
             }
        }
    }
}