using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shu.IDAL;
using System.Data;
using Shu.Model;
using System.Data.SqlClient;
using System.Configuration;
using Shu.Factroy;
using System.Web.UI.WebControls;
using Shu.Comm;
using Shu.BLL;

namespace Shu.BLL
{
    /// <summary>
    /// 公共业务方法
    /// </summary>
    public partial class Common_BLL
    {
        private readonly ICommon_DAL dal = AbstractFactory.CreateCommon_DAL();
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ToString();

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
            return dal.GetData(tableName, fieldList, sqlWhere, orderBy);

        }

        /// <summary>
        /// 根据SQL语句查询
        /// </summary>
        /// <param name="strSQL">SQL</param>
        /// <returns></returns>
        public DataTable GetDataBySQL(string strSQL)
        {
            return dal.GetDataBySQL(strSQL);
        }

        /// <summary>
        /// 根据SQL语句查询数量
        /// </summary>
        /// <param name="strSQL">SQL</param>
        /// <returns></returns>
        public string GetCount(string strSQL)
        {
            return dal.GetCount(strSQL);
        }

        /// <summary>
        /// 自定义删除
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应值</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public bool Delete(string tableName, string key, string value, string strWhere)
        {
            return dal.DeleteByCustom(tableName, key, value, strWhere);
        }

        /// <summary>
        /// 所有表审核状态退回到预存
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="stateName"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool ModifyState(string tableName, string key, string value, string stateName, string state)
        {
            return dal.ModifyState(tableName, key, value, stateName, state);
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
            return dal.PageData(tableName, fieldList, primaryKey, orderBy, pageSize, pageIndex, strWhere, out totalCount, out totalPageCount);
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
            return dal.GetPaginList(model, out totalCount, out totalPageCount);
        }



        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="moduleName">功能模块名称，如 系统管理 >> 系统提醒 </param>
        /// <param name="dataId">操作数据id</param>
        /// <param name="type">日志类型，如新增、删除、登录、修改，请传递汉字</param>
        /// <param name="remark">备注，如果是删除的时候，可以把该删除数据的记录保存在该字段中</param>
        /// <param name="userId">操作人ID</param>
        /// <param name="userDepCode">操作部门代码</param>
        public void AddLog(string moduleName, string dataId, string type, string remark, string userId, string userDepCode)
        {
            try
            {
                Sys_Log Log = new Sys_Log();
                Sys_LogBLL bllSys_Log = new Sys_LogBLL();
                Log.SysLogID = Guid.NewGuid().ToString();
                Log.SysLog_OperateUserID = userId;
                Log.SysLog_OperateDep = userDepCode;
                Log.SysLog_OperateDate = DateTime.Now;
                Log.SysLog_TableName = string.Empty;
                Log.SysLog_OperateFunName = moduleName;
                Log.SysLog_OperateDataID = dataId;
                Log.SysLog_OperateType = type;
                Log.SysLog_Remark = remark;
                Log.SysLog_Ip = GetIP();
                bllSys_Log.Add(Log);
            }
            catch { }
        }

        /// <summary>
        /// 批量添加日志
        /// </summary>
        /// <param name="moduleName">功能模块名称，如 系统管理 >> 系统提醒 </param>
        /// <param name="dataId">操作数据id</param>
        /// <param name="type">日志类型，如新增、删除、登录、修改，请传递汉字</param>
        /// <param name="remark">备注，如果是删除的时候，可以把该删除数据的记录保存在该字段中</param>
        /// <param name="userId">操作人ID</param>
        /// <param name="userDepCode">操作部门代码</param>
        public void AddBatchLog(string moduleName, List<string> dataId, string type, string remark, string userId, string userDepCode)
        {
            try
            {
                string msg = string.Empty;
                Sys_LogBLL bllSys_Log = new Sys_LogBLL();
                List<Sys_Log> list_log = new List<Sys_Log>();
                foreach (var item in dataId)
                {
                    Sys_Log Log = new Sys_Log();
                    Log.SysLogID = Guid.NewGuid().ToString();
                    Log.SysLog_OperateUserID = userId;
                    Log.SysLog_OperateDep = userDepCode;
                    Log.SysLog_OperateDate = DateTime.Now;
                    Log.SysLog_TableName = string.Empty;
                    Log.SysLog_OperateFunName = moduleName;
                    Log.SysLog_OperateDataID = item;
                    Log.SysLog_OperateType = type;
                    Log.SysLog_Remark = remark;
                    Log.SysLog_Ip = GetIP();
                    list_log.Add(Log);
                }
                bllSys_Log.Add(list_log);
            }
            catch { }
        }

