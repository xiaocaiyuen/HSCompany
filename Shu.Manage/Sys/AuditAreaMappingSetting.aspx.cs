using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.BLL;
using YDT.Comm;
using YDT.Model;
using YDT.Utility.Extensions;

namespace YDT.Web.Manage.Sys
{
    public partial class AuditAreaMappingSetting : BasePage
    {
        private List<Business_AreaDistribution> list = new List<Business_AreaDistribution>();
        public List<AreaClass> AreaList = new List<AreaClass>();
        private Sys_HierarchyBLL HierarchyBLL = new Sys_HierarchyBLL();
        public List<Sys_Hierarchy> HierarchyList1 = new List<Sys_Hierarchy>();
        public List<Sys_Hierarchy> HierarchyList2 = new List<Sys_Hierarchy>();
        private ApprovalPool_AuditAreaMappingBLL AuditAreaMappingBLL = new ApprovalPool_AuditAreaMappingBLL();
        public ApprovalPool_AuditAreaMapping AuditAreaMappingModel = new ApprovalPool_AuditAreaMapping();
        public List<ApprovalPool_AuditAreaMapping> AuditAreaMappingModel_List = new List<ApprovalPool_AuditAreaMapping>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hidRoleID.Value = base.RequstStr("roleID");
                hidUserID.Value = base.RequstStr("userID");

                list = new Business_AreaDistributionBLL().FindALL();
                AreaList = list.Select(p => new AreaClass() { AreaDistribution_ID = p.AreaDistribution_ID, AreaDistribution_Name = p.AreaDistribution_Name })
                    .Distinct().ToList();

                HierarchyList1 = HierarchyBLL.FindWhere(p => p.Hierarchy_ParentCode == "01" && p.Hierarchy_IsDel==false);
                HierarchyList2 = HierarchyBLL.FindWhere(p => p.Hierarchy_ParentCode == "02" && p.Hierarchy_IsDel == false);

                
                AuditAreaMappingModel_List = AuditAreaMappingBLL.FindWhere(p => p.AuditAreaMapping_RoleID == hidRoleID.Value);
                AuditAreaMappingModel = AuditAreaMappingModel_List.Find(p => p.AuditAreaMapping_UserID == hidUserID.Value);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            Button Button = sender as Button;

            if (hidRoleID.Value != "" && hidUserID.Value != "")
            {
                ApprovalPool_AuditAreaMapping AuditAreaMapping = AuditAreaMappingBLL.Find(p => p.AuditAreaMapping_RoleID == hidRoleID.Value && p.AuditAreaMapping_UserID == hidUserID.Value);
                string msg = string.Empty;
                if(AuditAreaMapping.IsNotNull())
                {
                    AuditAreaMapping.AuditAreaMapping_UserID = hidUserID.Value;
                    AuditAreaMapping.AuditAreaMapping_RoleID = hidRoleID.Value;
                    AuditAreaMapping.AuditAreaMapping_AreaCategory=Request.Form["m"];
                    AuditAreaMapping.AuditAreaMapping_AreaCategoryValue = SaveData.Value;
                    AuditAreaMapping.AuditAreaMapping_UpdateTime = DateTime.Now;
                    AuditAreaMapping.AuditAreaMapping_UpdateUserID = base.CurrUserInfo().UserID;
                    if (AuditAreaMappingBLL.Update(AuditAreaMapping, out msg))
                    {
                        MessageBox.ShowAndRedirect(this, "保存成功", "AuditAreaMappingSetting.aspx?roleID=" + hidRoleID.Value + "&userID=" + hidUserID.Value + "");

                        new Common_BLL().AddLog("系统管理>>审批区域管理", AuditAreaMapping.AuditAreaMappingID, "修改", "保存成功！", base.CurrUserInfo().UserID, base.CurrUserInfo().DepartmentCode);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this, "保存失败", "AuditAreaMappingSetting.aspx?roleID=" + hidRoleID.Value + "&userID=" + hidUserID.Value + "");
                        new Common_BLL().AddLog("系统管理>>审批区域管理", AuditAreaMapping.AuditAreaMappingID, "修改", "保存失败！", base.CurrUserInfo().UserID, base.CurrUserInfo().DepartmentCode);
                    }
                  
                }
                else
                {
                    AuditAreaMapping = new ApprovalPool_AuditAreaMapping();
                    AuditAreaMapping.AuditAreaMappingID = Guid.NewGuid().ToString();
                    AuditAreaMapping.AuditAreaMapping_UserID = hidUserID.Value;
                    AuditAreaMapping.AuditAreaMapping_RoleID = hidRoleID.Value;
                    AuditAreaMapping.AuditAreaMapping_AreaCategory = Request.Form["m"];
                    AuditAreaMapping.AuditAreaMapping_AreaCategoryValue = SaveData.Value;
                    AuditAreaMapping.AuditAreaMapping_CreateTime = DateTime.Now;
                    AuditAreaMapping.AuditAreaMapping_CreateUserID = base.CurrUserInfo().UserID;
                    if (AuditAreaMappingBLL.Add(AuditAreaMapping, out msg))
                    {
                        MessageBox.ShowAndRedirect(this, "保存成功", "AuditAreaMappingSetting.aspx?roleID=" + hidRoleID.Value + "&userID=" + hidUserID.Value + "");

                        new Common_BLL().AddLog("系统管理>>审批区域管理", AuditAreaMapping.AuditAreaMappingID, "修改", "保存成功！", base.CurrUserInfo().UserID, base.CurrUserInfo().DepartmentCode);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this, "保存失败", "AuditAreaMappingSetting.aspx?roleID=" + hidRoleID.Value + "&userID=" + hidUserID.Value + "");
                        new Common_BLL().AddLog("系统管理>>审批区域管理", AuditAreaMapping.AuditAreaMappingID, "修改", "保存失败！", base.CurrUserInfo().UserID, base.CurrUserInfo().DepartmentCode);
                    }
                }
            }

        }
    }
}
