using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.BLL;
using YDT.Model;
using YDT.Comm;

namespace YDT.Web.Manage.Sys
{
    public partial class Sys_Setting : System.Web.UI.Page
    {
        Sys_SettingBLL setBll = new Sys_SettingBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInfo();
            }
        }
        public void LoadInfo()
        {
            List<YDT.Model.Sys_Setting> list = setBll.FindALL();
            foreach (YDT.Model.Sys_Setting item in list)
            {
                if (item.Setting_Key == "SendWarn")//预警格式
                {
                    //lblSetting_Key.Text = item.Setting_Key;
                    ddlSetting_Value.SelectedValue = item.Setting_Value;
                    lblSetting_Name.Text = item.Setting_Name;
                    txtSetting_Remarks.Text = item.Setting_Remarks;
                }
                else if (item.Setting_Key == "MsgFormat")//执法短信回访格式
                {
                    //lblSetting_Key2.Text = item.Setting_Key;
                    lblSetting_Name2.Text = item.Setting_Name;
                    txtSetting_Remarks2.Text=item.Setting_Remarks;
                }
            }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
              string msgs = string.Empty;
              List<YDT.Model.Sys_Setting> list = setBll.FindALL();
              foreach (YDT.Model.Sys_Setting item in list)
              {

                if (item.Setting_Key == "SendWarn")//预警格式
                {
                    item.Setting_Value = ddlSetting_Value.Text;
                    string users = txtSetting_Remarks.Text;
                    if (!users.Contains("[user]"))
                    {
                        MessageBox.Show(this, "未包含编码：[user]，此编码将会被系统替换成短信接收对象的人名，所以必须包含。");
                    }
                    else if (!users.Contains("[warn]"))
                    {
                        MessageBox.Show(this, "未包含编码：[warn]，此编码将会被系统替换成短信接收对象的预警事由，所以必须包含。");
                    }
                    else
                    {
                        item.Setting_Remarks = txtSetting_Remarks.Text;
                    }
                }
                else if (item.Setting_Key == "MsgFormat")//执法短信回访格式
                {
                    string users= txtSetting_Remarks2.Text;
                    if (!users.Contains("{0}"))
                    {
                        MessageBox.Show(this, "未包含编码：{0}，此编码将会被系统替换为服务对象的姓名，所以必须包含。");
                    }
                    else if (!users.Contains("{1}"))
                    {
                        MessageBox.Show(this, "未包含编码：{1}，此编码将会被系统替换为服务事项的名称，所以必须包含。");
                    }
                    else
                    {
                        item.Setting_Remarks = txtSetting_Remarks2.Text;
                    }
                    bool bol = setBll.Update(item, out msgs);
                    if (bol == true && users.Contains("{0}") && users.Contains("{1}") && txtSetting_Remarks.Text.Contains("[user]") && txtSetting_Remarks.Text.Contains("[warn]"))
                    {
                        MessageBox.ShowAndRedirect(this, "保存成功", "Sys_Setting.aspx");
                    }
                }
              
              
            }
        }
    }
}