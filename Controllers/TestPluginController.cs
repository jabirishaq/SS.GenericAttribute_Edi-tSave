using SmartStore;
using SmartStore.Core.Data;
using SmartStore.Core.Domain.Common;
using SmartStore.Core.Domain.Customers;
using SmartStore.Data;
using SmartStore.Services;
using SmartStore.Services.Common;
using SmartStore.Services.Customers;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Framework.Security;
using SmartStore.Web.Framework.Settings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPlugin.Model;

namespace TestPlugin.Controllers
{
    public class TestPluginController : AdminControllerBase
    {
        private readonly ICommonServices _services;
        private readonly IDbContext _context;
        private readonly IGenericAttributeService _genericAttributeService;

        public TestPluginController(
           ICommonServices services, IDbContext context, IGenericAttributeService genericAttributeService)
        {
            _services = services;
            _context = context;
            _genericAttributeService = genericAttributeService;
        }
        public ActionResult Configure()
        {
            return View();
        }
       
        public ActionResult List()
        {
           
            return View();
        }
        [HttpGet]
        public JsonResult GetAttributesList()
        {

            var gaTable = _context.Set<GenericAttribute>().AsNoTracking(); //Generic Attributes

            var userId = _services.WorkContext.CurrentCustomer.Id; //Customer Id

            var query = (
            from a in gaTable
            where a.KeyGroup == "Customer" && a.EntityId == userId
            select a)
            .OrderBy(x => x.Id); // Query for the attributes 

            var attrList = query.Select(s => new AttributesViewModel
            {
                Id = s.Id,
                Value = s.Value.ToString(),
                Key = s.Key.ToString(),
            }).ToList(); // List building to show in grid

           
            return Json(attrList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditAttribute(List<AttributesViewModel> models)
        {
            try
            {
                var list = models.Select(a => new AttributesViewModel
                {
                    Id = a.Id,
                   Value = a.Value,
                   Key = a.Key
                }).ToList();


                foreach (var item in list)
                {
                    var attribute = _genericAttributeService.GetAttributeById(item.Id);
                    if (attribute != null)
                    {
                        attribute.Id = item.Id;  
                        attribute.Key = item.Key;  
                        attribute.Value = item.Value; 
                    _genericAttributeService.UpdateAttribute(attribute);
                    }
                }

                return Json(models, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GenericAttributesMenuItem()
        {
            return PartialView();
        }

        

    }
}