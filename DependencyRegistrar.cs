using Autofac;
using Autofac.Integration.Mvc;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;
using TestPlugin.Controllers;
using TestPlugin.Filters;

namespace CC.Plugins.ChamberlainAuditTool
{
	public class DependencyRegistrar : IDependencyRegistrar
	{
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            builder.RegisterType<AttributesFilter>().AsActionFilterFor<SmartStore.Web.Controllers.CommonController>().InstancePerRequest();
        }

        public int Order
		{
			get { return 1; }
		}

    }
}