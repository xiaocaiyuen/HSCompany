using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shu.BLL;
using Shu.Model;
using System.Data;
using Shu.Comm;
using Shu.Utility.Extensions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace Shu.Manage
{
    public class General : BasePage
    {
        public static void GetP_Business_SystemEvaluate(string ApplyId)
        {
            Common_BLL ComBLL = new Common_BLL();
            List<ParameterModel> ParameterModel = null;
            List<ParameterModel> ParameterList = new List<ParameterModel>();
            ParameterList.Add(new ParameterModel { ParamName = "ApplyBasicID", ParamValue = ApplyId, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            ComBLL.ExecStoredProcedures(new StoredProcModel { ProcName = "P_Business_SystemEvaluate", ParameterList = ParameterList }, out ParameterModel);
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="UserName">客户名称</param>
        /// <param name="Phone">手机号码</param>
        /// <param name="Content">
        /// Content说明：放款提醒格式：
        /// 客户名称,业务申请显示编号还款提醒格式：
        /// 客户名称,还款日期（日）逾期提醒格式：
        /// 客户名称,还款期数,还款日期（月日）,逾期天数
        /// </param>
        /// <param name="Model">Model说明：
        /// LoanSMSTemplate：财务放款短信模板；
        /// RepaymentSMS：还款提醒短信模板；
        /// BeOverdueSMS：逾期短信提醒模板；
        /// </param>
        /// <param name="IsAuto">是否自动手动（flase：手动 true： 自动）</param>
        public static void GetP_Msg_SendSMS(string UserName, string Phone, string Content, string Model, bool IsAuto)
        {
            Common_BLL ComBLL = new Common_BLL();
            List<ParameterModel> ParameterModel = null;
            List<ParameterModel> ParameterList = new List<ParameterModel>();
            ParameterList.Add(new ParameterModel { ParamName = "UserName", ParamValue = UserName, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            ParameterList.Add(new ParameterModel { ParamName = "Phone", ParamValue = Phone, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            ParameterList.Add(new ParameterModel { ParamName = "Content", ParamValue = Content, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            ParameterList.Add(new ParameterModel { ParamName = "Model", ParamValue = Model, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            ParameterList.Add(new ParameterModel { ParamName = "IsAuto", ParamValue = IsAuto, ParamMode = ParamEnumMode.InMode, ParamType = DbType.Boolean });
            ComBLL.ExecStoredProcedures(new StoredProcModel { ProcName = "P_Msg_SendSMS", ParameterList = ParameterList }, out ParameterModel);
        }

        /// <summary>
        /// 业务统计
        /// </summary>
        /// <param name="ApplyDateStart">申请开始时间</param>
        /// <param name="ApplyDateEnd">申请结束时间</param>
        /// <param name="Province">省</param>
        /// <param name="PageSize">每页输出的记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="TotalCount">记返回总记录</param>
        public static DataSet GetP_Business_Statistics(string ApplyDateStart, string ApplyDateEnd, string Province, int PageSize, int PageIndex, out int TotalCount)
        {
            TotalCount = int.MinValue;
            Common_BLL ComBLL = new Common_BLL();
            List<ParameterModel> ParameterModel = new List<ParameterModel>();
            List<ParameterModel> ParameterList = new List<ParameterModel>();
            ParameterList.Add(new ParameterModel { ParamName = "ApplyDateStart", ParamValue = ApplyDateStart, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            ParameterList.Add(new ParameterModel { ParamName = "ApplyDateEnd", ParamValue = ApplyDateEnd, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            ParameterList.Add(new ParameterModel { ParamName = "Province", ParamValue = Province, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            ParameterList.Add(new ParameterModel { ParamName = "PageSize", ParamValue = PageSize, ParamMode = ParamEnumMode.InMode, ParamType = DbType.Int32 });
            ParameterList.Add(new ParameterModel { ParamName = "PageIndex", ParamValue = PageIndex, ParamMode = ParamEnumMode.InMode, ParamType = DbType.Int32 });
            ParameterList.Add(new ParameterModel { ParamName = "TotalCount", ParamValue = TotalCount, ParamMode = ParamEnumMode.OutMode, ParamType = DbType.Int32 });

            ParameterModel.Add(new ParameterModel { ParamName = "TotalCount", ParamValue = TotalCount, ParamMode = ParamEnumMode.OutMode, ParamType = DbType.Int32 });

            return ComBLL.ExecStoredProcedures(new StoredProcModel { ProcName = "P_Business_Statistics", ParameterList = ParameterList }, out ParameterModel);
        }

        /// <summary>
        /// 流水
        /// </summary>
        /// <param name="ApplyBasisId">业务ID</param>
        /// <param name="Number">前缀名称编号</param>
        /// <param name="ContractNO">返回的流水编号</param>
        public static void GetP_ContractLS(string ApplyBasisId, string Number, out string ContractNO)
        {
            ContractNO = string.Empty;
            Common_BLL ComBLL = new Common_BLL();
            List<ParameterModel> ParameterModel = new List<ParameterModel>();
            List<ParameterModel> ParameterList = new List<ParameterModel>();
            ParameterList.Add(new ParameterModel { ParamName = "ApplyBasisId", ParamValue = ApplyBasisId, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            ParameterList.Add(new ParameterModel { ParamName = "Number", ParamValue = Number, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });

            ParameterModel.Add(new ParameterModel { ParamName = "ContractNO", ParamValue = ContractNO, ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            ComBLL.ExecStoredProcedures(new StoredProcModel { ProcName = "P_ContractLS", ParameterList = ParameterList }, out ParameterModel);
        }

        /// <summary>
        /// 已办任务
        /// </summary>
        /// <param name="TYPE">--数据类型（TQHK：提前还款，YWQX：业务取消，ZLBG：客户资料变更，ZLKD：快递资料，XBZL：续保，LPZL：理赔，LPKD：理赔资料快递，JYTX：解押提醒，YQJM：逾期减免，默认：业务申请。）</param>
        /// <param name="FieldList">显示列名，如果是全部字段则为*</param>
        /// <param name="PrimaryKey">单一主键或唯一值键</param>
        /// <param name="Where">查询条件</param>
        /// <param name="Order">--排序 不含'order by'字符，如id asc,userid desc，必须指定asc或desc --注意当 @SortType = 3时生效，记住一定要在最后加上主键</param>
        /// <param name="UserID">当前用户</param>
        /// <param name="PageSize">每页输出的记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataSet GetP_GetPendingMatterExAlready(string TYPE, string FieldList, string PrimaryKey, string Where, string Order, string UserID, int PageSize, int PageIndex, out int TotalCount)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("P_GetPendingMatterExAlready");
                db.AddInParameter(dbCommand, "TYPE", DbType.AnsiString, TYPE);
                db.AddInParameter(dbCommand, "FieldList", DbType.AnsiString, FieldList);
                db.AddInParameter(dbCommand, "PrimaryKey", DbType.AnsiString, PrimaryKey);
                db.AddInParameter(dbCommand, "Where", DbType.AnsiString, Where);
                db.AddInParameter(dbCommand, "Order", DbType.AnsiString, Order + " ," + PrimaryKey + " desc");
                db.AddInParameter(dbCommand, "UserID", DbType.AnsiString, UserID);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, PageSize);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                db.AddOutParameter(dbCommand, "TotalCount", DbType.Int32, int.MaxValue);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                TotalCount = (int)db.GetParameterValue(dbCommand, "TotalCount");
                return ds;
            }
            catch (Exception ex)
            {
                Shu.Comm.CMMLog.Error(ex.Message);
            }
            TotalCount = 0;
            return new DataSet();
        }

        /// <summary>
        /// 担保人历史纪录
        /// </summary>
        /// <param name="TYPE">--数据类型（1：身份证，2：手机号码。）</param>
        /// <param name="FieldList">显示列名，如果是全部字段则为*</param>
        /// <param name="PrimaryKey">单一主键或唯一值键</param>
        /// <param name="Where">查询条件</param>
        /// <param name="Order">--排序 不含'order by'字符，如id asc,userid desc，必须指定asc或desc --注意当 @SortType = 3时生效，记住一定要在最后加上主键</param>
        /// <param name="TenantID">查询标记字段</param>
        /// <param name="PageSize">每页输出的记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataSet GetP_GetBusiness_ApplyBasisHistory(int TYPE, string FieldList, string PrimaryKey, string Where, string Order, string TenantID, int PageSize, int PageIndex, out int TotalCount)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("P_GetBusiness_ApplyBasisHistory");
                db.AddInParameter(dbCommand, "TYPE", DbType.Int32, TYPE);
                db.AddInParameter(dbCommand, "FieldList", DbType.AnsiString, FieldList);
                db.AddInParameter(dbCommand, "PrimaryKey", DbType.AnsiString, PrimaryKey);
                db.AddInParameter(dbCommand, "Where", DbType.AnsiString, Where);
                db.AddInParameter(dbCommand, "Order", DbType.AnsiString, Order + " ," + PrimaryKey + " desc");
                db.AddInParameter(dbCommand, "TenantID", DbType.AnsiString, TenantID);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, PageSize);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                db.AddOutParameter(dbCommand, "TotalCount", DbType.Int32, int.MaxValue);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                TotalCount = (int)db.GetParameterValue(dbCommand, "TotalCount");
                return ds;
            }
            catch (Exception ex)
            {
                Shu.Comm.CMMLog.Error(ex.Message);
            }
            TotalCount = 0;
            return new DataSet();
        }


        /// <summary>
        /// 客户应收明细
        /// </summary>
        /// <param name="ApplyBasisID">申请编号</param>
        /// <returns></returns>
        public static void GetP_AutoCustomerReceivedDetail(string ApplyBasisID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("P_AutoCustomerReceivedDetail");
                db.AddInParameter(dbCommand, "ApplyBasisID", DbType.AnsiString, ApplyBasisID);
                db.ExecuteScalar(dbCommand);
            }
            catch (Exception ex)
            {
                Shu.Comm.CMMLog.Error(ex.Message);
            }
        }

        public static string OpinionContent(string state, string content)
        {
            if (content.IsNullOrEmpty())
            {
                return string.Format(Constant.OpinionContentNULL, state);
            }
            else
            {
                return string.Format(Constant.OpinionContent, state, content);
            }
        }
    }
}