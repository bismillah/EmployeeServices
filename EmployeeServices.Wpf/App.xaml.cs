using EmployeeServices.Api.Interfaces;
using EmployeeServices.Api.Providers;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EmployeeServices.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<MainWindow>();
        }       

    }
}
