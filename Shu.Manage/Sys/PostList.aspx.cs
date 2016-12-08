using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shu.BLL;
using Shu.Model;
using Shu.Comm;

namespace Shu.Manage.Sys
{
    public partial class PostList : BasePage
    {
        public Sys_PostBLL bllPost = new Sys_PostBLL();
        public Sys_UserInfoBLL bllYhxx = new Sys_UserInfoBLL();
        Sys_DepartmentBLL depdll = new Sys_DepartmentBLL();
        //BLLRisk_PostRiskLib riskBll = new BLLRisk_PostRiskLib();
        //Risk_PostRiskLibBLL bllGwfxk = new Risk_PostRiskLibBLL();
        public  string hid_DepCode="";
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.UCEasyUIDataGrid.AddClick += new UserControls.UCGrid.AddClickEventHandler(btn_AddClick);
            //this.UCEasyUIDataGrid.ModifyClick += new UserControls.UCGrid.ModifyClickEventHandler(btn_ModifyClick);
            //this.UCEasyUIDataGrid.DeleteClick += new UserControls.UCGrid.DeleteClickEventHandler(DelInfo);
            //this.UCGrid.CheckAllDeleteClick += new UserControls.UCGrid.CheckAllDeleteClickEventHandler(DelCheckBoxInfo);
            hid_DepCode = base.RequstStr("depCode") == "" ? "0" : base.RequstStr("depCode");
            Sys_Department deptModel = depdll.Get(p=>p.Department_Code==(hid_DepCode == "0" ? "001" : hid_DepCode));
            string str = hid_DepCode == "0" ? "001" : hid_DepCode;
            //List<Sys_Department> deptModel = depdll.FindWhere(p => p.Department_Code==str);
            //if (deptModel.Count>0)
            //{
            //    foreach (Sys_Department item in deptModel)
            ////    {
            //    txt_depName.Value = deptModel.Department_Name;
            //    }
                
           // }
           
                InitGrid();
        }
        //public void InitGrid()
        //{

        //    this.UCGrid.DataSource = "~/XML/Sys/GridPost.xml";

        //    this.UCGrid.InitGrid();


        //}

        //public void BindGrid()
        //{
        //    this.UCGrid.SQLWhere = GetSqlWhere();
        //    this.UCGrid.BindGrid();
        //}

        //public string GetSqlWhere()
        //{
        //    StringBuilder strWhere = new StringBuilder();
        //    strWhere.AppendFormat(" 1=1 and Post_DepCode like '" + this.hid_DepCode.Value + "%'");
        //    if (txtPost.Text.Trim() != "")
        //    {
        //        strWhere.AppendFormat(" and Post_Name like '%{0}%'", txtPost.Text.Trim());
        //    }
        //    if (txtDep.Text.Trim() != "")
        //    {
        //        strWhere.AppendFormat(" and Department_Name like '%{0}%'", txtDep.Text.Trim());
        //    }

        //    return strWhere.ToString();

        //}
        public void InitGrid()
        {

            this.UCEasyUIDataGrid.DataSource = "~/XML/Sys/GridPost.xml";
            this.UCEasyUIDataGrid.SQLWhere = GetSqlWhere();
            this.UCEasyUIDataGrid.EditType = 3;
            this.UCEasyUIDataGrid.AddType = 3;
            GetList();
        }

        public void GetList()
        {
            string active = HttpContext.Current.Request["active"];
            string id = HttpContext.Current.Request["id"];
            string Result = "";
            switch (active)
            {
                case "List"://加载列表
                    Response.Write(UCEasyUIDataGrid.jsonPerson());
                    Response.End();
                    break;
                case "DelButton"://删除数据
                    Result = UCEasyUIDataGrid.DelInfo(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                case "DeltBatchButton"://批量删除
                    Result = UCEasyUIDataGrid.BatchDelInfo(id);
                    Response.Write(Result.ToString());
                    Response.End();
                    break;
                default:
                    break;
            }
        }

        public string GetSqlWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1  and Post_DepCode like '" + hid_DepCode + "%' ");
            if (!string.IsNullOrEmpty(Request["txtDepName"]))
            {
                strWhere.AppendFormat(" and Department_Name like '%{0}%'", Request["txtDepName"]);
            }

            if (!string.IsNullOrEmpty(Request["txtPost"]))
            {
                strWhere.AppendFormat(" and Post_Name like '%{0}%'", Request["txtPost"]);
            }
            return strWhere.ToString();

        }




