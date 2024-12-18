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
using System.Windows;

namespace ELib.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private NavigationViewModel _navigationViewModel;
        private IBookService _bookService;
        private IUserSession _userSession;
        public ICommand OpenBookPageCommand { get; }
        public ICommand GetResultsCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand OpenAccountPageCommand { get; }
        public ICommand OpenChosenPageCommand { get; }
        public ICommand OpenOfflinePageCommand { get; }
        public ICommand OpenLoginWindowCommand { get; }
        public ICommand ExitAppCommand { get; }
        private List<GenreDto> _genreList;
        private ObservableCollection<MainBooksListDto> _mainBooksList;
        private ObservableCollection<BookPreviewDto> _recList;
        private ObservableCollection<SearchResultDto> _srchList;
        private ObservableCollection<BookPreviewDto> _readList;
        private ObservableCollection<BookPreviewDto> _topList;
        private SearchResultDto _selectedResult;
        private string _srchText;
        private string _userLogin;
        private const int count = 5;
        public event PropertyChangedEventHandler PropertyChanged;
        public string UserLogin
        {
            get => _userLogin;
            private set
            {
                _userLogin = value;
            }
        }
        public ObservableCollection<MainBooksListDto> MainBooksList
        {
            get => _mainBooksList;
            private set
            {
                _mainBooksList = value;
                NotifyPropertyChanged(nameof(MainBooksList));
            }
        }
        public ObservableCollection<BookPreviewDto> RecList
        {
            get => _recList;
            private set
            {
                _recList = value;
                NotifyPropertyChanged(nameof(RecList));
            }
        }
        public ObservableCollection<SearchResultDto> SearchList
        {
            get => _srchList;
            private set
            {
                _srchList = value;
                NotifyPropertyChanged(nameof(SearchList));
            }
        }
        public ObservableCollection<BookPreviewDto> ReadList
        {
            get => _readList;
            private set
            {
                _readList = value;
                NotifyPropertyChanged(nameof(ReadList));
            }
        }
        public ObservableCollection<BookPreviewDto> TopList
        {
            get => _topList;
            private set
            {
                _topList = value;
                NotifyPropertyChanged(nameof(TopList));
            }
        }
        public SearchResultDto SelectedResult
        {
            get => _selectedResult;
            set
            {
                _selectedResult = value;
                NotifyPropertyChanged(nameof(SelectedResult));
                if (_selectedResult != null)
                {
                    SrchText = _selectedResult.display_name;
                }
            }
        }
        public string SrchText
        {
            get => _srchText;
            set
            {
                _srchText = value;
                NotifyPropertyChanged(nameof(SrchText));
                UpdateSearchList(_srchText);
            }
        }
        private string _loginButtonText;
        public string LoginButtonText
        {
            get => _loginButtonText;
            set
            {
                _loginButtonText = value;
                NotifyPropertyChanged(nameof(LoginButtonText));
            }
        }
        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set
            {
                _isAuthenticated = value;
                NotifyPropertyChanged(nameof(IsAuthenticated));
                LoginButtonText = IsAuthenticated ? "Выйти из аккаунта" : "Войти/Зарегистрироваться";
            }
        }
        public bool HasRecommends
        {
            get => RecList != null && RecList.Any();
        }
        public bool HasReadings
        {
            get => ReadList != null && ReadList.Any();
        }
        public MainPageViewModel(NavigationViewModel navigationViewModel, IBookService bookService, IUserSession userSession)
        {
            _bookService = bookService;
            _userSession = userSession;
            _navigationViewModel = navigationViewModel;
            OpenBookPageCommand = new RelayCommand(OpenBookPage);
            OpenAccountPageCommand = new RelayCommand(OpenAccountPage);
            OpenChosenPageCommand = new RelayCommand(OpenChosenPage);
            OpenOfflinePageCommand = new RelayCommand(OpenOfflinePage);  
            OpenLoginWindowCommand = new RelayCommand(OpenLoginWindow);
            SearchCommand = new RelayCommand(OpenSearchResult);
            GetResultsCommand = new RelayCommand(GetResults);
            ExitAppCommand = new RelayCommand(ExitApp);
            MainBooksList = new ObservableCollection<MainBooksListDto>();
            SearchList = new ObservableCollection<SearchResultDto>();
            IsAuthenticated = _userSession.IsAuthenticated;
            UserLogin = _userSession.CurrentUser.login;
            LoadBooks();
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void LoadBooks()
        {
            _genreList = _bookService.GetGenres();
            foreach (var genre in _genreList)
            {
                var books = _bookService.GetGenreTopList(genre.id, count);
                MainBooksList.Add(new MainBooksListDto
                {
                    ListGenre = genre,
                    ListBookPreviews = books
                });
            }
            TopList = new ObservableCollection<BookPreviewDto>(_bookService.GetTopList());
            if (IsAuthenticated)
            {
                RecList = new ObservableCollection<BookPreviewDto>(_bookService.GetRecList(_userSession.CurrentUser.id));
                ReadList = new ObservableCollection<BookPreviewDto>(_bookService.GetLastReadingList(_userSession.CurrentUser.id));
            }
        }
        private void UpdateSearchList(string searchText)
        {
            var newList = _bookService.GetSearchList(searchText);
            SearchList.Clear();
            foreach (var item in newList)
            {
                SearchList.Add(item);
            }
        }
        private void OpenBookPage(object parameter)
        {
            var book = parameter as BookPreviewDto;
            _bookService.SetCurrentBook(book.id);
            _navigationViewModel.OpenBookPageCommand.Execute(parameter);
        }
        private void GetResults(object parameter)
        {
            string tx = parameter as string;
            if (!string.IsNullOrEmpty(tx))
            {
                UpdateSearchList(tx);
            }
        }
        private void OpenSearchResult(object parameter)
        {
            var result = parameter as SearchResultDto;
            switch (result.type)
            {
                case 1:
                    _bookService.SetCurrentBook(result.id);
                    _navigationViewModel.OpenBookPageCommand.Execute(parameter);
                    break;
                case 2:
                    _bookService.SetCurrentGenre(result.id);
                    _navigationViewModel.OpenGenrePageCommand.Execute(parameter);
                    break;
                case 3:
                    _bookService.SetCurrentAuthor(result.id);
                    _navigationViewModel.OpenAuthorPageCommand.Execute(parameter);
                    break;
            }
        }
        private void OpenLoginWindow(object parameter)
        {
            _navigationViewModel.OpenLoginWindowCommand.Execute(parameter);
        }
        private void OpenAccountPage(object parameter)
        {
            _navigationViewModel.OpenAccountPageCommand.Execute(parameter);
        }
        private void OpenChosenPage(object parameter)
        {
            _navigationViewModel.OpenChosenPageCommand.Execute(parameter);
        }
        private void OpenOfflinePage(object parameter)
        {
            _navigationViewModel.OpenOfflinePageCommand.Execute(parameter);
        }
        private void ExitApp(object parameter)
        {
            _navigationViewModel.ExitAppCommand.Execute(parameter);
        }
    }
}
