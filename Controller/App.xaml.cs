using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Windows;

namespace Controller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            #region dependencyInjection

            var builder = Host.CreateDefaultBuilder(e.Args).
                UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices(service => service.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.Load(nameof(Domain))));
            var host = builder.ConfigureContainer<ContainerBuilder>(options =>
                options.RegisterAssemblyModules(Assembly.GetExecutingAssembly(), Assembly.Load(nameof(Domain))))
                .Build();
            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            #endregion dependencyInjection
        }
    }
}