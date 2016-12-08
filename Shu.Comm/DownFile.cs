using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace Shu.Comm
{
    public class DownFile
    {
        public void DownloadFile(HttpRequest Request, HttpResponse Response, byte[] buffer, string filename)
        {
            string _fullPath = string.Format("~/Templates/{0}.docx", Guid.NewGuid().ToString());

            System.IO.Stream iStream = null;

            //byte[] buffer = new Byte[10240];

            int length;

            long dataToRead;
            string filepath = HttpContext.Current.Server.MapPath(_fullPath); //待下载的文件路径
            try
            {
                //string filename = FileHelper.Decrypt(Request["fn"]); //通过解密得到文件名

                // string filepath = HttpContext.Current.Server.MapPath(_fullPath); //待下载的文件路径

                // iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.Read);
                iStream = new System.IO.FileStream(filepath, System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                iStream.Write(buffer, 0, buffer.Length);

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

                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(filename));

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
                Response.Write("下载失败: 未找到您要下载的文件！该文件可能已经被删除,请关闭页面重新浏览！");
            }
            finally
            {
                if (iStream != null)
                {
                    iStream.Close();
                    FileDelete(filepath);
                }

                Response.End();
            }
        }


        public void FileDelete(string path)
        {

            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }

    }
}
