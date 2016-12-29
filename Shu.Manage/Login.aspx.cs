using Shu.BLL;
using Shu.Comm;
using Shu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shu.Manage
{
    public partial class Login : BasePage
    {
        public View_Sys_UserInfoBLL bllUserInfo = new View_Sys_UserInfoBLL();
        public Sys_UserInfoBLL bllUser = new Sys_UserInfoBLL();
        public Sys_SettingBLL bllSetting = new Sys_SettingBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] != null)
            {
                Response.Redirect("Manage/Main.aspx");
            }

            if (!IsPostBack)
            {

                //读取保存的Cookie信息
                HttpCookie cookies = Request.Cookies["USER_COOKIE"];
                if (cookies != null)
                {
                    //如果Cookie不为空，则将Cookie里面的用户名和密码读取出来赋值给前台的文本框。
                    this.txtUser.Text = cookies["UserName"];
                    this.txtPwd.Attributes.Add("value", cookies["UserPassword"]);
                    //这里依然把记住密码的选项给选中。
                    this.ckbRememberLogin.Checked = true;
                }
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnDl_Click(object sender, EventArgs e)
        {
            string UserName = txtUser.Text;
            string VerifyCode = Session["code"] == null ? "" : Session["code"].ToString();
            string Password = HttpUtility.UrlDecode(DESEncrypt.Encrypt(txtPwd.Text));
            string IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; 

            List<View_Sys_UserInfo> userInfoList = bllUserInfo.GetList(p => p.UserInfo_LoginUserName == UserName && p.UserInfo_Status == "0301").ToList();
            // and UserInfo_Status='0301' and UserInfo_LoginUserPwd='" + Password + "'"));
            if (userInfoList.Count > 0)
            {
                if (userInfoList[0].UserInfo_WorkingState == "2")
                {
                    MessageBox.Show(this, "你的账号已经锁定，请联系管理员！");
                }
                else
                {
                    if (userInfoList[0].UserInfo_LoginUserPwd == Password)
                    {
                        HttpCookie cookie = new HttpCookie("USER_COOKIE");
                        if (this.ckbRememberLogin.Checked)
                        {
                            //所有的验证信息检测之后，如果用户选择的记住密码，则将用户名和密码写入Cookie里面保存起来。
                            cookie.Values.Add("UserName", this.txtUser.Text.Trim());
                            cookie.Values.Add("UserPassword", this.txtPwd.Text.Trim());
                            //这里是设置Cookie的过期时间，这里设置一个星期的时间，过了一个星期之后状态保持自动清空。
                            cookie.Expires = System.DateTime.Now.AddDays(7.0);
                            HttpContext.Current.Response.Cookies.Add(cookie);
                        }
                        else
                        {
                            if (cookie["USER_COOKIE"] != null)
                            {
                                //如果用户没有选择记住密码，那么立即将Cookie里面的信息情况，并且设置状态保持立即过期。
                                Response.Cookies["USER_COOKIE"].Expires = DateTime.Now;
                            }
                        }
                        SessionUserModel model = new SessionUserModel();
                        model.UserID = userInfoList[0].UserInfoID;
                        model.UserName = userInfoList[0].UserInfo_FullName;
                        model.LoginUserName = userInfoList[0].UserInfo_LoginUserName;
                        model.DepartmentName = userInfoList[0].Department_Name;
                        model.DepartmentCode = userInfoList[0].UserInfo_DepCode;
                        model.PostID = userInfoList[0].UserInfo_Post;
                        model.PostName = userInfoList[0].UserInfo_PostName;
                        model.RoleID = userInfoList[0].UserInfo_RoleID;
                        model.RoleName = userInfoList[0].UserInfo_RoleName;
                        model.UserType = userInfoList[0].UserInfo_Type;
                        model.DepType = userInfoList[0].Department_Type;
                        if (!string.IsNullOrEmpty(model.RoleID))
                        {
                            //Sys_Role role = new Sys_RoleBLL().Get(p => p.RoleID == model.RoleID);
                            //if (role != null)
                            //{
                            //    if (role.Role_ShowType != null)
                            //    {
                            //        model.RoleShowType = role.Role_ShowType.ToString();
                            //    }
                            //}

                            //Sys_RoleDataMapping RoleDataMapping = new Sys_RoleDataMappingBLL().Find(p => p.RoleDataMapping_RoleID == model.RoleID);
                            //if (RoleDataMapping != null)
                            //{
                            //    model.RoleDataMapping_Name = RoleDataMapping.RoleDataMapping_Name;
                            //}
                        }
                        if (Session["UserInfo"] != null)
                        {
                            Session.Remove("UserInfo");
                        }
                        Session["UserInfo"] = model;
                        //添加系统日志
                        new Common_BLL().AddLog("登录系统", "0", "登录", "用户通过登录页面登录系统", userInfoList[0].UserInfoID, userInfoList[0].UserInfo_DepCode);
                        Response.Redirect("Main.aspx");
                    }
                    else
                    {
                        new Common_BLL().AddLog("登录系统", "0", "登录失败", "用户通过登录页面登录系统", userInfoList[0].UserInfoID, userInfoList[0].UserInfo_DepCode);
                        MessageBox.Show(this, "您输入的用户名或密码有错误，请您重新输入！");
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "您输入的用户名或密码有错误，请您重新输入！");
            }
            Session.Remove("UserInfo");
        }
    }
}