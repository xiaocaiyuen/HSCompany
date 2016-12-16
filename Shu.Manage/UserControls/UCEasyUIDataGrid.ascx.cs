using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;
using System.Xml.Linq;
using Shu.BLL;
using Shu.Model;
using System.Data;
using Shu.Utility.Extensions;
using Newtonsoft.Json;
using Shu.Utility;
using Newtonsoft.Json.Converters;
using System.Text;

namespace Shu.Manage.UserControls
{
    public partial class UCEasyUIDataGrid : System.Web.UI.UserControl
    {
        protected BasePage bg = new BasePage();
        protected Sys_Menu model = new Sys_Menu();
        #region 变量
        /// <summary>
        /// 每页显示多少数据（默认12）
        /// </summary>
        private int pageSize = 12;
        /// <summary>
        /// 当前第几页
        /// </summary>
        private int pageIndex = 1;

        /// <summary>
        /// 总数量
        /// </summary>
        private int totalCount = 0;

        /// <summary>
        /// 自定义数据源
        /// </summary>
        public DataSet Datas;
        public string CurrentUrl
        {
            get
            {//获得页面全路径
                string uri = this.Page.Request.Url.AbsoluteUri;
                //获得页面的URL
                return uri.Substring(uri.LastIndexOf('/') + 1);
            }
        }

        private string roleOperate { get; set; }

        private string tempXmlValue { get; set; }

        /// <summary>
        /// 每页显示多少数据
        /// </summary>
        public int PageSize { get { return pageSize; } set { pageSize = value; } }

        /// <summary>
        /// 当前第几页
        /// </summary>
        public int PageIndex { get { return pageIndex; } set { pageIndex = value; } }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get { return totalCount; } set { totalCount = value; } }

        #endregion

        #region 数据源属性

        /// <summary>
        /// XML数据源地址
        /// </summary>
        public string DataSource { get; set; }

        // 获得跟节点
        private XElement rootNode = null;
        private XElement RootNode
        {

            get
            {
                if (rootNode != null)
                {
                    return rootNode;
                }
                else
                {
                    rootNode = GetRoot();

                    return rootNode;
                }
            }
            set
            {
                rootNode = value;
            }
        }

        #region 私有属性
        //private string tableKey;
        //private string tableKeyField;
        //private string tableName;
        //private string tableView;
        //private string sQLField;
        private bool isDbOnClickRow;
        private bool isShowCheckBox;
        private bool isShowPagin;
        private int rowClickNum = -1;
        //private int rowImageNum = -1;
        private string imageURL;


        private string addURL;
        private string biaoJiURL;
        private string modifyURL;
        private string copyURL;
        private string detailURL;
        private string auditURL;
        private string operatingURL;
        private string scoreURL;

        private string seeChargeDepCode;
        private string seeChargeURL;
        private string isSeeCharge;
        private string menuURL;
        private string additionalBudgetURL;
        private string modifyAdditionalBudgetURL;

        private string auditAdditionalBudgetURL;
        private string courseURL;
        private string launchURL;
        private string vidoURL;
        private string sQLOrder;
        private string downloadURL;
        private string configureURL;
        private string resolveURL;
        private string revokeURL;
        private string writebackURL;
        private string trackURL;
        private string inspectionUrl;
        private string redoURL;
        private string importYouChuURL;
        private string importNongHangURL;
        private string importICBCURL;
        private string undoURL;
        private string resetScreenURL;
        private string screenURL;
        private string chooseURL;
        private string setResetScreenURL;
        private string printURL;
        private string calculatorURL;
        private string personageURL;
        private string companyURL;
        private string personageDirectURL;
        private string companyDirectURL;
        private string signingURL;
        private string decompressionURL;
        private string renewalURL;
        private string claimURL;
        private int dateType = 0;
        private string advancePayURL;
        private string distributionURL;
        private string openURL;
        private string submitURL;
        private string ispaf;
        private string fileURL;
        private string recordURL;
        private string borrowURL;
        private string returnURL;
        private string cancelURL;
        private string activeURL;
        private string dhcsURL;
        private string xccsURL;
        private string csclURL;
        private string dhhfURL;
        private string ywdelURL;


