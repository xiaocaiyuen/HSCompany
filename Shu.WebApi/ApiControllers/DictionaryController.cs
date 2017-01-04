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
    /// 数据字典功能
    /// </summary>
    public class DictionaryController : BaseController
    {
        /// <summary>
        /// 获取数据字典指定列表
        /// </summary>
        /// <param name="ParentCode">编号</param>
        /// <returns>数据实体</returns>
        [ActionName("DictionaryList")]
        public WebApiResult<IQueryable<Sys_DataDict>> GetDictionary(string ParentCode)
        {
            IQueryable<Sys_DataDict> DataDictList = new Sys_DataDictBLL().GetList(p => p.DataDict_ParentCode == ParentCode && p.DataDict_IsDel == false).OrderBy(p => p.DataDict_Sequence);
            var result = new WebApiPagingResult<IQueryable<Sys_DataDict>>
            {
                Data = DataDictList
            };

            return result;
        }

        /// <summary>
        /// 获取数据字典树结构列表
        /// </summary>
        /// <param name="ParentCode">编号</param>
        /// <returns>数据实体</returns>
        [ActionName("DictionaryTree")]
        public WebApiResult<List<combotree>> GetDictionaryTree(string ParentCode)
        {
            List<combotree> tree = new List<combotree>() {
                new combotree { id="",text=Constant.DrpChoiceName}
            };
            List<Sys_DataDict> DataDictList = new Sys_DataDictBLL().GetList(p => p.DataDict_ParentCode.StartsWith(ParentCode) && p.DataDict_IsDel == false).OrderBy(p => p.DataDict_Sequence).ToList();
            List<Sys_DataDict> OneDataDictList = DataDictList.Where(p => p.DataDict_ParentCode == ParentCode).ToList();

            OneDataDictList.ForEach(item =>
            {
                tree.Add(new combotree
                {
                    id = item.DataDict_Code,
                    text = item.DataDict_Name,
                    state = "open",
                    children = children(item.DataDict_Code, DataDictList)
                });
            });

            var result = new WebApiPagingResult<List<combotree>>
            {
                Data = tree
            };

            return result;
        }

        private List<combotree> children(string code, List<Sys_DataDict> treeList)
        {
            List<combotree> tree = new List<combotree>();
            List<Sys_DataDict> list = treeList.Where(p => p.DataDict_ParentCode == code).ToList();
            list.ForEach(item =>
            {
                tree.Add(new combotree
                {
                    id = item.DataDict_Code,
                    text = item.DataDict_Name,
                    children = children(item.DataDict_Code, treeList)
                });
            });
            return tree;
        }

    }
}