        /// <summary>
        /// 获取客户端真实IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string user_IP = string.Empty;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                user_IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                user_IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return user_IP;
        }


        /// <summary>
        /// 通过用户ID获取用户名称
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <returns>用户姓名</returns>
        public string GetUserNameByUserID(string uid)
        {
            string rtnVal = string.Empty;
            Sys_UserInfo userInfo = new Sys_UserInfoBLL().Get(p => p.UserInfoID == uid);
            if (userInfo != null)
            {
                rtnVal = userInfo.UserInfo_FullName;
            }
            return rtnVal;
        }




        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSystemSetting(string key)
        {
            string sRtn = string.Empty;
            List<Sys_Setting> Sys = new Sys_SettingBLL().GetList(p => p.Setting_Key == key).ToList();//" Setting_Key='" + key + "'");
            if (Sys.Count > 0)
            {
                sRtn = Sys[0].Setting_Value;
            }
            return sRtn;
        }


        /// <summary>
        /// 通用调用存储过程
        /// </summary>
        /// <param name="model">存储过程Model</param>
        /// <param name="ParameterList">输出参数List</param>
        /// <returns>数据集</returns>
        public DataSet ExecStoredProcedures(StoredProcModel model, out List<ParameterModel> ParameterList)
        {
            return dal.ExecStoredProcedures(model, out ParameterList);
        }

        #region 动态监控汇总中的方法
        /// <summary>
        /// 根据开始时间和截止时间获得所有的动态监控分值
        /// </summary>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public decimal GetTotalSocre(string sTime, string eTime, string userId)
        {
            decimal zwcp = ZwcpScore(sTime, eTime, userId);
            decimal zzcp = ZzcpScore(sTime, eTime, userId);
            decimal wlpl = WlplScore(sTime, eTime, userId);
            decimal xfjb = XfjbScore(sTime, eTime, userId);
            decimal wghjd = WghScore(sTime, eTime, userId);
            decimal mzcp = MzcpScore(sTime, eTime, userId);
            decimal totalScore = zwcp + zzcp + wlpl + xfjb + mzcp + wghjd;
            return totalScore;
        }

        /// <summary>
        /// 根据年份获得所有的动态监控分值
        /// </summary>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public decimal GetTotalSocre(string year, string userId)
        {
            decimal zwcp = ZwcpScore(year, userId);
            decimal zzcp = ZzcpScore(year, userId);
            decimal wlpl = WlplScore(year, userId);
            decimal xfjb = XfjbScore(year, userId);
            decimal wghjd = WghScore(year, userId);
            decimal mzcp = MzcpScore(year, userId);
            decimal totalScore = zwcp + zzcp + wlpl + xfjb + mzcp + wghjd;
            return totalScore;
        }

        /// <summary>
        /// 根据条件得到自我测评的分值
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal ZwcpScore(string sTime, string eTime, string userId)
        {
            DateTime time = Convert.ToDateTime(eTime);
            decimal score = 0;
            string sqlWhere = " SelfMonitor_AsseYear='" + time.Year.ToString() + "' and SelfMonitor_SendTime between '" + FormatTime(sTime) + "' and '" + FormatTime(eTime) + "' and SelfMonitor_EvaluUserID='" + userId + "' and SelfMonitor_InfoState='审核通过'";
            string strSql = "select sum(score) as score from View_DynMon_SelfMonitor where " + sqlWhere;
            DataSet myds = Query(strSql);
            if (myds.Tables[0].Rows.Count != 0 && myds.Tables[0].Rows[0]["score"] != null && myds.Tables[0].Rows[0]["score"].ToString() != "")
            {
                score = Math.Round(Convert.ToDecimal(myds.Tables[0].Rows[0]["score"].ToString()), 2);
            }
            return score;
        }
        /// <summary>
        /// 根据条件得到自我测评的分值
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal ZwcpScore(string year, string userId)
        {
            decimal score = 0;
            string sqlWhere = "SelfMonitor_AsseYear=" + year + " and SelfMonitor_EvaluUserID='" + userId + "' and SelfMonitor_InfoState='审核通过'";
            string strSql = "select sum(score) as score from View_DynMon_SelfMonitor where " + sqlWhere;
            DataSet myds = Query(strSql);
            if (myds.Tables[0].Rows.Count != 0 && myds.Tables[0].Rows[0]["score"] != null && myds.Tables[0].Rows[0]["score"].ToString() != "")
            {
                score = Math.Round(Convert.ToDecimal(myds.Tables[0].Rows[0]["score"].ToString()), 2);
            }
            return score;
        }

        /// <summary>
        /// 根据条件得到组织监测的分值
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal ZzcpScore(string sTime, string eTime, string userId)
        {
            DateTime time = Convert.ToDateTime(eTime);
            decimal score = 0;
            string sqlWhere = "OrganEvalu_EvaluYear='" + time.Year.ToString() + "' and OrganEvalu_AddTime between '" + FormatTime(sTime) + "' and '" + FormatTime(eTime) + "' and OrganEvalu_EvaluUserID='" + userId + "' and OrganEvalu_InfoState='审核通过'";
            string strSql = "select sum(fxscore) as fxscore from View_DynMon_OrganEvalu where " + sqlWhere;
            DataSet myds = Query(strSql);
            if (myds.Tables[0].Rows.Count != 0 && myds.Tables[0].Rows[0]["fxscore"] != null && myds.Tables[0].Rows[0]["fxscore"].ToString() != "")
            {
                score = Math.Round(Convert.ToDecimal(myds.Tables[0].Rows[0]["fxscore"].ToString()), 2);
            }
            return score;
        }

        /// <summary>
        /// 根据条件得到组织监测的分值
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal ZzcpScore(string year, string userId)
        {
            decimal score = 0;
            string sqlWhere = "OrganEvalu_EvaluYear=" + year + " and OrganEvalu_EvaluUserID='" + userId + "' and OrganEvalu_InfoState='审核通过'";
            string strSql = "select sum(fxscore) as fxscore from View_DynMon_OrganEvalu where " + sqlWhere;
            DataSet myds = Query(strSql);
            if (myds.Tables[0].Rows.Count != 0 && myds.Tables[0].Rows[0]["fxscore"] != null && myds.Tables[0].Rows[0]["fxscore"].ToString() != "")
            {
                score = Math.Round(Convert.ToDecimal(myds.Tables[0].Rows[0]["fxscore"].ToString()), 2);
            }
            return score;
        }

        /// <summary>
        /// 格式化时间，把年月日等汉子进行替换
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public string FormatTime(string time)
        {
            time = time.Replace("年", "-");
            time = time.Replace("月", "-");
            time = time.Replace("日", "");
            return time;
        }

        /// <summary>
        /// 根据条件得到民主测评的分值
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal MzcpScore(string sTime, string eTime, string userId)
        {
            DateTime time = Convert.ToDateTime(eTime);
            decimal score = 0;
            string sqlWhere = "substring(convert(nvarchar(50),Democracy_EvaluYear,20),0,5)='" + time.Year + "' and Democracy_EvaluDate between '" + FormatTime(sTime) + "' and '" + FormatTime(eTime) + "' and Democracy_EvaluUserId='" + userId + "' and Democracy_InfoState='提交'";
            string strSql = "select sum(score) as score from View_DynMon_Democracy where " + sqlWhere;
            DataSet myds = Query(strSql);
            if (myds.Tables[0].Rows.Count != 0 && myds.Tables[0].Rows[0]["score"] != null && myds.Tables[0].Rows[0]["score"].ToString() != "")
            {
                score = Math.Round(Convert.ToDecimal(myds.Tables[0].Rows[0]["score"].ToString()), 2);
            }
            return score;
        }

        /// <summary>
        /// 根据条件得到民主测评的分值
        /// </summary>
        /// <param name="year">年份，如2012</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal MzcpScore(string year, string userId)
        {
            decimal score = 0;
            string sqlWhere = "substring(convert(nvarchar(50),Democracy_EvaluYear,20),0,5)='" + year + "' and Democracy_EvaluUserId='" + userId + "' and Democracy_InfoState='提交'";
            string strSql = "select sum(score) as score from View_DynMon_Democracy where " + sqlWhere;
            DataSet myds = Query(strSql);
            if (myds.Tables[0].Rows.Count != 0 && myds.Tables[0].Rows[0]["score"] != null && myds.Tables[0].Rows[0]["score"].ToString() != "")
            {
                score = Math.Round(Convert.ToDecimal(myds.Tables[0].Rows[0]["score"].ToString()), 2);
            }
            return score;
        }

        /// <summary>
        /// 根据条件得到网络评廉测评的分值
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal WlplScore(string sTime, string eTime, string userId)
        {
            DateTime time = Convert.ToDateTime(eTime);
            decimal score = 0;
            string sqlWhere = "substring(convert(nvarchar(50),NetworkRatingLim_EvaluYear,20),0,5)='" + time.Year + "' and  NetworkRatingLim_EvaluDate >= '" + (sTime) + "' and   NetworkRatingLim_EvaluDate<='" + (eTime) + "' and NetworkRatingLim_BeEvaluUserId='" + userId + "' and NetworkRatingLim_InfoState='提交'";
            string strSql = "select sum(score) as score from View_DynMon_NetworkRatingLim where " + sqlWhere;
            DataSet myds = Query(strSql);
            if (myds.Tables[0].Rows.Count != 0 && myds.Tables[0].Rows[0]["score"] != null && myds.Tables[0].Rows[0]["score"].ToString() != "")
            {
                score = Math.Round(Convert.ToDecimal(myds.Tables[0].Rows[0]["score"].ToString()), 2);
            }
            return score;
        }

        /// <summary>
        /// 根据条件得到网络评廉测评的分值
        /// </summary>
        /// <param name="year">年，如2012</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal WlplScore(string year, string userId)
        {
            decimal score = 0;
            string sqlWhere = "substring(convert(nvarchar(50),NetworkRatingLim_EvaluYear,20),0,5)='" + year + "' and NetworkRatingLim_BeEvaluUserId='" + userId + "' and NetworkRatingLim_InfoState='提交'";
            string strSql = "select sum(score) as score from View_DynMon_NetworkRatingLim where " + sqlWhere;
            DataSet myds = Query(strSql);
            if (myds.Tables[0].Rows.Count != 0 && myds.Tables[0].Rows[0]["score"] != null && myds.Tables[0].Rows[0]["score"].ToString() != "")
            {
                score = Math.Round(Convert.ToDecimal(myds.Tables[0].Rows[0]["score"].ToString()), 2);
            }
            return score;
        }


        /// <summary>
        /// 根据条件得到信访举报的分值
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal XfjbScore(string sTime, string eTime, string userId)
        {
            DateTime time = Convert.ToDateTime(eTime);
            decimal score = 0;
            string sqlWhere = "Report_Year between '" + FormatTime(sTime) + "' and '" + FormatTime(eTime) + "' and Report_BeReportUserId='" + userId + "' and  Report_AuditOpinionName='属实'  and  Report_InfoState='已审核'";
            string strSql = "select sum(fxscore) as fxscore from View_DynMon_Report where " + sqlWhere;
            DataSet myds = Query(strSql);
            if (myds.Tables[0].Rows.Count != 0 && myds.Tables[0].Rows[0]["fxscore"] != null && myds.Tables[0].Rows[0]["fxscore"].ToString() != "")
            {
                score = Math.Round(Convert.ToDecimal(myds.Tables[0].Rows[0]["fxscore"].ToString()), 2);
            }
            return score;
        }

        /// <summary>
        /// 根据条件得到信访举报的分值
        /// </summary>
        /// <param name="year">年，如2012</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal XfjbScore(string year, string userId)
        {
            decimal score = 0;
            string sqlWhere = "substring(convert(nvarchar(50),Report_Year,20),0,5)=" + year + " and Report_BeReportUserId='" + userId + "' and  Report_AuditOpinionName='属实'  and  Report_InfoState='已审核'";
            string strSql = "select sum(fxscore) as fxscore from View_DynMon_Report where " + sqlWhere;
            DataSet myds = Query(strSql);
            if (myds.Tables[0].Rows.Count != 0 && myds.Tables[0].Rows[0]["fxscore"] != null && myds.Tables[0].Rows[0]["fxscore"].ToString() != "")
            {
                score = Math.Round(Convert.ToDecimal(myds.Tables[0].Rows[0]["fxscore"].ToString()), 2);
            }
            return score;
        }


        /// <summary>
        /// 根据条件得到网格化监督的分值
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal WghScore(string sTime, string eTime, string userId)
        {
            Common_BLL bllComm = new Common_BLL();
            decimal score = 0;
            DateTime dtStime = Convert.ToDateTime(sTime);
            //string st = eTime.Substring(0, 4) + "-12-31 59:59:59.000";
            DateTime dtEtime = DateTime.Now;
            if (eTime.Contains("-12-31"))
            {
                dtEtime = Convert.ToDateTime(eTime);
                ParameterModel param = new ParameterModel();
                param.ParamMode = ParamEnumMode.InMode;
                param.ParamName = "UserId";
                param.ParamType = DbType.String;
                param.ParamValue = userId;

                ParameterModel paramStart = new ParameterModel();
                paramStart.ParamMode = ParamEnumMode.InMode;
                paramStart.ParamName = "StartTime";
                paramStart.ParamType = DbType.DateTime;
                paramStart.ParamValue = Convert.ToDateTime(sTime);

                ParameterModel paramEnd = new ParameterModel();
                paramEnd.ParamMode = ParamEnumMode.InMode;
                paramEnd.ParamName = "EndTime";
                paramEnd.ParamType = DbType.DateTime;
                paramEnd.ParamValue = Convert.ToDateTime(dtEtime);


                ParameterModel param1 = new ParameterModel();
                param1.ParamMode = ParamEnumMode.OutMode;
                param1.ParamName = "SelfMonitor";
                param1.ParamType = DbType.Double;


                ParameterModel param2 = new ParameterModel();
                param2.ParamMode = ParamEnumMode.OutMode;
                param2.ParamName = "OrganEvalu";
                param2.ParamType = DbType.Double;


                ParameterModel param3 = new ParameterModel();
                param3.ParamMode = ParamEnumMode.OutMode;
                param3.ParamName = "Democracy";
                param3.ParamType = DbType.Double;

                ParameterModel param4 = new ParameterModel();
                param4.ParamMode = ParamEnumMode.OutMode;
                param4.ParamName = "NetworkRatingLim";
                param4.ParamType = DbType.Double;

                ParameterModel param5 = new ParameterModel();
                param5.ParamMode = ParamEnumMode.OutMode;
                param5.ParamName = "Report";
                param5.ParamType = DbType.Double;

                ParameterModel param6 = new ParameterModel();
                param6.ParamMode = ParamEnumMode.OutMode;
                param6.ParamName = "Mesher";
                param6.ParamType = DbType.Double;

                ParameterModel param7 = new ParameterModel();
                param7.ParamMode = ParamEnumMode.OutMode;
                param7.ParamName = "Score";
                param7.ParamType = DbType.Double;

                StoredProcModel models = new StoredProcModel();
                models.ProcName = "P_GetScoreByYearAndUserId";
                models.ParameterList = new List<ParameterModel>();
                models.ParameterList.Add(param);
                models.ParameterList.Add(paramStart);
                models.ParameterList.Add(paramEnd);
                models.ParameterList.Add(param1);
                models.ParameterList.Add(param2);
                models.ParameterList.Add(param3);
                models.ParameterList.Add(param4);
                models.ParameterList.Add(param5);
                models.ParameterList.Add(param6);
                models.ParameterList.Add(param7);

                List<ParameterModel> ParameterList = new List<ParameterModel>();

                bllComm.ExecStoredProcedures(models, out ParameterList);

                score = Convert.ToDecimal(ParameterList[5].ParamResults);
            }




            return score;
        }

        /// <summary>
        /// 根据条件得到网格化监督的分值
        /// </summary>
        /// <param name="year">年，如2012</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public decimal WghScore(string year, string userId)
        {
            decimal score = 0;
            string sTime = year + "-01-01";
            string eTime = year + "-12-31";
            DateTime dtStime = Convert.ToDateTime(sTime);
            DateTime dtEtime = Convert.ToDateTime(eTime);

            Common_BLL bllComm = new Common_BLL();

            ParameterModel param = new ParameterModel();
            param.ParamMode = ParamEnumMode.InMode;
            param.ParamName = "UserId";
            param.ParamType = DbType.String;
            param.ParamValue = userId;

            ParameterModel paramStart = new ParameterModel();
            paramStart.ParamMode = ParamEnumMode.InMode;
            paramStart.ParamName = "StartTime";
            paramStart.ParamType = DbType.DateTime;
            paramStart.ParamValue = Convert.ToDateTime(sTime);

            ParameterModel paramEnd = new ParameterModel();
            paramEnd.ParamMode = ParamEnumMode.InMode;
            paramEnd.ParamName = "EndTime";
            paramEnd.ParamType = DbType.DateTime;
            paramEnd.ParamValue = Convert.ToDateTime(eTime);


            ParameterModel param1 = new ParameterModel();
            param1.ParamMode = ParamEnumMode.OutMode;
            param1.ParamName = "SelfMonitor";
            param1.ParamType = DbType.Double;


            ParameterModel param2 = new ParameterModel();
            param2.ParamMode = ParamEnumMode.OutMode;
            param2.ParamName = "OrganEvalu";
            param2.ParamType = DbType.Double;


            ParameterModel param3 = new ParameterModel();
            param3.ParamMode = ParamEnumMode.OutMode;
            param3.ParamName = "Democracy";
            param3.ParamType = DbType.Double;

            ParameterModel param4 = new ParameterModel();
            param4.ParamMode = ParamEnumMode.OutMode;
            param4.ParamName = "NetworkRatingLim";
            param4.ParamType = DbType.Double;

            ParameterModel param5 = new ParameterModel();
            param5.ParamMode = ParamEnumMode.OutMode;
            param5.ParamName = "Report";
            param5.ParamType = DbType.Double;

            ParameterModel param6 = new ParameterModel();
            param6.ParamMode = ParamEnumMode.OutMode;
            param6.ParamName = "Mesher";
            param6.ParamType = DbType.Double;

            ParameterModel param7 = new ParameterModel();
            param7.ParamMode = ParamEnumMode.OutMode;
            param7.ParamName = "Score";
            param7.ParamType = DbType.Double;

            StoredProcModel models = new StoredProcModel();
            models.ProcName = "P_GetScoreByYearAndUserId";
            models.ParameterList = new List<ParameterModel>();
            models.ParameterList.Add(param);
            models.ParameterList.Add(paramStart);
            models.ParameterList.Add(paramEnd);
            models.ParameterList.Add(param1);
            models.ParameterList.Add(param2);
            models.ParameterList.Add(param3);
            models.ParameterList.Add(param4);
            models.ParameterList.Add(param5);
            models.ParameterList.Add(param6);
            models.ParameterList.Add(param7);

            List<ParameterModel> ParameterList = new List<ParameterModel>();

            bllComm.ExecStoredProcedures(models, out ParameterList);

            score = Convert.ToDecimal(ParameterList[5].ParamResults);


            return score;
        }

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
        #endregion

        #region 自定义通用方法
        /// <summary>
        /// 根据数据字典设置下拉框数据源
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ParentCode"></param>
        public static void SetDDLByDataDictCode(DropDownList ddl, string ParentCode)
        {
            //ddl.DataSource = new Sys_DataDictBLL().FindWhere(" DataDict_ParentCode='" + ParentCode + "' and DataDict_IsDel !='1'").OrderBy(p => p.DataDict_Sequence);
            ddl.DataSource = new Sys_DataDictBLL().GetList(p => p.DataDict_ParentCode == ParentCode && p.DataDict_IsDel == false).OrderBy(p => p.DataDict_Sequence).ToList();

            ddl.DataTextField = "DataDict_Name";
            ddl.DataValueField = "DataDict_Code";
            ddl.DataBind();
        }

        /// <summary>
        /// 根据数据字典设置下拉框数据源(包含请选择)
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ParentCode"></param>
        public static void SetDDLByDataDictCodeNULL(DropDownList ddl, string ParentCode, DropDownList ddlTwo = null, DropDownList ddlThree = null, DropDownList ddlFour = null, DropDownList ddlFive = null, DropDownList ddlSix = null)
        {
            //ddl.DataSource = new Sys_DataDictBLL().FindWhere(" DataDict_ParentCode='" + ParentCode + "' and DataDict_IsDel !='1'").OrderBy(p => p.DataDict_Sequence);
            List<Sys_DataDict> DataDictList = new List<Sys_DataDict>();
            DataDictList = new Sys_DataDictBLL().GetList(p => p.DataDict_ParentCode == ParentCode && p.DataDict_IsDel == false).OrderBy(p => p.DataDict_Sequence).ToList();
            DataDictList.Insert(0, new Sys_DataDict { DataDict_Name = Constant.DrpChoiceName, DataDict_Code = "" });

            ddl.DataSource = DataDictList;
            ddl.DataTextField = "DataDict_Name";
            ddl.DataValueField = "DataDict_Code";
            ddl.DataBind();

            if (ddlTwo != null)
            {
                ddlTwo.DataSource = DataDictList;
                ddlTwo.DataTextField = "DataDict_Name";
                ddlTwo.DataValueField = "DataDict_Code";
                ddlTwo.DataBind();
            }

            if (ddlThree != null)
            {
                ddlThree.DataSource = DataDictList;
                ddlThree.DataTextField = "DataDict_Name";
                ddlThree.DataValueField = "DataDict_Code";
                ddlThree.DataBind();
            }

            if (ddlFour != null)
            {
                ddlFour.DataSource = DataDictList;
                ddlFour.DataTextField = "DataDict_Name";
                ddlFour.DataValueField = "DataDict_Code";
                ddlFour.DataBind();
            }

            if (ddlFive != null)
            {
                ddlFive.DataSource = DataDictList;
                ddlFive.DataTextField = "DataDict_Name";
                ddlFive.DataValueField = "DataDict_Code";
                ddlFive.DataBind();
            }

            if (ddlSix != null)
            {
                ddlSix.DataSource = DataDictList;
                ddlSix.DataTextField = "DataDict_Name";
                ddlSix.DataValueField = "DataDict_Code";
                ddlSix.DataBind();
            }

        }

        /// <summary>
        /// 根据数据字典设置下拉框数据源(包含请选择)
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ParentCode"></param>
        public static void SetDDLByDataDictCodeAllNULL(DropDownList ddl, string ParentCode)
        {
            //ddl.DataSource = new Sys_DataDictBLL().FindWhere(" DataDict_ParentCode='" + ParentCode + "' and DataDict_IsDel !='1'").OrderBy(p => p.DataDict_Sequence);
            List<Sys_DataDict> DataDictList = new List<Sys_DataDict>();
            DataDictList = new Sys_DataDictBLL().GetList(p => p.DataDict_ParentCode == ParentCode && p.DataDict_IsDel == false).OrderBy(p => p.DataDict_Sequence).ToList();
            DataDictList.Insert(0, new Sys_DataDict { DataDict_Name = Constant.DrpAllName, DataDict_Code = "" });

            ddl.DataSource = DataDictList;
            ddl.DataTextField = "DataDict_Name";
            ddl.DataValueField = "DataDict_Code";
            ddl.DataBind();
        }


        /// <summary>
        /// 根据数据字典代码获取字典名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetDataDictNameByCode(string code)
        {
            string sRtn = string.Empty;
            Sys_DataDict dict = new Sys_DataDictBLL().Get(p => p.DataDict_Code == code);
            if (dict != null)
            {
                sRtn = dict.DataDict_Name;
            }
            return sRtn;
        }
        #endregion

        /// <summary>
        /// 写入系统消息
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="receiveUserID">接收对象</param>
        /// <example>消息内容的格式为：业务申请单号+空格+客户姓名+空格+状态+空格</example>
        /// <returns></returns>
        public bool AddSystemMessage(string content, string receiveUserID)
        {
            bool bRtn = false;
            try
            {
                Sys_Message Message = new Sys_Message();
                Sys_MessageBLL MessageBLL = new Sys_MessageBLL();
                Message.MessageID = Guid.NewGuid().ToString();
                Message.Message_Content = content;
                Message.Message_ReceiveUserID = receiveUserID;
                Message.Message_SendTime = DateTime.Now;
                Message.Message_IsShow = true;
                MessageBLL.Add(Message);
                bRtn = true;
                /// TODO 在此处增加插入Redis
            }
            catch { }
            return bRtn;
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
            return dal.F_GetWorkflowState(instanceID, modelID, iSetp, InstanceState);
        }


        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="modelID">业务ID</param>
        /// <returns></returns>
        public string F_GetWorkflowStateEx(string modelID)
        {
            return dal.F_GetWorkflowStateEx(modelID);
        }
    }
}
