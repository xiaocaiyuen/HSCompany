using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;

namespace YDT.Web.Manage.Sys
{
    public partial class TemplateImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = Request.Files["File1"];
            string temp = Request.QueryString["temp"];
            try
            {
                if (postedFile != null)
                {
                    if (postedFile.FileName != "")
                    {
                        string fileExtension = System.IO.Path.GetExtension(postedFile.FileName).ToLower();
                        if (fileExtension == ".frx")
                        {
                            //获取保存文件夹路径
                            string tempPath = "/Print/Template/";
                            //上传后的文件名字
                            string fname = tempPath + temp;
                            File.Delete(HttpContext.Current.Server.MapPath(fname));
                            //上传文件
                            postedFile.SaveAs(Server.MapPath(fname));

                            MessageBox.ResponseScript(this, "alert('上传成功！');col();");
                        }
                        else
                        {
                            MessageBox.Show(this, "模板格式错误！");
                        }
                        
                        
                    }
                    else
                    {
                        MessageBox.Show(this, "上传失败！");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}