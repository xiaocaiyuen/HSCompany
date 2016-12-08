using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Shu.Comm
{
    public class Constant
    {
        #region 系统设置
        //超级管理员
        public static string SysAdminRoleName = "系统管理员";
        public static string SuperAdminRoleName = "超级管理员";
        //最大时间
        public static string Sys_MaxDateTime = "2099-12-31 00:00:00";
        //最小时间
        public static string Sys_MinDateTime = "1900-01-01 00:00:00";


        //发送短信相关设置
        public static string AutoSendMsg = "AutoSendMsg";
        public static string SendMsgFromHour = "SendMsgFromHour";
        public static string SendMsgToHour = "SendMsgToHour";
        public static string SMSUSERID = "SMSUSERID";
        public static string SMSUSERPWD = "SMSUSERPWD";
        public static string SendStatus0 = "未发送";
        public static string SendStatus1 = "已发送";

        // LOG
        public static readonly string DS_PROFILE_LOG = "YDTDALLog";

        //CMMException
        public static string MSG_CMMEXCEPTION_TOSTRING = "错误消息:{0} (编号:{1})";
        public static string MSG_CMMEXCEPTION_TOFULLSTRING = "错误编号: {0}\n错误信息: {1}\n错误StackTrace:{2}";
        #endregion

        #region 流程编号
        public const string WF001 = "0b4d857c-daa1-402b-911e-293e0dfca4b7";
        #endregion

        #region 系统配置
        public enum SettingKey
        {
            /// <summary>
            /// 授权区域
            /// </summary>
            AuthorizedArea
        }
        #endregion

        #region 登记
        /// <summary>
        /// 国有建设用地
        /// </summary>
        public enum GYJZYD
        {
            /// <summary>
            /// 使用权
            /// </summary>
            SYQ = 150701,
            /// <summary>
            /// 房屋所有权
            /// </summary>
            FDCQ = 150702,
            /// <summary>
            /// 地役权
            /// </summary>
            DYIQ = 150703,
            /// <summary>
            /// 抵押权
            /// </summary>
            DYAQ = 150704,
            /// <summary>
            /// 预告
            /// </summary>
            YG = 150705,
            /// <summary>
            /// 更正
            /// </summary>
            GZ = 150706,
            /// <summary>
            /// 查封
            /// </summary>
            CF = 150707,
            /// <summary>
            /// 异议
            /// </summary>
            YY = 150708,
            /// <summary>
            /// 其他
            /// </summary>
            QT = 150709,
        }

        /// <summary>
        /// 集体土地
        /// </summary>
        public enum JTTD
        {
            /// <summary>
            /// 所有权
            /// </summary>
            SYQ = 150801,
            /// <summary>
            /// 地役权
            /// </summary>
            DYIQ = 150802,
            /// <summary>
            /// 更正
            /// </summary>
            GZ = 150803,
            /// <summary>
            /// 异议
            /// </summary>
            YY = 150804,
            /// <summary>
            /// 查封
            /// </summary>
            CF = 150805,
            /// <summary>
            /// 其他
            /// </summary>
            QT = 150806,
        }

        /// <summary>
        /// 宅基地
        /// </summary>
        public enum ZJD
        {
            /// <summary>
            /// 使用权
            /// </summary>
            SYQ = 150901,
            /// <summary>
            /// 房屋所有权
            /// </summary>
            FDCQ = 150902,
            /// <summary>
            /// 地役权
            /// </summary>
            DYIQ = 150903,
            /// <summary>
            /// 更正
            /// </summary>
            GZ = 150904,
            /// <summary>
            /// 异议
            /// </summary>
            YY = 150905,
            /// <summary>
            /// 查封
            /// </summary>
            CF = 150906,
            /// <summary>
            /// 其他
            /// </summary>
            QT = 150906,
        }

        /// <summary>
        /// 集体建筑用地
        /// </summary>
        public enum JTJSZD
        {
            /// <summary>
            /// 使用权
            /// </summary>
            SYQ = 151001,
            /// <summary>
            /// 构(建)筑物所有权
            /// </summary>
            JZWSQY = 151002,
            /// <summary>
            /// 地役权
            /// </summary>
            DYIQ = 151003,
            /// <summary>
            /// 抵押权
            /// </summary>
            DYAQ = 151004,
            /// <summary>
            /// 预告
            /// </summary>
            YG = 151005,
            /// <summary>
            /// 更正
            /// </summary>
            GZ = 151006,
            /// <summary>
            /// 异议
            /// </summary>
            YY = 151007,
            /// <summary>
            /// 查封
            /// </summary>
            CF = 151008,
            /// <summary>
            /// 其他
            /// </summary>
            QT = 151009,
        }

        /// <summary>
        /// 国有海域
        /// </summary>
        public enum GYHY
        {
            /// <summary>
            /// 使用权
            /// </summary>
            SYQ = 151101,
            /// <summary>
            /// 构(建)筑物所有权
            /// </summary>
            JZWSQY = 151102,
            /// <summary>
            /// 地役权
            /// </summary>
            DYIQ = 151103,
            /// <summary>
            /// 更正
            /// </summary>
            GZ = 151104,
            /// <summary>
            /// 异议
            /// </summary>
            YY = 151105,
            /// <summary>
            /// 其他
            /// </summary>
            QT = 151106,
        }

        /// <summary>
        /// 国有林地
        /// </summary>
        public enum GYLD
        {
            /// <summary>
            /// 土地承包经营权
            /// </summary>
            TDCBJYQ = 151201,
            /// <summary>
            /// 国有农地使用权
            /// </summary>
            GYNDSYQ = 151202,
            /// <summary>
            /// 地役权登记
            /// </summary>
            DYIQ = 151203,
            /// <summary>
            /// 更正
            /// </summary>
            GZ = 151204,
            /// <summary>
            /// 异议
            /// </summary>
            YY = 151205,
            /// <summary>
            /// 其他
            /// </summary>
            QT = 151206,
            /// <summary>
            /// 林权
            /// </summary>
            LQ = 151207,
        }

        /// <summary>
        /// 国有草地
        /// </summary>
        public enum GYCD
        {
            /// <summary>
            /// 土地承包经营权
            /// </summary>
            TDCBJYQ = 151301,
            /// <summary>
            /// 国有农地使用权
            /// </summary>
            GYNDSYQ = 151302,
            /// <summary>
            /// 地役权登记
            /// </summary>
            DYIQ = 151303,
            /// <summary>
            /// 更正
            /// </summary>
            GZ = 151304,
            /// <summary>
            /// 异议
            /// </summary>
            YY = 151305,
            /// <summary>
            /// 其他
            /// </summary>
            QT = 151306,
        }
        /// <summary>
        /// 国有水域
        /// </summary>
        public enum GYSY
        {
            /// <summary>
            /// 土地承包经营权
            /// </summary>
            TDCBJYQ = 151401,
            /// <summary>
            /// 国有农地使用权
            /// </summary>
            GYNDSYQ = 151402,
            /// <summary>
            /// 地役权登记
            /// </summary>
            DYIQ = 151403,
            /// <summary>
            /// 更正
            /// </summary>
            GZ = 151404,
            /// <summary>
            /// 异议
            /// </summary>
            YY = 151405,
            /// <summary>
            /// 其他
            /// </summary>
            QT = 151406,
        }

        /// <summary>
        /// 国有耕地
        /// </summary>
        public enum GYGD
        {
            /// <summary>
            /// 土地承包经营权
            /// </summary>
            TDCBJYQ = 151701,
            /// <summary>
            /// 国有农地使用权
            /// </summary>
            GYNDSYQ = 151702,
            /// <summary>
            /// 地役权登记
            /// </summary>
            DYIQ = 151703,
            /// <summary>
            /// 更正
            /// </summary>
            GZ = 151704,
            /// <summary>
            /// 异议
            /// </summary>
            YY = 151705,
            /// <summary>
            /// 其他
            /// </summary>
            QT = 151706,
        }

        #endregion

        /// <summary>
        /// 登记类型
        /// </summary>
        public enum HouseType
        {
            [Description("有效")]
            aquamarine,
            [Description("注销")]
            crimson,
            [Description("预告")]
            aqua,
            [Description("异议")]
            indianred,
            [Description("抵押")]
            coral,
            [Description("查封")]
            gold
        }

        #region 不是流程步骤节点编号
        public enum NodeNumber
        {
            /// <summary>  
            /// 申请受理 
            /// </summary>  
            WF001JD01,
            /// <summary>
            /// 收件
            /// </summary>
            WF001JD02,
            /// <summary>
            /// 收费
            /// </summary>
            WF001JD03,
            /// <summary>
            /// 初审
            /// </summary>
            WF001JD04,
            /// <summary>
            /// 复审
            /// </summary>
            WF001JD05,
            /// <summary>
            /// 登记公告
            /// </summary>
            WF001JD06,
            /// <summary>
            /// 登记簿
            /// </summary>
            WF001JD07,
            /// <summary>
            /// 缮证
            /// </summary>
            WF001JD08,
            /// <summary>
            /// 发证
            /// </summary>
            WF001JD09,
            /// <summary>
            /// 结束
            /// </summary>
            WF001JD10,
        }
        #endregion

        #region 业务管理
        //下拉框默认名称
        public static string DrpChoiceName = "--请选择--";
        public static string DrpAllName = "--全部--";

        //车辆评估报告状态
        public static string VehicleAssessInfoStatus0 = "预存";
        public static string VehicleAssessInfoStatus1 = "已提交待审核";
        public static string VehicleAssessInfoStatus2 = "审核通过";
        public static string VehicleAssessInfoStatus3 = "审核退回";

        public static string CustomerSignedDataFileType = "客户签约资料";
        /// <summary>
        /// 预存
        /// </summary>
        public static string InstanceStateFORPreStored = "预存";
        /// <summary>
        /// 暂存
        /// </summary>
        public static string InstanceStateTemporary = "暂存";
        /// <summary>
        /// 拒绝
        /// </summary>
        public static string InstanceStateFORRefuse = "拒绝";
        /// <summary>
        /// 有条件批复
        /// </summary>
        public static string InstanceStateRefuseConditional = "有条件批复";
        ///<summary>
        ///退回
        ///</summary>
        public static string InstanceStateRefuseReturn = "退回";

        /// <summary>
        /// 取消
        /// </summary>
        public static string InstanceCancelRefuseReturn = "取消";

        /// <summary>
        /// 驳回
        /// </summary>
        public static string InstanceRejectRefuseReturn = "驳回";

        /// <summary>
        /// 通过
        /// </summary>
        public static string InstanceThroughRefuseReturn = "通过";

        /// <summary>
        /// 提交
        /// </summary>
        public static string InstanceSubmitRefuseReturn = "提交";
        //半年
        public static string HalfYear_FirstHalfYear = "FirstHalfYear"; //上半年
        public static string HalfYear_SecondHalfYear = "SecondHalfYear"; //下半年
        public static string HalfYear_FirstHalfYearName = "上半年"; //上半年
        public static string HalfYear_SecondHalfYearName = "下半年"; //下半年


        //季度数值
        public static string Quarter_Number1 = "1";
        public static string Quarter_Number2 = "2";
        public static string Quarter_Number3 = "3";
        public static string Quarter_Number4 = "4";

        //季度名称
        public static string QuarterName_One = "第一季度";
        public static string QuarterName_Two = "第二季度";
        public static string QuarterName_Three = "第三季度";
        public static string QuarterName_Four = "第四季度";

        #endregion

        #region 待办事项常量配置
        public static string TesksAuditTitle = "你有{0}数：{1}条";
        public static string TesksNoSibmitTitle = "你有{0}数：{1}条";

        public static string TaskCYGYWSP = "车易购业务审批";
        public static string TaskCYGTQHK = "车易购业务提前还款";
        public static string TaskCLIENTFILE = "客户资料管理";     //ExpressInfoAudit.aspx
        public static string TaskCLIENTFILESHPI = "快递资料审批"; //ExpressInfoEdit.aspx  6个参数
        public static string TaskCYGYWKHXXBG = "车易购业务客户信息变更";  //CustomerInfoApprove.aspx 2个参数
        public static string TaskCYGYWBDXB = "车易购业务保单续保";
        public static string TaskCYGYWBDLP = "车易购业务保单理赔";
        #endregion

        #region 消息配置
        /// <summary>
        /// 消息内容的格式为：业务申请单号+空格+客户姓名+空格+状态+空格
        /// </summary>
        public static string MessageContent = "{0} {1} {2} ";

        /// <summary>
        /// 意见内容的格式为：按钮动作+意见内容
        /// </summary>
        public static string OpinionContent = "【意见】{0}, {1}";
        /// <summary>
        /// 意见内容的格式为：按钮动作+意见内容
        /// </summary>
        public static string OpinionContentNULL = "{0}";
        #endregion 

        #region 附件上传自定义控件参数配置
        public const string fileType1 = "申请类资料";
        public const string fileType2 = "初审上传";
        public const string fileType3 = "信贷审批或者信贷主管";
        public const string fileType4 = "放款类材料（签约）";
        public const string fileType5 = "贷后：变更卡号";
        public const string fileType6 = "贷后：续保";
        public const string fileType7 = "已删除资料";
        public const string fileType8 = "贷前：GPS";
        public const string Classification = "未归类";

        //区分左边树显示
        public const string typecode1 = "2801";  //个人产品
        public const string typecode2 = "2802";  //企业产品

        public const string ManualDataSource = "人工分单";
        public const string SystemDataSource = "系统分单";



        #endregion

        #region 角色参数配置
        /// <summary>
        /// 运营专员角色ID
        /// </summary>
        public static string JRZYRoleId = "adc18a7a-3259-48ae-8cc6-0292f8ced6d3";
        /// <summary>
        /// 销售顾问角色ID
        /// </summary>
        public static string XSGWRoleId = "2d4b62e6-d257-4e82-8638-b9b32d9cd83b";

        /// <summary>
        /// 保理专员角色ID
        /// </summary>
        public static string BLZYRoleId = "34425c62-f71e-4b96-9532-6f4363f263e1";
        /// <summary>
        /// 贷后专员角色ID
        /// </summary>
        public static string DHZYRoleId = "1137b054-c6a6-4d45-841f-557cb3d831a5";
        #endregion
    }
}
