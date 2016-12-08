using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;
using Shu.BLL;
using System.Data;
using System.IO;

namespace Shu.Manage.Sys
{
    public partial class DepartmentManage : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.UCGrid.ExportClick += new UserControls.UCGrid.ExportClickEventHandler(btn_Export_Click);
            if (!IsPostBack)
            {
                Common_BLL.SetDDLByDataDictCodeNULL(ddlDepType, "45");
                //CommTools.SetDDLByDataDictCode(ddlDepClass, "09");

                    this.hid_dep.Value = "2";
                
            }
        }




    }
}