        ///// <summary>
        ///// 添加
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="e"></param>
        //protected void btn_AddClick(object obj, EventArgs e)
        //{
        //    hid_Type.Value = "add";
        //    txtGwName.Text = "";
        //}

        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="id"></param>
        //protected void btn_ModifyClick(object obj, string id)
        //{
        //    hid_Type.Value = "modify";
        //    hid_GwValue.Value = id;

        //    var plist = bllPost.FindWhere(p => p.PostID == id);

        //    if(plist.Count>0)
        //    {
        //        Sys_Post modelGwxx = plist[0];
        //        if (modelGwxx != null)
        //        {
                    
        //            //this.txt_depName.Text = depdll.FindByCode(modelGwxx.Post_DepCode).Department_Name;

        //            string str = (modelGwxx.Post_DepCode);
        //            List<Sys_Department> list = depdll.FindWhere(p => p.Department_Code == str);
        //            foreach (Sys_Department item in list)
        //            {
        //                this.txt_depName.Text = item.Department_Name;
        //            }

        //            this.hid_DepCode.Value = modelGwxx.Post_DepCode;
        //            txtGwName.Text = modelGwxx.Post_Name;
        //            txtpx.Text = modelGwxx.Post_Seqencing.ToString();
        //        }

        //    }
        //    else
        //    {
        //        MessageBox.ShowAndRedirect(this, "该数据不存在！",  "PostList.aspx?depCode=" + this.hid_DepCode.Value + "");
        //    }
        //}

        ///// <summary>
        ///// 保存
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="e"></param>
        //protected void btnSave_Click(object obj, EventArgs e)
        //{

        //    Sys_Post modelGwxx = new Sys_Post();
        //    string msg = string.Empty;
        //    if (hid_Type.Value == "add")
        //    {
        //        modelGwxx.PostID = Guid.NewGuid().ToString();
        //        modelGwxx.Post_AddUserID = base.CurrUserInfo().UserID;
        //        modelGwxx.Post_AddTime = DateTime.Now;
        //        modelGwxx.Post_Name = txtGwName.Text.Trim();
        //        modelGwxx.Post_Seqencing = int.Parse(txtpx.Text.Trim());
        //        modelGwxx.Post_DepCode = this.hid_DepCode.Value;
        //        if (bllPost.Add(modelGwxx,out msg))
        //        {
        //            //Risk_PostRiskLib modelGwfxk = new Risk_PostRiskLib();
        //            //modelGwfxk.PostRiskLibID = Guid.NewGuid().ToString();
        //            //modelGwfxk.PostRiskLib_EvaluateLevel = "";
        //            //modelGwfxk.PostRiskLib_Score = 0;
        //            //modelGwfxk.PostRiskLib_AddUserId = base.CurrUserInfo().UserID;
        //            //modelGwfxk.PostRiskLib_AddTime = DateTime.Now;
        //            //modelGwfxk.PostRiskLib_PostID = modelGwxx.PostID;
        //            //bllGwfxk.Add(modelGwfxk,out msg);
        //            MessageBox.Show(this, "添加成功");
        //        }
        //        else
        //        {
        //            MessageBox.Show(this, msg);
        //        }
        //    }
        //    else
        //    {
        //        //string ids = " PostID ='" + hid_GwValue.Value + "'";
        //        //modelGwxx = bllPost.FindWhere(ids)[0];
        //        modelGwxx = bllPost.FindWhere(p=>p.PostID==hid_GwValue.Value)[0];
        //        //modelGwxx.Post_DepCode = this.hid_DepCode.Value;
        //        modelGwxx.Post_Name = txtGwName.Text.Trim();
        //        modelGwxx.Post_Seqencing = int.Parse(txtpx.Text.Trim());
        //        if (bllPost.Update(modelGwxx,out msg))
        //        {

