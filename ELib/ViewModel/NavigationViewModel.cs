using ELib.View;
using ELib.View.Pages;
using Ninject;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using BookReader.ViewModel;

namespace ELib.ViewModel
{
    public class NavigationViewModel
    {
        private readonly IKernel kernel;
        public ICommand OpenLoginWindowCommand { get; }
        public ICommand OpenRegisterWindowCommand { get; }
        public ICommand OpenMainWindowCommand { get; }
        public ICommand OpenMainPageCommand { get; }
        public ICommand OpenBookPageCommand { get; }
        public ICommand OpenReadPageCommand { get; }
        public ICommand OpenAccountPageCommand { get; }
        public ICommand GoBackCommand { get; }
        private Frame NavFrame;
        public NavigationViewModel(IKernel kernel)
        {
            OpenLoginWindowCommand = new RelayCommand(OpenLoginWindow);
            OpenRegisterWindowCommand = new RelayCommand(OpenRegisterWindow);
            OpenMainWindowCommand = new RelayCommand(OpenMainWindow);
            OpenMainPageCommand = new RelayCommand(OpenMainPage);
            OpenBookPageCommand = new RelayCommand(OpenBookPage);
            OpenReadPageCommand = new RelayCommand(OpenReadPage);
            OpenAccountPageCommand = new RelayCommand(OpenAccountPage);
            GoBackCommand = new RelayCommand(GoBack);
            this.kernel = kernel;
        }

        private void OpenLoginWindow(object parameter)
        {
            (parameter as Window)?.Close();

            var loginWindow = new LoginWindow();
            loginWindow.DataContext = new LoginViewModel(this, kernel.Get<IUserService>(), kernel.Get<IUserSession>());
            loginWindow.Show();
        }

        private void OpenRegisterWindow(object parameter)
        {
            (parameter as Window)?.Close();

            var registerWindow = new RegisterWindow();
            registerWindow.DataContext = new RegisterViewModel(this, kernel.Get<IUserService>(), kernel.Get<IUserSession>());
            registerWindow.Show();
        }
        private void OpenMainWindow(object parameter)
        {
            var window = parameter as Window;
            window?.Close();
            var mainWindow = new MainWindow();
            var mainModel = new MainViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>());
            NavFrame = mainWindow.MainFrame;
            mainWindow.DataContext = mainModel;
            var mainPage = new MainPage();
            mainPage.DataContext = new MainPageViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>());
            NavFrame.Navigate(mainPage);
            mainWindow.Show();
        }
        private void OpenMainPage(object parameter)
        {
            var mainPage = new MainPage();
            mainPage.DataContext = new MainPageViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>());
            NavFrame.Navigate(mainPage);
        }
        private void OpenBookPage(object parameter)
        {
            var bookPage = new BookPage();
            bookPage.DataContext = new BookPageViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>(), kernel.Get<ITariffService>());
            NavFrame.Navigate(bookPage);
        }
        private void OpenReadPage(object parameter)
        {
            var readPage = new ReadPage();
            readPage.DataContext = new ReadViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>(), kernel.Get<IReadingService>());
            NavFrame.Navigate(readPage);
        }
        private void OpenAccountPage(object parameter)
        {
            var accountPage = new AccountPage();
            accountPage.DataContext = new AccountPageViewModel(this, kernel.Get<IUserSession>(), kernel.Get<ITariffService>());
            NavFrame.Navigate(accountPage);
        }
        private void GoBack(object parameter)
        {
            if (NavFrame.CanGoBack)
            {
                NavFrame.GoBack();
            }
        }
    }
}
