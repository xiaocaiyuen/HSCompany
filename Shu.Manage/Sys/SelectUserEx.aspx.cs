using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.BLL;
using YDT.Model;
using YDT.Comm;

namespace YDT.Web.Manage.Sys
{
    public partial class SelectUserEx : System.Web.UI.Page
    {
        public Sys_UserChargeDepBLL bllSys_UserChargeDep = new Sys_UserChargeDepBLL();
        public static string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string depCodeStr = "";
                hidUserID.Value = Id;
                List<Sys_UserChargeDep> list= bllSys_UserChargeDep.FindWhere(" UserChargeDep_ExecutiveOfficerID='" + Id + "'");
                foreach (Sys_UserChargeDep ucp in list)
                {
                    depCodeStr = depCodeStr + ucp.UserChargeDep_ChargeCrewDepCode + ",";
                }
                if (depCodeStr != "")
                {
                    depCodeStr = depCodeStr.Substring(0, depCodeStr.Length - 1);
                    UCExpAllDeptEasyUICheckBoxTree1.DefaultValue = depCodeStr;
                }
            }
        }


        /// <summary>
        /// 添加分管部门
        /// </summary>
        public void AddFgbm(string strGuid)
        {
            bllSys_UserChargeDep.DeleteWhere(" UserChargeDep_ExecutiveOfficerID='" + strGuid + "'");
            if (strid != "")
            {
                string[] arrStr = strid.Split(',');
                hidCount.Value = arrStr.Length.ToString();
                for (int i = 0; i < arrStr.Length; i++)
                {
                    Sys_UserChargeDep modelFgbm = new Sys_UserChargeDep();
                    modelFgbm.UserChargeDepID = Guid.NewGuid().ToString();
                    modelFgbm.UserChargeDep_ExecutiveOfficerID = strGuid;
                    modelFgbm.UserChargeDep_ChargeCrewID = "";
                    modelFgbm.UserChargeDep_ChargeCrewDepCode = arrStr[i];
                    string errmsg = string.Empty;
                    bllSys_UserChargeDep.Add(modelFgbm, out errmsg);
                }
            }
            else
            {
                hidCount.Value = "0";
            }
        }


        /// <summary>
        /// 确认所选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            strid = this.hidDepList.Value;
            AddFgbm(Id);
            MessageBox.ResponseScript(this, "msg", "back()");
        }

        /// <summary>
        /// 获取id
        /// </summary>
        private string Id
        {
            get
            {
                if (Request.QueryString["id"] != null)
                {
                    return Request.QueryString["id"].ToString();
                }
                return "";
            }
        }
    }
}