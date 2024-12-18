using DTO;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ELib.ViewModel
{
    public class BonusDialogViewModel : INotifyPropertyChanged
    {
        private ITariffService _tariffService;
        private IUserSession _userSession;
        private AccountPageViewModel _accountPageViewModel;
        public ICommand SpareBonusesCommand { get; }
        public ICommand ExitCommand { get; }
        private int _currentBonuses;
        public int CurrentBonuses
        {
            get => _currentBonuses;
            set
            {
                _currentBonuses = value;
                NotifyPropertyChanged(nameof(CurrentBonuses));
            }
        }
        private int _bonusesToSpare;
        public int BonusesToSpare
        {
            get => _bonusesToSpare;
            set
            {
                _bonusesToSpare = value;
                NotifyPropertyChanged(nameof(BonusesToSpare));
            }
        }
        public BonusDialogViewModel(AccountPageViewModel accountPageViewModel, IUserSession userSession, ITariffService tariffService)
        {
            _accountPageViewModel = accountPageViewModel;
            _userSession = userSession;
            _tariffService = tariffService;
            CurrentBonuses = userSession.CurrentUser.bonuses;
            BonusesToSpare = 0;
            SpareBonusesCommand = new RelayCommand(SpareBonuses);
            ExitCommand = new RelayCommand(Exit);
        }
        private void SpareBonuses(object parameter)
        {
            _userSession.SpareBonuses(BonusesToSpare);
            (parameter as Window)?.Close();
        }
        private void Exit(object parameter)
        {
            (parameter as Window)?.Close();
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
