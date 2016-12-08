using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;
using Shu.Model;

namespace Shu.Manage
{
    public partial class Main : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionUserModel userInfo = base.CurrUserInfo();
                //this.HomeTime.Text = DateTimeUtil.getCurrDataToChinese();
                LoginName.Text = userInfo.UserName;
                
            }
        }
    }
}