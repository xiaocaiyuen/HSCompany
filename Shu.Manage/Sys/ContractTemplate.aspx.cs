using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.BLL;
using YDT.Model;

namespace YDT.Web.Manage.Sys
{
    public partial class ContractTemplate : System.Web.UI.Page
    {
        Contracts_TemplateBLL tempBll = new Contracts_TemplateBLL();
        Sys_DataDictBLL dataBll = new Sys_DataDictBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Show();
            }
        }

        protected void Show()
        {
            List<Contracts_Template> TepmList = tempBll.FindALL().OrderBy(p=>p.Template_UpdateTime).ToList();

            List<Sys_DataDict> dataList = dataBll.FindWhere("DataDict_ParentCode='65'").OrderBy(p=>p.DataDict_Sequence).ToList();

            string str1 = "";

            for (int i = 0; i < TepmList.Count; i++)
            {
                string type = "";
                string ty = "";
                ty = TepmList[i].Template_PositionValue;
                if (TepmList[i].Template_PositioningMode=="0")
                {
                    type = "坐标";
                }
                else if (TepmList[i].Template_PositioningMode == "1")
                {
                    type = "关键字";
                }
                else
                {
                    type = "无";
                    ty = "无";
                }
                 str1 += "<tr>";
                 str1 += "<td style=\"text-align:center; display:none;\">" + GetTempType(dataList, TepmList[i].Template_BussType) + "</td>";
                 str1 += "<td style=\"text-align:center;\">" + TepmList[i].Template_Title + "</td>";
                 str1 += "<td style=\"text-align:center;\">" + type + "</td>";
                 str1 += "<td style=\"text-align:center;\">" + ty + "</td>";
                 str1 += "<td style=\"text-align:center; color:red;\"><a onclick=\"OpenFile('/Print/Template/" + TepmList[i].Template_PDFFile + "', '" + TepmList[i].Template_Title + ".frx')\" title=\"下载模板\">下载模板</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a onclick=\"Upfile('" + TepmList[i].Template_PDFFile + "')\" title=\"上传模板\">上传模板</a></td>";
                 str1 += "</tr>";
             }
           

            this.LitContent1.Text = str1;

        }

        protected string GetTempType(List<Sys_DataDict> da,string code) 
        {
            string name = "";
            Sys_DataDict ds = da.Find(p => p.DataDict_Code == code);
            if(ds!=null)
            {
                name = ds.DataDict_Name;
            }
            return name;
        }
    }
}