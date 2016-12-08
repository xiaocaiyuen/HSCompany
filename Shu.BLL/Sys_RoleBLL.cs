using Shu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shu.BLL
{
    public partial class Sys_RoleBLL : BaseBLL<Sys_Role>
    {
        public bool Add(Sys_Role model, List<Sys_RolePurview> list)
        {
            this.DBSession.Sys_RoleDal.Add(model);
            this.DBSession.Sys_RolePurviewDal.Add(list);
            return DBSession.SaveChanges();
        }

        public bool Update(Sys_Role model, List<Sys_RolePurview> list)
        {
            this.DBSession.Sys_RoleDal.Update(model);
            this.DBSession.Sys_RolePurviewDal.Delete(p=>p.RolePurviewID== model.RoleID);
            this.DBSession.Sys_RolePurviewDal.Add(list);
            return this.DBSession.SaveChanges();

        }
    }
}
