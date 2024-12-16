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
    internal class GenrePageViewModel : INotifyPropertyChanged
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
        private GenreDto _currentGenre;
        public GenreDto CurrentGenre
        {
            get => _currentGenre;
            private set
            {
                _currentGenre = value;
                NotifyPropertyChanged(nameof(CurrentGenre));
            }
        }
        public GenrePageViewModel(NavigationViewModel navigationViewModel, IBookService bookService, IUserSession userSession)
        {
            OpenBookPageCommand = new RelayCommand(OpenBookPage);
            GoBackCommand = new RelayCommand(GoBack);
            _bookService = bookService;
            _userSession = userSession;
            _navigationViewModel = navigationViewModel;
            CurrentGenre = _bookService.CurrentGenre;
            LoadBooks();
        }
        public void LoadBooks()
        {
            BestList = new ObservableCollection<BookPreviewDto>(_bookService.GetGenreTopList(CurrentGenre.id, count));
            AllList = new ObservableCollection<BookPreviewDto>(_bookService.GetGenreBooks(CurrentGenre.id));
        }
        private void OpenBookPage(object parameter)
        {
            var book = parameter as BookPreviewDto;
            _bookService.SetCurrentBook(book.id);
            _navigationViewModel.OpenBookPageCommand.Execute(parameter);
        }
        private void GoBack(object parameter)
        {
            _bookService.RemoveCurrentGenre();
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
