using SmartStore.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestPlugin.Filters
{
    public class AttributeProcessFilter : IActionFilter
    {
        private readonly Lazy<IMenuService> _menuService;
        public AttributeProcessFilter(Lazy<IMenuService> menuService, Lazy<IWidgetProvider> widgetProvider = null)
        {
            _menuService = menuService;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //if (filterContext.IsChildAction)
            //    return;
            var controller = filterContext.RouteData.Values["controller"].Equals("Common");
            var menu = filterContext.RouteData.Values["action"].Equals("Navbar");
            if (controller && menu)
            {
                var rootNode = _menuService.Value.GetRootNode("admin");
                var cmsMenuItem = rootNode.Children.Where(n => n.Value.Id == "configuration").SingleOrDefault();

                if (cmsMenuItem != null)
                {
                    var menueditorNode = cmsMenuItem.SelectNode(n => n.Value.Id == "TestPlugin");
                    //var menuSearchNode = cmsMenuItem.SelectNode(n => n.Value.Id == "SearchAuditProcessor");

                    if (menueditorNode == null)
                    {
                        menueditorNode = new SmartStore.Collections.TreeNode<MenuItem>(new MenuItem()
                        {
                            Id = "AttributeProcessFilter",
                            Text = "Attribute ProcessFilter",
                            Icon = "fa fa-file",
                            Url = "/Plugins/TestPlugin/TestPlugin/GetAttributesList"
                        });

                        cmsMenuItem.Append(menueditorNode);
                    }

                    var partialResult = new PartialViewResult();
                    partialResult.ViewData.Model = rootNode;
                    filterContext.Result = partialResult;
                }
            }
        }
    }
}