        //            ////var lis = bllGwfxk.FindWhere(" PostRiskLib_PostID='" + modelGwxx.PostID + "'");
        //            //var lis = bllGwfxk.FindWhere(p=>p.PostRiskLib_PostID==modelGwxx.PostID);
        //            //if (lis.Count == 0)
        //            //{
        //            //    Risk_PostRiskLib modelGwfxk = new Risk_PostRiskLib();
        //            //    modelGwfxk.PostRiskLibID = Guid.NewGuid().ToString();
        //            //    modelGwfxk.PostRiskLib_EvaluateLevel = "";
        //            //    modelGwfxk.PostRiskLib_Score = 0;
        //            //    modelGwfxk.PostRiskLib_AddUserId = base.CurrUserInfo().UserID;
        //            //    modelGwfxk.PostRiskLib_AddTime = DateTime.Now;
        //            //    modelGwfxk.PostRiskLib_PostID = modelGwxx.PostID;
        //            //    bllGwfxk.Add(modelGwfxk,out msg);
        //            //}


        //            MessageBox.Show(this, "修改成功");
        //        }
        //        else
        //        {
        //            MessageBox.Show(this, msg);
        //        }
        //    }
        //    hid_Type.Value = "";
        //    InitGrid();
        //}


        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //protected void DelInfo(object sender, string id)
        //{
        //    //List<View_Sys_UserInfo> ListView = bllYhxx.FindALL("UserInfo_Post like '%" + id + "%'");
        //    View_Sys_UserInfoBLL aa = new View_Sys_UserInfoBLL();
        //    List<View_Sys_UserInfo> ListView = aa.FindWhere(p=>p.UserInfo_Post.Contains(id));

        //    if (ListView.Count > 0)
        //    {
        //        MessageBox.ShowAndRedirect(this, "该岗位下存在用户，不能删除！", "PostList.aspx?depCode=" + this.hid_DepCode.Value + "");
        //    }
        //    else
        //    {
        //        //bllPost.Delete(id);
               
        //        bllPost.DeleteWhere("PostID='"+id+"'");
        //        //bllGwfxk.DeleteWhere(" PostRiskLib_PostID='" + id + "'");//岗位风险
        //        //Response.Write("<script type='text/javascript'>alert('删除成功！');location.href='PostList.aspx?depCode='"+this.hid_DepCode.Value+"''</script>");
        //        MessageBox.ShowAndRedirect(this, "删除成功！", "PostList.aspx?depCode=" + this.hid_DepCode.Value + "");
        //    }

        //    InitGrid();
        //}
        ///// <summary>
        ///// 删除多条数据
        ///// </summary>
        //protected void DelCheckBoxInfo(object sender, string id)
        //{


        //    //List<Sys_Post> listGwxx = bllPost.FindWhere("PostID in(" + id + ")");
        //    List<Sys_Post> listGwxx = bllPost.FindWhere(p=>p.PostID == id);
        //    foreach (Sys_Post model in listGwxx)
        //    {
        //        //List<View_Sys_UserInfo> listView = bllYhxx.FindViewUserList("UserInfo_Post like '%" + model.PostID + "%'");
        //        View_Sys_UserInfoBLL bllYhxx = new View_Sys_UserInfoBLL();
        //        List<View_Sys_UserInfo> listView = bllYhxx.FindWhere(p=>p.UserInfo_Post.Contains(model.PostID));
        //        if (listView.Count == 0)
        //        {
        //            //bllPost.Delete(model.PostID);
        //            bllPost.DeleteWhere(model.PostID);

        //            //bllGwfxk.DeleteWhere(" PostRiskLib_PostID='" + id + "'");  //岗位风险
        //        }
        //    }

        //    InitGrid();
        //}


    }
}