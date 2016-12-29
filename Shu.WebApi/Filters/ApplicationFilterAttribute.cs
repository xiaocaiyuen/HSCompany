using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;
using Shu.Utility.Extensions;
using Shu.BLL;

namespace Shu.WebApi.Filters
{
    public class ApplicationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var appKeyString = actionContext.Request.GetQueryString("appKey");
            //Guid appKey;
            //Guid.TryParse(appKeyString, out appKey);
            if (string.IsNullOrWhiteSpace(appKeyString)
                || !new Sys_ModuleBLL().Check(appKeyString, actionContext))
            {
                throw new UnauthorizedAccessException(
                    string.Format("参数 appKey '{0}' 无效.", appKeyString));
            }
            else
            {
                base.OnActionExecuting(actionContext);
            }
        }

        public override System.Threading.Tasks.Task OnActionExecutingAsync(System.Web.Http.Controllers.HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}