using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Comm
{
    /// <summary>
    /// Json 还款计划
    /// </summary>
    public partial class Repayment
    {
        /// <summary>
        /// 偿还利息
        /// </summary>
        public string ReplyInterest { get; set; }

        /// <summary>
        /// 偿还本金
        /// </summary>
        public string ReplyPrincipal { get; set; }

        /// <summary>
        /// 偿还本息
        /// </summary>
        public string ReplyPrincipalIntreest { get; set; }

        /// <summary>
        /// 剩余本金
        /// </summary>
        public string SurplusPrincipal { get; set; }
    }
}
