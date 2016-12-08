using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shu.Manage.UserControls
{
    public partial class UCFiles : System.Web.UI.UserControl
    {
        public string path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                path = Server.MapPath("/");
                if (string.IsNullOrEmpty(FileMulti)) { FileMulti = "true"; };
                if (string.IsNullOrEmpty(FileTypeDesc))
                {
                    FileTypeDesc = "支持格式：jpg、gif、png、bmp、jpeg";
                }
                if (string.IsNullOrEmpty(FileTypeExts))
                {
                    FileTypeExts = "*.jpg;*.gif;*.png;*.bmp;*.jpeg;";
                    //FileTypeExts = "*.jpg;*.gif;*.png;*.bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx;*.pdf;*.flv;*.zip;*.rar;";
                }
                if (string.IsNullOrEmpty(FileSizeLimit))
                {
                    FileSizeLimit = "30";
                }
                if (string.IsNullOrEmpty(FileButtonText))
                {
                    FileButtonText = "选择文件";
                }
            }
        }
        /// <summary>
        /// 文件对应的业务ID(必填项)
        /// </summary>
        public string FileOperationID
        {
            get { return Hidden_OperationID.Value; }
            set { Hidden_OperationID.Value = value; }
        }
        /// <summary>
        /// 控件名称(必填项)
        /// </summary>
        public string FilesNname
        {
            get { return Hidden_FileNmae.Value; }
            set { Hidden_FileNmae.Value = value; }
        }
        /// <summary>
        /// 文件描述
        /// </summary>
        public string FileTypeDesc
        {
            get { return Hidden_fileTypeDesc.Value; }
            set { Hidden_fileTypeDesc.Value = value; }
        }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileTypeExts
        {
            get { return Hidden_fileTypeExts.Value; }
            set { Hidden_fileTypeExts.Value = value;}
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSizeLimit
        {
            get { return Hidden_FileSizeLimit.Value; }
            set { Hidden_FileSizeLimit.Value = value;  }
        }
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string FileButtonText
        {
            get { return Hidden_ButtonText.Value; }
            set {  Hidden_ButtonText.Value = value;   }
        }
        /// <summary>
        /// 附件存储SessionID
        /// </summary>
        public string FileSessionID
        {
            get { return Hidden_SessionID.Value; }
            set { Hidden_SessionID.Value = value; }
        }

        /// <summary>
        /// 设置文件上传单个还是多个（true 为开启多个上传，false 是关闭多个默认为：true）
        /// </summary>
        public string FileMulti
        {
            get { return Hidden_Multi.Value; }
            set { Hidden_Multi.Value = value; }

        }


        /// <summary>
        /// 上传附件的类型（用于多个上传控件，区分文件上传类型）
        /// </summary>
        public string FileType
        {
            get { return Hidden_FileType.Value; }
            set { Hidden_FileType.Value = value; }

        }

        /// <summary>
        /// 操作类型（是修改还是新增）
        /// </summary>
        public string FileOperationType
        {
            get { return Hidden_OperationType.Value; }
            set { Hidden_OperationType.Value = value; }
        }
    }
}