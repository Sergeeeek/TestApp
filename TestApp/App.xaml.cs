using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestApp.Service;
using TestApp.ViewModel;

namespace TestApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();

            var mainWindow = new MainWindow();

            var timeService = new TimeCalculationService();

            var calcVM = new CalculationViewModel(timeService);
            var settingsVM = new SettingsViewModel(timeService);

            var mainVM = new MainViewModel(calcVM, settingsVM);
            mainWindow.DataContext = mainVM;
            mainWindow.Show();
            app.InitializeComponent();
            app.Run();
        }
    }
}
