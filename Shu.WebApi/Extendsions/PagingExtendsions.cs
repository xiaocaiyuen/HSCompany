using Shu.Utility.WebApi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace Shu.WebApi.Extendsions
{
    public static class PagingExtendsions
    {
        /// <summary>
        /// 分页工具栏
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="sizeCount">记录总数</param>
        /// <param name="startPageNum">起始页码：默认1</param>
        /// <param name="pageOfNumber">每页页码个数：默认10</param>
        /// <param name="queryString">queryString查询参数</param>
        /// <returns></returns>
        public static MvcHtmlString Paging(this HtmlHelper htmlHelper, int pageIndex = 0, int pageSize = 10, int sizeCount = 0, int startPageNum = 1, int pageOfNumber = 10, string queryString = "")
        {
            //第一页，上一页 ... 5，6，7，8，9 ... 下一页，最后一页，转到{0}页，确定

            //计算出总页数
            var pageCount = sizeCount % pageSize == 0 ? sizeCount / pageSize : sizeCount / pageSize + 1;

            //如果只有一页不需要分页
            if (pageCount == 1) return new MvcHtmlString("");

            var pagedHtmlString = new StringBuilder();
            pagedHtmlString.Append("<ul class='pagination'>");

            //第一页
            if (pageIndex > startPageNum)
            {
                pagedHtmlString.AppendFormat("<li><a href='?pageindex={0}&pagesize={1}&{2}' title='第一页'>第一页</a></li>",
                    startPageNum,
                    pageSize,
                    queryString);
            }
            else
            {
                pagedHtmlString.Append("<li class='disabled'><a href='#' title='第一页'>第一页</a></li>");
            }


            //上一页
            if (pageIndex - 1 >= startPageNum && pageIndex - 1 <= pageCount)
            {
                pagedHtmlString.AppendFormat("<li><a href='?pageindex={0}&pagesize={1}&{2}' title='上一页'>上一页</a></li>",
                    pageIndex - 1,
                    pageSize,
                    queryString);
            }
            else
            {
                pagedHtmlString.Append("<li class='disabled'><a href='#' title='上一页'>上一页</a></li>");
            }

            //当前页
            var pageItem = pageOfNumber - 1 >= startPageNum ? pageOfNumber - 1 : startPageNum;
            var pageStartIndex = pageIndex - pageItem / 2 >= startPageNum ? pageIndex - pageItem / 2 : startPageNum;
            var pageEndIndex = pageIndex + pageItem / 2 <= pageCount ? pageIndex + pageItem / 2 : pageCount;

            //尽量平均分配分页按钮的数量
            var offset = pageItem - (pageEndIndex - pageStartIndex);
            if (offset > 0)
            {
                var leftPatch = pageStartIndex - offset > startPageNum;
                if (leftPatch)
                {
                    //向前扩展
                    pageStartIndex = pageStartIndex - offset;
                }
                else
                {
                    //向后扩展
                    pageEndIndex = pageEndIndex + offset < pageCount ? pageEndIndex + offset : pageEndIndex;
                }
            }

            for (var i = pageStartIndex; i < pageEndIndex; i++)
            {
                if (i == pageIndex)
                {
                    pagedHtmlString.AppendFormat("<li class='active'><a href='#' title='当前页'>{0}<span class='sr-only'>(current)</span></a></li>",
                        pageIndex + 1);
                }
                else
                {
                    pagedHtmlString.AppendFormat("<li><a href='?pageindex={0}&pagesize={1}&{2}' title='第{3}页'>{4}</a></li>",
                        i,
                        pageSize,
                        queryString,
                        i,
                        i + 1);
                }
            }

            //下一页
            if (pageIndex + 1 >= startPageNum && pageIndex + 1 < pageCount)
            {
                pagedHtmlString.AppendFormat("<li ><a href='?pageindex={0}&pagesize={1}&{2}' title='下一页'>下一页</a></li>",
                    pageIndex + 1,
                    pageSize,
                    queryString);
            }
            else
            {
                pagedHtmlString.Append("<li class='disabled'><a href='#' title='下一页'>下一页</a></li>");
            }

            //最后一页
            if (pageCount > pageIndex + 1)
            {
                pagedHtmlString.AppendFormat("<li ><a href='?pageindex={0}&pagesize={1}&{2}' title='最后一页'>最后一页</a></li>",
                    pageCount - 1,
                    pageSize,
                    queryString);
            }
            else
            {
                pagedHtmlString.Append("<li class='disabled'><a href='#' title='最后一页'>最后一页</a></li>");
            }


            pagedHtmlString.Append("</ul>");

            return new MvcHtmlString(pagedHtmlString.ToString());
        }

        /// <summary>
        /// 分页工具栏
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="sizeCount">记录总数</param>
        /// <param name="queryString">queryString查询参数</param>
        /// <returns></returns>
        public static MvcHtmlString Paging(this HtmlHelper htmlHelper, int pageIndex = 0, int pageSize = 10, int sizeCount = 0, string queryString = "")
        {
            return Paging(htmlHelper, pageIndex, pageSize, sizeCount, 0, 10, queryString);
        }

        /// <summary>
        /// 将 WebApiPagingResult 实例数据进行分页处理
        /// </summary>
        /// <typeparam name="T">数据实体类型</typeparam>
        /// <param name="helper">helper</param>
        /// <param name="options">分页参数</param>
        /// <returns>返回数据分页后的 WebApiPagingResult 实例</returns>
        public static WebApiPagingResult<IQueryable<T>> AsPaging<T>(
            this WebApiPagingResult<IQueryable<T>> helper, PagingOptions options)
        {
            if (helper.Data != null && options != null)
            {
                var queryable = helper.Data;

                #region Filters
                var filters = options.GetFilter();
                if (!string.IsNullOrWhiteSpace(filters))
                {
                    queryable = queryable.Where(filters, options.FilterParameters);

                    var queryable2 = queryable.Where(filters, options.FilterParameters);
                    helper.RowsCount = queryable2.Count();
                }
                else
                {
                    var queryable2 = queryable.Where("true");
                    helper.RowsCount = queryable2.Count();
                }
                #endregion

                #region Sort
                var sort = options.GetOrder();

                if (!string.IsNullOrWhiteSpace(sort))
                {
                    queryable = queryable.OrderBy(sort);
                }

                #endregion

                #region Paging
                if (options.PageIndex > 0 && options.PageSize > 0)
                {
                    queryable = queryable.Skip((options.PageIndex - 1) * options.PageSize).Take(options.PageSize);
                }
                #endregion

                helper.Data = queryable;
            }

            return helper;
        }

    }

    public class PagingOptions
    {
        #region 属性
        /// <summary>
        /// 获取或设置页索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 获取或设置每页显示行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 获取或设置排序参数集合
        /// </summary>
        public List<PagingSort> Sort { get; set; }
        /// <summary>
        /// 获取或设置查询参数集合
        /// </summary>
        public List<PagingFilter> Filters { get; set; }
        /// <summary>
        /// 获取或设置查询参数值
        /// </summary>
        public object[] FilterParameters { get; private set; }
        #endregion

        #region 构造函数
        public PagingOptions()
        {
            PageIndex = 1;
            PageSize = 20;
            Sort = new List<PagingSort>();
            Filters = new List<PagingFilter>();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取排序参数
        /// </summary>
        /// <returns>返回排序参数</returns>
        public string GetOrder()
        {
            return string.Join(",", this.Sort.Select(x => x.SortOrder).ToArray());
        }
        /// <summary>
        /// 获取查询参数
        /// </summary>
        /// <returns>返回查询参数</returns>
        public string GetFilter()
        {
            FilterParameters = new object[Filters.Count];
            var expressions = new string[Filters.Count];
            for (var i = 0; i < Filters.Count; i++)
            {
                var filter = Filters[i];
                expressions[i] = filter.GetExpression(i);
                FilterParameters[i] = filter.Term;
            }
            return string.Join(" and ", expressions);
        }
        #endregion
    }

    /// <summary>
    /// 分页排序参数
    /// </summary>
    public class PagingSort
    {
        /// <summary>
        /// 获取或设置排序字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 获取或设置排序方式
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 获取排序字符串
        /// </summary>
        public string SortOrder
        {
            get { return string.Format("{0} {1}", Field, Sort); }
        }
    }

    /// <summary>
    /// 分页查询参数
    /// </summary>
    public class PagingFilter
    {
        /// <summary>
        /// 获取或设置查询字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 获取或设置查询比较符
        /// </summary>
        public string Op { get; set; }
        /// <summary>
        /// 获取或设置查询值
        /// </summary>
        public string Term { get; set; }
        /// <summary>
        /// 获取查询表达式
        /// </summary>
        /// <param name="index">参数索引</param>
        /// <returns>返回查询表达式</returns>
        public string GetExpression(int index)
        {
            var result = string.Empty;
            var op = string.Format("{0}", Op).ToLower();
            switch (op)
            {
                case "like":
                default:
                    result = string.Format("{0}.Contains(@{1})", Field, index);
                    break;
            }

            return result;
        }
    }
}