using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shu.Model;
using Shu.Comm;
using System.Text;
using Shu.BLL;
using System.Web.SessionState;
using Shu.Utility.Extensions;

namespace Shu.Manage.Handler
{
    /// <summary>
    /// DepartmentHandler 的摘要说明
    /// </summary>
    public class DepartmentHandler : BasePage, IHttpHandler, IRequiresSessionState
    {
        public SessionUserModel CurrUserInfo = new SessionUserModel();
        Sys_DepartmentBLL bll = new Sys_DepartmentBLL();
        Sys_UserInfoBLL bllUser = new Sys_UserInfoBLL();
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
            List<Sys_Department> list = null;
            CurrUserInfo = new BasePage().CurrUserInfo();
            list = bll.GetList(p => p.Department_IsDel == "0").OrderBy(p => p.Department_Sequence).ToList();
            StringBuilder strMenu = new StringBuilder();
            strMenu.Append("{\"total\":" + list.Count + ",\"rows\":[");

            int index = 0;

            foreach (Sys_Department menu in list)
            {
                index++;

                if (menu.Department_ParentCode == "0")
                {

                    strMenu.Append("{\"id\":\"" + menu.Department_Code + "\",\"name\":\"" + menu.Department_Name + "\",\"sort\":\"" + menu.Department_Sequence + "\",\"type\":\"" + Common_BLL.GetDataDictNameByCode(menu.Department_Type) + "\",\"personCharge\":\"" + index + "\",\"teachers\":\"" + index + "\",\"students\":\"" + index + "\"},");
                }
                else
                {
                    strMenu.Append("{\"id\":\"" + menu.Department_Code + "\",\"name\":\"" + menu.Department_Name + "\",\"sort\":\"" + menu.Department_Sequence + "\",\"type\":\"" + Common_BLL.GetDataDictNameByCode(menu.Department_Type) + "\",\"personCharge\":\"" + index + "\",\"teachers\":\"" + index + "\",\"students\":\"" + index + "\",\"_parentId\":\"" + menu.Department_ParentCode + "\"}");

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
            List<Sys_Department> lists = bll.FindWhere(" Department_Code ='" + pcode + "'");
            if (lists.Count > 0)
            {


                List<Sys_Department> list = bll.FindWhere(" Department_ParentCode ='" + pcode + "'");

                if (list.Count > 0)
                {
                    Sys_Department model = list.OrderByDescending(p => p.Department_Sequence).First();

                    return (model.Department_Sequence + 1).ToString();
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

            string sort = context.Request.QueryString["sort"];
            string content = context.Request.QueryString["content"];
            string type = context.Request.QueryString["type"];
            string depClass = context.Request.QueryString["depClass"];
            string personCharge = context.Request.QueryString["personCharge"];
            string teachers = context.Request.QueryString["teachers"];
            string students = context.Request.QueryString["students"];

            //组织单位对象
            Sys_Department depmodel = new Sys_Department();

            depmodel.DepartmentID = Guid.NewGuid().ToString();
            depmodel.Department_ParentCode = pcode;
            depmodel.Department_Name = name;
            depmodel.Department_Level = (bll.Get(p => p.Department_Code == pcode).Department_Level + 1); ;

            depmodel.Department_Respon = content;
            depmodel.Department_Sequence = int.Parse(sort);
            depmodel.Department_Code = bll.GetCode(pcode, "son");
            depmodel.Department_AddTime = DateTime.Now;
            depmodel.Department_Type = type;
            //depmodel.Department_Class = depClass;
            //depmodel.Department_PersonCharge = personCharge;
            depmodel.Department_IsDel = "0";

            //新增组织机构
            bool abl = bll.Add(depmodel);

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
            List<Sys_UserInfo> userList = bllUser.GetList(p => p.UserInfo_DepCode == pcode).ToList();//FindWhere(" UserInfo_DepCode='" + pcode + "'");
            List<Sys_Department> list = bll.GetList(p => p.Department_ParentCode == pcode && p.Department_IsDel == "0").ToList();//FindWhere(" Department_ParentCode='" + pcode + "' AND Department_IsDel=0");
            if (userList.Count > 0)
            {
                return "2";
            }
            else
            {
                if (list.Count > 0)
                {
                    return "0";
                }
                else
                {
                    Sys_Department Department = bll.Get(p => p.Department_Code == pcode);
                    Department.Department_IsDel = "1";

                    //bool rtn = bll.DeleteWhere("Department_Code='" + pcode + "'");
                    bool rtn = bll.Update(Department);
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

        }
        #endregion

        #region 修改

        public string ShowInfo(HttpContext context)
        {
            string code = context.Request.QueryString["code"];

            Sys_Department model = bll.Get(p => p.Department_Code == code);
            if (model != null)
            {
                string json = model.ToJson<Sys_Department>();
                return json;
            }
            else
            {
                return "0";
            }

        }

        public string Modify(HttpContext context)
        {
            string name = context.Request.QueryString["name"];
            string id = context.Request.QueryString["id"];
            string sort = context.Request.QueryString["sort"];
            string content = context.Request.QueryString["content"];
            string type = context.Request.QueryString["type"];
            string personCharge = context.Request.QueryString["personCharge"];
            string teachers = context.Request.QueryString["teachers"];
            string students = context.Request.QueryString["students"];
            string depClass = context.Request.QueryString["depClass"];
            Sys_Department depmodel = bll.Get(p => p.DepartmentID == id);
            if (depmodel == null)
            {
                return "0";
            }
            else
            {


                depmodel.DepartmentID = id;
                depmodel.Department_Name = name;
                depmodel.Department_Sequence = int.Parse(sort);
                depmodel.Department_Type = type;
                //depmodel.Department_PersonCharge = personCharge;
                //depmodel.Department_Class = depClass;
                //if (!string.IsNullOrEmpty(teachers))
                //{
                //    depmodel.Department_Teachers = int.Parse(teachers);
                //}
                //if (!string.IsNullOrEmpty(students))
                //{
                //    depmodel.Department_Students = int.Parse(students);
                //}

                string msg = string.Empty;
                if (bll.Update(depmodel))
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}