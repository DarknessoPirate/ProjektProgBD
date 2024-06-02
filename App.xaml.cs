using System.Configuration;
using System.Data;
using System.Windows;

namespace ProjektProgBD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceProvider = Tools.ConfigurationSetup.ConfigureServices();

            // Show the main window manually
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }

}
