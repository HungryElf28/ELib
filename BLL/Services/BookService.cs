using DomainModel;
using DTO;
using Interfaces.Repository;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookService: IBookService
    {
        private IDbRepos db;
        public BookDto CurrentBook { get; set; }
        public AuthorDto CurrentAuthor { get; set; }
        public GenreDto CurrentGenre { get; set; }
        public BookService(IDbRepos db)
        {
            this.db = db;
        }
        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }
        public BookDto GetBook(int id)
        {
            return new BookDto(db.books.GetItem(id));
        }
        public void SetCurrentBook(int id)
        {
            CurrentBook =  new BookDto(db.books.GetItem(id));
        }
        public void SetCurrentAuthor(int id)
        {
            CurrentAuthor = new AuthorDto(db.authors.GetItem(id));
        }
        public void SetCurrentGenre(int id)
        {
            CurrentGenre = new GenreDto(db.genres.GetItem(id));
        }
        public void RemoveCurrentBook()
        {
            CurrentBook = null;
        }
        public void RemoveCurrentAuthor()
        {
            CurrentAuthor = null;
        }
        public void RemoveCurrentGenre()
        {
            CurrentGenre = null;
        }
        public void CreateBook(BookDto bk)
        {
            db.books.Create(new books()
            { id = bk.id, bookTitle = bk.bookTitle , releaseDate = bk.releaseDate, description = bk.description, fileLink = bk.fileLink, coverLink = bk.coverLink});
            Save();
        }
        public void UpdateBook(BookDto bk)
        {
            books bok = db.books.GetItem(bk.id);
            bok.bookTitle = bk.bookTitle;
            bok.releaseDate = bk.releaseDate;
            bok.description = bk.description;
            bok.fileLink = bk.fileLink;
            bok.coverLink = bk.coverLink;
            db.Save();
        }
        public void UpdateCurrentBookRating()
        {
            books bok = db.books.GetItem(CurrentBook.id);
            var reviews = db.bookReviews.GetBookReviews(bok.id);
            if (reviews != null && reviews.Any())
            {
                var averageRating = reviews.Average(r => r.mark);
                bok.rating = averageRating;
                db.Save();
            }
            else
            {
                bok.rating = null;
                db.Save();
            }
            CurrentBook.rating = bok.rating;
        }
        public void DeleteBook(int id)
        {
            books bok = db.books.GetItem(id);
            if (bok != null)
            {
                db.books.Delete(bok.id);
                db.Save();
            }
        }
        public bool GetChosenStatus(int usId, int bkId)
        {
            return db.chosen.Check(usId, bkId);
        }
        public void ChangeChosenStatus(int usId, int bkId)
        {
            if(db.chosen.Check(usId, bkId))
            {
                db.chosen.Delete(usId, bkId);
                db.Save();
            }
            else
            {
                db.chosen.Add(usId, bkId);
                db.Save();
            }
        }
        public bool GetOfflineStatus(int usId, int bkId)
        {
            return db.offline.IsOffline(usId, bkId);
        }
        public void ChangeOfflineStatus(int usId, int bkId)
        {
            if (db.offline.IsOffline(usId, bkId))
            {
                db.offline.DeleteBook(usId, bkId);
                db.Save();
            }
            else
            {
                db.offline.DownloadBook(usId, bkId);
                db.Save();
            }
        }
        public List<SearchResultDto> GetSearchList(string query)
        {
            return db.showBooks.GetSearchResults(query);
        }
        public List<BookPreviewDto> GetGenreBooks(int GenreId)
        {
            return db.showBooks.GetAllBooksByGenreName(GenreId);
        }
        public List<BookPreviewDto> GetGenreTopList(int GenreId, int count)
        {
            return db.showBooks.GetTopBooksByGenreName(GenreId, count);
        }
        public List<BookPreviewDto> GetAuthorBooks(int AuthorId)
        {
            return db.showBooks.GetAllBooksByAuthorName(AuthorId);
        }
        public List<BookPreviewDto> GetAuthorTopList(int AuthorId, int count)
        {
            return db.showBooks.GetTopBooksByAuthorName(AuthorId, count);
        }
        public List<BookPreviewDto> GetRecList(int usId)
        {
            return db.showBooks.GetRecomendationList(usId);
        }
        public List<BookPreviewDto> GetLastReadingList(int usId)
        {
            return db.showBooks.GetReadBooks(usId);
        }
        public List<BookPreviewDto> GetTopList()
        {
            return db.showBooks.GetTopBooks();
        }
        public List<GenreDto> GetGenres()
        {
            return db.genres.GetAll().AsEnumerable().Select(genre => new GenreDto(genre)).ToList();
        }
        public List<AuthorDto> GetAuthors()
        {
            return db.authors.GetAll().Select(i => new AuthorDto(i)).ToList();
        }
        public List<ReviewDto> GetReviews(int bkId)
        {
            return db.bookReviews.GetBookReviews(bkId).Where(rv => rv.mark != null).ToList();
        }
        public List<BookPreviewDto> GetChosenList(int usId)
        {
            return db.chosen.GetChosenBooks(usId);
        }
        public List<BookPreviewDto> GetOfflineList(int usId)
        {
            return db.offline.GetOfflineBooks(usId);
        }
    }
}
