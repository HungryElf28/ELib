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
        public void DeleteBook(int id)
        {
            books bok = db.books.GetItem(id);
            if (bok != null)
            {
                db.books.Delete(bok.id);
                db.Save();
            }
        }
        public List<BookPreviewDto> GetTopList(int GenreId, int count)
        {
            return db.showBooks.GetTopBooksByGenreName(GenreId, count);
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
            return db.bookReviews.GetBookReviews(bkId);
        }

    }
}
