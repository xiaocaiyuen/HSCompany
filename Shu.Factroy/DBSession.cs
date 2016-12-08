using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Shu.DAL;
using Shu.IDAL;
using Shu.Model;

namespace Shu.Factroy
{
    /// <summary>
    /// 数据会话层：就是一个工厂类，负责完成所有数据操作类实例的创建，然后业务层通过数据会话层来获取要操作数据类的实例。所以数据会话层将业务层与数据层解耦。
    /// 在数据会话层中提供一个方法：完成所有数据的保存。
    /// </summary>
    public partial class DBSession : IDBSession
    {
       public ShuEntities Db
       {
           get
           {
               return  DBContextFactory.CreateDbContext(); 
           }
       }
       /// <summary>
       /// 一个业务中经常涉及到对多张操作，我们希望链接一次数据库，完成对张表数据的操作。提高性能。 工作单元模式。
       /// </summary>
       /// <returns></returns>
       public bool SaveChanges()
       {
           return Db.SaveChanges() > 0;
       }
       public int ExecuteSql(string sql, params SqlParameter[] pars)
       {
          return Db.Database.ExecuteSqlCommand(sql,pars);
       }
       public List<T> ExecuteQuery<T>(string sql, params SqlParameter[] pars)
       {
           return Db.Database.SqlQuery<T>(sql,pars).ToList();
       }

    }
}
