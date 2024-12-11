using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Windows;
using ELib.View;
using ELib.ViewModel;
using Interfaces.Services;
using DTO;
using System.Windows.Data;
using Ninject.Parameters;

namespace BookReader.ViewModel
{
    public class ReadViewModel : INotifyPropertyChanged
    {
        private List<string> _pages;
        private int _currentPage;
        private int _linesPerPage = 15;
        private double _fontSize = 12;
        private const double MaxFontSize = 22;
        private const double MinFontSize = 2;
        private IReadingService _readingService;
        private IBookService _bookService;
        private IUserSession _userSession;
        private NavigationViewModel _navigationViewModel;
        public int TotalPages => _pages.Count;
        public double Progress => (double)(_currentPage + 1) / TotalPages * 100;

        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand GoToPageCommand { get; }
        public ICommand IncreaseFontSizeCommand { get; }
        public ICommand DecreaseFontSizeCommand { get; }
        public ICommand GoBackCommand { get; }
        public double FontSize
        {
            get => _fontSize;
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    OnPropertyChanged(nameof(FontSize));
                }
            }
        }
        private BookDto _currentBook;
        public BookDto CurrentBook
        {
            get => _currentBook;
            private set
            {
                _currentBook = value;
                OnPropertyChanged(nameof(CurrentBook));
            }
        }
        private string _bookTitle;
        public string BookTitle
        {
            get => _bookTitle;
            private set
            {
                _bookTitle = value;
                OnPropertyChanged(nameof(BookTitle));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ReadViewModel(NavigationViewModel navigationViewModel, IBookService bookService, IUserSession userSession, IReadingService readingService)
        {
            GoToPageCommand = new RelayCommand(GoToPage);
            NextPageCommand = new RelayCommand(NextPage);
            PreviousPageCommand = new RelayCommand(PreviousPage);
            IncreaseFontSizeCommand = new RelayCommand(_ => IncreaseFontSize(), _ => FontSize < MaxFontSize);
            DecreaseFontSizeCommand = new RelayCommand(_ => DecreaseFontSize(), _ => FontSize > MinFontSize);
            GoBackCommand = new RelayCommand(GoBack);
            _bookService = bookService;
            _userSession = userSession;
            _readingService = readingService;
            _navigationViewModel = navigationViewModel;
            readingService.SetCurrentReading(userSession.CurrentUser.id, bookService.CurrentBook.id);
            _currentBook = bookService.CurrentBook;
            _currentPage = readingService.CurrentReading.current_page;
            LoadBook();
        }

        public string CurrentPageText => _pages[_currentPage];
        public int CurrentPage
        {
            get => _currentPage + 1;
            set
            {
                if (value > 0 && value <= TotalPages)
                {
                    _currentPage = value - 1;
                    OnPropertyChanged(nameof(CurrentPageText));
                    OnPropertyChanged(nameof(Progress));
                }
            }
        }
        private void NextPage(object obj)
        {
            if (_currentPage < TotalPages - 1)
            {
                _currentPage++;
                OnPropertyChanged(nameof(CurrentPageText));
                OnPropertyChanged(nameof(CurrentPage));
                OnPropertyChanged(nameof(Progress));
            }
        }

        private void PreviousPage(object obj)
        {
            if (_currentPage > 0)
            {
                _currentPage--;
                OnPropertyChanged(nameof(CurrentPageText));
                OnPropertyChanged(nameof(CurrentPage));
                OnPropertyChanged(nameof(Progress));
            }
        }

        private void GoToPage(object obj)
        {
            OnPropertyChanged(nameof(CurrentPageText));
            OnPropertyChanged(nameof(Progress));
        }
        private void IncreaseFontSize()
        {
            if (FontSize < MaxFontSize)
                FontSize++;
        }

        private void DecreaseFontSize()
        {
            if (FontSize > MinFontSize)
                FontSize--;
        }
        private void GoBack(object parameter)
        {
            _readingService.SaveCurrentPage(_currentPage);
            _navigationViewModel.GoBackCommand.Execute(parameter);
        }

        private void LoadBook()
        {
            var lines = File.ReadAllLines(_currentBook.fileLink);
            _pages = new List<string>();

            for (int i = 0; i < lines.Length; i += _linesPerPage)
            {
                _pages.Add(string.Join(Environment.NewLine, lines, i, Math.Min(_linesPerPage, lines.Length - i)));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