        #endregion
        /// <summary>
        /// 导出pdf
        /// </summary>
        public string Ispaf
        {
            get
            {
                tempXmlValue = GetGridAttr("Ispdf");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return ispaf;
            }
            set { ispaf = value; }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string TableKey { get { return GetGridAttr("Key"); } }

        /// <summary>
        /// 主键扩展
        /// </summary>
        public string TableKeyField { get { return GetGridAttr("KeyField"); } }

        public string tableName { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(tableName))
                    return GetGridAttr("TableName");
                else return tableName;
            }
            set { tableName = value; }
        }

        private string tableView;
        /// <summary>
        /// 视图
        /// </summary>
        public string TableView { get { return tableView; } set { tableView = value; } }

        /// <summary>
        /// 排序
        /// </summary>
        public string SQLOrder
        {
            get
            {
                return sQLOrder;
            }
            set
            { sQLOrder = value; }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SQLWhere { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string SQLField { get { return GetGridAttr("SQLField"); } }

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        public bool IsShowCheckBox
        {
            get
            {
                tempXmlValue = GetGridAttr("IsShowCheckBox");
                if (tempXmlValue != "")
                    return bool.Parse(tempXmlValue);
                else return isShowCheckBox;
            }
            set { isShowCheckBox = value; }
        }

        /// <summary>
        /// 是否支持双击行事件
        /// </summary>
        public bool IsDbOnClickRow
        {
            get
            {
                tempXmlValue = GetGridAttr("IsDbOnClickRow");
                if (tempXmlValue != "")
                    return bool.Parse(tempXmlValue);
                else return isDbOnClickRow;
            }
            set { isDbOnClickRow = value; }
        }





        /// <summary>
        /// 是否显示分页
        /// </summary>
        public bool IsShowPagin
        {
            get
            {
                tempXmlValue = GetGridAttr("IsShowPagin");
                if (tempXmlValue != "")
                    return bool.Parse(tempXmlValue);
                else return isShowPagin;
            }
            set { isShowPagin = value; }
        }

        /// <summary>
        /// 通过点击文字链接详细页，此为需要此功能的列，如果为-1则不加载
        /// </summary>
        public int RowClickNum
        {
            get
            {
                tempXmlValue = GetGridAttr("RowClickNum");
                if (tempXmlValue != "")
                    return int.Parse(tempXmlValue);
                else
                    return rowClickNum;
            }
            set { rowClickNum = value; }
        }


        ///// <summary>
        ///// 通过显示图片，此为需要此功能的列，如果为-1则不加载
        ///// </summary>
        //public int RowImageNum
        //{
        //    get
        //    {
        //        tempXmlValue = GetGridAttr("RowImageNum");
        //        if (tempXmlValue != "")
        //            return int.Parse(tempXmlValue);
        //        else
        //            return rowImageNum;
        //    }
        //    set { rowImageNum = value; }
        //}

        /// <summary>
        /// 图片相关超链接
        /// </summary>
        public string ImageURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ImageURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else
                    return imageURL;
            }
            set { imageURL = value; }
        }

        /// <summary>
        /// 新增页面的URL地址
        /// </summary>
        public string AddURL
        {
            get
            {
                tempXmlValue = GetGridAttr("AddURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return addURL;
            }
            set { addURL = value; }
        }
        /// <summary>
        /// 标记页面的URL地址
        /// </summary>
        public string BiaoJiURL
        {
            get
            {
                tempXmlValue = GetGridAttr("BiaoJiURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return biaoJiURL;
            }
            set { biaoJiURL = value; }
        }
        /// <summary>
        /// 修改页面的URL地址
        /// </summary>
        public string ModifyURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ModifyURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return modifyURL;
            }
            set { modifyURL = value; }
        }

        /// <summary>
        /// 修改页面的URL地址
        /// </summary>
        public string CopyURL
        {
            get
            {
                tempXmlValue = GetGridAttr("CopyURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return copyURL;
            }
            set { copyURL = value; }
        }

        /// <summary>
        /// 详细页面的URL地址
        /// </summary>
        public string DetailURL
        {
            get
            {
                tempXmlValue = GetGridAttr("DetailURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return detailURL;
            }
            set { detailURL = value; }
        }

        /// <summary>
        /// 审核页面的URL地址
        /// </summary>
        public string AuditURL
        {
            get
            {
                tempXmlValue = GetGridAttr("AuditURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return auditURL;
            }
            set { auditURL = value; }
        }

        /// <summary>
        /// 录入页面的URL地址
        /// </summary>
        public string OperatingURL
        {
            get
            {
                tempXmlValue = GetGridAttr("OperatingURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return operatingURL;
            }
            set
            {
                operatingURL = value;
            }
        }

        /// <summary>
        /// 分值调整URL地址
        /// </summary>
        public string ScoreURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ScoreURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return scoreURL;
            }
            set { scoreURL = value; }
        }

        /// <summary>
        /// 按部门查看部门字段Code
        /// </summary>
        public string SeeChargeDepCode
        {
            get
            {
                tempXmlValue = GetGridAttr("SeeChargeDepCode");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return seeChargeDepCode;
            }
            set
            { seeChargeDepCode = value; }
        }
        /// <summary>
        /// 查看范围使用URL
        /// </summary>
        public string SeeChargeURL
        {
            get
            {
                tempXmlValue = GetGridAttr("SeeChargeURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return seeChargeURL;
            }
            set { seeChargeURL = value; }
        }
        /// <summary>
        /// 是否使用查看范围（"1"为不使用）
        /// </summary>
        public string IsSeeCharge
        {
            get
            {
                tempXmlValue = GetGridAttr("IsSeeCharge");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return isSeeCharge;
            }
            set { isSeeCharge = value; }
        }

        /// <summary>
        /// 菜单URL
        /// </summary>
        public string MenuURL
        {
            get
            {
                tempXmlValue = GetGridAttr("MenuURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return menuURL;
            }
            set { menuURL = value; }
        }

        /// <summary>
        /// 追加预算页面的URL地址
        /// </summary>
        public string AdditionalBudgetURL
        {
            get
            {
                tempXmlValue = GetGridAttr("AdditionalBudgetURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return additionalBudgetURL;
            }
            set { additionalBudgetURL = value; }
        }

        /// <summary>
        /// 追加预算修改页面的URL地址
        /// </summary>
        public string ModifyAdditionalBudgetURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ModifyAdditionalBudgetURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return modifyAdditionalBudgetURL;
            }
            set { modifyAdditionalBudgetURL = value; }
        }

        /// <summary>
        /// 审核追加预算页面的URL地址
        /// </summary>
        public string AuditAdditionalBudgetURL
        {
            get
            {
                tempXmlValue = GetGridAttr("AuditAdditionalBudgetURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else
                    return auditAdditionalBudgetURL;
            }
            set { auditAdditionalBudgetURL = value; }
        }

        /// <summary>
        /// 下载URL地址
        /// </summary>
        public string DownloadURL
        {
            get
            {
                tempXmlValue = GetGridAttr("DownloadURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return downloadURL;
            }
            set { downloadURL = value; }
        }

        /// <summary>
        /// 下载URL地址
        /// </summary>
        public string ConfigureURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ConfigureURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else
                    return configureURL;
            }
            set { configureURL = value; }
        }

        /// <summary>
        /// 分解URL地址
        /// </summary>
        public string ResolveURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ResolveURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else
                    return resolveURL;
            }
            set { resolveURL = value; }
        }

        /// <summary>
        /// 撤销分解URL地址
        /// </summary>
        public string RevokeURL
        {
            get
            {
                tempXmlValue = GetGridAttr("RevokeURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else
                    return revokeURL;
            }
            set { revokeURL = value; }
        }

        /// <summary>
        /// 撤销分解URL地址
        /// </summary>
        public string WritebackURL
        {
            get
            {
                tempXmlValue = GetGridAttr("WritebackURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else
                    return writebackURL;
            }
            set { writebackURL = value; }
        }

        /// <summary>
        /// 追踪URL地址
        /// </summary>
        public string TrackURL
        {
            get
            {
                tempXmlValue = GetGridAttr("TrackURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else
                    return trackURL;
            }
            set { trackURL = value; }
        }

        /// <summary>
        /// 督办URL地址
        /// </summary>
        public string InspectionURL
        {
            get
            {
                tempXmlValue = GetGridAttr("InspectionURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return InspectionURL;
            }
            set { InspectionURL = value; }
        }

        /// <summary>
        /// 添加文字课程
        /// </summary>
        public string CourseURL
        {
            get
            {
                tempXmlValue = GetGridAttr("CourseURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return courseURL;
            }
            set { courseURL = value; }
        }

        /// <summary>
        /// 发起
        /// </summary>
        public string LaunchURL
        {
            get
            {
                tempXmlValue = GetGridAttr("LaunchURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else
                    return launchURL;
            }
            set { launchURL = value; }
        }

        /// <summary>
        /// 添加视频课程
        /// </summary>
        public string VidoURL
        {
            get
            {
                tempXmlValue = GetGridAttr("VidoURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return vidoURL;
            }
            set { vidoURL = value; }
        }

        /// <summary>
        /// 导入
        /// </summary>
        public string RedoURL
        {
            get
            {
                tempXmlValue = GetGridAttr("RedoURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return redoURL;
            }
            set { redoURL = value; }
        }

        /// <summary>
        /// 邮储银行导入
        /// </summary>
        public string ImportYouChuURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ImportYouChuURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return importYouChuURL;
            }
            set { importYouChuURL = value; }
        }

        /// <summary>
        /// 农业银行导入
        /// </summary>
        public string ImportNongHangURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ImportNongHangURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return importNongHangURL;
            }
            set { importNongHangURL = value; }
        }

        /// <summary>
        /// 公司银行导入
        /// </summary>
        public string ImportICBCURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ImportICBCURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return importICBCURL;
            }
            set { importICBCURL = value; }
        }


        /// <summary>
        /// 导出
        /// </summary>
        public string UndoURL
        {
            get
            {
                tempXmlValue = GetGridAttr("UndoURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return undoURL;
            }
            set { undoURL = value; }
        }

        /// <summary>
        /// 重置筛选
        /// </summary>
        public string ResetScreenURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ResetScreenURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else
                    return resetScreenURL;
            }
            set { resetScreenURL = value; }
        }

        /// <summary>
        /// 筛选
        /// </summary>
        public string ScreenURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ScreenURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return screenURL;
            }
            set { screenURL = value; }
        }

        /// <summary>
        /// 选取
        /// </summary>
        public string ChooseURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ChooseURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return chooseURL;
            }
            set { chooseURL = value; }
        }

        /// <summary>
        /// 重置筛选
        /// </summary>
        public string SetResetScreenURL
        {
            get
            {
                tempXmlValue = GetGridAttr("SetResetScreenURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else
                    return setResetScreenURL;
            }
            set { setResetScreenURL = value; }
        }

        /// <summary>
        /// 重置筛选
        /// </summary>
        public string PrintURL
        {
            get
            {
                tempXmlValue = GetGridAttr("PrintURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return printURL;
            }
            set { printURL = value; }
        }

        /// <summary>
        /// 计算器
        /// </summary>
        public string CalculatorURL
        {
            get
            {
                tempXmlValue = GetGridAttr("CalculatorURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return calculatorURL;
            }
            set { calculatorURL = value; }
        }

        /// <summary>
        /// 新增个人回租
        /// </summary>
        public string PersonageURL
        {
            get
            {
                tempXmlValue = GetGridAttr("PersonageURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return personageURL;
            }
            set { personageURL = value; }
        }

        /// <summary>
        /// 新增企业回租
        /// </summary>
        public string CompanyURL
        {
            get
            {
                tempXmlValue = GetGridAttr("CompanyURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return companyURL;
            }
            set { companyURL = value; }
        }

        /// <summary>
        /// 新增个人直租
        /// </summary>
        public string PersonageDirectURL
        {
            get
            {
                tempXmlValue = GetGridAttr("PersonageDirectURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return personageDirectURL;
            }
            set { personageDirectURL = value; }
        }

        /// <summary>
        /// 新增企业直租
        /// </summary>
        public string CompanyDirectURL
        {
            get
            {
                tempXmlValue = GetGridAttr("CompanyDirectURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return companyDirectURL;
            }
            set { companyDirectURL = value; }
        }

        /// <summary>
        /// 签约
        /// </summary>
        public string SigningURL
        {
            get
            {
                tempXmlValue = GetGridAttr("SigningURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return signingURL;
            }
            set { signingURL = value; }
        }

        /// <summary>
        /// 选择申请
        /// </summary>
        public string DecompressionURL
        {
            get
            {
                tempXmlValue = GetGridAttr("DecompressionURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return decompressionURL;
            }
            set { decompressionURL = value; }
        }

        /// <summary>
        /// 续保
        /// </summary>
        public string RenewalURL
        {
            get
            {
                tempXmlValue = GetGridAttr("RenewalURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return renewalURL;
            }
            set { renewalURL = value; }
        }

        /// <summary>
        /// 续保
        /// </summary>
        public string ClaimURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ClaimURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return claimURL;
            }
            set { claimURL = value; }
        }

        /// <summary>
        /// 提交
        /// </summary>
        public string SubmitURL
        {
            get
            {
                tempXmlValue = GetGridAttr("SubmitURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return submitURL;
            }
            set { submitURL = value; }
        }

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        public int DateType
        {
            get
            {
                tempXmlValue = GetGridAttr("DateType");
                if (tempXmlValue != "")
                    return int.Parse(tempXmlValue);
                else return dateType;
            }
            set { dateType = value; }
        }

        /// <summary>
        /// 提前还款
        /// </summary>
        public string AdvancePayURL
        {
            get
            {
                tempXmlValue = GetGridAttr("AdvancePayURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return advancePayURL;
            }
            set { advancePayURL = value; }
        }

        public string DistributionURL
        {
            get
            {
                tempXmlValue = GetGridAttr("DistributionURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return distributionURL;
            }
            set { distributionURL = value; }
        }

        public string OpenURL
        {
            get
            {
                tempXmlValue = GetGridAttr("OpenURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return openURL;
            }
            set { openURL = value; }
        }

        /// <summary>
        /// 归档页面的URL地址
        /// </summary>
        public string FileURL
        {
            get
            {
                tempXmlValue = GetGridAttr("FileURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return fileURL;
            }
            set { fileURL = value; }
        }

        /// <summary>
        /// 履历页面的URL地址
        /// </summary>
        public string RecordURL
        {
            get
            {
                tempXmlValue = GetGridAttr("RecordURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return recordURL;
            }
            set { recordURL = value; }
        }

        /// <summary>
        /// 借出页面的URL地址
        /// </summary>
        public string BorrowURL
        {
            get
            {
                tempXmlValue = GetGridAttr("BorrowURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return borrowURL;
            }
            set { borrowURL = value; }
        }

        /// <summary>
        /// 归还页面的URL地址
        /// </summary>
        public string ReturnURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ReturnURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return returnURL;
            }
            set { returnURL = value; }
        }

        /// <summary>
        /// 取消页面的URL地址
        /// </summary>
        public string CancelURL
        {
            get
            {
                tempXmlValue = GetGridAttr("CancelURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return cancelURL;
            }
            set { cancelURL = value; }
        }

        /// <summary>
        /// 激活页面的URL地址
        /// </summary>
        public string ActiveURL
        {
            get
            {
                tempXmlValue = GetGridAttr("ActiveURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return activeURL;
            }
            set { activeURL = value; }
        }

        /// <summary>
        /// 电话催收的URL地址
        /// </summary>
        public string DHCSURL
        {
            get
            {
                tempXmlValue = GetGridAttr("DHCSURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return dhcsURL;
            }
            set { dhcsURL = value; }
        }

        /// <summary>
        /// 现场催收的URL地址
        /// </summary>
        public string XCCSURL
        {
            get
            {
                tempXmlValue = GetGridAttr("XCCSURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return xccsURL;
            }
            set { xccsURL = value; }
        }

        /// <summary>
        /// 催收车辆的URL地址
        /// </summary>
        public string CSCLURL
        {
            get
            {
                tempXmlValue = GetGridAttr("CSCLURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return csclURL;
            }
            set { csclURL = value; }
        }

        /// <summary>
        /// 电话回访的URL地址
        /// </summary>
        public string DHHFURL
        {
            get
            {
                tempXmlValue = GetGridAttr("DHHFURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return dhhfURL;
            }
            set { dhhfURL = value; }
        }

        /// <summary>
        /// 业务删除的URL地址：输入删除原因
        /// </summary>
        public string YWDelURL
        {
            get
            {
                tempXmlValue = GetGridAttr("YWDelURL");
                if (tempXmlValue != "")
                    return tempXmlValue;
                else return ywdelURL;
            }
            set { ywdelURL = value; }
        }



        /// <summary>
        /// 表格标题
        /// </summary>
        public string GridTitle { get; set; }

        public string ModelTitle { get; set; }

        #endregion

        #region XML数据源配置读取

        /// <summary>
        /// 读取属性
        /// </summary>
        /// <param name="key">属性名称</param>
        /// <returns></returns>
        private string GetGridAttr(string key)
        {
            if (RootNode != null)
            {
                if (RootNode.Attributes().Any(p => p.Name == key))
                {
                    return RootNode.Attribute(key).Value;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }


        private XDocument doc { get; set; }
        /// <summary>
        /// 获得根节点
        /// </summary>
        /// <returns></returns>
        private XElement GetRoot()
        {
            string filePath = HttpContext.Current.Server.MapPath(DataSource);
            if (doc == null)
            {
                doc = XDocument.Load(filePath);
            }
            RootNode = doc.Element("Grid");
            return RootNode;
        }


        private IEnumerable<XElement> elements { get; set; }
        /// <summary>
        /// 获取字段和名称
        /// </summary>
        protected IEnumerable<XElement> Node
        {
            get
            {
                #region 加载列
                XElement root = RootNode;
                if (elements == null)
                {
                    elements = from x in root.Elements("Column")
                               select x;
                }
                return elements;
                #endregion
            }
        }
        #endregion

        #region 当然页面URL地址
        /// <summary>
        /// 当然页面URL地址
        /// </summary>
        public string url
        {
            get
            {
                string url = "";
                if (MenuURL != "")
                {
                    url = MenuURL;
                }
                else
                {
                    //获得页面全路径
                    string uri = this.Page.Request.Url.AbsoluteUri;
                    url = uri;
                    //获得页面的URL
                    url = url.Substring(uri.LastIndexOf('/') + 1);
                }
                return url;
            }
        }


        #endregion

        #region 角色权限控制
        private string RoleSet
        {
            get
            {
                if (string.IsNullOrEmpty(roleOperate))
                {
                    //获得MemuCode
                    roleOperate = "";
                    Sys_MenuBLL dalMenu = new Sys_MenuBLL();
                    model = dalMenu.FindByURL(url);
                    if (model != null)
                    {
                        Sys_RolePurviewBLL dalRole = new Sys_RolePurviewBLL();
                        List<Sys_RolePurview> roleList = dalRole.FindByRoleAndMenu(bg.CurrUserInfo().RoleID, model.Menu_Code);
                        foreach (Sys_RolePurview role in roleList)
                        {
                            if (!string.IsNullOrEmpty(role.RolePurview_OperatePurview))
                            {
                                if (!roleOperate.Contains(role.RolePurview_OperatePurview))
                                {
                                    roleOperate += role.RolePurview_OperatePurview + ",";
                                }
                            }
                        }
                    }
                }
                return roleOperate;
            }
        }

        #region RoleSet 按钮属性
        private int editType = 1;
        private int copyType = 6;
        private int operatingType = 1;
        private int delType = 2;
        private int submitType = 97;
        private int openType = 96;
        private int checkOff = 99;
        private int auditType = 1;
        private int trackType = 1;
        private int inspectionType = 1;
        private int configureType = 1;
        private int resolveType = 1;
        private int revokeType = 2;
        private int writebackType = 1;
        private int detailType = 4;
        private int scoreType = 1;
        private int additionalBudgetType = 1;
        private int modifyAdditionalBudgetType = 1;
        private int deleteBudgetType = 2;
        private int auditBudgetType = 1;
        private int downloadType = 2;
        private int screenType = 3;
        private int chooseType = 4;
        private int setResetScreenType = 5;
        private int printType = 5;
        private int signingType = 1;
        private int decompressionType = 1;
        private int renewalType = 1;
        private int claimType = 1;
        private int distributionType = 3;
        private int receiveType = 95;
        private int lockType = 95;
        private int CancelCheck = 94;

        private int userGotoworkType = 101;

        private int userLeaveType = 102;

        private int userlockType = 103;

        private int exportExcelType = 5;

        private int fileType = 1;

        private int recordType = 4;

        private int returnType = 1;

        private int cancelType = 1;

        private int activeType = 1;

        private int dhcsType = 1;

        private int xccsType = 1;

        private int csclType = 1;

        private int dhhfType = 1;

        private int ywdelType = 1;

        /// <summary>
        /// 编辑属性(1.链接2.事件,3.弹出框,4.新页面,5.自定义)
        /// </summary>
        public int EditType { get { return editType; } set { editType = value; } }
        /// <summary>
        /// 录入属性
        /// </summary>
        public int CopyType { get { return copyType; } set { copyType = value; } }
        /// <summary>
        /// 录入属性
        /// </summary>
        public int OperatingType { get { return operatingType; } set { operatingType = value; } }
        /// <summary>
        /// 删除属性
        /// </summary>
        public int DelType { get { return delType; } set { delType = value; } }
        /// <summary>
        /// 提交
        /// </summary>
        public int SubmitType { get { return submitType; } set { submitType = value; } }
        /// <summary>
        /// 提交
        /// </summary>
        public int OpenType { get { return openType; } set { openType = value; } }
        /// <summary>
        /// 核销属性
        /// </summary>
        public int CheckOff { get { return checkOff; } set { checkOff = value; } }
        /// <summary>
        /// 审核属性
        /// </summary>
        public int AuditType { get { return auditType; } set { auditType = value; } }
        /// <summary>
        /// 配置属性
        /// </summary>
        public int ConfigureType { get { return configureType; } set { configureType = value; } }
        /// <summary>
        /// 分解属性
        /// </summary>
        public int ResolveType { get { return resolveType; } set { resolveType = value; } }
        /// <summary>
        /// 撤销分解属性
        /// </summary>
        public int RevokeType { get { return revokeType; } set { revokeType = value; } } 
            /// <summary>
        /// 冲红属性
        /// </summary>
        public int WritebackType { get { return writebackType; } set { writebackType = value; } }
        /// <summary>
        /// 查看属性
        /// </summary>
        public int DetailType { get { return detailType; } set { detailType = value; } }
        /// <summary>
        /// 分值调整属性
        /// </summary>
        public int ScoreType { get { return scoreType; } set { scoreType = value; } }
        /// <summary>
        /// 追加预算值属性
        /// </summary>
        public int AdditionalBudgetType { get { return additionalBudgetType; } set { additionalBudgetType = value; } }
        /// <summary>
        /// 追加预算修改属性
        /// </summary>
        public int ModifyAdditionalBudgetType { get { return modifyAdditionalBudgetType; } set { modifyAdditionalBudgetType = value; } }
        /// <summary>
        /// 删除追加预算属性
        /// </summary>
        public int DeleteBudgetType { get { return deleteBudgetType; } set { deleteBudgetType = value; } }
        /// <summary>
        /// 审核追加预算属性
        /// </summary>
        public int AuditBudgetType { get { return auditBudgetType; } set { auditBudgetType = value; } }
        /// <summary>
        /// 下载属性
        /// </summary>
        public int DownloadType { get { return downloadType; } set { downloadType = value; } }
        /// <summary>
        /// 追踪属性
        /// </summary>
        public int TrackType { get { return trackType; } set { trackType = value; } }

        /// <summary>
        /// 筛选
        /// </summary>
        public int ScreenType { get { return screenType; } set { screenType = value; } }

        /// <summary>
        /// 选取
        /// </summary>
        public int ChooseType { get { return chooseType; } set { chooseType = value; } }

        /// <summary>
        /// 重置筛选
        /// </summary>
        public int SetResetScreenType { get { return setResetScreenType; } set { setResetScreenType = value; } }

        /// <summary>
        /// 签约
        /// </summary>
        public int SigningType { get { return signingType; } set { signingType = value; } }

        /// <summary>
        /// 打印
        /// </summary>
        public int PrintType { get { return printType; } set { printType = value; } }

        /// <summary>
        /// 解押
        /// </summary>
        public int DecompressionType { get { return decompressionType; } set { decompressionType = value; } }

        /// <summary>
        /// 续保
        /// </summary>
        public int RenewalType { get { return renewalType; } set { renewalType = value; } }

        /// <summary>
        /// 理赔
        /// </summary>
        public int ClaimType { get { return claimType; } set { claimType = value; } }

        /// <summary>
        /// 分配
        /// </summary>
        public int DistributionType { get { return distributionType; } set { distributionType = value; } }

        /// <summary>
        /// 分配
        /// </summary>
        public int ReceiveType { get { return receiveType; } set { receiveType = value; } }

        /// <summary>
        /// 锁定
        /// </summary>
        public int LockType { get { return lockType; } set { lockType = value; } }


        /// <summary>
        /// 用户上班
        /// </summary>
        public int UserGotoworkType { get { return this.userGotoworkType; } set { this.userGotoworkType = value; } }

        /// <summary>
        /// 用户请假
        /// </summary>
        public int UserLeaveType { get { return this.userLeaveType; } set { this.userLeaveType = value; } }

        /// <summary>
        /// 用户锁定
        /// </summary>
        public int UserlockType { get { return this.userlockType; } set { this.userlockType = value; } }


        /// <summary>
        /// 导出Excel
        /// </summary>
        public int ExportExcelType { get { return exportExcelType; } set { exportExcelType = value; } }

        /// <summary>
        /// 归档属性 
        /// </summary>
        public int FileType { get { return fileType; } set { fileType = value; } }

        /// <summary>
        /// 履历属性
        /// </summary>
        public int RecordType { get { return recordType; } set { recordType = value; } }

        /// <summary>
        /// 归还属性
        /// </summary>
        public int ReturnType { get { return returnType; } set { returnType = value; } }

        /// <summary>
        /// 取消
        /// </summary>
        public int CancelType { get { return cancelType; } set { cancelType = value; } }

        /// <summary>
        /// 激活属性
        /// </summary>
        public int ActiveType { get { return activeType; } set { activeType = value; } }

        /// <summary>
        /// 电话催收
        /// </summary>
        public int DHCSType { get { return dhcsType; } set { dhcsType = value; } }

        /// <summary>
        /// 现场催收
        /// </summary>
        public int XCCSType { get { return xccsType; } set { xccsType = value; } }

        /// <summary>
        /// 催收车辆
        /// </summary>
        public int CSCLType { get { return csclType; } set { csclType = value; } }

        /// <summary>
        /// 电话回访
        /// </summary>
        public int DHHFType { get { return dhhfType; } set { dhhfType = value; } }

        /// <summary>
        /// 业务删除属性
        /// </summary>
        public int YWDelType { get { return ywdelType; } set { ywdelType = value; } }

        #endregion

        #region Toolbar按钮属性
        private int addType = 1;
        private int biaoJiType = 1;
        private int auditBatchType = 2;
        private int deltBatchType = 2;
        private int addCourseType = 1;
        private int launchType = 1;
        private int addVidoType = 1;
        private int redoType = 1;
        private int undoType = 5;
        private int resetScreenType = 5;
        private int calculatorType = 3;
        private int addPersonageType = 1;
        private int addCompanyType = 1;
        private int batchCheckOff = 99;
        private int batchSendSms = 98;
        private int batchDistributionType = 3;
        private int batchReceiveType = 95;
        private int batchLockType = 95;
        private int borrowType = 1;

        /// <summary>
        /// 新增属性
        /// </summary>
        public int AddType { get { return addType; } set { addType = value; } }

        /// <summary>
        /// 标记属性
        /// </summary>
        public int BiaoJiType { get { return biaoJiType; } set { biaoJiType = value; } }
        /// <summary>
        /// 批量审核属性
        /// </summary>
        public int AuditBatchType { get { return auditBatchType; } set { auditBatchType = value; } }
        /// <summary>
        /// 批量删除属性
        /// </summary>
        public int DeltBatchType { get { return deltBatchType; } set { deltBatchType = value; } }
        /// <summary>
        /// 批量核销属性
        /// </summary>
        public int BatchCheckOff { get { return batchCheckOff; } set { batchCheckOff = value; } }
        /// <summary>
        /// 批量发短信
        /// </summary>
        public int BatchSendSms { get { return batchSendSms; } set { batchSendSms = value; } }
        /// <summary>
        /// 添加增加文字课程属性
        /// </summary>
        public int AddCourseType { get { return addCourseType; } set { addCourseType = value; } }
        /// <summary>
        /// 发起属性
        /// </summary>
        public int LaunchType { get { return launchType; } set { launchType = value; } }
        /// <summary>
        /// 增加视频课程属性
        /// </summary>
        public int AddVidoType { get { return addVidoType; } set { addVidoType = value; } }
        /// <summary>
        /// 导入属性
        /// </summary>
        public int RedoType { get { return redoType; } set { redoType = value; } }
        /// <summary>
        /// 导出属性
        /// </summary>
        public int UndoType { get { return undoType; } set { undoType = value; } }

        /// <summary>
        /// 导出属性
        /// </summary>
        public int ResetScreenType { get { return resetScreenType; } set { resetScreenType = value; } }

        /// <summary>
        /// 计算器属性
        /// </summary>
        public int CalculatorType { get { return calculatorType; } set { calculatorType = value; } }

        /// <summary>
        /// 新增个人
        /// </summary>
        public int AddPersonageType { get { return addPersonageType; } set { addPersonageType = value; } }

        /// <summary>
        /// 新增企业
        /// </summary>
        public int AddCompanyType { get { return addCompanyType; } set { addCompanyType = value; } }

        /// <summary>
        /// 批量分配
        /// </summary>
        public int BatchDistributionType { get { return batchDistributionType; } set { batchDistributionType = value; } }

        /// <summary>
        /// 领单
        /// </summary>
        public int BatchReceiveType { get { return batchReceiveType; } set { batchReceiveType = value; } }

        /// <summary>
        /// 批量锁定
        /// </summary>
        public int BatchLockType { get { return batchLockType; } set { batchLockType = value; } }

        /// <summary>
        /// 借出
        /// </summary>
        public int BorrowType { get { return borrowType; } set { borrowType = value; } }


        #endregion


        /// <summary>
        /// Toolbar控件展示
        /// </summary>
        /// <returns></returns>
        protected List<Toolbar> ToolbarButton()
        {
            List<Toolbar> list = new List<Toolbar>();

            if (!string.IsNullOrEmpty(RoleSet))
            {
                if (RoleSet.Contains("新增"))
                {
                    list.Add(new Toolbar() { Id = "AddButton", Name = "新增", Icon = "add", Type = AddType, Url = AddURL });
                }
                if (RoleSet.Contains("标记"))
                {
                    list.Add(new Toolbar() { Id = "BiaoJiButton", Name = "标记", Icon = "add", Type = BiaoJiType, Url = BiaoJiURL });
                }
                if (RoleSet.Contains("批量审核"))
                {
                    if (IsShowCheckBox)
                    {
                        list.Add(new Toolbar() { Id = "AuditBatchButton", Name = "批量审核", Icon = "audit", Type = AuditBatchType });
                    }
                }
                if (RoleSet.Contains("批量删除"))
                {
                    if (IsShowCheckBox)
                    {
                        list.Add(new Toolbar() { Id = "DeltBatchButton", Name = "批量删除", Icon = "no", Type = DeltBatchType });
                    }
                }
                if (RoleSet.Contains("批量核销"))
                {
                    if (IsShowCheckBox)
                    {
                        list.Add(new Toolbar() { Id = "BatchCheckOffButton", Name = "批量核销", Icon = "checkoff", Type = BatchCheckOff });
                    }
                }
                if (RoleSet.Contains("批量发短信"))
                {
                    if (IsShowCheckBox)
                    {
                        list.Add(new Toolbar() { Id = "BatchSendSmsButton", Name = "批量发短信", Icon = "sendsms", Type = BatchSendSms });
                    }
                }
                //if (RoleSet.Contains("增加文字课程"))
                //{
                //    list.Add(new Toolbar() { Id = "AddCourseButton", Name = "增加文字课程", Icon = "add", Type = AddCourseType, Url = CourseURL });
                //}

                //if (RoleSet.Contains("发起"))
                //{
                //    list.Add(new Toolbar() { Id = "LaunchButton", Name = "发起", Icon = "redo", Type = LaunchType, Url = LaunchURL });
                //}

                //if (RoleSet.Contains("增加视频课程"))
                //{
                //    list.Add(new Toolbar() { Id = "AddVidoButton", Name = "增加视频课程", Icon = "add", Type = AddVidoType, Url = VidoURL });
                //}

                if (RoleSet.Contains("【导入】"))
                {
                    list.Add(new Toolbar() { Id = "RedoButton", Name = "导入", Icon = "redo", Type = RedoType, Url = RedoURL });
                }

                if (RoleSet.Contains("邮储银行导入"))
                {
                    list.Add(new Toolbar() { Id = "ImportYouChuButton", Name = "邮储银行导入", Icon = "redo", Type = calculatorType, Url = ImportYouChuURL });
                }

                if (RoleSet.Contains("农业银行导入"))
                {
                    list.Add(new Toolbar() { Id = "ImportNongHangButton", Name = "农业银行导入", Icon = "redo", Type = calculatorType, Url = ImportNongHangURL });
                }

                if (RoleSet.Contains("工商银行导入"))
                {
                    list.Add(new Toolbar() { Id = "ImportICBCButton", Name = "工商银行导入", Icon = "redo", Type = calculatorType, Url = ImportICBCURL });
                }

                if (RoleSet.Contains("导出"))
                {
                    list.Add(new Toolbar() { Id = "UndoButton", Name = "导出", Icon = "undo", Type = UndoType, Url = UndoURL });
                }

                if (RoleSet.Contains("产品计算器"))
                {
                    list.Add(new Toolbar() { Id = "CalculatorButton", Name = "产品计算器", Icon = "calculator", Type = CalculatorType, Url = CalculatorURL });
                }

                if (RoleSet.Contains("【新$增个人回租】"))
                {
                    list.Add(new Toolbar() { Id = "PersonageButton", Name = "新增个人回租", Icon = "gerenhuizu", Type = AddPersonageType, Url = PersonageURL });
                }

                if (RoleSet.Contains("【新$增企业回租】"))
                {
                    list.Add(new Toolbar() { Id = "CompanyButton", Name = "新增企业回租", Icon = "qiyehuizu", Type = AddCompanyType, Url = CompanyURL });
                }
                if (RoleSet.Contains("【新$增个人直租】"))
                {
                    list.Add(new Toolbar() { Id = "PersonageDirectButton", Name = "新增个人直租", Icon = "gerenzhizu", Type = AddPersonageType, Url = PersonageDirectURL });
                }

                if (RoleSet.Contains("【新$增企业直租】"))
                {
                    list.Add(new Toolbar() { Id = "CompanyDirectButton", Name = "新增企业直租", Icon = "qiyezhizu", Type = AddCompanyType, Url = CompanyDirectURL });
                }

                if (RoleSet.Contains("提前还款"))
                {
                    list.Add(new Toolbar() { Id = "AdvancePayButton", Name = "提前还款", Icon = "calculator", Type = addType, Url = AdvancePayURL });
                }

                if (RoleSet.Contains("批量分配"))
                {
                    list.Add(new Toolbar() { Id = "BatchDistributionButton", Name = "批量分配", Icon = "distribution", Type = BatchDistributionType, Url = DistributionURL });
                }

                if (RoleSet.Contains("批量领单"))
                {
                    list.Add(new Toolbar() { Id = "BatchReceiveButton", Name = "批量领单", Icon = "distribution", Type = BatchReceiveType });
                }

                if (RoleSet.Contains("批量锁定"))
                {
                    list.Add(new Toolbar() { Id = "BatchLockButton", Name = "批量锁定", Icon = "lock", Type = BatchLockType });
                }

                if (RoleSet.Contains("导 出Excel"))
                {
                    list.Add(new Toolbar() { Id = "ExportExcelButton", Name = "导出Excel", Icon = "excel", Type = ExportExcelType, Url = model.Menu_Name });
                }

                if (RoleSet.Contains("借出"))
                {
                    list.Add(new Toolbar() { Id = "BorrowButton", Name = "借出", Icon = "add", Type = BorrowType, Url = BorrowURL });
                }

                //if (RoleSet.Contains("筛 选"))
                //{
                //    list.Add(new Toolbar() { Id = "ScreenButton", Name = "筛选", Icon = "search", Type = ScreenType, Url = ScreenURL });
                //}
                //if (RoleSet.Contains("重置筛选"))
                //{
                //    list.Add(new Toolbar() { Id = "ResetScreenButton", Name = "重置筛选", Icon = "search", Type = ResetScreenType, Url = ResetScreenURL });
                //}
            }
            return list;
        }


        /// <summary>
        /// 操作按钮属性
        /// </summary>
        /// <returns></returns>
        protected List<Toolbar> RoleSetButton()
        {
            List<Toolbar> list = new List<Toolbar>();
            if (RoleSet.Contains("编辑"))
            {
                list.Add(new Toolbar() { Id = "EditButton", Name = "编辑", Icon = "edit", Type = EditType, Url = ModifyURL });
            }
            if (RoleSet.Contains("拷贝"))
            {
                list.Add(new Toolbar() { Id = "CopyButton", Name = "拷贝", Icon = "edit", Type = CopyType, Url = CopyURL });
            }
            if (RoleSet.Contains("录入"))
            {
                list.Add(new Toolbar() { Id = "OperatingButton", Name = "录入", Icon = "redo", Type = OperatingType, Url = OperatingURL });
            }
            if (RoleSet.Contains("办理"))
            {
                list.Add(new Toolbar() { Id = "OperatingButton", Name = "办理", Icon = "redo", Type = OperatingType, Url = OperatingURL });
            }
            if (RoleSet.Contains("签约"))
            {
                list.Add(new Toolbar() { Id = "SigningButton", Name = "签约", Icon = "signing", Type = SigningType, Url = SigningURL });

            }
            if (RoleSet.Contains("解押"))
            {
                list.Add(new Toolbar() { Id = "DecompressionButton", Name = "解押", Icon = "decompression", Type = DecompressionType, Url = DecompressionURL });
            }
            if (RoleSet.Contains("续保"))
            {
                list.Add(new Toolbar() { Id = "RenewalButton", Name = "续保", Icon = "renewal", Type = RenewalType, Url = RenewalURL });
            }
            if (RoleSet.Contains("理赔"))
            {
                list.Add(new Toolbar() { Id = "ClaimButton", Name = "理赔", Icon = "claim", Type = ClaimType, Url = ClaimURL });
            }
            if (RoleSet.Contains("提交"))
            {
                list.Add(new Toolbar() { Id = "SubmitButton", Name = "提交", Icon = "submit", Type = SubmitType, Url = SubmitURL });
            }
            if (RoleSet.Contains("启用"))
            {
                list.Add(new Toolbar() { Id = "OpenButton", Name = "启用", Icon = "switch", Type = OpenType });
            }

            if (RoleSet.Contains("核销"))
            {
                list.Add(new Toolbar() { Id = "CheckOffButton", Name = "核销", Icon = "checkoff", Type = CheckOff });
            }
            if (RoleSet.Contains("撤回核销"))
            {
                list.Add(new Toolbar() { Id = "CancelCheckButton", Name = "撤回核销", Icon = "redo", Type = CancelCheck });
            }
            if (RoleSet.Contains("审核"))
            {
                list.Add(new Toolbar() { Id = "AuditButton", Name = "审核", Icon = "audit", Type = AuditType, Url = AuditURL });
            }
            if (RoleSet.Contains("配置"))
            {
                list.Add(new Toolbar() { Id = "ConfigureButton", Name = "配置", Icon = "configure", Type = ConfigureType, Url = ConfigureURL });
            }
            if (RoleSet.Contains("分解"))
            {
                list.Add(new Toolbar() { Id = "ResolveButton", Name = "分解", Icon = "audit", Type = ResolveType, Url = ResolveURL });
            }
            if (RoleSet.Contains("撤销分解"))
            {
                list.Add(new Toolbar() { Id = "RevokeButton", Name = "撤销分解", Icon = "audit", Type = RevokeType, Url = RevokeURL });
            }
            if (RoleSet.Contains("冲红"))
            {
                list.Add(new Toolbar() { Id = "WriteBackButton", Name = "冲红", Icon = "undo", Type = WritebackType, Url = WritebackURL });
            }
            if (RoleSet.Contains("查看"))
            {
                list.Add(new Toolbar() { Id = "DetailButton", Name = "查看", Icon = "search", Type = DetailType, Url = DetailURL });
            }
            if (RoleSet.Contains("删除"))
            {
                list.Add(new Toolbar() { Id = "DelButton", Name = "删除", Icon = "no", Type = DelType });
            }
            if (RoleSet.Contains("分值调整"))
            {
                list.Add(new Toolbar() { Id = "ScoreButton", Name = "分值调整", Icon = "search", Type = ScoreType, Url = ScoreURL });
            }

            //if (RoleSet.Contains("追加预算值"))
            //{
            //    list.Add(new Toolbar() { Id = "AdditionalBudgetButton", Name = "追加预算值", Icon = "add", Type = AdditionalBudgetType, Url = AdditionalBudgetURL });
            //}
            //if (RoleSet.Contains("有条件删除"))
            //{
            //    list.Add(new Toolbar() { Id = "ModifyAdditionalBudgetButton", Name = "有条件删除", Icon = "edit", Type = ModifyAdditionalBudgetType, Url = ModifyAdditionalBudgetURL });
            //}
            //if (RoleSet.Contains("删除追加预算"))
            //{
            //    list.Add(new Toolbar() { Id = "DeleteBudgetButton", Name = "删除追加预算", Icon = "no", Type = DeleteBudgetType });
            //}
            //if (RoleSet.Contains("审核追加预算"))
            //{
            //    list.Add(new Toolbar() { Id = "AuditBudgetButton", Name = "审核追加预算", Icon = "no", Type = AuditBudgetType, Url = AuditAdditionalBudgetURL });
            //}
            if (RoleSet.Contains("下载"))
            {
                list.Add(new Toolbar() { Id = "DownloadButton", Name = "下载", Icon = "download", Type = DownloadType, Url = DownloadURL });
            }
            if (RoleSet.Contains("追踪"))
            {
                list.Add(new Toolbar() { Id = "TrackButton", Name = "追踪", Icon = "search", Type = TrackType, Url = TrackURL });
            }
            if (RoleSet.Contains("督办"))
            {
                list.Add(new Toolbar() { Id = "InspectionButton", Name = "督办", Icon = "print", Type = inspectionType, Url = InspectionURL });
            }
            if (RoleSet.Contains("选取"))
            {
                list.Add(new Toolbar() { Id = "ChooseButton", Name = "选取", Icon = "search", Type = ChooseType, Url = ChooseURL });
            }
            if (RoleSet.Contains("重置 筛选"))
            {
                list.Add(new Toolbar() { Id = "ResetScreenButton", Name = "重置筛选", Icon = "search", Type = SetResetScreenType, Url = SetResetScreenURL });
            }

            if (RoleSet.Contains("打印"))
            {
                list.Add(new Toolbar() { Id = "PrintButton", Name = "打印", Icon = "print", Type = PrintType, Url = PrintURL });

            }

            if (RoleSet.Contains("分配"))
            {
                list.Add(new Toolbar() { Id = "DistributionButton", Name = "分配", Icon = "distribution", Type = DistributionType, Url = DistributionURL });
            }

            if (RoleSet.Contains("领单"))
            {
                list.Add(new Toolbar() { Id = "ReceiveButton", Name = "领单", Icon = "distribution", Type = ReceiveType });
            }

            if (RoleSet.Contains("【锁定】"))
            {
                list.Add(new Toolbar() { Id = "LockButton", Name = "锁定", Icon = "lock", Type = LockType });
            }


            if (RoleSet.Contains("上班模式"))
            {
                list.Add(new Toolbar() { Id = "UserGotoWorkButton", Name = "上班模式", Icon = "userGotoWork", Type = this.UserGotoworkType });
            }
            if (RoleSet.Contains("请假模式"))
            {
                list.Add(new Toolbar() { Id = "UserLeaveButton", Name = "请假模式", Icon = "userLeave", Type = this.UserLeaveType });
            }
            if (RoleSet.Contains("【锁定账号】"))
            {
                list.Add(new Toolbar() { Id = "UserLockButton", Name = "锁定账号", Icon = "userLock", Type = this.UserlockType });
            }
            if (RoleSet.Contains("归档"))
            {
                list.Add(new Toolbar() { Id = "FileButton", Name = "归档", Icon = "edit", Type = FileType, Url = FileURL });
            }
            if (RoleSet.Contains("履历"))
            {
                list.Add(new Toolbar() { Id = "RecordButton", Name = "履历", Icon = "search", Type = RecordType, Url = RecordURL });
            }
            if (RoleSet.Contains("归还"))
            {
                list.Add(new Toolbar() { Id = "ReturnButton", Name = "归还", Icon = "edit", Type = ReturnType, Url = ReturnURL });
            }
            if (RoleSet.Contains("取消"))
            {
                list.Add(new Toolbar() { Id = "CancelButton", Name = "取消", Icon = "no", Type = CancelType, Url = CancelURL });
            }
            if (RoleSet.Contains("激活"))
            {
                list.Add(new Toolbar() { Id = "ActiveButton", Name = "激活", Icon = "edit", Type = ActiveType, Url = ActiveURL });
            }
            if (RoleSet.Contains("电话催收"))
            {
                list.Add(new Toolbar() { Id = "DHCSButton", Name = "电话催收", Icon = "add", Type = DHCSType, Url = DHCSURL });
            }
            if (RoleSet.Contains("现场催收"))
            {
                list.Add(new Toolbar() { Id = "XCCSButton", Name = "现场催收", Icon = "add", Type = XCCSType, Url = XCCSURL });
            }
            if (RoleSet.Contains("催收车辆"))
            {
                list.Add(new Toolbar() { Id = "CSCLButton", Name = "催收车辆", Icon = "add", Type = CSCLType, Url = CSCLURL });
            }
            if (RoleSet.Contains("电话回访"))
            {
                list.Add(new Toolbar() { Id = "DHHFButton", Name = "电话回访", Icon = "add", Type = DHHFType, Url = DHHFURL });
            }
            if (RoleSet.Contains("删YW除"))
            {
                list.Add(new Toolbar() { Id = "YWDelButton", Name = "删除", Icon = "no", Type = YWDelType, Url = YWDelURL });
            }
            return list;
        }
        #endregion

        #region 设置表格主键

        public string SetKey()
        {
            string strurl = "";
            string key = TableKey;

            if (TableKeyField != "")
            {
                string[] temp = TableKeyField.Split(',');
                foreach (var rows in temp)
                {
                    strurl += "&" + rows + "=\"+rec." + rows + "+ \"";
                    //" + rec.<%=TableKey %> + "
                }
                return strurl;
            }
            else
            {
                return strurl;
            }
        }

        public string vSetKey()
        {
            string strurl = "";
            string key = TableKey;

            if (TableKeyField != "")
            {
                string[] temp = TableKeyField.Split(',');
                foreach (var rows in temp)
                {
                    strurl += "&" + rows + "=\'+rec." + rows + "+ \'";
                    //' + rec.<%=TableKey %> + '
                    //" + rec.<%=TableKey %> + "
                }
                return strurl;
            }
            else
            {
                return strurl;
            }
        }

        #endregion

        #region 主程序（绑定数据）
        /// <summary>
        /// 主程序
        /// </summary>
        /// <param name="page">第几页（从1开始）</param>
        /// <param name="rows">每页显示的条数</param>
        /// <param name="url">页面url地址</param>
        /// <param name="SQLWhere">条件</param>
        /// <returns></returns>
        public string jsonPerson()
        {
            Common_BLL pg = new Common_BLL();
            int totalcount = int.MinValue;
            int totalpagecount = int.MinValue;

            if (Datas.IsNull())
            {

                BasicVariable();//基本变量赋值

                Where();//执行获取查看范围
            }

            if (TableView.IsNullOrEmpty())
            {
                TableView = GetGridAttr("TableView");
            }

            #region 读取数据
            DataTable list_x = null;
            DataSet ds;
            if (Datas.IsNull())
            {
                if (TableView != "")
                {
                    //判断是否需要分页
                    if (IsShowPagin)
                    {
                        ds = pg.PageData(TableView, SQLField, TableKey, SQLOrder, PageSize, PageIndex, SQLWhere, out totalcount, out totalpagecount);
                    }
                    else
                    {
                        ds = pg.GetData(TableView, SQLField, SQLWhere, SQLOrder);
                    }
                }
                else
                {
                    //判断是否需要分页
                    if (IsShowPagin)
                    {
                        ds = pg.PageData(TableName, SQLField, TableKey, SQLOrder, PageSize, PageIndex, SQLWhere, out totalcount, out totalpagecount);
                    }
                    else
                    {
                        ds = pg.GetData(TableName, SQLField, SQLWhere, SQLOrder);
                    }
                }
                TotalCount = totalcount;
            }
            else
            {
                ds = Datas;
            }

            if (ds.Tables.Count > 0)
            {
                list_x = ds.Tables[0];
                #region 处理字段长度
                //处理字段长度
                foreach (DataRow rews in list_x.Rows)
                {
                    foreach (XElement e in Node)
                    {
                        if (e.Attribute("MaxLength") != null && rews[e.Attribute("Key").Value] != null)
                        {
                            rews[e.Attribute("Key").Value] = rews[e.Attribute("Key").Value].ToString().IfNullReturnEmpty().SubstringByByte(e.Attribute("MaxLength").Value.ToInt(10), "…");
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region 转换成josn
            //计算出总的数量为了给easyUI控件实现分页
            int total = TotalCount;
            var list = list_x;

            //使用匿名类来实现前台要求的Json格式，序列化为此格式
            var data = new { total = total, rows = list };
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();//这里使用自定义日期格式，默认是ISO8601格式     

            if (DateType == 0)
            {
                timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm";//设置时间格式
            }
            else if (DateType == 1)
            {
                timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm";//设置时间格式
            }
            else if (DateType == 2)
            {
                timeConverter.DateTimeFormat = "yyyy-MM-dd";//设置时间格式
            }
            string jsonPerson = JsonConvert.SerializeObject(data, timeConverter);
            #endregion

            return jsonPerson;
        }
        #endregion

        #region 单表删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public string DelInfo(string id)
        {
            if (id.Length > 0)
            {
                Common_BLL pg = new Common_BLL();
                Sys_MenuBLL bllMenu = new Sys_MenuBLL();
                if (pg.Delete(TableName, TableKey, "'" + id + "'", ""))
                {
                    //删除待办事项
                    new RoleConfig().DeleteMatterTasks(id.Replace("'", ""));
                    string rtnUrl = Request.RawUrl;
                    Sys_Menu MenuModel = bllMenu.FindByURL(rtnUrl);
                    if (MenuModel != null)
                    {
                        if (!string.IsNullOrEmpty(MenuModel.Menu_Name))
                        {
                            pg.AddLog(MenuModel.Menu_Name, id, "删除", "ID=" + id + "", bg.CurrUserInfo().UserID, bg.CurrUserInfo().DepartmentCode);
                        }
                    }
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        #endregion

        #region 批量删除
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public string BatchDelInfo(string id)
        {
            if (id.Length > 0)
            {
                Common_BLL pg = new Common_BLL();
                Sys_MenuBLL bllMenu = new Sys_MenuBLL();
                StringBuilder sqlstr = new StringBuilder();
                List<string> listId = new List<string>();
                listId = id.Split(',').ToList();
                foreach (var item in listId)
                {
                    sqlstr.Append("'" + item + "',");
                }
                string sql = sqlstr.ToString().Remove(sqlstr.Length - 1);
                if (pg.Delete(TableName, TableKey, sql, ""))
                {
                    //删除待办事项
                    new RoleConfig().DeleteBatchMatterTasks(sql);
                    string rtnUrl = Request.RawUrl;
                    Sys_Menu MenuModel = bllMenu.FindByURL(rtnUrl);
                    if (MenuModel != null)
                    {
                        if (!string.IsNullOrEmpty(MenuModel.Menu_Name))
                        {
                            pg.AddLog(MenuModel.Menu_Name, "", "批量删除成功", "批量删除通过：" + listId, bg.CurrUserInfo().UserID, bg.CurrUserInfo().DepartmentCode);


                        }
                    }
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        #endregion

        #region 获取查看范围
        public void Where()
        {
            string seeUrl = "";   //列表查看范围使用URL
            seeUrl = SeeChargeURL;
            if (string.IsNullOrEmpty(SeeChargeURL))
            {
                seeUrl = url;
            }

            if (IsSeeCharge != "1")  //使用查看范围
            {
                if (bg.IsLogin())
                {
                    SQLWhere = SQLWhere + " and " + new RoleConfig().GetSQlWhere(seeUrl, bg.CurrUserInfo().RoleID, SeeChargeDepCode);
                    SQLWhere = SQLWhere.Replace("{0}", bg.CurrUserInfo().UserID);
                    SQLWhere = SQLWhere.Replace("{1}", bg.CurrUserInfo().DepartmentCode);
                    SQLWhere = SQLWhere.Replace("{2}", bg.CurrUserInfo().RoleID);
                }
                else
                {
                    SQLWhere = SQLWhere + " and 1<>1";
                }
            }
        }
        #endregion

        #region 基本变量
        /// <summary>
        /// 基本变量赋值
        /// </summary>
        public void BasicVariable()
        {
            PageIndex = Request["page"] == null ? pageIndex : Request["page"].ToInt(pageIndex);
            PageSize = Request["rows"] == null ? pageSize : Request["rows"].ToInt(pageSize);
            string sort = Request["sort"] != null ? Request["sort"] : "";
            string order = Request["order"] != null ? Request["order"] : "asc";
            if (sort != "" && sort != null)
            {
                SQLOrder = sort + " " + order;
            }
            else
            {
                SQLOrder = GetGridAttr("SqlOrder");
            }
        }
        #endregion

        #region 导出Excel所有满足条件的数据
        /// <summary>
        /// 导出所有相关的数据
        /// </summary>
        public string ExportExcel(string fileName)
        {
            Common_BLL pg = new Common_BLL();

            BasicVariable();//基本变量赋值

            Where();//执行获取查看范围

            if (TableView.IsNullOrEmpty())
            {
                TableView = GetGridAttr("TableView");
            }

            string ExcelField = string.Empty;
            foreach (XElement e in Node)
            {
                ExcelField += " " + e.Attribute("Key").Value + " AS [" + e.Attribute("Name").Value + "],";
            }
            ExcelField = ExcelField.Substring(0, ExcelField.Length - 1);

            #region 读取数据
            DataTable list_x = null;
            DataSet ds = pg.GetData(TableView.IsNullOrEmpty() ? TableName : TableView, ExcelField, SQLWhere, SQLOrder);

            if (ds.Tables.Count > 0)
            {
                list_x = ds.Tables[0];
                //Sys_Menu MenuModel = new Sys_MenuBLL().FindByURL(Request.RawUrl);
                string ph = AsposeExcel.OutFileToDisk(list_x, fileName, false);
                if (ph != "0")
                {
                    return "javascript:" + ph + ","+ fileName;
                }
                else
                {
                    return "javascript:'" + ph + "',''";
                }
            }
            return "javascript:''0'',''";
            #endregion
        }
        #endregion
    }
}