using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Model;
using Shu.BLL;
using Shu.Comm;

namespace Shu.Manage.Sys
{
    public partial class UserInfoAddOr : BasePage
    {
        Sys_UserInfo userInfo = new Sys_UserInfo();
        public Sys_UserInfoBLL bllUserInfo = new Sys_UserInfoBLL();
        public Sys_DataDictBLL bllSys_DataDict = new Sys_DataDictBLL();
        public Sys_DepartmentBLL bllSys_Department = new Sys_DepartmentBLL();
        public Sys_RoleBLL bllRole = new Sys_RoleBLL();
        public Sys_PostBLL bllPost = new Sys_PostBLL();
        Sys_UserInfoBLL bllUser = new Sys_UserInfoBLL();
        Sys_UserInfo modelUser = new Sys_UserInfo();
        View_Sys_UserInfo modelViewUser = new View_Sys_UserInfo();
        Sys_DataDictBLL bllDataDict = new Sys_DataDictBLL();
        Sys_DataDict modelDataDict = new Sys_DataDict();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DdlUserTypeBind();
                this.hid_Dep.Value = RequstStr("DepCode");
                DdlUserStatusBind();
                InitBind();
                BindDDL();
                InitBinds();

            }
        }
        /// <summary>
        /// 绑定用户类型
        /// </summary>
        private void DdlUserTypeBind()
        {
            this.ddlUserType.DataSource = bllSys_DataDict.GetList(p => p.DataDict_ParentCode == "04").OrderBy(m => m.DataDict_Sequence).ToList();
            this.ddlUserType.DataTextField = "DataDict_Name";
            this.ddlUserType.DataValueField = "DataDict_Code";
            this.ddlUserType.DataBind();
        }


        /// <summary>
        /// 绑定用户状态
        /// </summary>
        private void DdlUserStatusBind()
        {
            this.ddlStatus.DataSource = bllSys_DataDict.GetList(p => p.DataDict_ParentCode == "03").ToList();
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

            //btnAdd.Visible = RequstStr("type") == "" ? true : false;
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
                Sys_UserInfo account = bllUserInfo.Get(p => p.UserInfoID == hid_AccountId.Value);
                if (account == null)
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
            this.ddlDepart.DataSource = bllSys_Department.GetAll().OrderBy(m => m.Department_Level).OrderBy(m => m.Department_Sequence).ToList();
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
                //List<Sys_UserChargeDep> list = bll.GetList("UserChargeDep_ExecutiveOfficerID='" + RequstStr("id") + "'");
                List<Sys_UserChargeDep> list = bll.GetList(p => p.UserChargeDep_ExecutiveOfficerID == str).ToList();
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
                //List<Sys_UserChargeDep> list = bll.GetList("UserChargeDep_ExecutiveOfficerID='" + RequstStr("id") + "'");
                //List<Sys_UserChargeDep> list = bll.GetList(p => p.UserChargeDep_ExecutiveOfficerID == RequstStr("id")).ToList();
                string userChargeDepMessage = "";// string.Format("您已添加{0}个" + st, list.Count);
                lblFgbm.Text = userChargeDepMessage;
            }

            //List<Sys_Department> d = bllSys_Department.GetList(p => p.Department_Code == this.hid_Dep.Value).ToList();
            //foreach (Sys_Department list_dep in d)
            //{
            //    sqlWhere += " and cast(Role_Calss as int )>=" + list_dep.Department_Class;

            //}


            if (roleName.Contains("超级管理员"))
            {
                //sqlWhere = " Role_Name in('系统管理员','部门管理员','总队管理员') and Role_Name !='超级管理员'";
                //this.ckbxlist.DataSource = bllRole.GetList(sqlWhere);
                this.ckbxlist.DataSource = bllRole.GetList(p => p.Role_Name == "系统管理员" || p.Role_Name == "部门管理员" || p.Role_Name == "总队管理员" || p.Role_Name != "超级管理员").ToList();
            }
            else
            {
                //this.ckbxlist.DataSource = bllRole.GetList(sqlWhere + " AND Role_Name not in('超级管理员') ");
                this.ckbxlist.DataSource = bllRole.GetList(p => (p.Role_Name == "系统管理员" || p.Role_Name == "部门管理员" || p.Role_Name == "总队管理员" || p.Role_Name != "超级管理员") && p.Role_Name != "超级管理员").ToList();
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
            List<Sys_Department> depModel = bllSys_Department.GetList(p => p.Department_Code == ddlDepart.SelectedValue).ToList();
            foreach (Sys_Department rwo in depModel)
            {
                strWhere = "Post_DepCode = '" + rwo.Department_Code + "'";
                //chbGwList.DataSource = bllPost.GetList(strWhere).OrderBy(m => m.Post_Sort);
                chbGwList.DataSource = bllPost.GetList(p => p.Post_DepCode == rwo.Department_Code).OrderBy(m => m.Post_Sort).ToList();
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
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
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
            Sys_UserInfo account = bllUserInfo.Get(p => p.UserInfoID == id);
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
            //if (bllUserInfo.GetList(strWhere).Count > 0)
            if (bllUserInfo.GetList(p => p.UserInfo_LoginUserName == txtLoginName.Text && p.UserInfoID == RequstStr("id") && p.UserInfo_Status == "0301").ToList().Count > 0)
            {
                MessageBox.Show(this, "您输入的用户帐号已经存在,请重新输入！");
                // txtLoginName.Text = "";
                return;
            }

            if (this.t_UserInfo_PhoneNumber.Text.Trim() != "")//手机号码
            {
                List<Sys_UserInfo> bu = bllUser.GetList(p => p.UserInfo_PhoneNumber == this.t_UserInfo_PhoneNumber.Text.Trim().ToString() && p.UserInfoID != id).ToList();
                if (bu.Count > 0)
                {
                    MessageBox.Show(this, "手机号码已存在,请重新输入！");
                    return;
                }
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
            // account.UserInfo_LoginUserPwd = t_UserInfo_LoginUserPwd.Text;
            account.UserInfo_PhoneNumber = t_UserInfo_PhoneNumber.Text;
            account.UserInfo_Nation = t_UserInfo_Nation.SelectedValue;
            account.UserInfo_Sex = t_UserInfo_Sex.SelectedValue;
            account.UserInfo_IdentityCred = t_UserInfo_IdentityCred.Text;
            if (t_UserInfo_Age.Text != "")
            {
                account.UserInfo_Age = Convert.ToInt32(t_UserInfo_Age.Text.Trim());

            }
            if (t_UserInfo_DateBirth.Text != "")
            {
                account.UserInfo_DateBirth = Convert.ToDateTime(t_UserInfo_DateBirth.Text.Trim());

            }
            //modelUser.UserInfo_Status = t_UserInfo_StatusName.Text;
            //modelUser. = t_Department_Name.Text;
            //modelUser.UserInfo_Post = t_UserInfo_PostName.Text;
            account.UserInfo_Hometown = t_UserInfo_Hometown.Text;
            account.UserInfo_Address = t_UserInfo_Address.Text;
            account.UserInfo_PoliticalLandscape = t_UserInfo_PoliticalLandscape.Text;
            account.UserInfo_EducationalLevel = t_UserInfo_EducationalLevel.Text;
            account.UserInfo_Schools = t_UserInfo_Schools.Text;
            account.UserInfo_Specialty = t_UserInfo_Specialty.Text;
            if (t_UserInfo_StartWorkDate.Text != "")
            {
                account.UserInfo_StartWorkDate = Convert.ToDateTime(t_UserInfo_StartWorkDate.Text);
            }
            if (t_UserInfo_JoinPartyDate.Text != "")
            {
                account.UserInfo_JoinPartyDate = Convert.ToDateTime(t_UserInfo_JoinPartyDate.Text);
            }
            if (t_UserInfo_EnlistTime.Text != "")
            {
                account.UserInfo_NowHoldPostTime = Convert.ToDateTime(t_UserInfo_EnlistTime.Text);

            }
            account.UserInfo_Badge = t_UserInfo_Badge.Text;
            account.UserInfo_EduBackground = t_UserInfo_EnlistPlace.Text;

            account.UserInfo_Position = t_UserInfo_Position.Text;


            account.UserInfo_PositionLevel = t_UserInfo_PositionLevel.Text;
            if (t_UserInfo_NowHoldPostTime.Text != "")
            {
                account.UserInfo_NowHoldPostTime = Convert.ToDateTime(t_UserInfo_NowHoldPostTime.Text);

            }
            account.UserInfo_OccupTitle = t_UserInfo_OccupTitle.Text;

            // account.UserInfo_LoginUserPwd = DESEncrypt.Encrypt(t_UserInfo_LoginUserPwd.Text.ToString().Trim());

            if (txt_Yhxx_DTcsrq.Value != "")
            {
                account.UserInfo_DateBirth = Convert.ToDateTime(txt_Yhxx_DTcsrq.Value);
            }
            if (txt_Yhxx_Iage.Value != "")
            {
                account.UserInfo_Age = Convert.ToInt32(txt_Yhxx_Iage.Value);
            }
            bool i = new Sys_UserInfoBLL().Update(account);
            if (i)
            {
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
                MessageBox.ShowAndRedirect(this, "保存出现问题，请重试", "UserInfoList.aspx?depCode=" + this.hid_Dep.Value);
            }
        }


        /// <summary>
        /// 保存新增的信息
        /// </summary>
        private void AddUserInfo()
        {

            userInfo.UserInfoID = hid_AccountId.Value;

            if (bllUserInfo.GetList(p => p.UserInfo_LoginUserName == txtLoginName.Text.Trim() && p.UserInfo_Status == "0301").ToList().Count > 0 && ddlStatus.SelectedValue.Equals("0301"))
            {
                MessageBox.Show(this, "您输入的用户帐号已经存在,请重新输入！");
                return;
            }
            else
            {
                userInfo.UserInfo_LoginUserName = this.txtLoginName.Text;
            }

            if (this.t_UserInfo_PhoneNumber.Text.Trim() != "")//手机号码
            {
                List<Sys_UserInfo> bu = bllUser.GetList(p => p.UserInfo_PhoneNumber == this.t_UserInfo_PhoneNumber.Text.Trim().ToString()).ToList();
                if (bu.Count > 0)
                {
                    MessageBox.Show(this, "手机号码已存在,请重新输入！");
                    return;
                }
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
            userInfo.UserInfo_PhoneNumber = t_UserInfo_PhoneNumber.Text;
            userInfo.UserInfo_Nation = t_UserInfo_Nation.SelectedValue;
            userInfo.UserInfo_Sex = t_UserInfo_Sex.SelectedValue;
            userInfo.UserInfo_IdentityCred = t_UserInfo_IdentityCred.Text;
            userInfo.UserInfo_IsDelete = false;
            if (t_UserInfo_Age.Text != "")
            {
                userInfo.UserInfo_Age = Convert.ToInt32(t_UserInfo_Age.Text.Trim());

            }
            if (t_UserInfo_DateBirth.Text != "")
            {
                userInfo.UserInfo_DateBirth = Convert.ToDateTime(t_UserInfo_DateBirth.Text.Trim());

            }
            userInfo.UserInfo_Hometown = t_UserInfo_Hometown.Text;
            userInfo.UserInfo_Address = t_UserInfo_Address.Text;
            userInfo.UserInfo_PoliticalLandscape = t_UserInfo_PoliticalLandscape.Text;
            userInfo.UserInfo_EducationalLevel = t_UserInfo_EducationalLevel.Text;
            userInfo.UserInfo_Schools = t_UserInfo_Schools.Text;
            userInfo.UserInfo_Specialty = t_UserInfo_Specialty.Text;
            if (t_UserInfo_StartWorkDate.Text != "")
            {
                userInfo.UserInfo_StartWorkDate = Convert.ToDateTime(t_UserInfo_StartWorkDate.Text);
            }
            if (t_UserInfo_JoinPartyDate.Text != "")
            {
                userInfo.UserInfo_JoinPartyDate = Convert.ToDateTime(t_UserInfo_JoinPartyDate.Text);
            }
            if (t_UserInfo_EnlistTime.Text != "")
            {
                userInfo.UserInfo_NowHoldPostTime = Convert.ToDateTime(t_UserInfo_EnlistTime.Text);

            }
            userInfo.UserInfo_Badge = t_UserInfo_Badge.Text;
            userInfo.UserInfo_EduBackground = t_UserInfo_EnlistPlace.Text;

            userInfo.UserInfo_Position = t_UserInfo_Position.Text;


            userInfo.UserInfo_PositionLevel = t_UserInfo_PositionLevel.Text;
            if (t_UserInfo_NowHoldPostTime.Text != "")
            {
                userInfo.UserInfo_NowHoldPostTime = Convert.ToDateTime(t_UserInfo_NowHoldPostTime.Text);

            }
            userInfo.UserInfo_OccupTitle = t_UserInfo_OccupTitle.Text;

            if (txt_Yhxx_DTcsrq.Value != "")
            {
                userInfo.UserInfo_DateBirth = Convert.ToDateTime(txt_Yhxx_DTcsrq.Value);
            }
            if (txt_Yhxx_Iage.Value != "")
            {
                userInfo.UserInfo_Age = Convert.ToInt32(txt_Yhxx_Iage.Value);
            }
            bool i = new Sys_UserInfoBLL().Add(userInfo);
            if (i)
            {
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
            //bll.DeleteWhere(" UserChargeDep_ExecutiveOfficerID='" + this.hid_AccountId.Value + "'");
            bll.Delete(p => p.UserChargeDep_ExecutiveOfficerID == this.hid_AccountId.Value);
            string pwd = txtLoginPwd.Text.Trim();
            RoleBind();
            txtLoginPwd.Attributes.Add("value", pwd);
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        private void BindDDL()
        {
            BindListControl(t_UserInfo_Nation, "0801");//民族
            BindListControl(t_UserInfo_PoliticalLandscape, "0802");//政治面貌
            BindListControl(t_UserInfo_EducationalLevel, "0803");//学历
            BindListControl(t_UserInfo_OccupTitle, "0804");//职称
            BindListControl(t_UserInfo_PositionLevel, "0805");//职务级别

        }
        /// <summary>
        /// 绑定控件信息
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="rootcode"></param>
        public void BindListControl(System.Web.UI.WebControls.ListControl lc, string rootcode)
        {
            var list = bllDataDict.GetList(p => p.DataDict_ParentCode == rootcode).OrderBy(p => p.DataDict_Sequence).ToList();//(" DataDict_ParentCode= " + rootcode + "order by DataDict_Sequence asc"); ;
            //var model = new Sys_DataDict { DataDict_Name = "-请选择-", DataDict_Code = "" };
            //list.Insert(0, model);
            lc.DataSource = list;
            lc.DataTextField = "DataDict_Name";
            lc.DataValueField = "DataDict_Name";
            lc.DataBind();

            lc.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 信息初始化绑定
        /// </summary>
        protected void InitBinds()
        {
            string id = RequstStr("id");
            if (!string.IsNullOrEmpty(id))
            {
                //modelViewUser = bllUser.FindViewUser(id);
                View_Sys_UserInfoBLL bllUser = new View_Sys_UserInfoBLL();
                View_Sys_UserInfo modelViewUser = bllUser.Get(p => p.UserInfoID == id);
                //FormModel.SetForm<View_Sys_UserInfo>(this, modelViewUser, "t_");
                t_UserInfo_LoginUserName.Text = modelViewUser.UserInfo_LoginUserName;
                t_UserInfo_FullName.Text = modelViewUser.UserInfo_FullName;
                //t_UserInfo_LoginUserPwd.Attributes.Add("value", DESEncrypt.Decrypt(modelViewUser.UserInfo_LoginUserPwd));
                //modelUser.UserInfo_LoginUserPwd = DESEncrypt.Encrypt(t_UserInfo_LoginUserPwd.Text.ToString().Trim());
                //t_UserInfo_LoginUserPwd.Text = modelViewUser.UserInfo_LoginUserPwd;
                t_UserInfo_PhoneNumber.Text = modelViewUser.UserInfo_PhoneNumber;
                t_UserInfo_Nation.SelectedValue = modelViewUser.UserInfo_Nation;
                t_UserInfo_Sex.SelectedValue = modelViewUser.UserInfo_Sex;
                t_UserInfo_IdentityCred.Text = modelViewUser.UserInfo_IdentityCred;
                t_UserInfo_Age.Text = Convert.ToInt32(modelViewUser.UserInfo_Age).ToString();
                if (modelViewUser.UserInfo_DateBirth != null)
                {
                    t_UserInfo_DateBirth.Text = Convert.ToDateTime(modelViewUser.UserInfo_DateBirth).ToString("yyyy年MM月dd日");

                }
                t_UserInfo_StatusName.Text = modelViewUser.UserInfo_StatusName;
                t_Department_Name.Text = modelViewUser.Department_Name;
                t_UserInfo_PostName.Text = modelViewUser.UserInfo_PostName;
                t_UserInfo_Hometown.Text = modelViewUser.UserInfo_Hometown;
                t_UserInfo_Address.Text = modelViewUser.UserInfo_Address;
                t_UserInfo_PoliticalLandscape.Text = modelViewUser.UserInfo_PoliticalLandscape;
                t_UserInfo_EducationalLevel.Text = modelViewUser.UserInfo_EducationalLevel;
                t_UserInfo_Schools.Text = modelViewUser.UserInfo_Schools;
                t_UserInfo_Specialty.Text = modelViewUser.UserInfo_Specialty;
                if (modelViewUser.UserInfo_StartWorkDate != null)
                {
                    t_UserInfo_StartWorkDate.Text = Convert.ToDateTime(modelViewUser.UserInfo_StartWorkDate).ToString("yyyy年MM月dd日");
                }
                if (modelViewUser.UserInfo_JoinPartyDate != null)
                {
                    t_UserInfo_JoinPartyDate.Text = Convert.ToDateTime(modelViewUser.UserInfo_JoinPartyDate).ToString("yyyy年MM月dd日");
                }
                if (modelViewUser.UserInfo_NowHoldPostTime != null)
                {
                    t_UserInfo_EnlistTime.Text = Convert.ToDateTime(modelViewUser.UserInfo_NowHoldPostTime).ToString("yyyy年MM月dd日");

                }
                t_UserInfo_Badge.Text = modelViewUser.UserInfo_Badge;
                t_UserInfo_EnlistPlace.Text = modelViewUser.UserInfo_EduBackground;

                t_UserInfo_Position.Text = modelViewUser.UserInfo_Position;


                t_UserInfo_PositionLevel.Text = modelViewUser.UserInfo_PositionLevel;
                if (modelViewUser.UserInfo_NowHoldPostTime != null)
                {
                    t_UserInfo_NowHoldPostTime.Text = Convert.ToDateTime(modelViewUser.UserInfo_NowHoldPostTime).ToString("yyyy年MM月dd日");

                }
                t_UserInfo_OccupTitle.Text = modelViewUser.UserInfo_OccupTitle;

                // t_UserInfo_LoginUserPwd.Attributes.Add("value", DESEncrypt.Decrypt(modelViewUser.UserInfo_LoginUserPwd));
                //t_UserInfo_DateBirth.Text = modelViewUser.UserInfo_DateBirth != null ? Convert.ToDateTime(modelViewUser.UserInfo_DateBirth).ToString("yyyy年MM月dd日") : "";
                txt_Yhxx_DTcsrq.Value = modelViewUser.UserInfo_DateBirth != null ? Convert.ToDateTime(modelViewUser.UserInfo_DateBirth).ToString("yyyy年MM月dd日") : "";
                t_UserInfo_Age.Text = modelViewUser.UserInfo_Age != null ? modelViewUser.UserInfo_Age.ToString() : "";
                txt_Yhxx_Iage.Value = modelViewUser.UserInfo_Age != null ? modelViewUser.UserInfo_Age.ToString() : "";
            }
        }

    }
}