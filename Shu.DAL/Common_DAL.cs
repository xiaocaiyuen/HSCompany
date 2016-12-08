using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shu.IDAL;
using System.Data;
using Shu.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Shu.DAL
{
    public partial class Common_DAL : ICommon_DAL
    {
        DatabaseProviderFactory factory = new DatabaseProviderFactory();
        /// <summary>
        /// 无分页查询
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldList">字段</param>
        /// <param name="sqlWhere">条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public DataSet GetData(string tableName, string fieldList, string sqlWhere, string orderBy)
        {

            try
            {
                //Database db = DatabaseFactory.CreateDatabase();
                Database db = factory.CreateDefault();
                string strSql = "Select " + fieldList + " FROM " + tableName;

                if (sqlWhere != "")
                {
                    strSql += " Where " + sqlWhere;
                }

                if (orderBy != "")
                {
                    strSql += " Order by " + orderBy;
                }

                DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
                return db.ExecuteDataSet(dbCommand);
            }
            catch
            {
                return new DataSet();
            }
        }

        /// <summary>
        /// 根据SQL语句查询
        /// </summary>
        /// <param name="strSQL">SQL</param>
        /// <returns></returns>
        public DataTable GetDataBySQL(string strSQL)
        {
            try
            {
                //Database db = DatabaseFactory.CreateDatabase();
                Database db = factory.CreateDefault();
                DbCommand dbCommand = db.GetSqlStringCommand(strSQL);
                return db.ExecuteDataSet(dbCommand).Tables[0];
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// 根据SQL语句查询数量
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public string GetCount(string strSQL)
        {
            try
            {
                //Database db = DatabaseFactory.CreateDatabase();
                Database db = factory.CreateDefault();
                DbCommand dbCommand = db.GetSqlStringCommand(strSQL);
                return db.ExecuteScalar(dbCommand).ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 自定义删除
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应值</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public bool DeleteByCustom(string tableName, string key, string value, string strWhere)
        {
            int rows = 0;
            try
            {
                //Database db = DatabaseFactory.CreateDatabase();
                Database db = factory.CreateDefault();
                string strSql = " DELETE FROM " + tableName + " WHERE ";

                int len = value.Split(',').Length;

                if (len > 1)
                {
                    strSql += key + " in (" + value + ")";


                }
                else
                {
                    strSql += key + " = " + value;
                }

                strSql += strWhere;
                DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
                rows = db.ExecuteNonQuery(dbCommand);

            }
            catch (Exception ex)
            {
                rows = 0;
            }

            return rows > 0;
        }

        public bool ModifyState(string tableName, string key, string value, string stateName, string state)
        {
            //Database db = DatabaseFactory.CreateDatabase();
            Database db = factory.CreateDefault();
            string strSql = "UPDATE " + tableName + " SET " + stateName + " = '" + state + "'  WHERE ";
            int len = value.Split(',').Length;

            if (len > 1)
            {
                strSql += key + " in (" + value + ")";


            }
            else
            {
                strSql += key + " = " + value;
            }

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            int rows = db.ExecuteNonQuery(dbCommand);

            return rows > 0;
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldList">显示列名，如果是全部字段则为*</param>
        /// <param name="primaryKey">单一主键或唯一值键</param>
        /// <param name="orderBy">排序 不含'order by'字符，如id asc,userid desc，必须指定asc或desc </param>
        /// <param name="pageSize">每页输出的记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="strWhere">查询条件 不含'where'字符，如id>10 and len(userid)>9</param>
        /// <param name="totalCount">记返回总记录数</param>
        /// <param name="totalPageCount">返回总页数</param>
        /// <returns>分页数据集</returns>
        public DataSet PageData(string tableName, string fieldList, string primaryKey, string orderBy, int pageSize, int pageIndex, string strWhere, out int totalCount, out int totalPageCount)
        {
            PaginModel model = new PaginModel();
            model.TableName = tableName;
            model.FieldList = fieldList;
            model.PrimaryKey = primaryKey;
            model.WhereStr = strWhere;

            if (orderBy != "")
            {
                model.Order = orderBy + " ," + primaryKey + " desc";
            }
            else
            {
                model.Order = primaryKey + " desc ";
            }
            model.SortType = 3;
            model.PageSize = pageSize;
            model.PageIndex = pageIndex;
            model.RecorderCount = 0;
            return GetPaginList(model, out totalCount, out totalPageCount);
        }


        /// <summary>
        /// 获取分页List
        /// </summary>
        /// <param name="model">分页Model</param>
        /// <param name="totalCount">记返回总记录数</param>
        /// <param name="totalPageCount">返回总页数</param>
        /// <returns>数据集</returns>
        public DataSet GetPaginList(PaginModel model, out int totalCount, out int totalPageCount)
        {
            try
            {
                //Database db = DatabaseFactory.CreateDatabase();
                Database db = factory.CreateDefault();
                DbCommand dbCommand = db.GetStoredProcCommand("Paging");
                db.AddInParameter(dbCommand, "TableName", DbType.AnsiString, model.TableName);
                db.AddInParameter(dbCommand, "FieldList", DbType.AnsiString, model.FieldList);
                db.AddInParameter(dbCommand, "PrimaryKey", DbType.AnsiString, model.PrimaryKey);
                db.AddInParameter(dbCommand, "Where", DbType.AnsiString, model.WhereStr);
                db.AddInParameter(dbCommand, "Order", DbType.AnsiString, model.Order);
                db.AddInParameter(dbCommand, "SortType", DbType.Int32, model.SortType);
                db.AddInParameter(dbCommand, "RecorderCount", DbType.Int32, model.RecorderCount);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, model.PageSize);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, model.PageIndex);
                db.AddOutParameter(dbCommand, "TotalCount", DbType.Int32, int.MaxValue);
                db.AddOutParameter(dbCommand, "TotalPageCount", DbType.Int32, int.MaxValue);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                totalCount = (int)db.GetParameterValue(dbCommand, "TotalCount");
                totalPageCount = (int)db.GetParameterValue(dbCommand, "TotalPageCount");
                return ds;
            }
            catch (Exception ex)
            {
                Shu.Comm.CMMLog.Error(ex.Message);
                //HttpContext.Current.Response.Write(ex.Message);
                // 
                //HttpContext.Current.Response.Redirect("~/SessionError.htm");
                //throw new Exception(ex.Message);
            }

            totalCount = 0;
            totalPageCount = 0;
            return new DataSet();
        }


        /// <summary>
        /// 通用调用存储过程
        /// </summary>
        /// <param name="model">存储过程Model</param>
        /// <param name="ParameterList">输出参数List</param>
        /// <returns>数据集</returns>
        public DataSet ExecStoredProcedures(StoredProcModel model, out List<ParameterModel> ParameterList)
        {
            try
            {
                //Database db = DatabaseFactory.CreateDatabase();
                Database db = factory.CreateDefault();
                DbCommand dbCommand = db.GetStoredProcCommand(model.ProcName);
                foreach (ParameterModel Param in model.ParameterList)
                {
                    if (Param.ParamMode == ParamEnumMode.InMode)//输入参数
                    {
                        db.AddInParameter(dbCommand, Param.ParamName, Param.ParamType, Param.ParamValue);
                    }
                    if (Param.ParamMode == ParamEnumMode.OutMode)//输出参数
                    {
                        db.AddOutParameter(dbCommand, Param.ParamName, Param.ParamType, int.MaxValue);
                    }
                }

                DataSet ds = db.ExecuteDataSet(dbCommand);
                ParameterList = new List<ParameterModel>();
                foreach (ParameterModel Param in model.ParameterList)
                {
                    if (Param.ParamMode == ParamEnumMode.OutMode)//输出参数
                    {
                        Param.ParamResults = db.GetParameterValue(dbCommand, Param.ParamName);
                        ParameterList.Add(Param);
                    }
                }

                return ds;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
            ParameterList = new List<ParameterModel>();
            return new DataSet();
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="instanceID">流程实例</param>
        /// <param name="modelID">业务ID</param>
        /// <param name="iSetp">当前步骤</param>
        /// <param name="InstanceState">当前状态</param>
        /// <returns></returns>
        public string F_GetWorkflowState(string instanceID, string modelID, int? iSetp, string InstanceState)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string strSql = "select dbo.F_GetWorkflowState('" + instanceID + "','" + modelID + "','','') as v";
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            return db.ExecuteDataSet(dbCommand).Tables[0].Rows[0][0].ToString();
        }
        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="modelID">业务ID</param>
        /// <returns></returns>
        public string F_GetWorkflowStateEx(string modelID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string strSql = "select dbo.F_GetWorkflowStateEx('" + modelID + "') as v";
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            return db.ExecuteDataSet(dbCommand).Tables[0].Rows[0][0].ToString();
        }
    }
}
