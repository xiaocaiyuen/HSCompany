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
    public partial class UCButtonGrid : System.Web.UI.UserControl
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
        private bool isDbOnClickRow;
        private bool isShowCheckBox;
        private bool isShowPagin;
        private int rowClickNum = -1;

        private string seeChargeDepCode;
        private string seeChargeURL;
        private string isSeeCharge;
        private string menuURL;
        private string sQLOrder;
        private int dateType = 0;
        private string ispaf;
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
        protected string GetGridAttr(string key)
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
        /// <summary>
        /// Toolbar控件展示
        /// </summary>
        /// <returns></returns>
        protected List<Sys_MenuOperatingButton> ToolbarButton()
        {
            List<Sys_MenuOperatingButton> list = new List<Sys_MenuOperatingButton>();

            if (!string.IsNullOrEmpty(RoleSet))
            {
                string[] MenuButton = RoleSet.Split(',');
                List<Sys_MenuOperatingButton> ButtonList = model.Sys_MenuOperatingButton.Where(p => p.Type == 0 && MenuButton.Contains(p.Name)).OrderBy(p => p.Sort).ToList();

                foreach (var item in ButtonList)
                {
                    if (!item.Url.IsNullOrEmpty())
                    {
                        item.Url = GetGridAttr(item.Url);
                    }
                    list.Add(item);
                }
            }
            return list;
        }


        /// <summary>
        /// 操作按钮属性
        /// </summary>
        /// <returns></returns>
        protected List<Sys_MenuOperatingButton> RoleSetButton()
        {
            List<Sys_MenuOperatingButton> list = new List<Sys_MenuOperatingButton>();

            if (!string.IsNullOrEmpty(RoleSet))
            {
                string[] MenuButton = RoleSet.Split(',');
                List<Sys_MenuOperatingButton> ButtonList = model.Sys_MenuOperatingButton.Where(p => p.Type == 1 && MenuButton.Contains(p.Name)).OrderBy(p => p.Sort).ToList();
                foreach (var item in ButtonList)
                {
                    if (!item.Url.IsNullOrEmpty())
                    {
                        item.Url = GetGridAttr(item.Url);
                    }
                    list.Add(item);
                }
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
                    return "javascript:" + ph + "," + fileName;
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