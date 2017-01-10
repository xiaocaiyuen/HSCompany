using Shu.Utility.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shu.Model;
using Shu.WebApi.Models;
using Shu.BLL;
using System.Web.Http.Controllers;
using Shu.Utility.Extensions;
using Shu.Comm;

namespace Shu.WebApi.ApiControllers
{
    /// <summary>
    /// 楼宇功能
    /// </summary>
    public class BuildController : BaseController
    {
        /// <summary>
        /// 获取楼宇指定类型列表
        /// </summary>
        /// <param name="Property">楼宇属性(Property:为空 获取所有)</param>
        /// <returns>数据实体</returns>
        [ActionName("BuildList")]
        public WebApiResult<List<combotree>> GetBuildList(string Property)
        {
            List<D_Build> Data;
            List<combotree> tree = new List<combotree>();
            if (Property.IsNullOrEmpty())
            {
                IQueryable<Sys_DataDict> DictionaryList = new Sys_DataDictBLL().GetList(p => p.DataDict_ParentCode == "47");
                Data = new D_BuildBLL().GetAll().ToList();
                DictionaryList.ForEach(p =>
                {
                    tree.Add(new combotree { id = p.DataDict_Code, text = p.DataDict_Name, attributes = "", children = children(p.DataDict_Code, Data) });
                });
            }
            else
            {
                Sys_DataDict DictionaryInfo = new Sys_DataDictBLL().Get(p => p.DataDict_Code == Property);
                Data = new D_BuildBLL().GetList(p => p.Property == Property).ToList();
                tree.Add(new combotree { id = Property, text = DictionaryInfo.DataDict_Name, attributes = "", children = children("", Data) });
            }
            var result = new WebApiPagingResult<List<combotree>>
            {
                Data = tree
            };
            return result;
        }

        private List<combotree> children(string code, List<D_Build> treeList)
        {
            List<combotree> tree = new List<combotree>();
            List<D_Build> list;
            if (code.IsNullOrEmpty())
            {
                list = treeList;
            }
            else
            {
                list = treeList.Where(p => p.Id == code).ToList();
            }
            list.ForEach(item =>
            {
                tree.Add(new combotree
                {
                    id = item.Id,
                    text = item.Name,
                    attributes = item.Property
                    //children = children(item.Id, treeList)
                });
            });
            return tree;
        }
    }
}