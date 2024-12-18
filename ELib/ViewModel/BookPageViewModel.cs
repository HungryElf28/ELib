using DTO;
using ELib.View;
using Interfaces.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;

namespace ELib.ViewModel
{
    public class StarItem
    {
        public PackIconKind Icon { get; set; }
    }
    public class BookPageViewModel : INotifyPropertyChanged
    {
        private readonly IKernel kernel;
        private IBookService _bookService;
        private IUserSession _userSession;
        private ITariffService _tariffService;
        private NavigationViewModel _navigationViewModel;
        public ICommand GoBackCommand { get; }
        public ICommand ReadBookCommand { get; }
        public ICommand ChooseCommand { get; }
        public ICommand OfflineCommand { get; }
        public ICommand OpenReviewWindowCommand { get; }
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
        private string _chosenButtonText;
        public string ChosenButtonText
        {
            get => _chosenButtonText;
            set
            {
                _chosenButtonText = value;
                NotifyPropertyChanged(nameof(ChosenButtonText));
            }
        }
        private string _offlineButtonText;
        public string OfflineButtonText
        {
            get => _offlineButtonText;
            set
            {
                _offlineButtonText = value;
                NotifyPropertyChanged(nameof(OfflineButtonText));
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
        private bool _isBookChosen;
        public bool IsBookChosen
        {
            get => _isBookChosen;
            set
            {
                _isBookChosen = value;
                NotifyPropertyChanged(nameof(IsBookChosen));
                ChosenButtonText = IsBookChosen ? "Удалить из избранного" : "В избранное";
            }
        }
        private bool _isBookOffline;
        public bool IsBookOffline
        {
            get => _isBookOffline;
            set
            {
                _isBookOffline = value;
                NotifyPropertyChanged(nameof(IsBookOffline));
                OfflineButtonText = IsBookOffline ? "Удалить c устройства" : "Загрузить на устройство";
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
                NotifyPropertyChanged(nameof(RatingText));
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
        public bool IsAuthenticated
        {
            get => _userSession.IsAuthenticated;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public BookPageViewModel(NavigationViewModel navigationViewModel, IBookService bookService, IUserSession userSession, ITariffService tariffService, IKernel kernel)
        {
            GoBackCommand = new RelayCommand(GoBack);
            ReadBookCommand = new RelayCommand(ReadBook);
            OpenReviewWindowCommand = new RelayCommand(OpenReview);
            ChooseCommand = new RelayCommand(Choose);
            OfflineCommand = new RelayCommand(Download);
            _bookService = bookService;
            _userSession = userSession;
            _tariffService = tariffService;
            _navigationViewModel = navigationViewModel;
            CurrentBook = bookService.CurrentBook;
            if (IsAuthenticated)
            {
                IsBookAccessible = _tariffService.CheckTariff(CurrentBook.id, _userSession.CurrentUser.id);
                IsBookChosen = _bookService.GetChosenStatus(_userSession.CurrentUser.id, CurrentBook.id);
                IsBookOffline = _bookService.GetOfflineStatus(_userSession.CurrentUser.id, CurrentBook.id);
            }
            LoadReviews();
            this.kernel = kernel;
        }
        private void LoadReviews()
        {
            _bookService.UpdateCurrentBookRating();
            CurrentBook = _bookService.CurrentBook;
            NotifyPropertyChanged(nameof(RatingText));
            UpdateRatingStars();
            ReviewList = _bookService.GetReviews(CurrentBook.id);
        }
        public ObservableCollection<StarItem> RatingStars { get; } = new ObservableCollection<StarItem>();
        private void UpdateRatingStars()
        {
            RatingStars.Clear();
            if (CurrentBook.rating != null)
            {
                int fullStars = (int)CurrentBook.rating;

                bool hasHalfStar = CurrentBook.rating - fullStars >= 0.5;

                for (int i = 0; i < fullStars; i++)
                {
                    RatingStars.Add(new StarItem { Icon = PackIconKind.Star });
                }

                if (hasHalfStar)
                {
                    RatingStars.Add(new StarItem { Icon = PackIconKind.StarHalf });
                }

                int emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);
                for (int i = 0; i < emptyStars; i++)
                {
                    RatingStars.Add(new StarItem { Icon = PackIconKind.StarOutline });
                }
            }
        }
        private void GoBack(object parameter)
        {
            _bookService.RemoveCurrentBook();
            _navigationViewModel.GoBackCommand.Execute(parameter);
        }
        private void ReadBook(object parameter)
        {

            _navigationViewModel.OpenReadPageCommand.Execute(parameter);
        }
        private void Choose(object parameter)
        {
            _bookService.ChangeChosenStatus(_userSession.CurrentUser.id, CurrentBook.id);
            IsBookChosen = _bookService.GetChosenStatus(_userSession.CurrentUser.id, CurrentBook.id);
        }
        private void Download(object parameter)
        {
            _bookService.ChangeOfflineStatus(_userSession.CurrentUser.id, CurrentBook.id);
            IsBookOffline = _bookService.GetOfflineStatus(_userSession.CurrentUser.id, CurrentBook.id);
        }
        private void OpenReview(object parameter)
        {
            var reviewWindow = new MakeReviewWindow();
            Frame NavFrame = _navigationViewModel.GetMainFrame();
            reviewWindow.DataContext = new MakeReviewViewModel(this, _userSession, _bookService, kernel.Get<IReviewService>());
            reviewWindow.Owner = Window.GetWindow(NavFrame);
            reviewWindow.ShowDialog();
            LoadReviews();
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public enum StarState
    {
        Empty,
        Half,
        Full
    }
}
