using Shu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Shu.Utility.Extensions;

namespace Shu.BLL
{
    public partial class Sys_ModuleBLL : BaseBLL<Sys_Module>
    {
        /// <summary>
        /// 验证 AppKey
        /// </summary>
        /// <param name="appKey">AppKey</param>
        /// <param name="actionContext">ActionContext</param>
        /// <returns>成功返回 true, 否则返回 false。</returns>
        public virtual bool Check(string appKey, HttpActionContext actionContext)
        {
            var appKeyEntity = Get(p => p.Id == appKey);
            if (appKeyEntity != null
                && appKeyEntity.IsDelete == false
                && appKeyEntity.IsAccredit== true)
            {
                var context = actionContext.Request.GetHttpContext();
                var domain = context.Request.UrlReferrer == null ? null : context.Request.UrlReferrer.Authority;
                var ipAddress = actionContext.Request.GetClientIpAddress();
                if (!string.IsNullOrWhiteSpace(appKeyEntity.DomainUrl)
                    && string.Compare(domain, appKeyEntity.DomainUrl, true) != 0)
                {
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(appKeyEntity.IPAddress)
                    && string.Compare(ipAddress, appKeyEntity.IPAddress, true) != 0)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
