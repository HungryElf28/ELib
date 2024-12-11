using Interfaces.Services;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DTO;
using System.Windows.Controls;

namespace ELib.ViewModel
{
    public class PasswordData
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class RegisterViewModel: INotifyPropertyChanged
    {
        public ICommand OpenLoginWindowCommand { get; }
        public ICommand RegisterCommand { get; }
        private NavigationViewModel _navigationViewModel;
        public event PropertyChangedEventHandler PropertyChanged;
        private IUserService _userService;
        private IUserSession _userSession;
        private string _name;
        private string _surname;
        private string _email;
        private string _login;
        private string _password;
        private string _confirmPassword;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                NotifyPropertyChanged(nameof(Surname));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                NotifyPropertyChanged(nameof(Email));
            }
        }

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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                NotifyPropertyChanged(nameof(ConfirmPassword));
            }
        }
        private ISnackbarMessageQueue _snackbarMessageQueue = new SnackbarMessageQueue();
        public ISnackbarMessageQueue SnackbarMessageQueue
        {
            get => _snackbarMessageQueue;
            set => _snackbarMessageQueue = value;
        }
        public bool SwitchWindows { get; private set; }

        public RegisterViewModel(NavigationViewModel navigationViewModel, IUserService userService, IUserSession userSession)
        {
            _navigationViewModel = navigationViewModel;
            OpenLoginWindowCommand = new RelayCommand(OpenLoginWindow);
            RegisterCommand = new RelayCommand(Register);
            _userService = userService;
            _userSession = userSession;
        }

        private void OpenLoginWindow(object parameter)
        {
            SwitchWindows = true;
            _navigationViewModel.OpenLoginWindowCommand.Execute(parameter);
            SwitchWindows = false;
        }
        private void Register(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            Password = passwordBox.Password;
            if (!_userService.ValidateLogin(Login))
                {
                    SnackbarMessageQueue.Enqueue("Логин занят, придумайте новый!");
                }
                else
                {
                    if (!_userService.ValidateEmail(Email))
                    {
                        SnackbarMessageQueue.Enqueue("Email уже используется аккаунтом, укажите другой!");
                    }
                    else
                    {
                        UserDto us = new UserDto();
                        us.login = Login;
                        us.password = Password;
                        us.name = Name;
                        us.surname = Surname;
                        us.e_mail = Email;
                        us.reg_date = DateTime.Today;
                        us.bonuses = 0;
                        _userService.RegisterUser(us);
                        _navigationViewModel.OpenMainWindowCommand.Execute(parameter);
                    }
                }
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
