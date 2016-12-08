using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Shu.Model;
using System.Net;

namespace Shu.Manage.Files
{
    public partial class Download : System.Web.UI.Page
    {

        ModelFile dal = new ModelFile();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetFileInfo();
            }
        }

        /// <summary>
        /// 获取需要下载的文件
        /// </summary>
        public void GetFileInfo()
        {
            //////判断是否存在ID，如果存在获取NewsFile数据库文件路径
            //if (Request.QueryString["id"] != null)
            //{

            //    string fileId = Request.QueryString["id"].ToString();
            //    string type = Request.QueryString["type"];
            //    //后期添加廉政课堂所用到的Type类型
            //    if (type == "sys")
            //    {
            //        ModelFile sysdal = new ModelFile();
            //        Sys_Files file = sysdal.FindByID(fileId);
            //        Response.Clear();

            //        if (file != null)
            //        {
            //            DownloadFile(Page.Request, Page.Response, file.File_Path, file.File_Name, file.File_Extension);
            //        }
            //    }
            //    else
            //    {
            //        DALFiles sysdal = new DALFiles();
            //        Sys_Files file = sysdal.FindByID(fileId);
            //        Response.Clear();

            //        if (file != null)
            //        {
            //            DownloadFile(Page.Request, Page.Response, file.File_Path, file.File_Name, file.File_Extension);
            //        }
            //    }



            //}
            //else
            //{

            string path = Request.QueryString["path"].ToString();
            string filename = Server.UrlDecode(Request.QueryString["filename"].ToString());
            filename = filename.Replace(" ","");

            Response.Clear();

            if (path != "")
            {
                DownloadFile(Page.Request, Page.Response, filename, path);
            }
            //}
        }

        /// <summary>
        /// 文件下载方法
        /// </summary>
        /// <param name="_Request"></param>
        /// <param name="_Response"></param>
        /// <param name="_fullPath"></param>
        /// <param name="_fileName"></param>
        /// <param name="_speed"></param>
        /// <returns></returns>
        public void DownloadFile(HttpRequest Request, HttpResponse Response, string _fullPath, string filename, string exam)
        {
            //_fullPath += filename;
            LoadFileTO(Request, Response, _fullPath, filename, exam);
        }

        /// <summary>
        /// 文件下载方法
        /// </summary>
        /// <param name="_Request"></param>
        /// <param name="_Response"></param>
        /// <param name="_fullPath"></param>
        /// <returns></returns>
        public void DownloadFile(HttpRequest Request, HttpResponse Response, string filename, string _fullPath)
        {
            //filename=" + System.Web.HttpUtility.UrlEncode("文件名", System.Text.Encoding.UTF8)
            string fileName = _fullPath.Substring(_fullPath.LastIndexOf('/')); //文件名称   

            string exam = fileName.Substring(fileName.LastIndexOf('.'));//文件扩展名

            LoadFileTO(Request, Response, _fullPath, filename, exam);
        }


        public void LoadFile(HttpRequest Request, HttpResponse Response, string _fullPath, string exam)
        {

            string filepath = HttpContext.Current.Server.MapPath(_fullPath); //待下载的文件路径

            if (!File.Exists(filepath))
            {
                Response.Write("下载失败: 未找到您要下载的文件,该文件可能已经被删除！");

                Response.End();

                return;
            }

            System.IO.Stream iStream = null;

            byte[] buffer = new Byte[10240];

            int length;

            long dataToRead;



            try
            {
                //string filename = FileHelper.Decrypt(Request["fn"]); //通过解密得到文件名



                iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);



                dataToRead = iStream.Length;

                long p = 0;
                if (Request.Headers["Range"] != null)
                {
                    Response.StatusCode = 206;

                    p = long.Parse(Request.Headers["Range"].Replace("bytes=", "").Replace("-", ""));
                }
                if (p != 0)
                {
                    Response.AddHeader("Content-Range", "bytes " + p.ToString() + "-" + ((long)(dataToRead - 1)).ToString() + "/" + dataToRead.ToString());
                }

                Response.AddHeader("Content-Length", ((long)(dataToRead - p)).ToString());

                Response.ContentType = GetContentType(exam);

                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(System.Text.Encoding.GetEncoding(65001).GetBytes(Path.GetFileName(_fullPath))));

                iStream.Position = p;

                dataToRead = dataToRead - p;

                while (dataToRead > 0)
                {
                    if (Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10240);

                        Response.OutputStream.Write(buffer, 0, length);
                        Response.Flush();

                        buffer = new Byte[10240];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("下载失败: 未找到您要下载的文件,该文件可能已经被删除！");
            }
            finally
            {
                if (iStream != null)
                {
                    iStream.Close();
                }
                Response.End();
            }
        }

        public void LoadFileTO(HttpRequest Request, HttpResponse Response, string _fullPath, string filename, string exam)
        {

            string filepath = HttpContext.Current.Server.MapPath(_fullPath); //待下载的文件路径

            if (!File.Exists(filepath))
            {
                #region  网页地址先保存到服务器，再下载，存放位置同下载压缩文件

                //网页文件存放服务器地址
                string savetopath = string.Empty;
                //文件名称
                string name = string.Empty;
                try
                {   //下载到服务器
                    string url =  _fullPath.Substring(0, 5).ToLower() == "http:" ? _fullPath : "http:" + _fullPath;
                    name = _fullPath.Substring(_fullPath.LastIndexOf('/') + 1); 
                    savetopath = Server.MapPath("/") + "Files\\ZipTemp\\" + name;
                    WebClient mywebclient = new WebClient();
                    mywebclient.DownloadFile(url, savetopath);
                }
                catch
                {
                    //服务器地址文件不存在的，或者网页下载出现异常的
                    Response.Write("下载失败: 未找到您要下载的文件,该文件可能已经被删除！");
                    Response.End();
                    return;
                }

                if (!File.Exists(savetopath))
                {
                    Response.Write("下载失败: 未找到您要下载的文件,该文件可能已经被删除！");
                    Response.End();
                    return;
                }
                else
                {
                    filepath = savetopath;
                    filename = name;
                }

               #endregion
            }

            try
            {
                Response.ContentType = GetContentType(exam);
                HttpBrowserCapabilities bc = new HttpBrowserCapabilities();
                bc = Request.Browser;
                string types = bc.Type;

                if (types.Contains("Firefox"))
                {
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                }
                else
                {
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
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
            }
        }


        private string GetContentType(string exam)
        {
            string type = "";

            switch (exam)
            {
                case "avi":
                    type = "video/avi";
                    break;

                case "bmp":
                    type = "application/x-bmp";
                    break;

                case "doc":
                    type = "application/msword";
                    break;

                case "docx":
                    type = "application/msword";
                    break;

                case "dot":
                    type = "application/msword";
                    break;

                case "xls":

                    type = "application/ms-excel";
                    break;

                case "xlsx":

                    type = "application/ms-excel";
                    break;

                case "ppt":

                    type = "application/x-ppt";
                    break;


                case "exe":
                    type = "application/x-msdownload";
                    break;

                case "gif":
                    type = "image/gif";
                    break;

                case "ico":
                    type = "image/x-icon";
                    break;

                case "jpe":
                    type = "image/jpeg";
                    break;

                case "jpeg":
                    type = "image/jpeg";
                    break;

                case "jpg":
                    type = "image/jpeg";
                    break;

                case "png":
                    type = "image/png";
                    break;

                case "movie":
                    type = "video/x-sgi-movie";
                    break;

                case "mpa":
                    type = "video/x-mpg";
                    break;

                case "mp4":
                    type = "video/mpeg4";
                    break;

                case "mp3":
                    type = "audio/mp3";
                    break;

                case "mpe":
                    type = "video/x-mpeg";
                    break;

                case "mpeg":
                    type = "video/mpg";
                    break;

                case "mpg":
                    type = "video/mpg";
                    break;

                case "rm":
                    type = "video/mpg";
                    break;

                case "rmvb":
                    type = "application/vnd.rn-realmedia-vbr";
                    break;

                case "swf":
                    type = "application/x-shockwave-flash";
                    break;

                case "pdf":
                    type = "application/pdf";
                    break;

                default:
                    type = "application/octet-stream";
                    break;

            }

            return type;
        }
    }
}