using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using ProjektProgBD.Views;

namespace ProjektProgBD
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);



            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}