using BLL.Services;
using DTO;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ELib.ViewModel
{
    public class ChosenPageViewModel : INotifyPropertyChanged
    {
        private NavigationViewModel _navigationViewModel;
        private IBookService _bookService;
        private IUserSession _userSession;
        public ICommand OpenBookPageCommand { get; }
        public ICommand GoBackCommand { get; }
        private ObservableCollection<BookPreviewDto> _chList;
        public ObservableCollection<BookPreviewDto> ChList
        {
            get => _chList;
            private set
            {
                _chList = value;
                NotifyPropertyChanged(nameof(ChList));
            }
        }
        private string _userLogin;
        public string UserLogin
        {
            get => _userLogin;
            private set
            {
                _userLogin = value;
            }
        }
        public ChosenPageViewModel(NavigationViewModel navigationViewModel, IBookService bookService, IUserSession userSession)
        { 
            _bookService = bookService;
            _userSession = userSession;
            _navigationViewModel = navigationViewModel;
            OpenBookPageCommand = new RelayCommand(OpenBookPage);
            GoBackCommand = new RelayCommand(GoBack);
            UserLogin = _userSession.CurrentUser.login;
            LoadChosen();
        }
        private void OpenBookPage(object parameter)
        {
            var book = parameter as BookPreviewDto;
            _bookService.SetCurrentBook(book.id);
            _navigationViewModel.OpenBookPageCommand.Execute(book);
        }
        private void GoBack(object parameter)
        {
            _navigationViewModel.GoBackCommand.Execute(parameter);
        }
        private void LoadChosen()
        {
            ChList = new ObservableCollection<BookPreviewDto>(_bookService.GetChosenList(_userSession.CurrentUser.id));
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
