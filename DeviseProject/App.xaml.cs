using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using Devise.Configuration;
using Devise.Ui.Views;

namespace DeviseProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void MainApp_OnStartup(object sender, StartupEventArgs e)
        {
            var culture = CultureInfo.CreateSpecificCulture(Settings.Default.Language);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            AppDomain.CurrentDomain.SetData("ProgramData", Directory.GetCurrentDirectory());

            (new DeviseMainWindow()).Show();
        }
    }
}
