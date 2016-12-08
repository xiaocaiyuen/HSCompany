using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Shu.Model;

namespace Shu.IDAL
{
    /// <summary>
    /// 公共接口
    /// </summary>
    public partial interface ICommon_DAL
    {
        /// <summary>
        /// 无分页查询
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldList">字段</param>
        /// <param name="sqlWhere">条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        DataSet GetData(string tableName, string fieldList, string sqlWhere, string order);

        /// <summary>
        /// 根据SQL语句查询
        /// </summary>
        /// <param name="strSQL">SQL</param>
        /// <returns></returns>
        DataTable GetDataBySQL(string strSQL);

        /// <summary>
        /// 根据SQL语句查询数量
        /// </summary>
        /// <param name="strSQL">SQL</param>
        /// <returns></returns>
        string GetCount(string strSQL);

        /// <summary>
        /// 自定义删除
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应值</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        bool DeleteByCustom(string tableName, string key, string value, string strWhere);

        bool ModifyState(string tableName, string key, string value, string stateName, string state);

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
        DataSet PageData(string tableName, string fieldList, string primaryKey, string orderBy, int pageSize, int pageIndex, string strWhere, out int totalCount, out int totalPageCount);


        /// <summary>
        /// 获取分页List
        /// </summary>
        /// <param name="model">分页Model</param>
        /// <param name="totalCount">记返回总记录数</param>
        /// <param name="totalPageCount">返回总页数</param>
        /// <returns>数据集</returns>
        DataSet GetPaginList(PaginModel model, out int totalCount, out int totalPageCount);

        /// <summary>
        /// 通用调用存储过程
        /// </summary>
        /// <param name="model">存储过程Model</param>
        /// <param name="ParameterList">输出参数List</param>
        /// <returns>数据集</returns>
        DataSet ExecStoredProcedures(StoredProcModel model, out List<ParameterModel> ParameterList);

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="instanceID">流程实例</param>
        /// <param name="modelID">业务ID</param>
        /// <param name="iSetp">当前步骤</param>
        /// <param name="InstanceState">当前状态</param>
        /// <returns></returns>
        string F_GetWorkflowState(string instanceID, string modelID, int? iSetp, string InstanceState);


        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="modelID">业务ID</param>
        /// <returns></returns>
        string F_GetWorkflowStateEx(string modelID);
    }
}
