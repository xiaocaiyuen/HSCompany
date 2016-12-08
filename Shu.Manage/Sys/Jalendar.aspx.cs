using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using YDT.BLL;
using System.Data;
using System.IO;
using YDT.Model;

namespace YDT.Web.Manage.Sys
{
    public partial class Jalendar : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.UCGrid.ExportClick += new UserControls.UCGrid.ExportClickEventHandler(btn_Export_Click);
            if (!IsPostBack)
            {
                List<Sys_DataDict> HourList = new List<Sys_DataDict>();
                for (int i = 0; i < 24; i++)
                {
                    string h = i.ToString();
                    if (i < 10)
                    {
                        h = "0" + h;
                    }
                    HourList.Add(new Sys_DataDict { DataDict_Name = h, DataDict_Code = h });
                }

                SetDDL(ddlSH, HourList);
                SetDDL(ddlEH, HourList);

                //Minute
                List<Sys_DataDict> MinuteList = new List<Sys_DataDict>();
                //Hour
                for (int j = 0; j < 59; j += 5)
                {
                    string m = j.ToString();
                    if (j < 10)
                    {
                        m = "0" + m;
                    }
                    MinuteList.Add(new Sys_DataDict { DataDict_Name = m, DataDict_Code = m });
                }
                SetDDL(ddlSM, MinuteList);
                SetDDL(ddlEM, MinuteList);
            }
        }

        /// <summary>
        /// 根据数据字典设置下拉框数据源
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ParentCode"></param>
        public static void SetDDL(DropDownList ddl, List<Sys_DataDict> list)
        {
            ddl.DataSource = list;
            ddl.DataTextField = "DataDict_Name";
            ddl.DataValueField = "DataDict_Code";
            ddl.DataBind();
        }
    }
}