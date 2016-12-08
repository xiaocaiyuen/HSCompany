using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Shu.IDAL;
using System.Data.Entity;
using Shu.Model;

namespace Shu.DAL
{
    public class BaseDal<T> : IBaseDal<T> where T : class, new()
    {
        ShuEntities Db = DBContextFactory.CreateDbContext();//完成EF上下文创建.DbContext
        public bool Add(List<T> entityList)
        {
            foreach (T item in entityList)
            {
                Db.Set<T>().Add(item);
            }
            return true;
        }

        public bool Add(T entity)
        {
            Db.Set<T>().Add(entity);
            return true;
        }

        public T AddEntity(T entity)
        {
            Db.Set<T>().Add(entity);
            return entity;
        }

        public bool Delete(Expression<Func<T, bool>> where)
        {
            var entity = Db.Set<T>().Where<T>(where);
            Db.Set<T>().RemoveRange(entity);
            return true;
        }

        public bool Delete(T entity)
        {
            Db.Entry<T>(entity).State = EntityState.Deleted;
            return true;
        }

        public bool Exists(Expression<Func<T, bool>> where)
        {
            return Db.Set<T>().Any(where);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return Db.Set<T>().FirstOrDefault<T>(where);
        }

        public int GetCount(Expression<Func<T, bool>> where)
        {
            return Db.Set<T>().Where<T>(where).Count();
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> where)
        {
            return Db.Set<T>().Where<T>(where);
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderby, bool isAsc)
        {
            int totalCount = 0;
            return GetList<object>(1, 0, out totalCount, where, orderby, isAsc);
        }

        public IQueryable<T> GetList<S>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> where, Expression<Func<T, S>> orderby, bool isAsc)
        {
            var temp = Db.Set<T>().Where<T>(where);
            totalCount = 0;
            if (pageSize != 0)
            {
                totalCount = temp.Count();
                temp = temp.Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }

            if (isAsc)//升序
            {
                temp = temp.OrderBy<T, S>(orderby);
            }
            else
            {
                temp = temp.OrderByDescending<T, S>(orderby);
            }
            return temp;
        }

        public bool Update(T entity)
        {
            Db.Entry<T>(entity).State = EntityState.Modified;
            return true;
        }

        public bool Update(List<T> entityList)
        {
            foreach (T item in entityList)
            {
                Db.Entry<T>(item).State = EntityState.Modified;
            }
            return true;
        }
    }
}
