﻿using BLL.Services;
using DTO;
using Interfaces.Services;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace ELib.ViewModel
{
    public class TariffViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime? EndDate { get; set; }
        public Visibility ButtonVisibility => IsActive ? Visibility.Collapsed : Visibility.Visible;
        public Visibility DateVisibility => IsActive ? Visibility.Visible : Visibility.Collapsed;
    }
    public class AccountPageViewModel : INotifyPropertyChanged
    {
        private ITariffService _tariffService;
        private IUserSession _userSession;
        private NavigationViewModel _navigationViewModel;
        private UserDto _currentUser;
        private ObservableCollection<TariffViewModel> _tariffPreviews;
        public ICommand ChangeCommand { get; }
        public ICommand SetTariffCommand { get; }
        public UserDto CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                NotifyPropertyChanged(nameof(CurrentUser));
            }
        }
        public ObservableCollection<TariffViewModel> TariffPreviews
        {
            get { return _tariffPreviews; }
            set
            {
                _tariffPreviews = value;
                NotifyPropertyChanged(nameof(TariffPreviews));
            }
        }
        private ISnackbarMessageQueue _snackbarMessageQueue = new SnackbarMessageQueue();
        public ISnackbarMessageQueue SnackbarMessageQueue
        {
            get => _snackbarMessageQueue;
            set => _snackbarMessageQueue = value;
        }
        public AccountPageViewModel(NavigationViewModel navigationViewModel, IUserSession userSession, ITariffService tariffService)
        {
            _navigationViewModel = navigationViewModel;
            _userSession = userSession;
            _tariffService = tariffService;
            CurrentUser = userSession.CurrentUser;
            ChangeCommand = new RelayCommand(ValidateChanges);
            SetTariffCommand = new RelayCommand(SetTariff);
            TariffPreviews = new ObservableCollection<TariffViewModel>();
            LoadTariffs();
        }
        private void LoadTariffs()
        {
            TariffPreviews.Clear();
            var tariffPreviewsList = _tariffService.GetTariffPreviews(CurrentUser.id);
            var tariffViewModels = tariffPreviewsList.Select(tariff => new TariffViewModel
            {
                Id = tariff.id,
                Name = tariff.name,
                Price = tariff.price,
                IsActive = tariff.isActive,
                EndDate = tariff.endDate
            }).ToList();
            TariffPreviews = new ObservableCollection<TariffViewModel>(tariffViewModels);
        }
        private void ValidateChanges(object parameter)
        {
            if (!_userSession.ValidateLogin(CurrentUser.id, CurrentUser.login))
            {
                SnackbarMessageQueue.Enqueue("Логин занят, придумайте новый!");
                CurrentUser = _userSession.CurrentUser;
            }
            else
            {
                if (!_userSession.ValidateEmail(CurrentUser.id, CurrentUser.e_mail))
                {
                    SnackbarMessageQueue.Enqueue("Email уже используется аккаунтом, укажите другой!");
                    CurrentUser = _userSession.CurrentUser;
                }
                else
                {
                    _userSession.UpdateUser(CurrentUser);
                }
            }
        }
        private void SetTariff(object parameter)
        {
            var tariff = parameter as TariffViewModel;
            _tariffService.SetTariff(CurrentUser.id, tariff.Id);
            LoadTariffs();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
