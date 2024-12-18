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
        public ICommand OpenLoginFromRegisterWindowCommand { get; }
        public ICommand OpenRegisterWindowCommand { get; }
        public ICommand OpenMainWindowCommand { get; }
        public ICommand OpenMainPageCommand { get; }
        public ICommand OpenBookPageCommand { get; }
        public ICommand OpenAuthorPageCommand { get; }
        public ICommand OpenGenrePageCommand { get; }
        public ICommand OpenReadPageCommand { get; }
        public ICommand OpenAccountPageCommand { get; }
        public ICommand OpenChosenPageCommand { get; }
        public ICommand OpenOfflinePageCommand { get; }
        public ICommand OpenReviewWindowCommand { get; }
        public ICommand CloseReviewWindowCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand ExitAppCommand { get; }
        public event Action ReviewWindowClosed;
        public bool IsReviewWindowClosed { get; private set; }
        private Frame NavFrame;
        public NavigationViewModel(IKernel kernel)
        {
            OpenLoginWindowCommand = new RelayCommand(OpenLoginWindow);
            OpenRegisterWindowCommand = new RelayCommand(OpenRegisterWindow);
            OpenMainWindowCommand = new RelayCommand(OpenMainWindow);
            OpenMainPageCommand = new RelayCommand(OpenMainPage);
            OpenBookPageCommand = new RelayCommand(OpenBookPage);
            OpenAuthorPageCommand = new RelayCommand(OpenAuthorPage);
            OpenGenrePageCommand = new RelayCommand(OpenGenrePage);
            OpenReadPageCommand = new RelayCommand(OpenReadPage);
            OpenAccountPageCommand = new RelayCommand(OpenAccountPage);
            OpenChosenPageCommand = new RelayCommand(OpenChosenPage);
            OpenOfflinePageCommand = new RelayCommand(OpenOfflinePage);
            OpenLoginFromRegisterWindowCommand = new RelayCommand(OpenLoginWindowFromRegister);
            ExitAppCommand = new RelayCommand(ExitApp);
            GoBackCommand = new RelayCommand(GoBack);
            this.kernel = kernel;
        }

        private void OpenLoginWindow(object parameter)
        {

            var loginWindow = new LoginWindow();
            loginWindow.DataContext = new LoginViewModel(this, kernel.Get<IUserService>(), kernel.Get<IUserSession>());
            Application.Current.MainWindow = loginWindow;
            loginWindow.Show();
            var currentWindow = Window.GetWindow(NavFrame);
            currentWindow?.Close();
            //var currentWindow = Application.Current.MainWindow;
            //if (Application.Current.MainWindow == null)
            //{
            //    Application.Current.Shutdown();
            //}
        }

        private void OpenLoginWindowFromRegister(object parameter)
        {

            var loginWindow = new LoginWindow();
            loginWindow.DataContext = new LoginViewModel(this, kernel.Get<IUserService>(), kernel.Get<IUserSession>());
            Application.Current.MainWindow = loginWindow;
            loginWindow.Show();
            (parameter as Window)?.Close();
            if (Application.Current.MainWindow == null)
            {
                Application.Current.Shutdown();
            }
        }
        private void OpenRegisterWindow(object parameter)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.DataContext = new RegisterViewModel(this, kernel.Get<IUserService>(), kernel.Get<IUserSession>());
            Application.Current.MainWindow = registerWindow;
            registerWindow.Show();
            var currentWindow = Application.Current.MainWindow;
            if (Application.Current.MainWindow == null)
            {
                Application.Current.Shutdown();
            }

        }
        private void OpenMainWindow(object parameter)
        {
            var mainWindow = new MainWindow();
            var mainModel = new MainViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>());
            NavFrame = mainWindow.MainFrame;
            Application.Current.MainWindow = mainWindow;
            mainWindow.DataContext = mainModel;
            var mainPage = new MainPage();
            mainPage.DataContext = new MainPageViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>());
            NavFrame.Navigate(mainPage);
            mainWindow.Show();
            (parameter as Window)?.Close();

            var currentWindow = Application.Current.MainWindow;
            //currentWindow?.Close();

            //if (Application.Current.MainWindow == null)
            //{
            //    Application.Current.Shutdown();
            //}
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
            bookPage.DataContext = new BookPageViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>(), kernel.Get<ITariffService>(), kernel);
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
        private void OpenChosenPage(object parameter)
        {
            var chosenPage = new ChosenPage();
            chosenPage.DataContext = new ChosenPageViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>());
            NavFrame.Navigate(chosenPage);
        }
        private void OpenAuthorPage(object parameter)
        {
            var authorPage = new AuthorPage();
            authorPage.DataContext = new AuthorPageViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>());
            NavFrame.Navigate(authorPage);
        }
        private void OpenGenrePage(object parameter)
        {
            var genrePage = new GenrePage();
            genrePage.DataContext = new GenrePageViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>());
            NavFrame.Navigate(genrePage);
        }
        private void OpenOfflinePage(object parameter)
        {
            var offlinePage = new OfflinePage();
            offlinePage.DataContext = new OfflinePageViewModel(this, kernel.Get<IBookService>(), kernel.Get<IUserSession>());
            NavFrame.Navigate(offlinePage);
        }
        private void ExitApp(object parameter)
        {
            Application.Current.Shutdown();
        }
        public Frame GetMainFrame()
        {
            return NavFrame;
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
