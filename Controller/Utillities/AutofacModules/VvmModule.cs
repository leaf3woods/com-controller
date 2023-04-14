using Autofac;
using System.Linq;
using System.Reflection;

namespace Controller.Utillities.AutofacModules
{
    public class VvmModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(a => a.Name.EndsWith("VM") || a.Name.EndsWith("Window"))
                .SingleInstance().AsSelf().PropertiesAutowired();
        }
    }
}