using SmartStore.Core.Plugins;
using SmartStore.Services.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace TestPlugin
{
    public class Plugin : BasePlugin, IConfigurable
    {

        public static string SystemName => "TestPlugin";

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "TestPlugin";
            routeValues = new RouteValueDictionary() { { "area", Plugin.SystemName } };
        }

        public override void Install()
        {
            base.Install();
        }
        public override void Uninstall()
        {
            base.Uninstall();
        }
    }
}