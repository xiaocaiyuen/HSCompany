using Shu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shu.BLL
{
    public partial class Sys_DepartmentBLL : BaseBLL<Sys_Department>
    {
        /// <summary>
        /// 判断编码是否存在
        /// </summary>
        /// <returns></returns>
        protected bool IsExitsCode(string code)
        {
            //List<Sys_Department> listDep = GetList(p=>p.Department_Code== code).ToList();
            //if (listDep.Count == 0)
            if (this.Exists(p => p.Department_Code == code))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 得到编码
        /// </summary>
        /// <param name="depCode">父级或同级编码</param>
        /// <param name="strLevel">父级(same),下级(son)</param>
        /// <returns></returns>
        public string GetCode(string depCode, string strLevel)
        {
            string strNewCode = "";
            string strWhere = "";
            if (strLevel == "same") //新增同级
            {
                if (depCode.Length <= 3) //根目录或为空
                {
                    strWhere = " len(Department_Code)=3";
                }
                else
                {
                    string strPcode = depCode.Substring(0, depCode.Length - 3);
                    strWhere = " Department_ParentCode='" + strPcode + "'";
                }
            }
            else
            {
                strWhere = " Department_ParentCode='" + depCode + "'";
            }
            List<Sys_Department> listDep = DBSession.ExecuteQuery<Sys_Department>("Select *  from " + typeof(Sys_Department).Name + " where " + strWhere);//FindWhere(strWhere);
            int count = listDep.Count + 1;
            while (count <= 999)
            {
                if (count < 10)
                {
                    strNewCode = depCode + "00" + count;
                }
                else if (count < 99)
                {
                    strNewCode = depCode + "0" + count;
                }
                else
                {
                    strNewCode = depCode + count.ToString();
                }
                count++;
                if (!IsExitsCode(strNewCode))
                {
                    break;
                }
            }
            return strNewCode;
        }

        /// <summary>
        /// 特殊情况Sql语句查询数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <returns></returns>
        public List<Sys_Department> FindWhere(string SqlWhere)
        {
            return DBSession.ExecuteQuery<Sys_Department>("Select *  from " + typeof(Sys_Department).Name + " where " + SqlWhere);
        }
    }
}
