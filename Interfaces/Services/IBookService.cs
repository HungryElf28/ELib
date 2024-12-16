using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IBookService
    {
        BookDto CurrentBook { get; set; }
        AuthorDto CurrentAuthor { get; set; }
        GenreDto CurrentGenre { get; set; }
        BookDto GetBook(int id);
        void CreateBook(BookDto bk);
        void UpdateBook(BookDto bk);
        void UpdateCurrentBookRating();
        bool GetChosenStatus(int usId, int bkId);
        void ChangeChosenStatus(int usId, int bkId);
        bool GetOfflineStatus(int usId, int bkId);
        void ChangeOfflineStatus(int usId, int bkId);
        void DeleteBook(int id);
        void SetCurrentBook(int id);
        void SetCurrentAuthor(int id);
        void SetCurrentGenre(int id);
        void RemoveCurrentBook();
        void RemoveCurrentAuthor();
        void RemoveCurrentGenre();
        List<BookPreviewDto> GetGenreTopList(int GenreId, int count);
        List<BookPreviewDto> GetAuthorTopList(int AuthorId, int count);
        List<SearchResultDto> GetSearchList(string query);
        List<BookPreviewDto> GetGenreBooks(int GenreId);
        List<BookPreviewDto> GetAuthorBooks(int AuthorId);
        List<GenreDto> GetGenres();
        List<AuthorDto> GetAuthors();
        List<ReviewDto> GetReviews(int bkId);
        List<BookPreviewDto> GetRecList(int usId);
        List<BookPreviewDto> GetChosenList(int usId);
        List<BookPreviewDto> GetOfflineList(int usId);
    }
}
