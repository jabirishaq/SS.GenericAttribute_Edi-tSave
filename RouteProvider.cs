using SmartStore.Web.Framework.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestPlugin
{
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("SmartStore.TestPlugin",
                 "Plugins/TestPlugin/{controller}/{action}",
                 new { controller = "TestPlugin", action = "Configure" },
                 new[] { "Plugins.TestPlugin" }
            )
            .DataTokens["area"] = "TestPlugin";

            routes.MapRoute("SmartStore.TestPlugin.AttributesList",
                "Plugins/TestPlugin/{controller}/{action}",
                new { controller = "TestPlugin", action = "GetAttributesList" },
                new[] { "Plugins.TestPlugin" }
           )
           .DataTokens["area"] = "TestPlugin";

            routes.MapRoute("SmartStore.TestPlugin.EditAttributeList",
               "Plugins/TestPlugin/{controller}/{action}",
               new { controller = "TestPlugin", action = "EditAttribute" },
               new[] { "Plugins.TestPlugin" }
          )
          .DataTokens["area"] = "TestPlugin";
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
