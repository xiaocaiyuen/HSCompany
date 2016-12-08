using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shu.Manage.UserControls
{
    public partial class UCExpAllDeptEasyUISingleTree : System.Web.UI.UserControl
    {
        protected string DepSeeCharge;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["depseecharge"]))
            {
                DepSeeCharge = Request.QueryString["depseecharge"];
            }
            else
            {
                DepSeeCharge = "3";
            }
        }
    }
}