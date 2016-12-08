using Shu.Factroy;
using Shu.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shu.BLL
{
    public abstract class BaseBLL<T> : BaseExtendsBLL<T> where T : class, new()
    {

        public IDAL.IBaseDal<T> Dal { get; set; }

        public abstract void SetCurrentDal();

        public BaseBLL()
        {
            SetCurrentDal();
        }

        /// <summary>
        /// 插入实体对象到数据库
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回bool类型</returns>
        public bool Add(T entity)
        {
            Dal.Add(entity);
            return DBSession.SaveChanges();
        }

        /// <summary>
        /// 插入实体对象到数据库
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回当前更新过的实体对象</returns>
        public T AddEntity(T entity)
        {
            Dal.AddEntity(entity);
            DBSession.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 插入实体对象列表到数据库
        /// </summary>
        /// <param name="entityList">实体对象列表</param>
        /// <returns>返回bool类型</returns>
        public bool Add(List<T> entityList)
        {
            Dal.Add(entityList);
            DBSession.SaveChanges();
            return true;
        }

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回bool类型</returns>
        public bool Update(T entity)
        {
            Dal.Update(entity);
            return DBSession.SaveChanges();
        }

        /// <summary>
        /// 更新实体对象列表
        /// </summary>
        /// <param name="entityList">实体对象列表</param>
        /// <returns>返回bool类型</returns>
        public bool Update(List<T> entityList)
        {
            Dal.Update(entityList);
            return DBSession.SaveChanges();
        }

        /// <summary>
        /// 删除实体对象数据.不要随意调用。
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回bool类型</returns>
        public bool Delete(T entity)
        {
            Dal.Delete(entity);
            return DBSession.SaveChanges();
        }

        /// <summary>
        /// 删除符合表达式条件数据
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns>返回bool类型</returns>
        public bool Delete(Expression<Func<T, bool>> where)
        {
            Dal.Delete(where);
            return DBSession.SaveChanges();
        }
    }
}
