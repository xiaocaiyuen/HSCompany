using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;
using Shu.BLL;
using Shu.Model;

namespace Shu.Manage.Sys
{
    public partial class UserInfoModify : BasePage
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
                BindDDL();
                InitBind();
            }
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
            //var list = bllDataDict.FindWhere(" DataDict_ParentCode= " + rootcode + "order by DataDict_Sequence asc");
            var list = bllDataDict.GetList(p => p.DataDict_ParentCode == rootcode).OrderBy(p => p.DataDict_Sequence).ToList();

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
            //modelViewUser = bllUser.FindViewUser(CurrUserInfo().UserID);
            View_Sys_UserInfoBLL bllUser = new View_Sys_UserInfoBLL();
            string userid = CurrUserInfo().UserID;
            modelViewUser = new View_Sys_UserInfo();
            //modelViewUser = bllUser.Find(p => p.UserInfoID == userid);
            modelViewUser = bllUser.Get(p => p.UserInfoID == userid);
            if (modelViewUser != null)
            {
                FormModel.SetForm<View_Sys_UserInfo>(this, modelViewUser, "t_");
                t_UserInfo_LoginUserPwd.Attributes.Add("value", DESEncrypt.Decrypt(modelViewUser.UserInfo_LoginUserPwd));
                t_UserInfo_DateBirth.Text = modelViewUser.UserInfo_DateBirth != null ? Convert.ToDateTime(modelViewUser.UserInfo_DateBirth).ToString("yyyy年MM月dd日") : "";
                txt_Yhxx_DTcsrq.Value = modelViewUser.UserInfo_DateBirth != null ? Convert.ToDateTime(modelViewUser.UserInfo_DateBirth).ToString("yyyy年MM月dd日") : "";
                t_UserInfo_Age.Text = modelViewUser.UserInfo_Age != null ? modelViewUser.UserInfo_Age.ToString() : "";
                txt_Yhxx_Iage.Value = modelViewUser.UserInfo_Age != null ? modelViewUser.UserInfo_Age.ToString() : "";
            }



        }


        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btntijiao_Click(object sender, ImageClickEventArgs e)
        {
            string userid = CurrUserInfo().UserID;
            modelUser = bllUser.Get(p => p.UserInfoID == userid);
            FormModel.GetForm<Sys_UserInfo>(this, modelUser, "t_");
            if (modelUser != null)
            {
                modelUser.UserInfo_LoginUserPwd = DESEncrypt.Encrypt(t_UserInfo_LoginUserPwd.Text.ToString().Trim());

            }
            //if (t_UserInfo_IdentityCred.Text != "")
            //{
            //    modelUser.UserInfo_Age = Convert.ToInt32(t_UserInfo_Age.Text);
            //    modelUser.UserInfo_DateBirth = Convert.ToDateTime(t_UserInfo_DateBirth.Text);
            //}
            if (txt_Yhxx_DTcsrq.Value != "")
            {
                modelUser.UserInfo_DateBirth = Convert.ToDateTime(Convert.ToDateTime(txt_Yhxx_DTcsrq.Value).ToString("yyyy年MM月dd日"));
            }
            if (txt_Yhxx_Iage.Value != "")
            {
                modelUser.UserInfo_Age = Convert.ToInt32(txt_Yhxx_Iage.Value);
            }
            string UserInfo_PhoneNumber = this.t_UserInfo_PhoneNumber.Text.Trim().ToString();
            //List<Sys_UserInfo> bu = bllUser.FindWhere("UserInfo_PhoneNumber='" + this.t_UserInfo_PhoneNumber.Text.Trim().ToString() + "'and UserInfoID not in('"+CurrUserInfo().UserID+"')");
            List<Sys_UserInfo> bu = bllUser.GetList(p => p.UserInfo_PhoneNumber == UserInfo_PhoneNumber && p.UserInfoID != userid).ToList();
            if (bu.Count > 0)
            {
                this.t_UserInfo_PhoneNumber.Text = "";
                MessageBox.Show(this, "手机号码已存在,请重新输入！");
            }
            else
            {
                if (bllUser.Update(modelUser))
                {
                    new Common_BLL().AddLog("系统管理>>个人信息维护", "", "修改", "修改个人信息维护", CurrUserInfo().UserID, CurrUserInfo().DepartmentCode);

                    MessageBox.ShowAndRedirect(this, "修改成功", "UserInfoModify.aspx?rn=" + new Random().Next());
                    //    //更新此人对应的岗位自查表模块表
                    //(new DAL.PositionChange()).UpdateGwfxwx(model.ManID, "1", model.Position);
                }
                else
                {
                    MessageBox.Show(this, "修改错误");
                }
            }
        }




    }
}