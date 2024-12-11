﻿using Interfaces.Services;
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

namespace ELib.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private NavigationViewModel _navigationViewModel;
        private IBookService _bookService;
        private IUserSession _userSession;
        public ICommand OpenBookPageCommand { get; }
        private List<GenreDto> _genreList;
        private ObservableCollection<MainBooksListDto> _mainBooksList;
        private string _userLogin;
        private const int count = 8;
        public event PropertyChangedEventHandler PropertyChanged;
        public string UserLogin
        {
            get => _userLogin;
            private set
            {
                _userLogin = value;
                NotifyPropertyChanged(nameof(MainBooksList));
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
        public bool SwitchWindows { get; private set; }
        public MainPageViewModel(NavigationViewModel navigationViewModel, IBookService bookService, IUserSession userSession)
        {
            _bookService = bookService;
            _userSession = userSession;
            _navigationViewModel = navigationViewModel;
            OpenBookPageCommand = new RelayCommand(OpenBookPage);
            MainBooksList = new ObservableCollection<MainBooksListDto>();
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
                var books = _bookService.GetTopList(genre.id, count);
                MainBooksList.Add(new MainBooksListDto
                {
                    ListGenre = genre,
                    ListBookPreviews = books
                });
            }
        }
        private void OpenBookPage(object parameter)
        {
            var book = parameter as BookPreviewDto;
            _bookService.SetCurrentBook(book.id);
            _navigationViewModel.OpenBookPageCommand.Execute(book);
        }
    }
}