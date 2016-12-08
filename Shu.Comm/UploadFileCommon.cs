using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Model;
using System.Web;
namespace Shu.Comm
{
    public class UploadFileCommon
    {
        /// <summary>
        /// 创建文件路径
        /// </summary>
        /// <param name="FilePathType">在Files文件夹中自己定义的文件夹名称</param>
        public static string CreateDir(string FilePathType)
        {
            string dirByMonth = "", dirByDay = "";

            dirByMonth = DateTime.Now.ToString("yyyyMM");

            dirByDay = DateTime.Now.ToString("dd");

            string path = "/Files/" + FilePathType + "/" + dirByMonth + "/" + dirByDay + "/";

            string dir = System.Web.HttpContext.Current.Request.MapPath(path);
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(null, e.Message);

            }
            finally { }


            return path;
        }




        /// <summary>
        /// 创建文件名称
        /// </summary>
        /// <param name="extension">文件的扩展名</param>
        /// <returns></returns>
        public static string CreateFileName(string extension)
        {
            string strR = "";
            strR += DateTime.Now.ToString("HH");
            strR += DateTime.Now.ToString("mm");
            strR += DateTime.Now.ToString("sss");
            Random roo = new Random();
            strR += roo.Next(11111, 99999);

            strR += "." + extension;
            return strR;
        }

        public static string CreateFileName(string extension,string nouse)
        {
            string strR = "";
            strR += DateTime.Now.ToString("HH");
            strR += DateTime.Now.ToString("mm");
            strR += DateTime.Now.ToString("sss");
            Random roo = new Random();
            strR += roo.Next(11111, 99999);

            strR += extension;
            return strR;
        }

        public static string ChangeExtesion(string filename,string extension)
        {
            
            return filename.Substring(0, filename.LastIndexOf('.')) + extension;
          
        }

        #region 文件校验

