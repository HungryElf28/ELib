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
    internal class AuthorPageViewModel : INotifyPropertyChanged
    {
        private IBookService _bookService;
        private IUserSession _userSession;
        private NavigationViewModel _navigationViewModel;
        private const int count = 5;
        public ICommand OpenBookPageCommand { get; }
        public ICommand GoBackCommand { get; }
        private ObservableCollection<BookPreviewDto> _bestList;
        public ObservableCollection<BookPreviewDto> BestList
        {
            get => _bestList;
            private set
            {
                _bestList = value;
                NotifyPropertyChanged(nameof(BestList));
            }
        }
        private ObservableCollection<BookPreviewDto> _allList;
        public ObservableCollection<BookPreviewDto> AllList
        {
            get => _allList;
            private set
            {
                _allList = value;
                NotifyPropertyChanged(nameof(AllList));
            }
        }
        private AuthorDto _currentAuthor;
        public AuthorDto CurrentAuthor
        {
            get => _currentAuthor;
            private set
            {
                _currentAuthor = value;
                NotifyPropertyChanged(nameof(CurrentAuthor));
            }
        }
        public AuthorPageViewModel(NavigationViewModel navigationViewModel, IBookService bookService, IUserSession userSession)
        {
            OpenBookPageCommand = new RelayCommand(OpenBookPage);
            GoBackCommand = new RelayCommand(GoBack);
            _bookService = bookService;
            _userSession = userSession;
            _navigationViewModel = navigationViewModel;
            CurrentAuthor = _bookService.CurrentAuthor;
            LoadBooks();
        }
        public void LoadBooks()
        {
            BestList = new ObservableCollection<BookPreviewDto>(_bookService.GetAuthorTopList(_bookService.CurrentAuthor.id, count));
            AllList = new ObservableCollection<BookPreviewDto>(_bookService.GetAuthorBooks(_bookService.CurrentAuthor.id));
        }
        private void OpenBookPage(object parameter)
        {
            var book = parameter as BookPreviewDto;
            _bookService.SetCurrentBook(book.id);
            _navigationViewModel.OpenBookPageCommand.Execute(parameter);
        }
        private void GoBack(object parameter)
        {
            _bookService.RemoveCurrentAuthor();
            _navigationViewModel.GoBackCommand.Execute(parameter);
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
