using ELib.Util;
using ELib.View;
using ELib.ViewModel;
using Ninject;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Threading;

namespace ELib
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            CultureInfo culture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            base.OnStartup(e);
            var kernel = new StandardKernel(new NinjectRegistrations(), new ReposModule());
            var navigationViewModel = new NavigationViewModel(kernel);
            var LoginViewModel = new LoginViewModel(navigationViewModel, kernel.Get<IUserService>(), kernel.Get<IUserSession>());
            var LoginWindow = new LoginWindow
            {
                DataContext = LoginViewModel
            };
            LoginWindow.Show();
        }
    }
}
