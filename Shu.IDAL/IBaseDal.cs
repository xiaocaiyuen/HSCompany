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
    public interface IBaseDal<T> : IBaseExtendsDal<T> where T : class, new()
    {
        /// <summary>
        /// 插入实体对象到数据库
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回bool类型</returns>
        bool Add(T entity);

        /// <summary>
        /// 插入实体对象到数据库
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回当前更新过的实体对象</returns>
        T AddEntity(T entity);

        /// <summary>
        /// 插入实体对象列表到数据库
        /// </summary>
        /// <param name="entityList">实体对象列表</param>
        /// <returns>返回bool类型</returns>
        bool Add(List<T> entityList);

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回bool类型</returns>
        bool Update(T entity);

        /// <summary>
        /// 更新实体对象列表
        /// </summary>
        /// <param name="entityList">实体对象列表</param>
        /// <returns>返回bool类型</returns>
        bool Update(List<T> entityList);

        /// <summary>
        /// 删除实体对象数据.不要随意调用。
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回bool类型</returns>
        bool Delete(T entity);

        /// <summary>
        /// 删除符合表达式条件数据
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns>返回bool类型</returns>
        bool Delete(Expression<Func<T, bool>> where);
    }
}
