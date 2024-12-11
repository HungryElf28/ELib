using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DTO;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ELib.View;
using System.Windows.Controls;

namespace ELib.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly NavigationViewModel _navigationViewModel;
        private readonly IBookService _bookService;
        private readonly IUserSession _userSession;
        private Frame _mainFrame;

        public ICommand NavigateToMainPageCommand { get; }
        public ICommand NavigateToBookPageCommand { get; }

        public Frame MainFrame
        {
            get => _mainFrame;
            set => _mainFrame = value;
        }

        public MainViewModel(NavigationViewModel navigationViewModel, IBookService bookService, IUserSession userSession)
        {
            _navigationViewModel = navigationViewModel;
            _bookService = bookService;
            _userSession = userSession;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
