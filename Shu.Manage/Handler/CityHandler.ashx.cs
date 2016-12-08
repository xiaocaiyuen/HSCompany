using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Shu.BLL;
using Shu.Comm;
using Shu.Model;
using Shu.Utility.Extensions;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// CityHandler 的摘要说明
    /// </summary>
    public class CityHandler : IHttpHandler
    {
        Sys_AreaBLL bll = new Sys_AreaBLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            context.Response.Cache.SetNoStore();

            string method = context.Request.QueryString["method"];

            if (method == "load")
            {
                context.Response.Write(LoadMuenu());
            }
            if (method == "sort")
            {
                context.Response.Write(GetSortNum(context));
            }
            if (method == "add")
            {
                context.Response.Write(Add(context));
            }

            if (method == "delete")
            {
                context.Response.Write(Delete(context));
            }

            if (method == "show")
            {
                context.Response.Write(ShowInfo(context));
            }

            if (method == "modify")
            {
                context.Response.Write(Modify(context));
            }
        }

        #region 加载
        /// <summary>
        /// 加载菜单树
        /// </summary>
        /// <returns></returns>
        public string LoadMuenu()
        {
            List<Sys_Area> list = bll.GetAll().OrderBy(p => p.Path).OrderBy(p => p.Sort).ToList();
            StringBuilder strMenu = new StringBuilder();
            int listCount = list.Count;
            strMenu.Append("{\"total\":" + listCount + ",\"rows\":[");
            int index = 0;
            for (int i = 0; i < listCount; i++)
            {
                Sys_Area menu = list[i];
                index++;
                if (menu.Depth == 1)
                {
                    strMenu.Append("{\"id\":\"" + menu.Id + "\",\"name\":\"" + menu.Name + "\",\"zipcode\":\"" + menu.PostCode + "\",\"areacode\":\"" + menu.AreaCode + "\",\"code\":\"" + menu.Id + "\",\"sort\":\"" + menu.Sort + "\"},");
                }
                else
                {
                    strMenu.Append("{\"id\":\"" + menu.Id + "\",\"name\":\"" + menu.Name + "\",\"zipcode\":\"" + menu.PostCode + "\",\"areacode\":\"" + menu.AreaCode + "\",\"code\":\"" + menu.Id + "\",\"sort\":\"" + menu.Sort + "\",\"_parentId\":\"" + menu.ParentId + "\"}");
                    if (index != listCount)
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
            int pcode = context.Request.QueryString["pcode"].ToInt(0);

            List<Sys_Area> list = bll.GetList(p => p.ParentId == pcode).ToList();

            if (list.Count > 0)
            {
                Sys_Area model = list.OrderByDescending(p => p.Sort).First();

                return (model.Sort + 1).ToString();
            }
            else
            {
                return "1";
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
            int pcode = context.Request.QueryString["pcode"].ToInt(0);

            string sort = context.Request.QueryString["sort"];

            string zipcode = context.Request.QueryString["zipcode"];
            string areacode = context.Request.QueryString["areacode"];

            Sys_Area area = new Sys_Area();
            Sys_Area parent = bll.Get(p => p.Id == pcode);
            area.ParentId = pcode;
            area.Name = name;
            area.Depth = parent.Depth + 1;
            area.PostCode = zipcode;
            area.AreaCode = areacode;

            area.Sort = int.Parse(sort);
            bool abl = bll.Add(area);
            //// 更新Path
            //area = bll.Get(p=>p.Id== area.Id);
            //area.Path = parent.Path + "/" + area.ID;
            //abl = bll.Update(area, out msg);
            if (abl)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        #endregion

        #region 删除

        public string Delete(HttpContext context)
        {
            int pcode = context.Request.QueryString["pcode"].ToInt(0);

            List<Sys_Area> list = bll.GetList(p => p.ParentId == pcode).ToList();
            if (list.Count > 0)
            {
                return "0";
            }
            else
            {
                bool rtn = false;
                Sys_Area areaInfo = bll.Get(p => p.Id == pcode);
                if (areaInfo.IsNotNull())
                {
                    rtn = bll.Delete(areaInfo);
                }
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
            int code = int.Parse(context.Request.QueryString["code"]);

            Sys_Area model = bll.Get(p => p.Id == code);

            string json = model.ToJson<Sys_Area>();

            return json;
        }

        public string Modify(HttpContext context)
        {
            string name = context.Request.QueryString["name"];
            int id = int.Parse(context.Request.QueryString["id"]);
            string sort = context.Request.QueryString["sort"];

            string zipcode = context.Request.QueryString["zipcode"];
            string areacode = context.Request.QueryString["areacode"];

            Sys_Area area = bll.Get(p => p.Id == id);
            area.Name = name;
            area.Sort = int.Parse(sort);
            area.PostCode = zipcode;
            area.AreaCode = areacode;
            if (bll.Update(area))
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