using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shu.IDAL
{
    /// <summary>
    /// 基础接口
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public interface IBaseExtendsDal<T> where T : class, new()
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> where);

        /// <summary>
        /// 获取符合条件的个数
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> where);

        /// <summary>
        /// 获取单实体对象
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> where);

        /// <summary>
        /// 获取符合条件的实体列表
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> where);

        /// <summary>
        /// 分页获取所有的实体列表
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <param name="orderby">排序表达式</param>
        /// <param name="isAsc">排序键是否降序</param>
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderby, bool isAsc);

        /// <summary>
        /// 分页获取符合条件的实体列表
        /// </summary>
        /// <typeparam name="S">排序键类型</typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">单页数量</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="where">条件表达式</param>
        /// <param name="orderby">排序表达式</param>
        /// <param name="isAsc">排序键是否降序</param>
        /// <returns></returns>
        IQueryable<T> GetList<S>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> where, Expression<Func<T, S>> orderby, bool isAsc);

    }
}
