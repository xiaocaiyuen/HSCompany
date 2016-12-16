using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shu.Model;
using Shu.BLL;
using System.Text;
using Shu.Comm;
using Shu.Utility.Extensions;
using Shu.Utility;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// MenuHandler 的摘要说明
    /// </summary>
    public class MenuHandler : IHttpHandler
    {

        Sys_MenuBLL bll = new Sys_MenuBLL();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            string method = context.Request.QueryString["method"];
            context.Response.Cache.SetNoStore();

            if (method == "load")
            {
                context.Response.Write(LoadMuenu());
            }
            else if (method == "sort")
            {
                context.Response.Write(GetSortNum(context));
            }
            else if (method == "add")
            {
                context.Response.Write(Add(context));
            }

            else if (method == "delete")
            {
                context.Response.Write(Delete(context));
            }

            else if (method == "show")
            {
                context.Response.Write(ShowInfo(context));
            }
            else if (method == "See")
            {
                context.Response.Write(ShowSee(context));
            }
            else if (method == "modify")
            {
                context.Response.Write(Modify(context));
            }
            else if (method == "UpdateIconStyle")//批量更新菜单图标样式
            {
                context.Response.Write(UpdateIconStyle(context));
            }



        }


        #region 修改时加载查看范围规则
        public string ShowSee(HttpContext context)
        {
            string code = context.Request.QueryString["code"];

            Sys_SeeChargeBLL bllSee = new Sys_SeeChargeBLL();
            //List<Sys_SeeCharge> seeList = bllSee.FindWhere(" SeeCharge_MenuID='" + code + "' order by SeeCharge_Sort asc");
            List<Sys_SeeCharge> seeList = bllSee.GetList(p => p.SeeCharge_MenuID == code).OrderBy(p => p.SeeCharge_Sort).ToList();
            string str = "";

            for (int i = 0; i < seeList.Count(); i++)
            {
                str += seeList[i].SeeCharge_Name + "@" + seeList[i].SeeCharge_Code + "#";
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }

            return str;
        }

        #endregion

        #region 加载菜单树
        /// <summary>
        /// 加载菜单树
        /// </summary>
        /// <returns></returns>
        public string LoadMuenu()
        {


            List<Sys_Menu> list = bll.GetAll().OrderBy(p => p.Menu_Sequence).ToList();

            StringBuilder strMenu = new StringBuilder();

            strMenu.Append("{\"total\":" + list.Count + ",\"rows\":[");

            int index = 0;

            foreach (Sys_Menu menu in list)
            {
                index++;

                if (menu.Menu_ParentCode == "0")
                {

                    strMenu.Append("{\"id\":\"" + menu.Menu_Code + "\",\"name\":\"" + menu.Menu_Name + "\",\"url\":\"" + menu.Menu_Url + "\",\"sort\":\"" + menu.Menu_Sequence + "\",\"iconCls\":\"icon-" + menu.Menu_IconName + "\",\"Opt\":\"" + menu.Menu_Operation + "\"},");
                }
                else
                {
                    strMenu.Append("{\"id\":\"" + menu.Menu_Code + "\",\"name\":\"" + menu.Menu_Name + "\",\"url\":\"" + menu.Menu_Url + "\",\"sort\":\"" + menu.Menu_Sequence + "\",\"iconCls\":\"icon-" + menu.Menu_IconName + "\",\"Opt\":\"" + menu.Menu_Operation + "\",\"_parentId\":\"" + menu.Menu_ParentCode + "\"}");

                    if (index != list.Count)
                    {

                        strMenu.Append(",");
                    }
                }

            }

            strMenu.Append("]}");

            return strMenu.ToString();
        }

        #endregion

        #region 获得排序号

        /// <summary>
        /// 获得排序号
        /// </summary>
        /// <param name="menucode"></param>
        /// <returns></returns>
        public string GetSortNum(HttpContext context)
        {
            string pcode = context.Request.QueryString["pcode"];

            List<Sys_Menu> lists = bll.GetList(p => p.Menu_Code == pcode).ToList();//" Menu_Code ='" + pcode + "'"
            if (lists.Count > 0)
            {


                List<Sys_Menu> list = bll.GetList(p => p.Menu_ParentCode == pcode).ToList();
                if (list.Count > 0)
                {
                    Sys_Menu model = list.OrderByDescending(p => p.Menu_Sequence).First();

                    return (model.Menu_Sequence + 1).ToString();
                }
                else
                {
                    return "1";
                }
            }
            else
            {
                return "0";
            }
        }

        #endregion

        #region 新增


        /// <summary>
        /// 新增
        /// </summary>
        public string Add(HttpContext context)
        {
            string name = context.Request.QueryString["name"];
            string pcode = context.Request.QueryString["pcode"];
            string url = context.Request.QueryString["url"];
            string sort = context.Request.QueryString["sort"];
            string opt = context.Request.QueryString["opt"];
            string see = context.Request.QueryString["seeCharge"];
            string IconPath = context.Request.QueryString["IconPath"];
            string IconName = context.Request.QueryString["IconName"];


            Sys_Menu menumodel = new Sys_Menu();

            menumodel.MenuID = Guid.NewGuid().ToString();
            menumodel.Menu_ParentCode = pcode;
            menumodel.Menu_Name = name;
            menumodel.Menu_Level = (bll.Get(p => p.Menu_Code == pcode).Menu_Level + 1);
            menumodel.Menu_Url = url;
            menumodel.Menu_Description = "";
            menumodel.Menu_Sequence = int.Parse(sort);
            menumodel.Menu_Code = bll.GetMaxNum(pcode, "bh");
            menumodel.Menu_AddTime = DateTime.Now;
            menumodel.Menu_AddUserID = "";
            menumodel.Menu_Operation = opt;
            menumodel.Menu_IconName = IconName;
            menumodel.Menu_IconPath = IconPath;
            string msg = string.Empty;
            bool abl = bll.Add(menumodel);

            List<Sys_SeeCharge> seList = new List<Sys_SeeCharge>();
            var da = see.Split('|');
            for (int i = 0; i < da.Count(); i++)
            {
                var ds = da[i].Split('^');
                if (!string.IsNullOrEmpty(ds[0]))
                {
                    Sys_SeeCharge se = new Sys_SeeCharge();
                    se.SeeChargeID = Guid.NewGuid().ToString();
                    se.SeeCharge_Name = ds[0].ToString();
                    se.SeeCharge_Code = ds[1].ToString();
                    se.SeeCharge_MenuID = menumodel.Menu_Code;
                    se.SeeCharge_Sort = i;

                    seList.Add(se);

                }
            }

            string s = "1";
            if (seList.Count > 0)
            {
                Sys_SeeChargeBLL bllSee = new Sys_SeeChargeBLL();
                bllSee.Delete(p => p.SeeCharge_MenuID == menumodel.Menu_Code); //" SeeCharge_MenuID='" + menumodel.Menu_Code + "'"
                if (bllSee.Add(seList))
                {
                    s = "1";
                }
                else
                {
                    s = "0";
                }
            }
            else
            {
                Sys_SeeChargeBLL bllSee = new Sys_SeeChargeBLL();
                bllSee.Delete(p => p.SeeCharge_MenuID == menumodel.Menu_Code);//" SeeCharge_MenuID='" + menumodel.Menu_Code + "'"
            }


            if (abl)
            {
                s = "1";
            }
            else
            {
                s = "0";
            }

            return s;



        }
        #endregion

        #region 删除

        public string Delete(HttpContext context)
        {
            string pcode = context.Request.QueryString["pcode"];

            //List<Sys_Menu> list = bll.FindWhere(" Menu_ParentCode ='" + pcode + "'");
            List<Sys_Menu> list = bll.GetList(p => p.Menu_ParentCode == pcode).ToList();
            if (list.Count > 0)
            {
                return "0";
            }
            else
            {
                bool rtn = bll.Delete(p => p.Menu_Code == pcode);//"Menu_Code='" + pcode + "'"

                if (rtn)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion

        #region 修改

        public string ShowInfo(HttpContext context)
        {
            string code = context.Request.QueryString["code"];

            Sys_Menu model = bll.Get(p => p.Menu_Code == code);
            string json = "";
            if (model != null)
            {
                string a = model.ToJson<Sys_Menu>();//JosnHandler.GetJson<Sys_Menu>(model);
                json = a;
            }
            else
            {
                string b = "";
                json = b;
            }


            return json;
        }

        public string Modify(HttpContext context)
        {
            string name = context.Request.QueryString["name"];
            string id = context.Request.QueryString["id"];
            string url = context.Request.QueryString["url"];
            string sort = context.Request.QueryString["sort"];
            string opt = context.Request.QueryString["opt"];
            string IconPath = context.Request.QueryString["IconPath"];
            string IconName = context.Request.QueryString["IconName"];

            string see = context.Request.QueryString["seeCharge"];
            string s = "1";
            //Sys_Menu menumodel = bll.Find(id);
            Sys_Menu menumodel = bll.Get(p => p.MenuID == id);
            if (menumodel == null)
            {
                s = "0";
            }
            else
            {

                // menumodel.MenuID = id;

                menumodel.Menu_Name = name;
                menumodel.Menu_Url = url;
                menumodel.Menu_Sequence = int.Parse(sort);
                menumodel.Menu_Operation = opt;
                menumodel.Menu_IconName = IconName;
                menumodel.Menu_IconPath = IconPath;


                List<Sys_SeeCharge> seList = new List<Sys_SeeCharge>();
                var da = see.Split('|');
                for (int i = 0; i < da.Count(); i++)
                {
                    var ds = da[i].Split('^');
                    if (!string.IsNullOrEmpty(ds[0]))
                    {
                        Sys_SeeCharge se = new Sys_SeeCharge();
                        se.SeeChargeID = Guid.NewGuid().ToString();
                        se.SeeCharge_Name = ds[0].ToString();
                        se.SeeCharge_Code = ds[1].ToString();

                        se.SeeCharge_MenuID = menumodel.Menu_Code;


                        se.SeeCharge_Sort = i;

                        seList.Add(se);

                    }
                }


                string msg = string.Empty;
                if (seList.Count > 0)
                {
                    Sys_SeeChargeBLL bllSee = new Sys_SeeChargeBLL();

                    bllSee.Delete(p => p.SeeCharge_MenuID == menumodel.Menu_Code);

                    if (bllSee.Add(seList))
                    {
                        s = "1";
                    }
                    else
                    {
                        s = "0";
                    }
                }
                else
                {
                    Sys_SeeChargeBLL bllSee = new Sys_SeeChargeBLL();

                    bllSee.Delete(p => p.SeeCharge_MenuID == menumodel.Menu_Code);


                }

                if (bll.Update(menumodel))
                {
                    s = "1";
                }
                else
                {
                    s = "0";
                }
            }
            return s;

        }

        #endregion

        #region 批量更新菜单图标样式
        public string UpdateIconStyle(HttpContext context)
        {
            string pcode = context.Request.QueryString["pcode"];

            bool isIconClass = new Sys_MenuBLL().IconMenu(System.Web.HttpContext.Current.Server.MapPath("/Content/Icons/iconMenu.css"));
            if (isIconClass)
            {
                return "1";
            }
            else
            {
                return "0";
            }

        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}