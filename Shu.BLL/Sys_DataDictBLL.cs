using Shu.Model;
using Shu.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shu.BLL
{
    public partial class Sys_DataDictBLL : BaseBLL<Sys_DataDict>
    {
        public string GetCode(string depCode, string strLevel)
        {
            string strNewCode = "";
            Expression<Func<Sys_DataDict, bool>> expression = t => true;
            //string strWhere = "";
            if (strLevel == "same") //新增同级
            {
                if (depCode.Length <= 2) //根目录或为空
                {
                    //strWhere = " len(DataDict_Code)=2";
                    expression = Shu.Comm.Utility.And(expression, p => p.DataDict_Code.Length == 2);
                }
                else
                {
                    string strPcode = depCode.Substring(0, depCode.Length - 2);
                    //strWhere = " DataDict_ParentCode='" + strPcode + "'";
                    expression = Shu.Comm.Utility.And(expression, p => p.DataDict_ParentCode == strPcode);
                }
            }
            else
            {
                //strWhere = " DataDict_ParentCode='" + depCode + "'";
                expression = Shu.Comm.Utility.And(expression, p => p.DataDict_ParentCode == depCode);
            }
            //List<Sys_DataDict> listDep = FindWhere(strWhere);
            List<Sys_DataDict> listDep = GetList(expression).ToList();
            int count = listDep.Count + 1;
            while (count <= 99)
            {
                if (count < 10)
                {
                    if (depCode != "0")
                    {
                        strNewCode = depCode + "0" + count;
                    }
                    else
                    {
                        strNewCode = depCode + count;
                    }
                }
                else
                {
                    if (depCode != "0")
                    {
                        strNewCode = depCode + count.ToString();
                    }
                    else
                    {
                        strNewCode = count.ToString();
                    }
                }
                List<Sys_DataDict> listDic = GetList(p => p.DataDict_Code == strNewCode).ToList(); //FindWhere("DataDict_Code='" + strNewCode + "'");
                if (listDic.Count == 0)
                {
                    break;
                }
                count++;
            }
            return strNewCode;
        }
        public Sys_DataDict FindByCode(string Code)
        {
            Sys_DataDict Dic = new Sys_DataDict();
            List<Sys_DataDict> DicList = GetList(p=>p.DataDict_Code==Code).ToList(); //FindWhere(" DataDict_Code='" + Code + "'");
            if (DicList.Count > 0)
            {
                Dic = DicList[0];
            }
            else
            {
                Dic = null;
            }
            return Dic;
        }
    }
}
