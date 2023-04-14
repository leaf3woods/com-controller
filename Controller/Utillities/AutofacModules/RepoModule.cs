using Autofac;
using AutoMapper;
using Domain.Repos;
using System.Linq;
using System.Reflection;

namespace Controller.Utillities.AutofacModules
{
    public class RepoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(Domain)))
                .Where(a => a.Name.Contains("Repo"))
                .AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterType<CtlDbContext>().AsSelf();
            builder.RegisterType<Mapper>().AsSelf();
        }
    }
}