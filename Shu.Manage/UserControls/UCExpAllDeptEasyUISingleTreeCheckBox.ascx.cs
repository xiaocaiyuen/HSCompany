using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shu.Manage.UserControls
{
    public partial class UCExpAllDeptEasyUISingleTreeCheckBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string DefaultValue
        {
            get { return hid_DepSeeCharge.Value; }
            set { hid_DepSeeCharge.Value = value; }
        }
    }
}