using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using Shu.Model;
using System.Web.SessionState;
using Shu.BLL;
using System.Text.RegularExpressions;
using Shu.Comm;
using Shu.Utility.Extensions;
using System.Data;
using Shu.Utility;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class FileHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Option = context.Request.QueryString["Option"];
            switch (Option)
            {
                case "add":
                    this.FilseAdd(context);
                    break;
                case "select":
                    this.FilseSelect(context);
                    break;
                case "selecttype":
                    this.DataFilseSelect(context);
                    break;
                case "dataAdd":
                    this.DataFilseAdd(context);
                    break;
                case "dataAddAll":   //获取session中的所有Url
                    this.DataFilseAddALL(context);
                    break;
                case "Dele":
                    this.DelFilseList(context);
                    break;
                case "GuiLei":  //归类
                    this.GuiLeiFilseList(context);
                    break;
                //case "GetNumbyType":  //根据文档类别获取对应类别需要上传的文件数
                //    this.GetNumbyType(context);
                //    break;
                case "addbeijing":  //上传背景图片和logo图片
                    this.FilseAddbeijing(context);
                    break;
                case "beijing":
                    context.Response.Write(Showbeijing(context));
                    break;
                case "logo":
                    context.Response.Write(Logo(context));
                    break;
            }
        }

        /// <summary>
        /// 附件添加
        /// </summary>
        /// <param name="context"></param>
        public void FilseAdd(HttpContext context)
        {
            string OperationID = context.Request.QueryString["OperationID"];//业务ID
            string SessionID = context.Request.QueryString["SessionID"];//SessionID
            string FileType = context.Request.QueryString["FileType"];//附件的类型
            SessionUserModel currentUser = context.Session["UserInfo"] as SessionUserModel;
            string pt = context.Request["Filename"].ToString();
            HttpPostedFile postedFile = context.Request.Files["Filedata"];//获取上传信息对象
            string filename = postedFile.FileName;//获取上传的文件 名字
            string tempPath = FileType == "系统必备工具" ? "/Download/" : UploadFileCommon.CreateDir("File");//获取保存文件夹路径。

            string savepath = context.Server.MapPath(tempPath);//获取保存路径
            string sExtension = filename.Substring(filename.LastIndexOf('.'));//获取拓展名
            int fileLength = postedFile.ContentLength;//获取文件大小
            string fileContentLength = fileSizs(postedFile.ContentLength);//转换成MB，GB,TB
            if (!Directory.Exists(savepath))//查看当前文件夹是否存在
            {
                Directory.CreateDirectory(savepath);
            }
            //string sNewFileName = DESEncrypt.Encrypt(DateTime.Now.ToString("yyyyMMddhhmmsfff"));//上传后的文件名字
            string sNewFileName = Guid.NewGuid().ToString();
            if (OperationID != "" && SessionID != "" && OperationID != null)
            {
                List<Sys_ModelFile> ModelFilelist = null;
                if (context.Session[SessionID] == null)
                {
                    ModelFilelist = new List<Sys_ModelFile>();
                }
                else
                {
                    ModelFilelist = context.Session[SessionID] as List<Sys_ModelFile>;
                }

                Sys_ModelFile model = new Sys_ModelFile();

                model.FileID = Guid.NewGuid().ToString();

                model.File_Name = filename;

                model.File_Extension = sExtension;

                model.File_Size = fileLength;

                model.File_Path = tempPath + sNewFileName + sExtension;

                model.File_AddTime = DateTime.Now;

                model.File_OperationID = OperationID;
                model.File_UserID = currentUser.UserID;
                model.File_Type = FileType;
                try
                {
                    //postedFile.SaveAs(savepath + "@/" + sNewFileName + sExtension);//保存
                    postedFile.SaveAs(savepath + sNewFileName + sExtension);//保存
                    postedFile = null;
                    ModelFilelist.Add(model);
                    context.Session[SessionID] = ModelFilelist;
                    string fileUrl = "/Files/Download.aspx?path=" + model.File_Path + "&filename=" + model.File_Name;
                    context.Response.Write("{filadd:'true',id:'" + model.FileID + "',filename:'" + model.File_Name + "',filesize:'" + fileContentLength + "',uploaddate:'" + Convert.ToDateTime(model.File_AddTime).ToString("yyyy年MM月dd日") + "',fileUrl:'" + fileUrl + "',filePicUrl:'"+ model.File_Path + "'}");
                }
                catch
                {
                    context.Response.Write("{filadd:'false',filename:'" + model.File_Name + "'}");
                }
            }
            else
            {
                context.Response.Write("{filadd:'false',filename:'" + filename + "'}");//在没有业务ID时候,SessionID不能为空
            }
            //context.Response.End();
        }


        /// <summary>
        /// 背景图片添加
        /// </summary>
        /// <param name="context"></param>
        public void FilseAddbeijing(HttpContext context)
        {
            SessionUserModel currentUser = context.Session["UserInfo"] as SessionUserModel;
            string pt = context.Request["Filename"].ToString();
            HttpPostedFile postedFile = context.Request.Files["Filedata"];//获取上传信息对象
            string filename = postedFile.FileName;//获取上传的文件 名字
            string tempPath = "/Styles/login/beijing/";//获取保存文件夹路径。
            string savepath = context.Server.MapPath(tempPath);//获取保存路径
            string sExtension = filename.Substring(filename.LastIndexOf('.'));//获取拓展名
            int fileLength = postedFile.ContentLength;//获取文件大小
            string fileContentLength = fileSizs(postedFile.ContentLength);//转换成MB，GB,TB
            if (!Directory.Exists(savepath))//查看当前文件夹是否存在
            {
                Directory.CreateDirectory(savepath);
            }
            //string sNewFileName = DESEncrypt.Encrypt(DateTime.Now.ToString("yyyyMMddhhmmsfff"));//上传后的文件名字
            string sNewFileName = Guid.NewGuid().ToString();
            string OperationID = context.Request.QueryString["OperationID"];//业务ID
            string SessionID = context.Request.QueryString["SessionID"];//SessionID
            string FileType = context.Request.QueryString["FileType"];//附件的类型
            if (OperationID != "" && SessionID != "" && OperationID != null)
            {
                List<Sys_ModelFile> ModelFilelist = null;
                if (context.Session[SessionID] == null)
                {
                    ModelFilelist = new List<Sys_ModelFile>();
                }
                else
                {
                    ModelFilelist = context.Session[SessionID] as List<Sys_ModelFile>;
                }

                Sys_ModelFile model = new Sys_ModelFile();

                model.FileID = Guid.NewGuid().ToString();

                model.File_Name = filename;

                model.File_Extension = sExtension;

                model.File_Size = fileLength;

                model.File_Path = tempPath + sNewFileName + sExtension;

                model.File_AddTime = DateTime.Now;

                model.File_OperationID = OperationID;
                model.File_UserID = currentUser.UserID;
                model.File_Type = FileType;
                try
                {
                    //postedFile.SaveAs(savepath + "@/" + sNewFileName + sExtension);//保存
                    postedFile.SaveAs(savepath + sNewFileName + sExtension);//保存
                    string url = tempPath + sNewFileName + sExtension;
                    postedFile = null;
                    System.Drawing.Image pic = System.Drawing.Image.FromFile(savepath + sNewFileName + sExtension);//strFilePath是该图片的绝对路径
                    int intWidth = pic.Width;//长度像素值
                    int intHeight = pic.Height;//高度像素值 
                    if (FileType == "登陆背景图片")
                    {
                        if (intWidth == 1920 && intHeight == 1080)
                        {
                            ModelFilelist.Add(model);
                            context.Session[SessionID] = ModelFilelist;
                            string fileUrl = "/Files/Download.aspx?path=" + model.File_Path + "&filename=" + model.File_Name;
                            pic.Dispose();
                            context.Response.Write("{filadd:'true',url:'" + url + "',id:'" + model.FileID + "',filename:'" + model.File_Name + "',filesize:'" + fileContentLength + "',uploaddate:'" + Convert.ToDateTime(model.File_AddTime).ToString("yyyy年MM月dd日") + "',fileUrl:'" + fileUrl + "'}");
                        }
                        else
                        {
                            pic.Dispose();
                            File.Delete(HttpContext.Current.Server.MapPath(model.File_Path));
                            context.Response.Write("{filadd:'false',name:'2',filename:'请上传分辨率为1920*1080的图片'}");
                        }
                    }
                    else if (FileType == "主框架LOGO背景图片")
                    {
                        if (intWidth == 417 && intHeight == 50)
                        {
                            ModelFilelist.Add(model);
                            context.Session[SessionID] = ModelFilelist;
                            string fileUrl = "/Files/Download.aspx?path=" + model.File_Path + "&filename=" + model.File_Name;
                            pic.Dispose();
                            context.Response.Write("{filadd:'true',url:'" + url + "',id:'" + model.FileID + "',filename:'" + model.File_Name + "',filesize:'" + fileContentLength + "',uploaddate:'" + Convert.ToDateTime(model.File_AddTime).ToString("yyyy年MM月dd日") + "',fileUrl:'" + fileUrl + "'}");
                        }
                        else
                        {
                            pic.Dispose();
                            File.Delete(HttpContext.Current.Server.MapPath(model.File_Path));
                            context.Response.Write("{filadd:'false',name:'2',filename:'请上传分辨率为417*50的图片'}");
                        }
                    }
                   
                  
                }
                catch
                {
                    context.Response.Write("{filadd:'false',name:'1',filename:'" + model.File_Name + "'}");
                }
            }
            else
            {
                context.Response.Write("{filadd:'false',name:'1',filename:'" + filename + "'}");//在没有业务ID时候,SessionID不能为空
            }
            //context.Response.End();
        }



        /// <summary>
        /// 背景图片添加
        /// </summary>
        /// <param name="context"></param>
        public string  Showbeijing(HttpContext context)
        {
            string url = "";
            Sys_ModelFileBLL bllModelFile = new Sys_ModelFileBLL();

            List<Sys_ModelFile> fileList = bllModelFile.GetList(p => p.File_Type == "登陆背景图片" && p.File_OperationID == "denglubeijing").OrderByDescending(p => p.File_AddTime).ToList(); ;
            if (fileList.Count > 0)
            {
                 url = fileList[0].File_Path;
                 int changdu = url.Length-1;
                 url = url.Substring(1, changdu);
                
            }
            else
            {
                url = "Styles/login/img/examples/01.jpg";
            }
            return url;
        }



        /// <summary>
        /// 背景图片添加
        /// </summary>
        /// <param name="context"></param>
        public string Logo(HttpContext context)
        {
            string url = "";
            Sys_ModelFileBLL bllModelFile = new Sys_ModelFileBLL();

            List<Sys_ModelFile> fileList = bllModelFile.GetList(p => p.File_Type == "主框架LOGO背景图片" && p.File_OperationID == "zhukuangjiatupian").OrderByDescending(p => p.File_AddTime).ToList(); ;
            if (fileList.Count > 0)
            {
                url = fileList[0].File_Path;
                //int changdu = url.Length - 1;
                //url = url.Substring(1, changdu);

            }
            else
            {
                url = "/Images/main/logo.png";
            }
            return url;
        }
        public void DataFilseAdd(HttpContext context)
        {
            SessionUserModel currentUser = context.Session["UserInfo"] as SessionUserModel;
            string pt = context.Request["Filename"].ToString();
            HttpPostedFile postedFile = context.Request.Files["Filedata"];//获取上传信息对象
            string filename = postedFile.FileName;//获取上传的文件 名字
            string tempPath = UploadFileCommon.CreateDir("File");//获取保存文件夹路径。
            string savepath = context.Server.MapPath(tempPath);//获取保存路径
            string sExtension = filename.Substring(filename.LastIndexOf('.'));//获取拓展名
            int fileLength = postedFile.ContentLength;//获取文件大小
            string fileContentLength = fileSizs(postedFile.ContentLength);//转换成MB，GB,TB
            if (!Directory.Exists(savepath))//查看当前文件夹是否存在
            {
                Directory.CreateDirectory(savepath);
            }
            //string sNewFileName =DESEncrypt.Encrypt(DateTime.Now.ToString("yyyyMMddhhmmsfff"));//上传后的文件名字
            string sNewFileName = Guid.NewGuid().ToString();
            string OperationID = context.Request.QueryString["OperationID"];//业务ID
            string SessionID = context.Request.QueryString["SessionID"];//SessionID
            string FileType = context.Request.QueryString["FileType"];//附件的类型
            if (!string.IsNullOrEmpty(FileType))
            {
                FileType = "未归类";
            }
            //if (!filename.Contains("-"))
            //{
            //    FileType = context.Request.QueryString["FileType"];//附件的类型
            //    if (!string.IsNullOrEmpty(FileType))
            //    {
            //        FileType = HttpUtility.UrlDecode(FileType);
            //    }
            //}
            //else
            //{
            //    string[] fname = filename.Split('-');
            //    if (fname.Count() > 0)
            //    {
            //        FileType = fname[0];
            //    }
            //    else
            //    {
            //        FileType = context.Request.QueryString["FileType"];//附件的类型
            //        if (!string.IsNullOrEmpty(FileType))
            //        {
            //            FileType = HttpUtility.UrlDecode(FileType);
            //        }
            //    }
            //}
           
           

            //string types = context.Request.QueryString["FileType2"];//当前选中的类别节点
            //if (!string.IsNullOrEmpty(types))
            //{
            //    types = HttpUtility.UrlDecode(types);
            //}

            if (OperationID != "" && SessionID != "" && OperationID != null)
            {
                List<Sys_ModelFile> ModelFilelist = null;
                if (context.Session[SessionID] == null)
                {
                    ModelFilelist = new List<Sys_ModelFile>();
                }
                else
                {
                    ModelFilelist = context.Session[SessionID] as List<Sys_ModelFile>;
                }

                Sys_ModelFile model = new Sys_ModelFile();
                model.FileID = Guid.NewGuid().ToString();
                model.File_Name = filename;
                model.File_Extension = sExtension;
                model.File_Size = fileLength;
                model.File_Path = tempPath + sNewFileName + sExtension;
                model.File_AddTime = DateTime.Now;
                model.File_OperationID = OperationID;
                model.File_UserID = currentUser.UserID;
                model.File_Type = FileType;

                string hz = "";
                Regex r = new Regex(@"[\u4e00-\u9fa5]+");
                Match mc1 = r.Match(FileType);
                if (mc1.Length != 0)  //类别中含有汉字
                {
                    hz = "..(未归类)";
                }
                

                try
                {

                    postedFile.SaveAs(savepath + sNewFileName + sExtension);//保存
                    postedFile = null;
                    ModelFilelist.Add(model);
                    context.Session[SessionID] = ModelFilelist;
                    string filename2 = model.File_Name;
                    if (filename2.Length > 9)
                    {
                        Match mc = r.Match(filename2);
                        if (mc.Length != 0)  //名称中含有汉字
                        {
                            filename2 = filename2.Substring(0, 7) + hz;
                        }
                        else
                        {
                            filename2 = filename2.Substring(0, 9) + hz;
                        }
                    }
                    else
                    {
                        filename2 = filename2 + hz;

                    }
                    context.Response.Write("{filadd:'true',id:'" + model.FileID + "',filename:'" + model.File_Name + "',filesize:'" + fileContentLength + "',uploaddate:'" + Convert.ToDateTime(model.File_AddTime).ToString("yyyy年MM月dd日") + "',fileUrl:'" + model.File_Path + "',filetitle:'" + filename2 + "'}");
                }
                catch
                {
                    context.Response.Write("{filadd:'false',filename:'" + model.File_Name + "'}");
                }
            }
            else
            {
                context.Response.Write("{filadd:'false',filename:'" + filename + "'}");//在没有业务ID时候,SessionID不能为空
            }
            //context.Response.End();
        }
        public void DataFilseAddALL(HttpContext context)
        {

            string OperationID = context.Request.QueryString["OperationID"];//业务ID
            string SessionID = context.Request.QueryString["SessionID"];//SessionID
            string FileType = context.Request.QueryString["FileType"];//附件的类型
            if (OperationID != "" && SessionID != "" && OperationID != null)
            {
                List<Sys_ModelFile> ModelFilelist = null;
                if (context.Session[SessionID] == null)
                {
                    ModelFilelist = new List<Sys_ModelFile>();
                }
                else
                {
                    ModelFilelist = context.Session[SessionID] as List<Sys_ModelFile>;
                }
                string str = "";
                for (int i = 0; i < ModelFilelist.Count; i++)
                {
                    str += ModelFilelist[i].File_Path + "|";
                }

                if (str.Length > 0)
                {
                    str = str.Substring(0, str.Length - 1);
                }

                context.Response.Write(str);

            }

        }
        /// <summary>
        /// 附件查询
        /// </summary>
        /// <param name="context"></param>
        public void FilseSelect(HttpContext context)
        {
            string OperationID = context.Request.QueryString["OperationID"];//业务ID
            string FileType = context.Request.QueryString["FileType"];//附件的类型
            string str = "";
            if (!string.IsNullOrEmpty(OperationID))
            {
                List<Sys_ModelFile> filelist = null;
                Sys_ModelFileBLL modelfile = new Sys_ModelFileBLL();
                if (string.IsNullOrEmpty(FileType))
                {
                    filelist = modelfile.GetList(p => p.File_OperationID == OperationID).OrderBy(a => a.File_AddTime).ToList();
                }
                else
                {
                    filelist = modelfile.GetList(p => p.File_OperationID == OperationID && p.File_Type == FileType).OrderBy(a => a.File_AddTime).ToList();
                }

                str += "[";
                foreach (Sys_ModelFile file in filelist)
                {
                    string fileContentLength = fileSizs(Convert.ToInt32(file.File_Size));
                    string filename2 = file.File_Name;
                    if (filename2.Length > 10)
                    {
                        filename2 = filename2.Substring(0, filename2.Length - 1) + "..";
                    }
                    //string fileUrl = "/Files/Download.aspx?path=" + file.File_Path + "&filename=" + file.File_Name;
                    str += "{\"id\":\"" + file.FileID + "\",\"filename\":\"" + file.File_Name + "\",\"filesize\":\"" + fileContentLength + "\",\"uploaddate\":\"" + Convert.ToDateTime(file.File_AddTime).ToString("yyyy年MM月dd日") + "\",\"fileUrl\":\"" + file.File_Path + "\",\"filetitle\":\"" + filename2 + "\"},";

                }
                str = str.Substring(0, str.Length - 1);
                str += "]";
                context.Response.Write(str);
            }

        }

        /// <summary>
        /// 查询数据库中和Session中的附件
        /// </summary>
        /// <param name="context"></param>
        public void DataFilseSelect(HttpContext context)
        {
            Regex r = new Regex(@"[\u4e00-\u9fa5]+");

            string OperationID = context.Request.QueryString["OperationID"];//业务ID
            string FileType = context.Request.QueryString["FileType"];//附件的类型
            string SessionID = context.Request.QueryString["SessionID"];//SessionID
            string type = context.Request.QueryString["type"];//类型1：未归类
            string productType = context.Request.QueryString["ProductType"];  //业务类别（个人、企业）
            string str = "";

            if (!string.IsNullOrEmpty(FileType))
            {
                FileType = HttpUtility.UrlDecode(FileType);
            }

            if (!string.IsNullOrEmpty(OperationID) || SessionID != "")
            {
                List<Sys_ModelFile> ModelFilelist = null;
                if (context.Session[SessionID] == null)
                {
                    ModelFilelist = new List<Sys_ModelFile>();
                }
                else
                {
                    ModelFilelist = context.Session[SessionID] as List<Sys_ModelFile>;
                }


                str += "[";

                List<Sys_ModelFile> filelist = null;
                Sys_ModelFileBLL modelfile = new Sys_ModelFileBLL();
                if (string.IsNullOrEmpty(FileType))
                {
                    filelist = modelfile.GetList(p => p.File_OperationID == OperationID).OrderBy(a => a.File_AddTime).ToList();
                }
                else
                {
                    string filetypes = "";
                    Sys_UploadDataTypeBLL upBll = new Sys_UploadDataTypeBLL();
                    List<Sys_UploadDataType> chlist = upBll.GetList(p => p.UploadDataType_TypeCode == FileType || p.UploadDataType_ProcessStage == FileType).ToList();

                    if (chlist.Count > 0)
                    {
                        for (int i = 0; i < chlist.Count; i++)
                        {
                            filetypes += chlist[i].UploadDataType_TypeCode;
                        }

                    }
                    if(FileType==Constant.fileType7)
                    {
                        filetypes = FileType;
                    }
                    else if (FileType == Constant.Classification)
                    {
                        filetypes = FileType;
                    }
                    
                    if (FileType == "0")
                    {
                        ModelFilelist = ModelFilelist.FindAll(p => !filetypes.Contains(p.File_Type));
                    }
                    else
                    {
                        if (FileType == Constant.Classification)
                        {
                            string lb = "";
                            Common_BLL ComBLL = new Common_BLL();
                            List<ParameterModel> ParameterModel = null;
                            List<ParameterModel> ParameterList = new List<ParameterModel>();
                            ParameterList.Add(new ParameterModel { ParamName = "ProductType", ParamValue = productType, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
                            DataSet ds = ComBLL.ExecStoredProcedures(new StoredProcModel { ProcName = "P_GetUploadDataType", ParameterList = ParameterList }, out ParameterModel);
                            int cout = ds.Tables[0].Rows.Count;
                            for (int j = 0; j < cout; j++)
                            {
                                lb += ds.Tables[0].Rows[j][0].ToString();
                            }

                            ModelFilelist = ModelFilelist.FindAll(p => filetypes.Contains(p.File_Type) || lb.Contains(p.File_Type)).OrderBy(a => a.File_AddTime).ToList();
                        }
                        else
                        {
                            ModelFilelist = ModelFilelist.FindAll(p => filetypes.Contains(p.File_Type) || p.File_Type == FileType).OrderBy(a => a.File_AddTime).ToList();
                        }
                        
                      
                    }



                    if (FileType == Constant.Classification)
                    {
                        string lb = "";
                        Common_BLL ComBLL = new Common_BLL();
                        List<ParameterModel> ParameterModel = null;
                        List<ParameterModel> ParameterList = new List<ParameterModel>();
                        ParameterList.Add(new ParameterModel { ParamName = "ProductType", ParamValue = productType, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
                        DataSet ds = ComBLL.ExecStoredProcedures(new StoredProcModel { ProcName = "P_GetUploadDataType", ParameterList = ParameterList }, out ParameterModel);
                        int cout = ds.Tables[0].Rows.Count;
                        for (int j = 0; j < cout; j++)
                        {
                            lb += ds.Tables[0].Rows[j][0].ToString();
                        }

                        filelist = modelfile.GetList(p => p.File_OperationID == OperationID && p.File_Type == FileType).OrderBy(a => a.File_AddTime).ToList();
                        
                    }
                    else
                    {
                        filelist = modelfile.GetList(p => p.File_OperationID == OperationID && (filetypes.Contains(p.File_Type) || p.File_Type == FileType)).OrderBy(a => a.File_AddTime).ToList();// || p.File_Type == FileType
                    }

                }


                foreach (Sys_ModelFile file in ModelFilelist)
                {
                    string filename2 = file.File_Name;
                    if (filename2.Length > 9)
                    {
                        
                        Match mc = r.Match(filename2);
                        Match mt = r.Match(file.File_Type);
                        if (mc.Length != 0)  //
                        {

                            if (mt.Length != 0 && file.File_Type!=Constant.fileType7)
                            {
                                filename2 = filename2.Substring(0, 7) + "..(未归类)";
                            }
                            else
                            {
                                filename2 = filename2.Substring(0, 9) + "..";
                            }

                        }
                        else
                        {
                            if (mt.Length != 0 && file.File_Type != Constant.fileType7)
                            {
                                filename2 = filename2.Substring(0, 9) + "..(未归类)";
                            }
                            else
                            {
                                filename2 = filename2.Substring(0, 9) + "..";
                            }


                        }

                    }
                    else
                    {
                       

                        Match mt = r.Match(file.File_Type);

                        if (mt.Length != 0 && file.File_Type != Constant.fileType7)
                        {
                            filename2 = filename2 + "(未归类)";
                        }

                    }
                    str += "{\"id\":\"" + file.FileID + "\",\"filename\":\"" + file.File_Name + "\",\"filesize\":\"\",\"uploaddate\":\"" + Convert.ToDateTime(file.File_AddTime).ToString("yyyy年MM月dd日") + "\",\"fileUrl\":\"" + file.File_Path + "\",\"filetitle\":\"" + filename2 + "\"},";
                }

                foreach (Sys_ModelFile file in filelist)
                {
                    string fileContentLength = fileSizs(Convert.ToInt32(file.File_Size));
                    string filename2 = file.File_Name;
                    if (filename2.Length > 9)
                    {
                        
                        Match mc = r.Match(file.File_Type);
                        if (mc.Length != 0 && file.File_Type != Constant.fileType7)  //类别中含有汉字
                        {
                            filename2 = filename2.Substring(0, 7) + "(未归类)";
                        }
                        else
                        {
                            filename2 = filename2.Substring(0, 9) + "..";
                        }


                    }
                    else
                    {
                        
                        Match mc = r.Match(file.File_Type);
                        if (mc.Length != 0 && file.File_Type != Constant.fileType7)  //类别中含有汉字
                        {
                            filename2 = filename2 + "(未归类)";
                        }

                    }
                    str += "{\"id\":\"" + file.FileID + "\",\"filename\":\"" + file.File_Name + "\",\"filesize\":\"" + fileContentLength + "\",\"uploaddate\":\"" + Convert.ToDateTime(file.File_AddTime).ToString("yyyy年MM月dd日") + "\",\"fileUrl\":\"" + file.File_Path + "\",\"filetitle\":\"" + filename2 + "\"},";

                }
                if (str.Length > 1)
                {
                    str = str.Substring(0, str.Length - 1);
                }

                str += "]";
                context.Response.Write(str);
            }

        }

        /// <summary>
        /// 文件大小处理
        /// </summary>
        public string fileSizs(int Sizs)
        {
            string ContentLength = "";
            double BCount = 1024;
            double KBCount = BCount * 1024;
            double MBCount = KBCount * 1024;
            double GBCount = MBCount * 1024;
            double TBCount = GBCount * 1024;
            if (Sizs <= BCount)
            {

                ContentLength = Sizs + "B";
            }
            if (Sizs > BCount)
            {

                ContentLength = Math.Round(((double)(Sizs) / BCount), 2) + "KB";
            }
            if (Sizs > KBCount)
            {
                ContentLength = Math.Round(((double)(Sizs) / KBCount), 2) + "MB";

            }
            if (Sizs > MBCount)
            {

                ContentLength = Math.Round(((double)(Sizs) / MBCount), 2) + "GB";

            }
            if (Sizs > GBCount)
            {

                ContentLength = Math.Round(((double)(Sizs) / GBCount), 2) + "TB";

            }

            return ContentLength;
        }


        public void DelFilseList(HttpContext context)
        {
            string re = "1";

            string id = context.Request.QueryString["id"];
            string SessionID = context.Request.QueryString["SessionID"];
            string FileType = context.Request.QueryString["FileType"];
            if (FileType == Constant.fileType1)  //如果是第一部 真删除
            {
                List<Sys_ModelFile> list = null;
                if (HttpContext.Current.Session[SessionID] != null)
                {
                    list = HttpContext.Current.Session[SessionID] as List<Sys_ModelFile>;


                    List<Sys_ModelFile> modelList = list.FindAll(p => id.Contains(p.FileID));
                    if (modelList.Count > 0)
                    {
                        for (int i = 0; i < modelList.Count; i++)
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(modelList[i].File_Path));

                            if (modelList[i] != null)
                            {
                                list.Remove(modelList[i]);
                            }
                        }

                        HttpContext.Current.Session[SessionID] = list;
                    }

                }

                Sys_ModelFileBLL bllfile = new Sys_ModelFileBLL();

                string[] picid = id.Split(',');
                //string picid = "'" + id.Replace(",", "','") + "'";

                //List<Sys_ModelFile> fileList = bllfile.FindWhere(" FileID in (" + picid + ")");
                //bool b = bllfile.DeleteWhere(" FileID in (" + picid + ")");
                List<Sys_ModelFile> fileList = bllfile.GetList(p=>picid.Contains(p.FileID)).ToList();
                bool b = bllfile.Delete((p=>picid.Contains(p.FileID)));
                if (b)
                {
                    for (int i = 0; i < fileList.Count; i++)
                    {
                        try
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(fileList[i].File_Path));
                        }
                        catch { }
                        re = "1";
                    }

                }
            }
            else
            {
                List<Sys_ModelFile> list = null;
                if (HttpContext.Current.Session[SessionID] != null)
                {
                    list = HttpContext.Current.Session[SessionID] as List<Sys_ModelFile>;
                    for (int i = 0; i < list.Count; i++)
                    {
                        string fileid = list[i].FileID;
                        if (id.Contains(fileid))
                        {
                            list[i].File_Type = Constant.fileType7;
                        }
                    }

                    HttpContext.Current.Session[SessionID] = list;

                }

                Sys_ModelFileBLL bllfile = new Sys_ModelFileBLL();

                string[] picid = id.Split(',');
                
                List<Sys_ModelFile> fileList = bllfile.GetList(p => picid.Contains(p.FileID)).ToList();
                //string picid = "'" + id.Replace(",", "','") + "'";

                //List<Sys_ModelFile> fileList = bllfile.FindWhere(" FileID in (" + picid + ")");
                for (int i = 0; i < fileList.Count; i++)
                {
                    fileList[i].File_Type = Constant.fileType7;
                }
                bool bl = bllfile.Update(fileList);

                if (!bl)
                {
                    re = "2";
                }
            }
            context.Response.Write(re);


        }


        public void GuiLeiFilseList(HttpContext context)
        {
            string re = "1";

            string id = context.Request.QueryString["id"];
            string SessionID = context.Request.QueryString["SessionID"];
            string FileType = context.Request.QueryString["FileType"];
            if (!string.IsNullOrEmpty(FileType))
            {
                FileType = HttpUtility.UrlDecode(FileType);
            }
            List<Sys_ModelFile> list = null;
            if (HttpContext.Current.Session[SessionID] != null)
            {
                list = HttpContext.Current.Session[SessionID] as List<Sys_ModelFile>;
                //List<Sys_ModelFile> list2 = list.FindAll(p => id.Contains(p.FileID));
                for (int i = 0; i < list.Count; i++)
                {
                    //for (int j = 0; j < list.Count;j++ )
                    //{
                    //    if(list2[i].FileID){}
                    //}
                    string fileid = list[i].FileID;
                    if (id.Contains(fileid))
                    {
                        list[i].File_Type = FileType;
                    }
                }

                HttpContext.Current.Session[SessionID] = list;
                List<Sys_ModelFile> lisst = HttpContext.Current.Session[SessionID] as List<Sys_ModelFile>;
            }

            Sys_ModelFileBLL bllfile = new Sys_ModelFileBLL();

            //string picid = "'" + id.Replace(",", "','") + "'";

            //List<Sys_ModelFile> fileList = bllfile.FindWhere(" FileID in (" + picid + ")");
            string[] picid = id.Split(',');
            List<Sys_ModelFile> fileList = bllfile.GetList(p => picid.Contains(p.FileID)).ToList();
            for (int i = 0; i < fileList.Count; i++)
            {
                fileList[i].File_Type = FileType;
            }
            string b = "";
            bool bl = bllfile.Update(fileList);

            if (!bl)
            {
                re = "2";
            }

            context.Response.Write(re);


        }


        //public void GetNumbyType(HttpContext context)
        //{

        //    string type = context.Request["fileType"].ToString();  //附件类别
        //    if (!string.IsNullOrEmpty(type))
        //    {
        //        type = HttpUtility.UrlDecode(type);
        //    }
        //    string processId = context.Request.QueryString["ProductID"]; //产品ID

        //    string id = context.Request.QueryString["id"];           //业务ID
        //    string SessionID = context.Request.QueryString["SessionID"];

        //     string pt = context.Request.QueryString["ProductType"];  //业务类别（个人、企业）
        //    bool IsShowBackup = context.Request.QueryString["IsShowBackup"].ToBoolean(true);//判断分类数量是否在
        //    string productType="";
        //    if(pt=="1")
        //    {
        //        productType = Constant.typecode1;
        //    }
        //    else if(pt=="2")
        //    {
        //        productType = Constant.typecode2;
        //    }

        //    List<Sys_ModelFile> list = null;
        //    if (HttpContext.Current.Session[SessionID] != null)
        //    {
        //        list = HttpContext.Current.Session[SessionID] as List<Sys_ModelFile>;
        //    }



        //    Sys_UploadDataTypeBLL upBll = new Sys_UploadDataTypeBLL();
        //    List<Sys_UploadDataType> allTypeList = upBll.GetList(p => p.UploadDataType_ProcessStage == type && p.UploadDataType_ProductType == productType);
        //    string typecodes = "";
        //    for (int i = 0; i < allTypeList.Count; i++)
        //    {
        //        typecodes += "'" + allTypeList[i].UploadDataType_TypeCode + "',";
        //    }
        //    if (typecodes.Length > 0)
        //    {
        //        typecodes = typecodes.Substring(0, typecodes.Length - 1);
        //    }
        //    Sys_ModelFileBLL bllfile = new Sys_ModelFileBLL();
        //    List<Sys_ModelFile> moList = bllfile.FindWhere(" File_OperationID='" + id + "' and File_Type in (" + typecodes + ")").ToList();

        //    string str = "[";
        //    //Sys_ProductDataMappingBLL prbll = new Sys_ProductDataMappingBLL();
        //    //List<Sys_ProductDataMapping> prList = prbll.FindWhere(p => p.ProductDataMapping_ProductID == processId);

        //    //获取当前产品类别

        //    List<Sys_ProductDataMapping> prList = new List<Sys_ProductDataMapping>();

        //    if (!IsShowBackup)//判断是否从备份资料分类取
        //    {
        //        Sys_ProductDataMappingBLL prbll = new Sys_ProductDataMappingBLL();
        //        prList = prbll.FindWhere(p => p.ProductDataMapping_ProductID == processId);
        //    }
        //    else
        //    {
        //        //备份处理
        //        List<Sys_ProductDataMappingBackup> prBackupList = new List<Sys_ProductDataMappingBackup>();
        //        prBackupList = new Sys_ProductDataMappingBackupBLL().FindWhere(p => p.ProductDataMapping_ProductID == processId && p.roductDataMapping_ApplyBasisID == id);
        //        foreach (Sys_ProductDataMappingBackup item in prBackupList)
        //        {
        //            prList.Add(new Sys_ProductDataMapping { ProductDataMapping_DataNumber = item.ProductDataMapping_DataNumber, ProductDataMapping_ID = item.ProductDataMapping_ID, ProductDataMapping_IsDelete = item.ProductDataMapping_IsDelete, ProductDataMapping_ProductID = item.ProductDataMapping_ProductID, ProductDataMapping_SortNo = item.ProductDataMapping_SortNo, ProductDataMapping_TypeID = item.ProductDataMapping_TypeID, ProductDataMapping_UpdateTime = item.ProductDataMapping_UpdateTime, ProductDataMapping_UpdateUerID = item.ProductDataMapping_UpdateUerID });
        //        }
        //    }
        //    for (int i = 0; i < allTypeList.Count; i++)
        //    {
        //        int resNum = 0;
        //        int num1 = 0;  //需要上传数量
        //        for (int j = 0; j < prList.Count; j++)
        //        {
        //            if (allTypeList[i].UploadDataType_ID == prList[j].ProductDataMapping_TypeID)
        //            {
        //                num1 = Convert.ToInt32(prList[j].ProductDataMapping_DataNumber);
        //            }
        //        }

        //        int num2 = 0;  //已经上传数量
        //        if (list.IsNotNull())
        //        {
        //            num2 = list.FindAll(p => p.File_Type == allTypeList[i].UploadDataType_TypeCode).Count;
        //        }
        //        num2 = num2 + moList.FindAll(p => p.File_Type == allTypeList[i].UploadDataType_TypeCode).ToList().Count;
        //        resNum = num1 - num2;  //还差多少附件

        //        if (resNum > 0)
        //        {
        //            str += "{\"id\":\"" + allTypeList[i].UploadDataType_TypeCode + "\",\"filename\":\"" + allTypeList[i].UploadDataType_Name + "\",\"num\":\"" + resNum + "\"},";
        //        }
        //    }
        //    if (str.Length > 2)
        //    {
        //        str = str.Substring(0, str.Length - 1);
        //    }
        //    str += "]";
        //    context.Response.Write(str);


        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}