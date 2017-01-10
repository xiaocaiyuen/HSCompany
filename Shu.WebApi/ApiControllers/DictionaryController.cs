using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shu.Utility.Extensions;
using Shu.Comm;
using Shu.Model;
using Shu.BLL;
using Shu.Utility.WebApi;

namespace Shu.WebApi.ApiControllers
{
    public class DictionaryController : BaseController
    {
        /// <summary>
        /// 获取数据字典指定列表
        /// </summary>
        /// <param name="ParentCode">编号</param>
        /// <returns>数据实体</returns>
        [ActionName("DictionaryList")]
        public List<Sys_DataDict> GetDictionaryList(string ParentCode)
        {
            List<Sys_DataDict> DataDictList = new Sys_DataDictBLL().GetList(p => p.DataDict_ParentCode == ParentCode && p.DataDict_IsDel == false).OrderBy(p => p.DataDict_Sequence).ToList();
            DataDictList.Insert(0, new Sys_DataDict { DataDict_Name = Constant.DrpChoiceName, DataDict_Code = "" });
            //var result = new WebApiPagingResult<List<Sys_DataDict>>
            //{
            //    Data = DataDictList
            //};

            return DataDictList;
        }

        /// <summary>
        /// 获取数据字典树结构列表
        /// </summary>
        /// <param name="ParentCode">编号</param>
        /// <returns>数据实体</returns>
        [ActionName("DictionaryTree")]
        public List<combotree> GetDictionaryTree(string TreeCode)
        {
            List<combotree> tree = new List<combotree>() {
                new combotree { id="",text=Constant.DrpChoiceName}
            };
            List<Sys_DataDict> DataDictList = new Sys_DataDictBLL().GetList(p => p.DataDict_ParentCode.StartsWith(TreeCode) && p.DataDict_IsDel == false).OrderBy(p => p.DataDict_Sequence).ToList();
            List<Sys_DataDict> OneDataDictList = DataDictList.Where(p => p.DataDict_ParentCode == TreeCode).ToList();

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

            //var result = new WebApiPagingResult<List<combotree>>
            //{
            //    Data = tree
            //};

            return tree;
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
