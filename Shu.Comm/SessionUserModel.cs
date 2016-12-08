using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Model
{
    /// <summary>
    /// session存储的用户信息
    /// </summary>
    [Serializable] 
    public class SessionUserModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
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

        public string RoleShowType { get; set; }

        //public string RoleDataMapping_Name { get; set; }
    }
}
