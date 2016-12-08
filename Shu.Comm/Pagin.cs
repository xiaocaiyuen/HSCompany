using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Shu.Model;
using System.Web;
using System.Configuration;

namespace Shu.Comm
{
    public class Pagin
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ToString();

        #region 通用删除

        public bool Delete(string tableName, string key, string value)
        {
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


            return ExecuteSql(strSql) > 0;
        }


        public bool Delete(string tableName, string key, string value, string strWhere)
        {
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

            return ExecuteSql(strSql) > 0;
        }



        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        #endregion

        #region 无分页查询

        public DataSet GetData(string tableName, string fieldList, string sqlWhere, string order)
        {
            string sql = "Select " + fieldList + " FROM " + tableName;

            if (sqlWhere != "")
            {
                sql += " Where " + sqlWhere;
            }

            if (order != "")
            {
                sql += " Order by " + order;
            }

            return Query(sql);

        }

        #endregion

        #region 通用查询

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }


        #endregion

        #region 分页

        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="totalcount"></param>
        /// <param name="totalpagecount"></param>
        /// <returns></returns>
        public DataSet PageData(string tableName, string fieldList, string primaryKey, string orderby, int PageSize, int PageIndex, string strWhere, out int totalcount, out int totalpagecount)
        {
            PaginModel model = new PaginModel();
            model.TableName = tableName;
            model.FieldList = fieldList;
            model.PrimaryKey = primaryKey;
            model.WhereStr = strWhere;

            if (orderby != "")
            {
                model.Order = orderby + " ," + primaryKey + " desc";
            }
            else
            {
                model.Order = primaryKey + " desc ";
            }
            model.SortType = 3;
            model.PageSize = PageSize;
            model.PageIndex = PageIndex;
            model.RecorderCount = 0;
            return GetPaginList(model, out totalcount, out totalpagecount);
        }
        #endregion



        #region 分页存储过程
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="totalcount">out 总记录数</param>
        /// <param name="totalpagecount">out 总页数</param>
        /// <returns></returns>
        private DataSet RunProceForPagin(string storedProcName, IDataParameter[] parameters, out int totalcount, out int totalpagecount)
        {//

            try
            {

                SqlConnection conn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(storedProcName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                foreach (SqlParameter pram in parameters)
                {
                    cmd.Parameters.Add(pram);
                }
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter();
                DataSet ds = new DataSet();
                sda.SelectCommand = cmd;
                conn.Close();
                sda.Fill(ds);
                totalcount = int.Parse(cmd.Parameters[9].Value.ToString());
                totalpagecount = cmd.Parameters[10].Value.ToString() == "" ? 0 : int.Parse(cmd.Parameters[10].Value.ToString());
                return ds;
            }
            catch (Exception ex)
            {
                //    HttpContext.Current.Response.Write(ex.Message);
                // 
                HttpContext.Current.Response.Redirect("~/SessionError.htm");
            }

            totalcount = 0;
            totalpagecount = 0;
            return null;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">PaginModel</param>
        /// <param name="totalcount">记录数</param>
        /// <param name="totalpagecount">总页数</param>
        /// <returns></returns>
        private DataSet GetPaginList(PaginModel model, out int totalcount, out int totalpagecount)
        {
            //
            SqlParameter[] prams ={
                               new SqlParameter("@TableName",SqlDbType.VarChar), //表名
                               new SqlParameter("@FieldList",SqlDbType.VarChar),//显示列名，如果是全部字段则为*
                               new SqlParameter("@PrimaryKey",SqlDbType.VarChar),//单一主键或唯一值键
                               new SqlParameter("@Where",SqlDbType.VarChar),//查询条件 不含'where'字符，如id>10 and len(userid)>9
                               new SqlParameter("@Order",SqlDbType.VarChar),//排序 不含'order by'字符，如id asc,userid desc，必须指定asc或desc    
                               new SqlParameter("@SortType",SqlDbType.Int), //注意当@SortType=3时生效，记住一定要在最后加上主键,排序规则 1:正序asc 2:倒序desc 3:多列排序方法
                               new SqlParameter("@RecorderCount",SqlDbType.Int),//记录总数 0:会返回总记录
                               new SqlParameter("@PageSize",SqlDbType.Int), //每页输出的记录数
                               new SqlParameter("@PageIndex",SqlDbType.Int), //当前页数
                               new SqlParameter("@TotalCount",SqlDbType.Int),//记返回总记录数
                               new SqlParameter("@TotalPageCount",SqlDbType.Int)};//返回总页数

            prams[0].Value = model.TableName;//表名
            prams[1].Value = model.FieldList;//字段名
            prams[2].Value = model.PrimaryKey;//主键
            prams[3].Value = model.WhereStr;//条件
            prams[4].Value = model.Order;//排序 不含'order by'字符，如id asc;userid desc，必须指定asc或desc
            prams[5].Value = model.SortType;//排序规则 1:正序asc 2:倒序desc 3:多列排序方法
            prams[6].Value = model.RecorderCount;
            prams[7].Value = model.PageSize;//每页显示条数
            prams[8].Value = model.PageIndex;//页数
            prams[9].Direction = ParameterDirection.Output;
            prams[10].Direction = ParameterDirection.Output;
            DataSet ds = RunProceForPagin("Paging", prams, out totalcount, out totalpagecount);
            return ds;
        }




        #endregion


    }
}
