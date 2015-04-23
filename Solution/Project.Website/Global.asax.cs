using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Project.Website.Controllers;

namespace Project.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            Server.ClearError();
            int statusCode = 0;
            if (lastError == null) return;
            statusCode = lastError.GetType() == typeof(HttpException) ? ((HttpException)lastError).GetHttpCode() : 500;
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("message", lastError.Message);
            routeData.Values.Add("statusCode", statusCode);
            routeData.Values.Add("exception", lastError);
            IController controller = new ErrorController();

            RequestContext requestContext = new RequestContext(new HttpContextWrapper(Context), routeData);

            controller.Execute(requestContext);
            Response.End();
        }
    }
}
