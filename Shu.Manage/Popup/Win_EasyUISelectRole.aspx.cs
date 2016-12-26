using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shu.Manage.Popup
{
    public partial class Win_EasyUISelectRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["perseecharge"]))
                {
                    UCExpAllRoleEasyUISingleTree1.Person_SeeCharge = Request.QueryString["perseecharge"];
                }

                if (!string.IsNullOrEmpty(Request.QueryString["depseecharge"]))
                {
                    UCExpAllRoleEasyUISingleTree1.Dep_SeeCharge = Request.QueryString["depseecharge"];
                }
            }
        }


       
    }
}