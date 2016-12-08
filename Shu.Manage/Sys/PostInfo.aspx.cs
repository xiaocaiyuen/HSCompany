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
    public partial class PostInfo : System.Web.UI.Page//BasePage
    {
        string id = "";
        string hid_DepCode = "";
        //Sys_PostBLL bllPost = new Sys_PostBLL();
        Sys_Post modelGwxx = new Sys_Post();
        //Sys_DepartmentBLL depbll = new Sys_DepartmentBLL();
        Sys_Department depmodel = new Sys_Department();
        protected void Page_Load(object sender, EventArgs e)
        {
             id = Request["id"];
             hid_DepCode = Request["depCode"];
             if (!IsPostBack)
             {
                 show();
             }
        }

        /// <summary>
        /// /
        /// </summary>
        public void show() 
        {
            Sys_DepartmentBLL depbll = new Sys_DepartmentBLL();
            if (!string.IsNullOrEmpty(id))
            {
                Sys_PostBLL bllPost = new Sys_PostBLL();
                
                modelGwxx = bllPost.Get(p => p.PostID == id);
                if (modelGwxx != null)
                {
                    this.txtGwName.Text = modelGwxx.Post_Name;
                    this.txtpx.Text = modelGwxx.Post_Sort.ToString();
                    depmodel = depbll.Get(p => p.Department_Code == modelGwxx.Post_DepCode);
                    if (depmodel != null)
                    {
                        this.txt_depName.Text = depmodel.Department_Name;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(hid_DepCode))
                {
                    depmodel = depbll.Get(p => p.Department_Code == hid_DepCode);
                    if (depmodel != null)
                    {
                        this.txt_depName.Text = depmodel.Department_Name;
                    }
                }
                else
                {
                    depmodel = depbll.Get(p => p.Department_Code == "001");
                    if (depmodel != null)
                    {
                        this.txt_depName.Text = depmodel.Department_Name;
                    }
                }
            }
        }

        /// 保存
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object obj, EventArgs e)
        {
            Sys_PostBLL bllPost = new Sys_PostBLL();
            if (string.IsNullOrEmpty(id))
            {
                modelGwxx.PostID = Guid.NewGuid().ToString();
                modelGwxx.Post_AddUserId = "1";// base.CurrUserInfo().UserID;
                modelGwxx.Post_AddTime = DateTime.Now;
                modelGwxx.Post_Name = txtGwName.Text.Trim();
                modelGwxx.Post_Sort = int.Parse(txtpx.Text.Trim());
                modelGwxx.Post_DepCode = hid_DepCode;
                modelGwxx.Post_IsDelete = false;
                modelGwxx.Post_EditTime = DateTime.Now;
                modelGwxx.Post_EditUserId = "1";//base.CurrUserInfo().UserID;
                modelGwxx.Post_Sort = 0;
                bool IsPost = bllPost.Add(modelGwxx);
                if (IsPost)
                {
                    //Risk_PostRiskLib modelGwfxk = new Risk_PostRiskLib();
                    //modelGwfxk.PostRiskLibID = Guid.NewGuid().ToString();
                    //modelGwfxk.PostRiskLib_EvaluateLevel = "";
                    //modelGwfxk.PostRiskLib_Score = 0;
                    //modelGwfxk.PostRiskLib_AddUserId = base.CurrUserInfo().UserID;
                    //modelGwfxk.PostRiskLib_AddTime = DateTime.Now;
                    //modelGwfxk.PostRiskLib_PostID = modelGwxx.PostID;
                    //bllGwfxk.Add(modelGwfxk, out msg);

                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('添加成功！');coed('1')", true);
                   // MessageBox.Show(this, "添加成功");


                }
                else
                {
                    MessageBox.Show(this, "保存出错");
                }
            }
            else
            {
                //string ids = " PostID ='" + hid_GwValue.Value + "'";
                //modelGwxx = bllPost.FindWhere(ids)[0];
                modelGwxx = bllPost.Get(p => p.PostID ==id);
                //modelGwxx.Post_DepCode = this.hid_DepCode.Value;
                modelGwxx.Post_Name = txtGwName.Text.Trim();
                modelGwxx.Post_Sort = int.Parse(txtpx.Text.Trim());
                if (bllPost.Update(modelGwxx))
                {

                    ////var lis = bllGwfxk.FindWhere(" PostRiskLib_PostID='" + modelGwxx.PostID + "'");
                    //var lis = bllGwfxk.FindWhere(p => p.PostRiskLib_PostID == modelGwxx.PostID);
                    //if (lis.Count == 0)
                    //{
                    //    Risk_PostRiskLib modelGwfxk = new Risk_PostRiskLib();
                    //    modelGwfxk.PostRiskLibID = Guid.NewGuid().ToString();
                    //    modelGwfxk.PostRiskLib_EvaluateLevel = "";
                    //    modelGwfxk.PostRiskLib_Score = 0;
                    //    modelGwfxk.PostRiskLib_AddUserId = base.CurrUserInfo().UserID;
                    //    modelGwfxk.PostRiskLib_AddTime = DateTime.Now;
                    //    modelGwfxk.PostRiskLib_PostID = modelGwxx.PostID;
                    //    bllGwfxk.Add(modelGwfxk, out msg);
                    //}

                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('修改成功！');coed('2')", true);
                    //MessageBox.Show(this, "添加成功");

                }
                else
                {
                    MessageBox.Show(this,"保存出错");
                }
            }
        }
    }
}