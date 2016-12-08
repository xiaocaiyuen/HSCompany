using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using System.Text;
using YDT.BLL;

namespace YDT.Web.Manage.Sys
{
    public partial class UserInfoList_T : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                if (Request.QueryString["depCode"] != null)
                {
                    this.HidDepCode.Value = Request.QueryString["depCode"].ToString();
                }
                else
                {
                    this.HidDepCode.Value = "001";
                }

                InitGrid();

                BindGrid();
            }
        }


        public void InitGrid()
        {

            this.UCGrid.DataSource = "~/XML/Sys/GridUserList_T.xml";

            this.UCGrid.InitGrid();
            this.UCGrid.ModifyURL = "UserInfoModify_T.aspx?DepCode=" + this.HidDepCode.Value + "";
            //this.UCGrid.DetailURL = "UserInfoShow.aspx";
            //this.UCGrid.SetButton(true, false, true, false, true, false);


        }

        public void BindGrid()
        {
            this.UCGrid.SQLWhere = GetSqlWhere();
            this.UCGrid.BindGrid();
        }

        public string GetSqlWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            if (!base.CurrUserInfo().RoleName.Contains(Constant.SuperAdminRoleName))
            {
                strWhere.Append(" and UserInfoID not in('5E0F2DA5-D803-4146-BD6B-A450B6B05E7A','a3d8cbb0-a455-48eb-a435-aa9b47c45e98')");
            }
            if (this.txtDepName.Text.Trim() != "")
            {
                strWhere.AppendFormat(" and Department_Name like '%{0}%'", txtDepName.Text.Trim());
            }
            if (this.txtPostName.Text.Trim() != "")
            {
                strWhere.AppendFormat(" and UserInfo_PostName like '%{0}%'", txtPostName.Text.Trim());
            }
            if (this.HidDepCode.Value != "")
            {
                strWhere.Append(" and UserInfo_DepCode like '" + HidDepCode.Value + "%'");
            }
            return strWhere.ToString();

        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChongzhi_Click(object sender, ImageClickEventArgs e)
        {
            txtDepName.Text = "";
            txtPostName.Text = "";
            BindGrid();
        }
    }
}