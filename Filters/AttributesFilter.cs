using SmartStore.Core.Domain.DataExchange;
using SmartStore.Core.Events;
using SmartStore.Core.Plugins;
using SmartStore.Core.Security;
using SmartStore.Services.Customers;
using SmartStore.Services.DataExchange;
using SmartStore.Web.Framework.Events;
using SmartStore.Web.Framework.Modelling;
using SmartStore.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TestPlugin.Filters
{

    public class AttributesFilter : IActionFilter
    {
        private readonly Lazy<IMenuService> _menuService;
        private readonly Lazy<IWidgetProvider> _widgetProvider;
        private readonly IPluginFinder _pluginFinder;

        public AttributesFilter(Lazy<IMenuService> menuService, Lazy<IWidgetProvider> widgetProvider = null, IPluginFinder pluginFinder = null)
        {
            _menuService = menuService;
            _widgetProvider = widgetProvider;
            _pluginFinder = pluginFinder;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var pluginInstalled = _pluginFinder.GetPluginDescriptorBySystemName("TestPlugin");
            if (pluginInstalled != null)
            {
                if (filterContext.Controller.GetType().Equals(typeof(SmartStore.Web.Controllers.CommonController)) &&
                filterContext.ActionDescriptor.ActionName.Equals("AccountDropdown") && filterContext.IsChildAction)
                {
                    _widgetProvider.Value.RegisterAction("account_dropdown_after", "GenericAttributesMenuItem", "TestPlugin", new { area = Plugin.SystemName });
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}