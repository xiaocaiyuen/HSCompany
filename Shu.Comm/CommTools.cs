using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Xml.Linq;
using System.Web.UI.WebControls;
using Shu.Model;
using System.Data;

namespace Shu.Comm
{
    public class CommTools : System.Web.UI.Page
    {
        /// <summary>
        /// 获得webcofig中配置的名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetConfigName(string strName)
        {
            string name = "";
            if (ConfigurationManager.AppSettings[strName] != null)
            {
                name = ConfigurationManager.AppSettings[strName].ToString();
            }
            return name;
        }

        /// <summary>
        /// 获得系统的名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetXitongMingCheng()
        {
            string name = "";
            string sql = "select * from Sys_Setting where Setting_ValueType='1'";

            DataTable Setting = SqlHelper.GetTable(sql);
            DataSet ds = new DataSet();
            ds.Tables.Add(Setting);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                
                if (ds.Tables[0].Rows[i]["Setting_Key"].ToString() == "XiTongMingCheng")
                {
                    name = ds.Tables[0].Rows[i]["Setting_Value"].ToString();
                }
            }
                            
            return name;
        }
        /// <summary>
        /// 获取角色定义信息
        /// </summary>
        /// <param name="roleKey"></param>
        /// <returns></returns>
        public static string GetRoleDefinitionInfo(string roleKey)
        {
            string rtnRoleName = string.Empty;
            string filePath = HttpContext.Current.Server.MapPath("~/XML/RoleDefinition.xml");
            XDocument doc = XDocument.Load(filePath);
            XElement root = doc.Element("Role");
            IEnumerable<XElement> elements = from e in root.Elements("Name")
                                             where e.Attribute("key").Value == roleKey
                                             select e;
            foreach (XElement childElement in elements)
            {
                rtnRoleName = childElement.Attribute("value").Value;
            }
            return rtnRoleName;
        }


        /// <summary>
        /// 获取客户端真实IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string user_IP = string.Empty;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                user_IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                user_IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return user_IP;
        }


        /// <summary>
        /// 设置工作总结范围
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetSummaryRangeDropDownList(DropDownList ddl)
        {
            //ddl.Items.Add(new ListItem(Constant.SummaryRange_YearName, Constant.SummaryRange_Year));
            ////ddl.Items.Add(new ListItem(Constant.SummaryRange_HalfYearName, Constant.SummaryRange_HalfYear));
            //ddl.Items.Add(new ListItem(Constant.SummaryRange_QuarterName, Constant.SummaryRange_Quarter));
            //ddl.Items.Add(new ListItem(Constant.SummaryRange_MonthName, Constant.SummaryRange_Month));
        }

        /// 
        /// 将一位数字转换成中文数字
        /// 
        public static string ConvertChinese(string str)
        {
            string cstr = "";
            switch (str)
            {
                case "0": cstr = "零"; break;
                case "1": cstr = "一"; break;
                case "2": cstr = "二"; break;
                case "3": cstr = "三"; break;
                case "4": cstr = "四"; break;
                case "5": cstr = "五"; break;
                case "6": cstr = "六"; break;
                case "7": cstr = "七"; break;
                case "8": cstr = "八"; break;
                case "9": cstr = "九"; break;
            }
            return (cstr);
        }
    }
     
}
