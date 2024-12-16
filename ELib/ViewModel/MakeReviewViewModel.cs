using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DTO;
using Interfaces.Services;
using MaterialDesignThemes.Wpf;

namespace ELib.ViewModel
{
    public class MakeReviewViewModel: INotifyPropertyChanged
    {
        private IBookService _bookService;
        private IUserSession _userSession;
        private IReviewService _reviewService;
        private NavigationViewModel _navigationViewModel;
        public ICommand SaveReviewCommand { get; }
        public ICommand DeleteReviewCommand { get; }
        public ICommand ExitCommand { get; }
        public ObservableCollection<int> RatingOptions { get; set; }
        public MakeReviewViewModel(NavigationViewModel navigationViewModel, IUserSession userSession, IBookService bookService, IReviewService reviewService)
        {
            _bookService = bookService;
            _userSession = userSession;
            _reviewService = reviewService;
            _navigationViewModel = navigationViewModel;
            SaveReviewCommand = new RelayCommand(SaveReview);
            DeleteReviewCommand = new RelayCommand(DeleteReview);
            ExitCommand = new RelayCommand(Exit);
            CurrentReview = _reviewService.GetReview(_userSession.CurrentUser.id, _bookService.CurrentBook.id);
            RatingOptions = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private ReviewDto _currentReview;
        public ReviewDto CurrentReview
        {
            get => _currentReview;
            set
            {
                _currentReview = value;
                NotifyPropertyChanged(nameof(CurrentReview));
            }
        }
        private void SaveReview(object parameter)
        {
            if (CurrentReview.mark < 1 || CurrentReview.mark > 5)
            {
                CurrentReview.mark = _reviewService.CurrentReview.mark;
            }
            else
            {
                _reviewService.CreateOrUpdateReview(CurrentReview);
                _navigationViewModel.CloseReviewWindowCommand.Execute(parameter);
            }
        }
        private void DeleteReview(object parameter)
        {
            _reviewService.DeleteReview(CurrentReview.user_id, CurrentReview.book_id);
            _navigationViewModel.CloseReviewWindowCommand.Execute(parameter);
        }
        private void Exit(object parameter)
        {
            _navigationViewModel.CloseReviewWindowCommand.Execute(parameter);
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
