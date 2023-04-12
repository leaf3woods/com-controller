using Autofac;
using Autofac.Extensions.DependencyInjection;
using Controller.ViewModels;
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

            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowVM>();
            var serviceProvider = services.BuildServiceProvider();
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            var builder = Host.CreateDefaultBuilder(e.Args).
                UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.ConfigureContainer<ContainerBuilder>(options =>
                options.RegisterAssemblyModules(Assembly.GetExecutingAssembly()));
            mainWindow.Show();

            #endregion dependencyInjection
        }
    }
}