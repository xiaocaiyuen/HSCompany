using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shu.Model;
using Shu.Comm;
using System.Text;
using Shu.BLL;
using Shu.Utility.Extensions;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// DictionaryHandler 的摘要说明
    /// </summary>
    public class DictionaryHandler : IHttpHandler
    {

        Sys_DataDictBLL bll = new Sys_DataDictBLL();

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


            List<Sys_DataDict> list = bll.GetList(p => p.DataDict_IsDel == false).OrderBy(p => p.DataDict_Sequence).ToList();

            StringBuilder strMenu = new StringBuilder();

            strMenu.Append("{\"total\":" + list.Count + ",\"rows\":[");

            int index = 0;

            foreach (Sys_DataDict menu in list)
            {
                index++;

                if (menu.DataDict_Code == "0")
                {

                    strMenu.Append("{\"id\":\"" + menu.DataDict_Code + "\",\"name\":\"" + menu.DataDict_Name + "\",\"code\":\"" + menu.DataDict_Code + "\",\"sort\":\"" + menu.DataDict_Sequence + "\"},");
                }
                else
                {
                    strMenu.Append("{\"id\":\"" + menu.DataDict_Code + "\",\"name\":\"" + menu.DataDict_Name + "\",\"code\":\"" + menu.DataDict_Code + "\",\"sort\":\"" + menu.DataDict_Sequence + "\",\"_parentId\":\"" + menu.DataDict_ParentCode + "\"}");

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

            List<Sys_DataDict> list = bll.GetList(p => p.DataDict_ParentCode == pcode).ToList();

            if (list.Count > 0)
            {
                Sys_DataDict model = list.OrderByDescending(p => p.DataDict_Sequence).First();

                return (model.DataDict_Sequence + 1).ToString();
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
            string pcode = context.Request.QueryString["pcode"];

            string sort = context.Request.QueryString["sort"];
            string content = context.Request.QueryString["content"];



            Sys_DataDict menumodel = new Sys_DataDict();

            menumodel.DataDictID = Guid.NewGuid().ToString();
            menumodel.DataDict_ParentCode = pcode;
            menumodel.DataDict_Name = name;
            menumodel.DataDict_Level = 1;

            menumodel.DataDict_Remark = content;
            menumodel.DataDict_Sequence = int.Parse(sort);
            menumodel.DataDict_Code = bll.GetCode(pcode, "son");
            menumodel.DataDict_AddTime = DateTime.Now;
            menumodel.DataDict_IsDel = false;
            string msg = string.Empty;
            bool abl = bll.Add(menumodel);

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
            string pcode = context.Request.QueryString["pcode"];

            //List<Sys_DataDict> list = bll.FindWhere(" DataDict_ParentCode ='" + pcode + "'");
            List<Sys_DataDict> list = bll.GetList(p => p.DataDict_ParentCode == pcode).ToList();
            if (list.Count > 0)
            {
                return "0";
            }
            else
            {
                bool rtn = false;
                //List<Sys_DataDict> list2 = bll.FindWhere(" DataDict_Code ='" + pcode + "'");
                List<Sys_DataDict> list2 = bll.GetList(p => p.DataDict_Code == pcode).ToList();
                if (list2.Count == 1)
                {
                    Sys_DataDict da = list2[0];
                    da.DataDict_IsDel = true;  //1为删除   0为未删除
                    rtn = bll.Update(da);
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
            string code = context.Request.QueryString["code"];

            Sys_DataDict model = bll.Get(p => p.DataDict_Code == code);

            string json = model.ToJson<Sys_DataDict>();

            return json;
        }

        public string Modify(HttpContext context)
        {
            string name = context.Request.QueryString["name"];
            string id = context.Request.QueryString["id"];
            string sort = context.Request.QueryString["sort"];
            string content = context.Request.QueryString["content"];

            Sys_DataDict menumodel = bll.Get(p => p.DataDictID == id);
            menumodel.DataDictID = id;
            menumodel.DataDict_Name = name;
            menumodel.DataDict_Remark = content;
            menumodel.DataDict_Sequence = int.Parse(sort);
            if (bll.Update(menumodel))
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