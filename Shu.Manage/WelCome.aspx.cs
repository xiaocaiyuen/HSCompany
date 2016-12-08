using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shu.Comm;
using Shu.Model;
using Shu.BLL;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace Shu.Manage
{
    public partial class WelCome : BasePage
    {
        public Sys_NoticeBLL bllNotice = new Sys_NoticeBLL();
        public string NoticeHtml = string.Empty;//通知信息
        public string WarnHtml = string.Empty;//预警信息
        public string PendingMatterHtml = string.Empty;//待办事项Html
        public List<string> PendingMatterList = new List<string>();//待办事项集合
        public SessionUserModel userInfo = new SessionUserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userInfo = base.CurrUserInfo();
                this.LoadPendingMatterInfo();
                this.LoadInfo();
                //this.LoadWarnPersonelInfo();
                //Nzhsoft.Tester.WebCodeTimer.Initialize();
                //Response.Write(Nzhsoft.Tester.WebCodeTimer.Time("LoadPendingMatterInfo()执行时间", 1, () => { this.LoadPendingMatterInfo(); }));
            }
        }


        #region 个人预警信息

        /// <summary>
        /// 加载个人预警信息
        /// </summary>
        protected void LoadWarnPersonelInfo()
        {
            //var WarnList = new BLLWarn_Personel().FindWhere("  WarnPersonel_UserID='" + userInfo.UserID + "'").OrderBy(p => p.WarnPersonel_AddTime).Skip(0).Take(5).ToList();
            //foreach (Warn_Personel model in WarnList)
            //{
            //    string title = model.WarnPersonel_Title.ToString().Length > 13 ? model.WarnPersonel_Title.Substring(0, 13) + ".." : model.WarnPersonel_Title;
            //    WarnHtml += "<li>·<a href=javascript:showTabs('详细','/Manage/Warn/PersonelWarnInfo.aspx?id=" + model.WarnPersonel_ID + "');>" + title + "</a></li>";
            //}
        }

        #endregion

        #region 通知公告信息

        /// <summary>
        /// 加载公告信息
        /// </summary>
        protected void LoadInfo()
        {
            List<Sys_Notice> noticelist = bllNotice.GetList(p => p.IsDelete == false).OrderBy(p => p.AddTime).Skip(0).Take(8).ToList();//通知公告
            foreach (Sys_Notice model in noticelist)
            {
                string title = model.Notice_Title.ToString().Length > 18 ? model.Notice_Title.Substring(0, 18) + ".." : model.Notice_Title;
                NoticeHtml += "<li><a href=javascript:showTabs('详细','/Manage/Sys/NoticeShow.aspx?id=" + model.NoticeID + "');>" + title + "</a></li>";
            }
        }

        #endregion

        /// <summary>
        /// 加载个人静态风险
        /// </summary>
        /// <returns></returns>
        public string LoadPerRiskHtml()
        {
            string perRiskHtml = string.Empty;
            string sqlString = "select * from View_Risk_PerRiskLib where UserInfo_DepCode like '" + userInfo.DepartmentCode + "%' order by Department_Sequence,UserInfo_Sequence";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sqlString);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            perRiskHtml += "<tr>";
                            perRiskHtml += "<td>";
                            perRiskHtml += ds.Tables[0].Rows[i]["UserInfo_FullName"].ToString();
                            perRiskHtml += "</td>";
                            perRiskHtml += " <td>";
                            perRiskHtml += ds.Tables[0].Rows[i]["score"].ToString();
                            perRiskHtml += "</td>";
                            perRiskHtml += "<td>";
                            perRiskHtml += " <span style=\" color:Red;\">" + ds.Tables[0].Rows[i]["dj"].ToString() + "</span>";
                            perRiskHtml += "</td>";
                            perRiskHtml += " </tr>";
                        }
                    }
                }
            }
            return perRiskHtml;
        }


        /// <summary>
        /// 加载部门静态风险
        /// </summary>
        /// <returns></returns>
        public string LoadDepRiskHtml()
        {
            string depRiskHtml = string.Empty;
            string sqlString = "select * from View_Risk_DepRisKLib where Department_Code like '" + userInfo.DepartmentCode + "%'  order by Department_Level,Department_Sequence";
            Database db = DatabaseFactory.CreateDatabase("dfdf");
            DbCommand dbCommand = db.GetSqlStringCommand(sqlString);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            depRiskHtml += "<tr>";
                            depRiskHtml += "<td>";
                            depRiskHtml += ds.Tables[0].Rows[i]["Department_Name"].ToString();
                            depRiskHtml += "</td>";
                            depRiskHtml += " <td>";
                            depRiskHtml += ds.Tables[0].Rows[i]["Score"].ToString();
                            depRiskHtml += "</td>";
                            depRiskHtml += "<td>";
                            depRiskHtml += " <span style=\" color:Red;\">" + ds.Tables[0].Rows[i]["dj"].ToString() + "</span>";
                            depRiskHtml += "</td>";
                            depRiskHtml += " </tr>";
                        }
                    }
                }
            }
            return depRiskHtml;
        }

        /// <summary>
        /// 获取待办事项信息(取前8条)
        /// </summary>
        /// <returns></returns>
        public string GetPendingMatterInfo()
        {
            List<string> list = PendingMatterList.Skip(0).Take(5).ToList();
            foreach (string item in list)
            {
                PendingMatterHtml += item;
            }
            return PendingMatterHtml;
        }

        #region 其他待办事项
        /// <summary>
        /// 其他待办事项(PendingMatter_ToRoleName:多角色 userInfo.RoleName:多角色)
        /// </summary>
        public void LoadPendingMatterInfo()
        {
            string sqlString = "exec GetPendingMatterRecord '" + userInfo.RoleName + "','" + userInfo.UserID + "'";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sqlString);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string title = ds.Tables[0].Rows[i]["PendingMatter_Title"].ToString();
                            string url = ds.Tables[0].Rows[i]["PendingMatter_URL"].ToString();
                            string iCount = ds.Tables[0].Rows[i]["iCount"].ToString();
                            if (title.Contains("士官选取") || title.Contains("干部晋升"))
                            {
                            }
                            else
                            {
                                title = string.Format(Constant.TesksAuditTitle, title, iCount);
                            }
                            PendingMatterList.Add("<li>·<a href=javascript:showTabs('待办事项','" + url + "');>" + title + "</a></li>");
                        }
                    }
                }
            }
        }
        #endregion
    }
}