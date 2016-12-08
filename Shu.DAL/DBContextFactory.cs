using Shu.IDAL;
using Shu.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Shu.DAL
{
    /// <summary>
    /// 负责创建EF数据操作上下文实例，必须保证线程内唯一.
    /// </summary>
    public class DBContextFactory
    {
        //线程槽
        public static ShuEntities CreateDbContext()
        {
            ShuEntities dbContext = (ShuEntities)CallContext.GetData("dbContext");
            if (dbContext == null)
            {
                dbContext = new ShuEntities();
                CallContext.SetData("dbContext", dbContext);
            }
            return dbContext;
        }
    }
}
