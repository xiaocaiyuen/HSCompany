using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using System.IO;
using System.Data;
using YDT.BLL;
using YDT.Model;

namespace YDT.Web.Manage.Sys
{
    public partial class UserInfoImport : BasePage
    {
        Sys_UserInfoBLL userbll = new Sys_UserInfoBLL();
        Sys_PostBLL postbll = new Sys_PostBLL();
        Sys_DataDictBLL DataDictbll = new Sys_DataDictBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

            HttpPostedFile postedFile =Request.Files["File1"];
            string depCode = Request["depCode"];
            if (string.IsNullOrEmpty(depCode)) { depCode = "001"; }
            try
            {
                if (postedFile.FileName != "")
                {
                    string tempPath = UploadFileCommon.CreateDir("Uplod");//获取保存文件夹路径。
                    string savepath = Server.MapPath(tempPath);//获取保存路径
                    string fileExtension = System.IO.Path.GetExtension(postedFile.FileName).ToLower();
                    if (!Directory.Exists(savepath))//查看当前文件夹是否存在
                    {
                        Directory.CreateDirectory(savepath);
                    }
                    string sNewFileName = DateTime.Now.ToString("yyyyMMddhhmmsfff");//上传后的文件名字
                    string fname = tempPath + sNewFileName + fileExtension;
                    string allowedExtensions = ".xlsx|.xls";
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('请选择Excel文件！');col()", true);
                        MessageBox.Show(this, "请选择Excel文件");
                        return;
                    }
                    postedFile.SaveAs(Server.MapPath(fname));
                    string filename = Server.MapPath(fname);
                    ImportExcl excl = new ImportExcl();
                    DataTable dt = excl.ExcelToDataSet(filename);
                    if (dt != null)
                    {
                        int a = dt.Rows.Count;
                        List<Sys_UserInfo> listUsermodel = new List<Sys_UserInfo>();
                        List<Sys_Post> listpostmodel = new List<Sys_Post>();
                        for (int i = 0; i < a; i++)
                        {
                            Sys_UserInfo sysmodel = new Sys_UserInfo();
                            sysmodel.UserInfoID = Guid.NewGuid().ToString();
                            sysmodel.UserInfo_FullName = dt.Rows[i][1].ToString();
                            string Post_Name = dt.Rows[i][2].ToString();
                            Sys_Post postmodel = postbll.Find(p => p.Post_Name == Post_Name && p.Post_DepCode == depCode);
                            Sys_Post post = new Sys_Post();
                            if (postmodel == null)
                            {
                                post.PostID = Guid.NewGuid().ToString();
                                post.Post_Name = dt.Rows[i][2].ToString();
                                post.Post_DepCode = depCode;
                                post.Post_AddUserID = CurrUserInfo().UserID;
                                post.Post_AddTime = DateTime.Now;
                                sysmodel.UserInfo_Post = post.PostID;
                                listpostmodel.Add(post);
                            }
                            else
                            {
                                sysmodel.UserInfo_Post = postmodel.PostID;
                            }
                            sysmodel.UserInfo_Position = dt.Rows[i][3].ToString();
                            sysmodel.UserInfo_PhoneNumber = dt.Rows[i][4].ToString();
                            sysmodel.UserInfo_PositionLevel = dt.Rows[i][5].ToString();
                            string DataDict_Name = dt.Rows[i][6].ToString();
                            Sys_DataDict DataDictmodel = DataDictbll.Find(p => p.DataDict_Name == DataDict_Name);
                            if (DataDictmodel != null)
                            {
                                sysmodel.UserInfo_Type = DataDictmodel.DataDict_Code;

                            }
                            else { sysmodel.UserInfo_Type = "0403"; }
                            sysmodel.UserInfo_LoginUserName = dt.Rows[i][1].ToString();
                            sysmodel.UserInfo_LoginUserPwd = DESEncrypt.Encrypt("111111");
                            sysmodel.UserInfo_DepCode = depCode;
                            sysmodel.UserInfo_RoleID = "02f13817-9f74-4169-b279-4b00cc741a91";
                            sysmodel.UserInfo_Status = "0301";
                            sysmodel.UserInfo_Sequence = int.Parse(dt.Rows[i][0].ToString());
                            listUsermodel.Add(sysmodel);
                        }
                        bool u = userbll.AddLists(listpostmodel, listUsermodel);
                        if (u)
                        {
                            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('导入成功！');col()", true);
                        }
                        else
                        {

                            MessageBox.Show(this, "导入失败！");
                        }

                    }
                }
                else
                {
                    MessageBox.Show(this, "导入失败！");
                }
            }
            catch {  }
        }
    }
}