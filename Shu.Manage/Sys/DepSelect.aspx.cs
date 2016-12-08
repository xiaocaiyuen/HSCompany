using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shu.Manage.Sys
{
    public partial class DepSelect : System.Web.UI.Page
    {
        public string DepIDs;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ids = Request.QueryString["hid_DepId"];
                UcDepEasyUI1.DefaultValue = ids;
            }
        }
    }
}