        /// <summary>
        /// 检测用户上次文件扩展名是否正确
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CheckExtension(string name)
        {
            string extension = "jpg|gif|png|doc|docx|xls|xlsx|ppt|pptx|pdf";

            if (extension.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查文件扩展名是否正确
        /// </summary>
        /// <param name="name">文件名</param>
        /// <param name="extension">需要的扩展名 多个以 | 分割 列：（jpg|gif|png|doc|docx|xls|xlsx|ppt|pptx|pdf）</param>
        /// <returns></returns>
        public static bool CheckExtension(string name,string extension)
        {
 
            if (extension.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 校验文件大小是否符合标准（如果MaxLength为0，则调用内置的文件大小35MB进行控制。）
        /// </summary>
        /// <param name="fileLength">当前上传文件大小</param>
        /// <param name="MaxLength">设置的文件大小(MB)</param>
        /// <returns></returns>
        public static bool CheckFileLength(int fileLength,int MaxLength)
        {
            int sysLength = 0;

            if (MaxLength == 0)
            {
                sysLength = 35 * 1024 * 1024;
            }
            else
            {
                sysLength = MaxLength * 1024 * 1024;
            }

            if (fileLength > sysLength)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获得上次文件的扩展名
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public static string GetFileExtension(string fileName)
        {
            if (fileName != "")
            {
                return fileName.Substring(fileName.LastIndexOf('.') + 1);
            }
            else
            {
                return "";
            }
        }


        public static bool CheckFile(FileUpload fu, int filelength, string chkExtension, out string msg)
        {
            string fileName = Path.GetFileName(fu.PostedFile.FileName);

            if(chkExtension=="")
            {
               chkExtension = "jpg|gif|png|doc|docx|xls|xlsx|ppt|pptx|pdf";
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                //文件扩展名
                string fileExtension = UploadFileCommon.GetFileExtension(fu.PostedFile.FileName);

                //文件大小
                int fileContentLength = fu.PostedFile.ContentLength;

                if (!CheckFileLength(fileContentLength, 30))
                {
                    msg = "您上传的文件过大，请重新上传！";

                    return false;
                }

                if (!CheckExtension(fileExtension,chkExtension))
                {
                    msg = "您上传的文件类型不正确,请重新上传！";

                    return false;
                }
            }
            else
            {
                msg = "请选择上传文件！";

                return false;
            }

            msg = "";

            return true;
        }

        /// <summary>
        /// 纪检新闻中验证上传新闻图片格式
        /// </summary>
        /// <param name="fu"></param>
        /// <param name="filelength"></param>
        /// <param name="chkExtension"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool CheckNewsFile(FileUpload fu, int filelength, string chkExtension, out string msg)
        {
            string fileName = Path.GetFileName(fu.PostedFile.FileName);

            if (chkExtension == "")
            {
                chkExtension = "jpg|gif|bmp|jpeg|png";
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                //文件扩展名
                string fileExtension = UploadFileCommon.GetFileExtension(fu.PostedFile.FileName);

                //文件大小
                int fileContentLength = fu.PostedFile.ContentLength;

                if (!CheckFileLength(fileContentLength, 30))
                {
                    msg = "您上传的文件过大，请重新上传！";

                    return false;
                }

                if (!CheckExtension(fileExtension, chkExtension))
                {
                    msg = "您上传的文件类型不正确,请重新上传！";

                    return false;
                }
            }
            else
            {
                msg = "请选择上传文件！";

                return false;
            }

            msg = "";

            return true;
        }
        #endregion

        //#region 文件操作
        /// <summary>
        /// 通常是新增页面的文件上传,保存至Session中
        /// </summary>
        /// <param name="sessionId">session的ID</param>
        /// <param name="ManID">上传人的ID</param>
        /// <param name="fileFolderName">上传文件的文件夹名次</param>
        /// <param name="fu">上传控件</param>
        /// <param name="listbox">ListBox显示控件</param>
        public static void SessionAddFile(string sessionId, string ManID, string fileType,string fileFolderName, FileUpload fu, ListBox listbox,string ids)
        {
            List<ModelFile> list = null;

            //文件名称
            string fileName = Path.GetFileName(fu.PostedFile.FileName);

            //创建并且返回文件夹路径
            string filepath = UploadFileCommon.CreateDir(fileFolderName);

            //文件大小
            int fileContentLength = fu.PostedFile.ContentLength;

            //文件扩展名
            string fileExtension = fu.PostedFile.FileName.Substring(fu.PostedFile.FileName.LastIndexOf('.') + 1);

            if (HttpContext.Current.Session[sessionId] == null)
            {
                list = new List<ModelFile>();
            }
            else
            {
                list = HttpContext.Current.Session[sessionId] as List<ModelFile>;
            }

            ModelFile model = new ModelFile();

            model.File_ID = Guid.NewGuid().ToString();

            model.File_Name = fileName;

            model.File_Extension = fileExtension;

            model.File_Size = fileContentLength;

            model.File_Path = filepath + UploadFileCommon.CreateFileName(fileExtension);

            //model.File_Date = DateTime.Now;

            //model.File_CurrentMan = ManID;

            model.File_Type = fileType;

            fu.PostedFile.SaveAs(HttpContext.Current.Server.MapPath(model.File_Path));

            list.Add(model);

            HttpContext.Current.Session[sessionId] = list;

            listbox.Items.Add(new ListItem(fileName, model.File_ID));

        }

        /// <summary>
        /// 通常是新增页面的文件删除,从session删除对应的文件
        /// </summary>
        /// <param name="sessionId">session的ID</param>
        /// <param name="ManID">上传人的ID</param>
        /// <param name="fileFolderName">上传文件的文件夹名次</param>
        /// <param name="fu">上传控件</param>
        public static void SessionDeleteFile(string sessionId, string ManID, FileUpload fu, ListBox listbox)
        {
            List<ModelFile> list = null;

            if (HttpContext.Current.Session[sessionId] != null)
            {
                list = HttpContext.Current.Session[sessionId] as List<ModelFile>;
            }

            if (listbox.Items.Count < 1)
            {
                return;
            }
            for (int i = listbox.Items.Count - 1; i >= 0; i--)
            {
                if (listbox.Items[i].Selected)
                {
                    if (list != null)
                    {
                        ModelFile model = list.Find(p => p.File_ID.Equals(listbox.Items[i].Value));

                        File.Delete(HttpContext.Current.Server.MapPath(model.File_Path));

                        if (model != null)
                        {
                            list.Remove(model);
                        }

                        HttpContext.Current.Session[sessionId] = list;
                    }
                    //修改删除 删除上传的文件和数据库
                    listbox.Items.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 针对编辑页面，返回一个NewFile实体
        /// </summary>
        /// <param name="fu">file控件</param>
        /// <param name="fileFolderName">文件夹名称</param>
        /// <param name="ParentID">对应的主表ID</param>
        /// <param name="ManID">当前上传人</param>
        /// <returns></returns>
        public static ModelFile  AddFile(FileUpload fu, string fileFolderName, string ParentID, string ManID, string fileType, DateTime addtime)
        {
            ModelFile model = new ModelFile() ;
            //文件名称
            string fileName = fu.FileName;

            //创建并且返回文件夹路径
            string filepath = UploadFileCommon.CreateDir(fileFolderName);

            //文件大小
            int fileContentLength = fu.PostedFile.ContentLength;

            //文件扩展名
            string fileExtension = fu.PostedFile.FileName.Substring(fu.PostedFile.FileName.LastIndexOf('.') + 1);

            //DB.NewsFile model = new DB.NewsFile();

            model.File_ID = Guid.NewGuid().ToString();

            model.File_Name = fileName;

            model.File_Extension = fileExtension;

            model.File_Size = fileContentLength;

            model.File_Path = filepath + UploadFileCommon.CreateFileName(fileExtension);

            //model.CreateDate = DateTime.Now;

            //model.addTime = addtime;

            //model.news_id = ParentID;

            model.File_Type = fileType;

            //model.CreateUser = ManID;

            fu.PostedFile.SaveAs(HttpContext.Current.Server.MapPath(model.File_Path));

            return model;


        }

        /// <summary>
        /// 文件删除
        /// </summary>
        /// <param name="filePaht">文件路径</param>
        /// <returns></returns>
        public static bool DeleteFile(string filePaht)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(filePaht);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        //#endregion



        #region  基建项目使用

        public string CreateDirs(string FilePathType)
        {
            string dirByMonth = "", dirByDay = "";

            dirByMonth = DateTime.Now.ToString("yyyyMM");

            dirByDay = DateTime.Now.ToString("dd");

            string path = "/Files/" + FilePathType + "/" + dirByMonth + "/" + dirByDay + "/";

            string dir = System.Web.HttpContext.Current.Request.MapPath(path);
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(null, e.Message);

            }
            finally { }


            return path;
        }

        public string CreateFileNames(string extension)
        {
            string strR = "";
            strR += DateTime.Now.ToString("HH");
            strR += DateTime.Now.ToString("mm");
            strR += DateTime.Now.ToString("sss");
            Random roo = new Random();
            strR += roo.Next(11111, 99999);

            strR += "." + extension;
            return strR;
        }
        #endregion
    }
}
