using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.BLL;
using Shu.Comm;
using Shu.Model;

namespace Shu.Manage.Files
{
    public partial class DownloadZip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetFileInfo();
            }
        }

        public void GetFileInfo()
        {
            string idsString = Request.QueryString["ids"].ToString();
            string fileName = Request.QueryString["filename"].ToString();
            string SessionID = Request.QueryString["SessionID"].ToString();

            CMMLog.Debug(string.Format("ZIP打包下载入口GetFileInfo，接收参数。idsString：{0}，fileName：{1}，SessionID：{2}", idsString, fileName,SessionID));
            Sys_ModelFileBLL bllFiles = new Sys_ModelFileBLL();
            List<Sys_ModelFile> newsfiles = bllFiles.GetList(p => idsString.Contains(p.FileID)).ToList();
            CMMLog.Debug(string.Format("ZIP打包下载入口GetFileInfo，检索文件数。count：{0}", newsfiles.Count));
            if(!string.IsNullOrEmpty(SessionID))
            {
                List<Sys_ModelFile> list = null;
                if (HttpContext.Current.Session[SessionID] != null)
                {
                    list = HttpContext.Current.Session[SessionID] as List<Sys_ModelFile>;

                    for (int i = 0; i < list.Count; i++)
                    {
                        newsfiles.Add(list[i]);
                    }
                }
            }

            string path = Server.MapPath("/");
            if (newsfiles.Count > 0)
            {
                string curName = path + "Files/ZipTemp/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";
                ZipHelper.CreateZipFiles(newsfiles,path, curName, 5, 50);
                LoadFile(curName, fileName);
            }
        }

        public void LoadFile(string filepath, string filename)
        {
            if (!File.Exists(filepath))
            {
                Response.Write("下载失败: 未找到您要下载的文件,该文件可能已经被删除！");
                Response.End();
                return;
            }
            try
            {
                Response.ContentType = "application/x-zip-compressed";
               // Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ".zip");
               

                HttpBrowserCapabilities bc = new HttpBrowserCapabilities();
                bc = Request.Browser;
                string types = bc.Type;

                if (types.Contains("Firefox"))
                {

                    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".zip");
                }
                else
                {

                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename) + ".zip");
                }
    
                Response.TransmitFile(filepath);
            }
            catch
            {
                Response.Write("下载失败: 未找到您要下载的文件,该文件可能已经被删除！");
            }
            finally
            {
                Response.End();
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }
        }
    }
}