﻿
namespace SimpleOwinAspNetHost
{
    using SimpleOwinAspNetHost.Samples;
    using SimpleOwinAspNetHost.Samples.WebSockets.Helloworld;
    using SimpleOwinAspNetHost.Samples.WebSockets.HelloworldAutodetect;
    using System;
    using System.Web.Routing;

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var startupProperties = SimpleOwinAspNetHandler.GetStartupProperties();
            RouteTable.Routes.Add(new Route("helloworld", new SimpleOwinAspNetRouteHandler(Helloworld.OwinApp())));

            //RouteTable.Routes.Add(new Route("middlewareapps", new SimpleOwinAspNetRouteHandler(MiddlewareApps.OwinApp())));
            // SimpleOwinAspNetRouteHandler is capable of auto handling IEnumerable<Func<AppFunc,AppFunc>>
            RouteTable.Routes.Add(new Route("middlewareapps", new SimpleOwinAspNetRouteHandler(MiddlewareApps.OwinApps())));

            RouteTable.Routes.Add(new Route("SimpleOwinApp/{*pathInfo}", new SimpleOwinAspNetRouteHandler(SimpleOwinApp.OwinApp(), "SimpleOwinApp")));

            RouteTable.Routes.Add(new Route("websocket/helloworld", new SimpleOwinAspNetRouteHandler(HelloWorldWebSocket.OwinApp())));
            RouteTable.Routes.Add(new Route("websocket/helloworld/autodetect", new SimpleOwinAspNetRouteHandler(HelloWorldWebSocketAutodetect.OwinApp())));
        }
    }
}