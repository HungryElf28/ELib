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
    public class BookPageViewModel : INotifyPropertyChanged
    {
        private IBookService _bookService;
        private IUserSession _userSession;
        private ITariffService _tariffService;
        private NavigationViewModel _navigationViewModel;
        public ICommand GoBackCommand { get; }
        public ICommand ReadBookCommand { get; }
        private string _readButtonText;
        public string ReadButtonText
        {
            get => _readButtonText;
            set
            {
                _readButtonText = value;
                NotifyPropertyChanged(nameof(ReadButtonText));
            }
        }
        private bool _isBookAccessible;
        public bool IsBookAccessible
        {
            get => _isBookAccessible;
            set
            {
                _isBookAccessible = value;
                NotifyPropertyChanged(nameof(IsBookAccessible));
                ReadButtonText = IsBookAccessible ? "Читать" : "Чтение недоступно по тарифу";
            }
        }
        private BookDto _currentBook;
        public BookDto CurrentBook
        {
            get => _currentBook;
            private set
            {
                _currentBook = value;
                NotifyPropertyChanged(nameof(CurrentBook));
            }
        }
        private List<ReviewDto> _reviewList;
        public List<ReviewDto> ReviewList
        {
            get => _reviewList;
            private set
            {
                _reviewList = value;
                NotifyPropertyChanged(nameof(ReviewList));
            }
        }
        public string RatingText
        {
            get => CurrentBook.rating.HasValue ? $"Рейтинг: {CurrentBook.rating:F1}/5" : "Рейтинг: отсутствует";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public BookPageViewModel(NavigationViewModel navigationViewModel, IBookService bookService, IUserSession userSession, ITariffService tariffService)
        {
            GoBackCommand = new RelayCommand(GoBack);
            ReadBookCommand = new RelayCommand(ReadBook);
            _bookService = bookService;
            _userSession = userSession;
            _tariffService = tariffService;
            _navigationViewModel = navigationViewModel;
            CurrentBook = bookService.CurrentBook;
            IsBookAccessible = _tariffService.CheckTariff(CurrentBook.id, _userSession.CurrentUser.id);
            LoadReviews();
        }
        private void LoadReviews()
        {
            ReviewList = _bookService.GetReviews(CurrentBook.id);
        }
        private void GoBack(object parameter)
        {
            _navigationViewModel.GoBackCommand.Execute(parameter);
        }
        private void ReadBook(object parameter)
        {

            _navigationViewModel.OpenReadPageCommand.Execute(parameter);
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
