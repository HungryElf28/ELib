using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Interfaces.Services;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows;

namespace ELib.ViewModel
{
    public class LoginViewModel: INotifyPropertyChanged
    {
        public ICommand OpenRegisterWindowCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand NoAuthCommand { get; }
        private string _login;
        private string _password;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                NotifyPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }

        private ISnackbarMessageQueue _snackbarMessageQueue = new SnackbarMessageQueue();
        public ISnackbarMessageQueue SnackbarMessageQueue
        {
            get => _snackbarMessageQueue;
            set => _snackbarMessageQueue = value;
        }
        private NavigationViewModel _navigationViewModel;
        private IUserService _userService;
        private IUserSession _userSession;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool SwitchWindows { get; private set; }

        public LoginViewModel(NavigationViewModel navigationViewModel, IUserService userService, IUserSession userSession)
        {
            _navigationViewModel = navigationViewModel;
            OpenRegisterWindowCommand = new RelayCommand(OpenRegisterWindow);
            LoginCommand = new RelayCommand(AuthenticateUser);
            NoAuthCommand = new RelayCommand(ContinueAsGuest);
            _userService = userService;
            _userSession = userSession;
        }

        private void OpenRegisterWindow(object parameter)
        {
            SwitchWindows = true;
            _navigationViewModel.OpenRegisterWindowCommand.Execute(parameter);
            SwitchWindows = false;
        }
        private void OpenMainWindow(object parameter)
        {
            SwitchWindows = true;
            _navigationViewModel.OpenMainWindowCommand.Execute(parameter);
            SwitchWindows = false;
        }
        private void AuthenticateUser(object parameter)
        {
            dynamic parameters = parameter as dynamic;
            PasswordBox passwordBox = parameters.Password as PasswordBox;
            Window currWindow = parameters.Window as Window;
            Password = passwordBox.Password;
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                SnackbarMessageQueue.Enqueue("Введите логин и пароль!");
            }
            else
            {
                if (_userService.ValidateUser(Login, Password))
                {
                    _navigationViewModel.OpenMainWindowCommand.Execute(currWindow);

                }
                else
                {
                    SnackbarMessageQueue.Enqueue("Неверный логин или пароль!");
                }
            }
        }
        private void ContinueAsGuest(object parameter)
        {
            _userSession.ClearSession();
            _navigationViewModel.OpenMainWindowCommand.Execute(parameter);
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
