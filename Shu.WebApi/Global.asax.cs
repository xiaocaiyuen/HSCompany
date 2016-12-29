using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Shu.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //禁用X-AspNetMvc-Version头
            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var lastError = Server.GetLastError().GetBaseException();
            {
                var log = LogManager.GetLogger(typeof(WebApiApplication));
                log.Error(lastError);
            }
        }
    }
}
