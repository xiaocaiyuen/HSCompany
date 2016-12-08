using Shu.Factroy;
using Shu.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Shu.Model;

namespace Shu.BLL
{
    public abstract class BaseExtendsBLL<T> where T : class, new()
    {
        public IDBSession DBSession
        {
            get
            {
                return DBSessionFactory.CreateDBSession();
            }
        }

        public IDAL.IBaseExtendsDal<T> DalExtends { get; set; }

        public abstract void SetExtendsCurrentDal();

        public BaseExtendsBLL()
        {
            SetExtendsCurrentDal();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<T, bool>> where)
        {
            return DalExtends.Exists(where);
        }

        /// <summary>
        /// 获取符合条件的个数
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public int GetCount(Expression<Func<T, bool>> where)
        {
            return DalExtends.GetCount(where);
        }

        /// <summary>
        /// 获取单实体对象
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> where)
        {
            return DalExtends.Get(where);
        }

        /// <summary>
        /// 获取所有表数据列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return DalExtends.GetList(p => true);
        }

        /// <summary>
        /// 获取符合条件的实体列表
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public IQueryable<T> GetList(Expression<Func<T, bool>> where)
        {
            return DalExtends.GetList(where);
        }

        /// <summary>
        /// 分页获取所有的实体列表
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <param name="orderby">排序表达式</param>
        /// <param name="isAsc">排序键是否降序</param>
        /// <returns></returns>
        public IQueryable<T> GetList(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderby, bool isAsc)
        {
            return DalExtends.GetList(where, orderby, isAsc);
        }

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
        public IQueryable<T> GetList<S>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> where, Expression<Func<T, S>> orderby, bool isAsc)
        {
            return DalExtends.GetList<S>(pageIndex, pageSize, out totalCount, where, orderby, isAsc);
        }
    }
}
