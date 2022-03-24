using EasySave.MVVM.View;
using EasySave.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EasySave
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static Mutex _mutex = null;
        App()
        {
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            const string appName = "EasySave 3.0";
            bool createdNew;

            _mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Already an instance is running...");
                App.Current.Shutdown();
            }
        }

    }
}
