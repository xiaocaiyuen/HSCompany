using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using YDT.BLL;
using YDT.Model;

namespace YDT.Web.Manage.Sys
{
    public partial class UserInfoAdd : BasePage
    {
        Sys_UserInfo userInfo = new Sys_UserInfo();
        public Sys_UserInfoBLL bllUserInfo = new Sys_UserInfoBLL();
        public Sys_DataDictBLL bllSys_DataDict = new Sys_DataDictBLL();
        public Sys_DepartmentBLL bllSys_Department = new Sys_DepartmentBLL();
        public Sys_RoleBLL bllRole = new Sys_RoleBLL();
        public Sys_PostBLL bllPost = new Sys_PostBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                 DdlUserTypeBind();    
                this.hid_Dep.Value = RequstStr("DepCode");
                DdlUserStatusBind();
                InitBind();

            }
        }


        /// <summary>
        /// 绑定用户类型
        /// </summary>
        private void DdlUserTypeBind()
        {
            this.ddlUserType.DataSource = bllSys_DataDict.FindWhere("DataDict_ParentCode ='04'").OrderBy(m => m.DataDict_Sequence);
            //this.ddlUserType.DataSource = bllSys_DataDict.FindWhere(p => p.DataDict_ParentCode.Contains("04")).OrderBy(m => m.DataDict_Sequence);
            
            this.ddlUserType.DataTextField = "DataDict_Name";
            this.ddlUserType.DataValueField = "DataDict_Code";
            this.ddlUserType.DataBind();
        }


        /// <summary>
        /// 绑定用户状态
        /// </summary>
        private void DdlUserStatusBind()
        {
            this.ddlStatus.DataSource = bllSys_DataDict.FindWhere("DataDict_ParentCode ='03'");
            //this.ddlStatus.DataSource = bllSys_DataDict.FindWhere(p=>p.DataDict_ParentCode.Contains("03"));
            this.ddlStatus.DataTextField = "DataDict_Name";
            this.ddlStatus.DataValueField = "DataDict_Code";
            this.ddlStatus.DataBind();
        }


        /// <summary>
        /// 加载显示
        /// </summary>
        private void InitBind()
        {
            DepBind();
           
            btnAdd.Visible = RequstStr("type") == "" ? true : false;
            if (RequstStr("id") == "")  //添加
            {
                hid_AccountId.Value = Guid.NewGuid().ToString();
                txtLoginName.Enabled = true;
                PostBind();
                RoleBind();
            }
            else
            {

                hid_AccountId.Value = RequstStr("id");
                //Sys_UserInfo account = bllUserInfo.Find(RequstStr("id"));
                Sys_UserInfo account = bllUserInfo.Find(p => p.UserInfoID == hid_AccountId.Value);
                if (account==null)
                {
                    MessageBox.ShowAndRedirect(this, "该数据不存在！", "UserInfoList.aspx?depCode=" + this.hid_Dep.Value);
                }
                else
                {

                
                this.ddlDepart.SelectedValue = account.UserInfo_DepCode;
                this.txtLoginName.Text = account.UserInfo_LoginUserName;
                this.txtLoginPwd.Attributes.Add("value", DESEncrypt.Decrypt(account.UserInfo_LoginUserPwd));
                this.txtManName.Text = account.UserInfo_FullName;
                this.txtPx.Text = account.UserInfo_Sequence.ToString();
                this.hid_LoginName.Value = account.UserInfo_LoginUserName;
                ddlUserType.SelectedValue = account.UserInfo_Type;
                ddlStatus.SelectedValue = account.UserInfo_Status;
                this.ddlDepart.Enabled = true;
                PostBind();
                RoleBind();
                this.SetChecked(ckbxlist, account.UserInfo_RoleID, ",");
                this.SetChecked(chbGwList, account.UserInfo_Post, ",");
            }
            }
           
        }

        /// <summary>
        /// 绑定部门
        /// </summary>
        private void DepBind()
        {
            this.ddlDepart.DataSource = bllSys_Department.FindALL().OrderBy(m => m.Department_Level).OrderBy(m => m.Department_Sequence);
            this.ddlDepart.DataTextField = "Department_Name";
            this.ddlDepart.DataValueField = "Department_Code";
            this.ddlDepart.DataBind();
            this.ddlDepart.SelectedValue = this.hid_Dep.Value;
        }


        /// <summary>
        /// 绑定角色信息
        /// </summary>
        private void RoleBind()
        {
            string roleName = base.CurrUserInfo().RoleName;

            string sqlWhere = "";

            string code = ddlUserType.SelectedValue;

            if (code == "0401")//管理员
            {
                sqlWhere = " Role_Name in('系统管理员','部门管理员','总队管理员') and Role_Name !='超级管理员'";
                lt_Open.Visible = false;
                lblFgbm.Visible = false;
                lbl_FGtxt.Visible = false;
            }
            else if (code == "0402")//分管用户
            {
                lt_Open.Visible = true;
                lblFgbm.Visible = true;
                lbl_FGtxt.Visible = true;
                sqlWhere = " Role_Name in('分管领导','纪委领导') ";

                string st = "分管部门";
                this.lbl_FGtxt.Text = st;
                lt_Open.Text = " <a href=\"#\" style=\"color: Blue\" onclick=\"OpenForm('2');\">添加" + st + "</a>";
                Sys_UserChargeDepBLL bll = new Sys_UserChargeDepBLL();
                string str = RequstStr("id");
                //List<Sys_UserChargeDep> list = bll.FindWhere("UserChargeDep_ExecutiveOfficerID='" + RequstStr("id") + "'");
                List<Sys_UserChargeDep> list = bll.FindWhere(p => p.UserChargeDep_ExecutiveOfficerID==str);
                string userChargeDepMessage = string.Format("您已添加{0}个" + st, list.Count);
                lblFgbm.Text = userChargeDepMessage;
            }
            else//普通用户
            {
                lt_Open.Visible = true;
                lblFgbm.Visible = true;
                lbl_FGtxt.Visible = true;
                sqlWhere = "Role_Name not in('分管领导','纪委领导','超级管理员','系统管理员','部门管理员') ";
                string st = "";// "挂钩学校";
                this.lbl_FGtxt.Text = st;
                lt_Open.Text = "";// " <a href=\"#\" style=\"color: Blue\" onclick=\"OpenForm('1');\">添加" + st + "</a>";
                Sys_UserChargeDepBLL bll = new Sys_UserChargeDepBLL();
                //List<Sys_UserChargeDep> list = bll.FindWhere("UserChargeDep_ExecutiveOfficerID='" + RequstStr("id") + "'");
                List<Sys_UserChargeDep> list = bll.FindWhere(p => p.UserChargeDep_ExecutiveOfficerID == RequstStr("id"));
                string userChargeDepMessage = "";// string.Format("您已添加{0}个" + st, list.Count);
                lblFgbm.Text = userChargeDepMessage;
            }

            //Sys_Department d = bllSys_Department.FindByCode(this.hid_Dep.Value);
            List<Sys_Department> d = bllSys_Department.FindWhere(p => p.Department_Code == this.hid_Dep.Value);
            foreach (Sys_Department list_dep in d)
            {
                sqlWhere += " and cast(Role_Calss as int )>=" + list_dep.Department_Class;
               
            }
            

            if (roleName.Contains("超级管理员"))
            {
                //sqlWhere = " Role_Name in('系统管理员','部门管理员','总队管理员') and Role_Name !='超级管理员'";
                //this.ckbxlist.DataSource = bllRole.FindWhere(sqlWhere);
                this.ckbxlist.DataSource = bllRole.FindWhere(p => p.Role_Name == "系统管理员"||p.Role_Name == "部门管理员"||p.Role_Name == "总队管理员"||p.Role_Name!="超级管理员");
            }
            else
            {
                //this.ckbxlist.DataSource = bllRole.FindWhere(sqlWhere + " AND Role_Name not in('超级管理员') ");
                this.ckbxlist.DataSource = bllRole.FindWhere(p => (p.Role_Name == "系统管理员"||p.Role_Name == "部门管理员"||p.Role_Name == "总队管理员"||p.Role_Name!="超级管理员") && p.Role_Name!= "超级管理员" );
            }
            this.ckbxlist.DataTextField = "Role_Name";
            this.ckbxlist.DataValueField = "RoleID";
            this.ckbxlist.DataBind();
        }


        /// <summary>
        /// 岗位信息绑定
        /// </summary>
        private void PostBind()
        {
            chbGwList.Items.Clear();

            string strWhere = "";
            //Sys_Department depModel = bllSys_Department.FindByCode(ddlDepart.SelectedValue);
            List<Sys_Department> depModel = bllSys_Department.FindWhere(p => p.Department_Code == ddlDepart.SelectedValue);
            foreach (Sys_Department rwo in depModel)
            {
                strWhere = "Post_DepCode = '" + rwo.Department_Code + "'";
                //chbGwList.DataSource = bllPost.FindWhere(strWhere).OrderBy(m => m.Post_Seqencing);
                chbGwList.DataSource = bllPost.FindWhere(p => p.Post_DepCode == rwo.Department_Code).OrderBy(m => m.Post_Seqencing);
            }
            
            
            chbGwList.DataTextField = "Post_Name";
            chbGwList.DataValueField = "PostID";
            chbGwList.DataBind();
        }

        /// <summary>
        /// 部门改变重新绑定岗位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            PostBind();
        }


        /// <summary>
        /// 保存按钮 判断是新增还是保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string ids = Request.QueryString["id"];
            if (RequstStr("id") != "")
            {
                this.UpdateUserInfo();
            }
            else
            {
                this.AddUserInfo();
            }
        }

        /// <summary>
        /// 保存修改信息
        /// </summary>
        private void UpdateUserInfo()
        {
            string id = RequstStr("id");
            //Sys_UserInfo account = bllUserInfo.Find(RequstStr("id"));
            Sys_UserInfo account = bllUserInfo.Find(p => p.UserInfoID == id);
            string oldDepid = account.UserInfo_DepCode;
            string oldState = account.UserInfo_Status;
            string newDepid = "";
            string newState = "";
            bool changeState = false;

            string strWhere = "1<>1";
            if (ddlStatus.SelectedValue.Equals("0301"))
            {
                strWhere = "UserInfo_LoginUserName ='" + txtLoginName.Text + "' and UserInfoID != '" + RequstStr("id") + "' and UserInfo_Status='0301'";
            }
            //if (bllUserInfo.FindWhere(strWhere).Count > 0)
            if (bllUserInfo.FindWhere(p => p.UserInfo_LoginUserName == txtLoginName.Text && p.UserInfoID == RequstStr("id") && p.UserInfo_Status == "0301").Count > 0)
            {
                MessageBox.Show(this, "您输入的用户帐号已经存在,请重新输入！");
                // txtLoginName.Text = "";
                return;
            }
            account.UserInfo_LoginUserName = this.txtLoginName.Text;
            account.UserInfo_FullName = this.txtManName.Text.Trim();
            if (this.ddlDepart.SelectedItem != null)
            {
                account.UserInfo_DepCode = this.ddlDepart.SelectedValue;
                newDepid = this.ddlDepart.SelectedValue;
            }
            string pwd = this.txtLoginPwd.Text.Trim();
            account.UserInfo_LoginUserPwd = DESEncrypt.Encrypt(pwd);
            account.UserInfo_Sequence = Convert.ToInt32(txtPx.Text.Trim());
            string strRole = this.GetChecked(ckbxlist, ",");
            if (!strRole.Equals(""))
            {
                strRole = strRole.Substring(0, strRole.LastIndexOf(','));
            }
            account.UserInfo_RoleID = strRole;
            account.UserInfo_Type = ddlUserType.SelectedValue;
            string strGwxx = GetChecked(chbGwList, ",");
            if (!strGwxx.Equals(""))
            {
                strGwxx = strGwxx.Substring(0, strGwxx.LastIndexOf(','));
            }
            account.UserInfo_Post = strGwxx;
            account.UserInfo_Status = ddlStatus.SelectedValue;
            newState = ddlStatus.SelectedValue;
            string msg = string.Empty;
            bool i = bllUserInfo.Update(account,out msg);
            if (i)
            {
                /////个人风险库
                //Risk_PerRiskLib PerRiskLib = new Risk_PerRiskLib();
                //Risk_PerRiskLibBLL bllRisk_PerRiskLib = new Risk_PerRiskLibBLL();
                //PerRiskLib.PerRiskLib_AccountId = account.UserInfoID;
                //PerRiskLib.PerRiskLib_AddUserId = base.CurrUserInfo().UserID;
                //PerRiskLib.PerRiskLib_AddTime = DateTime.Now;
                //PerRiskLib.PerRiskLibID = Guid.NewGuid().ToString();
                //bllRisk_PerRiskLib.Add(PerRiskLib,out msg);
                ////List<Risk_PerRiskLib> pb = bllRisk_PerRiskLib.FindWhere("PerRiskLib_AccountId='" + account.UserInfoID + "'");
                //List<Risk_PerRiskLib> pb = bllRisk_PerRiskLib.FindWhere(p => p.PerRiskLib_AccountId == account.UserInfoID);
                //if (pb.Count == 0)
                //{
                //    PerRiskLib.PerRiskLib_AccountId = account.UserInfoID;
                //    PerRiskLib.PerRiskLib_AddUserId = base.CurrUserInfo().UserID;
                //    PerRiskLib.PerRiskLib_AddTime = DateTime.Now;
                //    PerRiskLib.PerRiskLibID = Guid.NewGuid().ToString();
                //    bllRisk_PerRiskLib.Add(PerRiskLib,out msg);
                //}


                new Common_BLL().AddLog("用户管理", account.UserInfoID, "修改", "", base.CurrUserInfo().UserID, base.CurrUserInfo().DepartmentCode);

                if (changeState)
                {
                    MessageBox.ShowAndRedirect(this, "修改用户信息成功，该用户具有分管范围，为了避免影响系统的正常使用，请及时调整领导的分管范围，以免有所遗漏！", "UserInfoList.aspx?depCode=" + this.hid_Dep.Value);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this, "修改用户信息成功", "UserInfoList.aspx?depCode=" + this.hid_Dep.Value);
                }

            }
            else
            {
                MessageBox.ShowAndRedirect(this, msg, "UserInfoList.aspx?depCode=" + this.hid_Dep.Value);
            }
        }


        /// <summary>
        /// 保存新增的信息
        /// </summary>
        private void AddUserInfo()
        {
            
            userInfo.UserInfoID = hid_AccountId.Value;

            if (bllUserInfo.FindWhere(p => p.UserInfo_LoginUserName == txtLoginName.Text.Trim() && p.UserInfo_Status == "0301").Count > 0 && ddlStatus.SelectedValue.Equals("0301"))
            {
                MessageBox.Show(this, "您输入的用户帐号已经存在,请重新输入！");
                //txtLoginName.Text = "";
                return;
            }
            else
            {
                userInfo.UserInfo_LoginUserName = this.txtLoginName.Text;
            }

            userInfo.UserInfo_FullName = this.txtManName.Text.Trim();
            if (this.ddlDepart.SelectedItem != null)
            {
                userInfo.UserInfo_DepCode = this.ddlDepart.SelectedValue;
            }
            string pwd = this.txtLoginPwd.Text.Trim();
            userInfo.UserInfo_LoginUserPwd = DESEncrypt.Encrypt(pwd);
            userInfo.UserInfo_AddTime = DateTime.Now;
            userInfo.UserInfo_Sequence = Convert.ToInt32(txtPx.Text.Trim());
            userInfo.UserInfo_Type = ddlUserType.SelectedValue;
            userInfo.UserInfo_Status = ddlStatus.SelectedValue;
            string strRole = this.GetChecked(ckbxlist, ",");
            if (!strRole.Equals(""))
            {
                strRole = strRole.Substring(0, strRole.LastIndexOf(','));
            }
            userInfo.UserInfo_RoleID = strRole;
            string strGwxx = GetChecked(chbGwList, ",");
            if (!strGwxx.Equals(""))
            {
                strGwxx = strGwxx.Substring(0, strGwxx.LastIndexOf(','));
            }
            userInfo.UserInfo_Post = strGwxx;
            string msg = string.Empty;
            bool i = bllUserInfo.Add(userInfo,out msg);
            if (i)
            {
                ///个人风险库
                //Risk_PerRiskLib PerRiskLib = new Risk_PerRiskLib();
                //Risk_PerRiskLibBLL bllRisk_PerRiskLib = new Risk_PerRiskLibBLL();
                //PerRiskLib.PerRiskLib_AccountId = userInfo.UserInfoID;
                //PerRiskLib.PerRiskLib_AddUserId = base.CurrUserInfo().UserID;
                //PerRiskLib.PerRiskLib_AddTime = DateTime.Now;
                //PerRiskLib.PerRiskLibID = Guid.NewGuid().ToString();
                //bllRisk_PerRiskLib.Add(PerRiskLib,out msg);
                new Common_BLL().AddLog("用户账户管理", userInfo.UserInfoID, "新增", "", base.CurrUserInfo().UserID, base.CurrUserInfo().DepartmentCode);
                MessageBox.ShowAndRedirect(this, "新增用户成功", "UserInfoList.aspx?depCode=" + this.hid_Dep.Value);
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "新增用户失败", "UserInfoList.aspx?depCode=" + this.hid_Dep.Value);
            }
        }

        /// <summary>
        /// 得到CheckBoxList中选中了的值
        /// </summary>
        /// <param name="checkList">CheckBoxList</param>
        /// <param name="separator">分割符号</param>
        /// <returns></returns>
        private string GetChecked(CheckBoxList checkList, string separator)
        {
            string selval = "";
            for (int i = checkList.Items.Count - 1; i >= 0; i--)
            {
                if (checkList.Items[i].Selected)
                {
                    selval += checkList.Items[i].Value + separator;
                }
            }
            return selval;
        }


        /// <summary>
        /// 初始化CheckBoxList中哪些是选中了的
        /// </summary>
        /// <param name="checkList"></param>
        /// <param name="selval">选中了的值串例如："0,1,1,2,1"</param>
        /// <param name="separator">值串中使用的分割符例如"0,1,1,2,1"中的逗号</param>
        /// <returns></returns>
        private string SetChecked(CheckBoxList checkList, string selval, string separator)
        {
            selval = separator + selval + separator;
            for (int i = 0; i < checkList.Items.Count; i++)
            {
                checkList.Items[i].Selected = false;
                string val = separator + checkList.Items[i].Value + separator;
                if (selval.IndexOf(val) != -1)
                {
                    checkList.Items[i].Selected = true;
                    selval = selval.Replace(val, separator);  //然后从原来的值串中删除已经选中的
                    if (selval == separator)  //selval的最后一项也被选中的话，此时经过Replace 后，只会剩下一个分隔符
                    {
                        selval += separator;  //添加一个分隔符
                    }
                }
            }
            selval = selval.Substring(1, selval.Length - 2);
            return selval;
        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sys_UserChargeDepBLL bll = new Sys_UserChargeDepBLL();
            bll.DeleteWhere(" UserChargeDep_ExecutiveOfficerID='"+this.hid_AccountId.Value+"'");
            string pwd = txtLoginPwd.Text.Trim();
            RoleBind();
            txtLoginPwd.Attributes.Add("value", pwd);
        }
    }
}