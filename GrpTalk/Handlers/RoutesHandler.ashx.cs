using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using GrpTalk.CommonClasses;
using GT.Utilities;
using Newtonsoft.Json.Linq;


namespace GrpTalk.Handlers
{
    /// <summary>
    /// Summary description for RoutesHandler
    /// </summary>
    public class RoutesHandler: IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            IHttpHandler handler = null;
            RouteData routeData = requestContext.RouteData;
            ApiHelper helper = new ApiHelper();
            string version = routeData.Values["Version"].ToString();
            JObject responseObj = new JObject();
            switch (version)
            {
                case "v2":
                    responseObj = new JObject(new JProperty("Success", false),
                           new JProperty("Message", "Please wait App is Under Maintenance"));
                    HttpContext.Current.Response.Write(responseObj);
                    HttpContext.Current.Response.End();
                    break;
                //case "v2.1":
                //    responseObj = new JObject(new JProperty("Success", false),
                //           new JProperty("Message", "Please wait App is Under Maintenance"));
                //    HttpContext.Current.Response.Write(responseObj);
                //    HttpContext.Current.Response.End();
                //    break;
                case "v1.1":
                    responseObj = new JObject(new JProperty("Success", false),
                           new JProperty("Message", "Please wait App is Under Maintenance"));
                    HttpContext.Current.Response.Write(responseObj);
                    HttpContext.Current.Response.End();
                    break;
                case "v1.2":
                    handler = new Handlers.V_1_2.AppService_V1__2__0();
                    break;
                case "v1.3":
                    handler = new Handlers.V_1_3.AppService_V1__3__0();
                    break;
                case "v2.0":
                    handler = new Handlers.V_1_4.AppService_V1__4__0();
                    break;
                case "v2.1":
                    handler = new Handlers.V_1_5.AppService();
                    break;
            }
            return handler;
        }

    }
}