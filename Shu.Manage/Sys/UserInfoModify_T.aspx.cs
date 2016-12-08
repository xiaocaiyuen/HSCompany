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
    public partial class UserInfoModify_T : BasePage
    {
        Sys_UserInfoBLL bllUser = new Sys_UserInfoBLL();
        Sys_UserInfo modelUser = new Sys_UserInfo();
        View_Sys_UserInfo modelViewUser = new View_Sys_UserInfo();
        Sys_DataDictBLL bllDataDict = new Sys_DataDictBLL();
        Sys_DataDict modelDataDict = new Sys_DataDict();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.hid_Dep.Value = RequstStr("DepCode");
                BindDDL();
                InitBind();
            }
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        private void BindDDL()
        {
            BindListControl(t_UserInfo_Nation, 0801);//民族
            BindListControl(t_UserInfo_PoliticalLandscape, 0802);//政治面貌
            BindListControl(t_UserInfo_EducationalLevel, 0803);//学历
            BindListControl(t_UserInfo_OccupTitle, 0804);//职称
            BindListControl(t_UserInfo_PositionLevel, 0805);//职务级别

        }
        /// <summary>
        /// 绑定控件信息
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="rootcode"></param>
        public void BindListControl(System.Web.UI.WebControls.ListControl lc, int rootcode)
        {
            var list = bllDataDict.FindWhere(" DataDict_ParentCode= " + rootcode + "order by DataDict_Sequence asc"); ;
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
        protected void InitBind()
        {
            string id = RequstStr("id");
            //modelViewUser = bllUser.FindViewUser(id);
            View_Sys_UserInfoBLL bllUser = new View_Sys_UserInfoBLL();
            View_Sys_UserInfo modelViewUser = bllUser.FindView(" UserInfoID= '" + id+"'");
            //FormModel.SetForm<View_Sys_UserInfo>(this, modelViewUser, "t_");
            t_UserInfo_LoginUserName.Text = modelViewUser.UserInfo_LoginUserName;
            t_UserInfo_FullName.Text = modelViewUser.UserInfo_FullName;
            t_UserInfo_LoginUserPwd.Attributes.Add("value", DESEncrypt.Decrypt(modelViewUser.UserInfo_LoginUserPwd));
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
            t_UserInfo_StatusName.Text=modelViewUser.UserInfo_StatusName;
            t_Department_Name.Text = modelViewUser.Department_Name;
            t_UserInfo_PostName.Text = modelViewUser.UserInfo_PostName;
            t_UserInfo_Hometown.Text = modelViewUser.UserInfo_Hometown;
            t_UserInfo_Address.Text = modelViewUser.UserInfo_Address;
            t_UserInfo_PoliticalLandscape.Text = modelViewUser.UserInfo_PoliticalLandscape;
            t_UserInfo_EducationalLevel.Text = modelViewUser.UserInfo_EducationalLevel;
            t_UserInfo_Schools.Text = modelViewUser.UserInfo_Schools;
            t_UserInfo_Specialty.Text = modelViewUser.UserInfo_Specialty;
            if (modelViewUser.UserInfo_StartWorkDate!= null)
            {
                t_UserInfo_StartWorkDate.Text = Convert.ToDateTime(modelViewUser.UserInfo_StartWorkDate).ToString("yyyy年MM月dd日");
            }
            if (modelViewUser.UserInfo_JoinPartyDate!= null)
            {
                t_UserInfo_JoinPartyDate.Text = Convert.ToDateTime(modelViewUser.UserInfo_JoinPartyDate).ToString("yyyy年MM月dd日");
            }
            if (modelViewUser.UserInfo_NowHoldPostTime!= null)
            {
                t_UserInfo_EnlistTime.Text = Convert.ToDateTime(modelViewUser.UserInfo_NowHoldPostTime).ToString("yyyy年MM月dd日");

            }
            t_UserInfo_Badge.Text = modelViewUser.UserInfo_Badge;
            t_UserInfo_EnlistPlace.Text=modelViewUser.UserInfo_EduBackground;

            t_UserInfo_Position.Text = modelViewUser.UserInfo_Position;
           
            
            t_UserInfo_PositionLevel.Text=modelViewUser.UserInfo_PositionLevel;
            if (modelViewUser.UserInfo_NowHoldPostTime!= null)
            {
                t_UserInfo_NowHoldPostTime.Text = Convert.ToDateTime(modelViewUser.UserInfo_NowHoldPostTime).ToString("yyyy年MM月dd日");

            }
            t_UserInfo_OccupTitle.Text=modelViewUser.UserInfo_OccupTitle;

            t_UserInfo_LoginUserPwd.Attributes.Add("value", DESEncrypt.Decrypt(modelViewUser.UserInfo_LoginUserPwd));
            //t_UserInfo_DateBirth.Text = modelViewUser.UserInfo_DateBirth != null ? Convert.ToDateTime(modelViewUser.UserInfo_DateBirth).ToString("yyyy年MM月dd日") : "";
            txt_Yhxx_DTcsrq.Value = modelViewUser.UserInfo_DateBirth != null ? Convert.ToDateTime(modelViewUser.UserInfo_DateBirth).ToString("yyyy年MM月dd日") : "";
            t_UserInfo_Age.Text = modelViewUser.UserInfo_Age != null ? modelViewUser.UserInfo_Age.ToString() : "";
            txt_Yhxx_Iage.Value = modelViewUser.UserInfo_Age != null ? modelViewUser.UserInfo_Age.ToString() : "";
        }


        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btntijiao_Click(object sender, ImageClickEventArgs e)
        {
            string id = RequstStr("id");
            //modelUser = bllUser.Find(p=>p.UserInfoID == id);
          //  FormModel.GetForm<Sys_UserInfo>(this, modelUser, "t_");
            Sys_UserInfoBLL bllUser = new Sys_UserInfoBLL();
            Sys_UserInfo modelUser = bllUser.Find(p => p.UserInfoID == id);
            //modelUser.UserInfo_LoginUserName = t_UserInfo_LoginUserName.Text;
            //modelUser.UserInfo_FullName = t_UserInfo_FullName.Text;
            modelUser.UserInfo_LoginUserPwd = t_UserInfo_LoginUserPwd.Text;
            modelUser.UserInfo_PhoneNumber = t_UserInfo_PhoneNumber.Text;
            modelUser.UserInfo_Nation = t_UserInfo_Nation.SelectedValue;
            modelUser.UserInfo_Sex = t_UserInfo_Sex.SelectedValue;
            modelUser.UserInfo_IdentityCred = t_UserInfo_IdentityCred.Text;
            //if (t_UserInfo_Age.Text !="")
            //{
            //    modelUser.UserInfo_Age = Convert.ToInt32(t_UserInfo_Age);
                
            //}
            //if (t_UserInfo_DateBirth.Text != "")
            //{
            //    modelUser.UserInfo_DateBirth = Convert.ToDateTime(t_UserInfo_DateBirth);

            //}
            //modelUser.UserInfo_Status = t_UserInfo_StatusName.Text;
            //modelUser. = t_Department_Name.Text;
            //modelUser.UserInfo_Post = t_UserInfo_PostName.Text;
            modelUser.UserInfo_Hometown = t_UserInfo_Hometown.Text;
            modelUser.UserInfo_Address = t_UserInfo_Address.Text;
            modelUser.UserInfo_PoliticalLandscape = t_UserInfo_PoliticalLandscape.Text;
            modelUser.UserInfo_EducationalLevel = t_UserInfo_EducationalLevel.Text;
            modelUser.UserInfo_Schools = t_UserInfo_Schools.Text;
            modelUser.UserInfo_Specialty = t_UserInfo_Specialty.Text;
            if (t_UserInfo_StartWorkDate != null)
            {
                modelUser.UserInfo_StartWorkDate = Convert.ToDateTime(t_UserInfo_StartWorkDate.Text);
            }
            if (t_UserInfo_JoinPartyDate != null)
            {
                modelUser.UserInfo_JoinPartyDate = Convert.ToDateTime(t_UserInfo_JoinPartyDate.Text);
            }
            if (t_UserInfo_EnlistTime != null)
            {
                modelUser.UserInfo_NowHoldPostTime = Convert.ToDateTime(t_UserInfo_EnlistTime.Text);

            }
            modelUser.UserInfo_Badge = t_UserInfo_Badge.Text;
            modelUser.UserInfo_EduBackground = t_UserInfo_EnlistPlace.Text;

            modelUser.UserInfo_Position = t_UserInfo_Position.Text;


            modelUser.UserInfo_PositionLevel = t_UserInfo_PositionLevel.Text;
            if (t_UserInfo_NowHoldPostTime != null)
            {
                modelUser.UserInfo_NowHoldPostTime = Convert.ToDateTime(t_UserInfo_NowHoldPostTime.Text);

            }
            modelUser.UserInfo_OccupTitle = t_UserInfo_OccupTitle.Text;

            modelUser.UserInfo_LoginUserPwd = DESEncrypt.Encrypt(t_UserInfo_LoginUserPwd.Text.ToString().Trim());

            if (txt_Yhxx_DTcsrq.Value != "")
            {
                modelUser.UserInfo_DateBirth = Convert.ToDateTime(txt_Yhxx_DTcsrq.Value);
            }
            if (txt_Yhxx_Iage.Value != "")
            {
                modelUser.UserInfo_Age = Convert.ToInt32(txt_Yhxx_Iage.Value);
            }
            //List<Sys_UserInfo> bu = bllUser.FindWhere("UserInfo_PhoneNumber='" + this.t_UserInfo_PhoneNumber.Text.Trim().ToString() + "' and UserInfoID not in('" + id + "')");
            
            List<Sys_UserInfo> bu = bllUser.FindWhere(p => p.UserInfo_PhoneNumber == this.t_UserInfo_PhoneNumber.Text.Trim().ToString() && p.UserInfoID != id);
            
            if(bu.Count>0){
                this.t_UserInfo_PhoneNumber.Text = "";
                MessageBox.Show(this, "手机号码已存在,请重新输入！");
            }else{
                string msg = string.Empty;
                if (bllUser.Update(modelUser,out msg))
                {

                    new Common_BLL().AddLog("系统管理>>用户信息维护", id, "修改", "修改用户信息维护", CurrUserInfo().UserID, CurrUserInfo().DepartmentCode);
                    MessageBox.ShowAndRedirect(this, "修改成功", "UserInfoList_T.aspx?depCode=" + this.hid_Dep.Value);
                    //    //更新此人对应的岗位自查表模块表
                    //(new DAL.PositionChange()).UpdateGwfxwx(model.ManID, "1", model.Position);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this, msg, "UserInfoList_T.aspx?depCode=" + this.hid_Dep.Value);
                }
            }
        }




    }
}