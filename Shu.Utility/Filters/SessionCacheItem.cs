using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shu.Utility.Filters
{
    [Serializable]
    public class SessionCacheItem
    {
        public string AppKey { get; set; }

        public string UserName { get; set; }

        public DateTime InvalidTime { get; set; }

        public string UserID { get; set; }

        public string LoginUserName { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public string PostName { get; set; }

        public string PostID { get; set; }

        public string RoleName { get; set; }

        public string RoleID { get; set; }

        public string Ip { get; set; }

        public string UserType { get; set; }

        public string DepType { get; set; }

    }
}