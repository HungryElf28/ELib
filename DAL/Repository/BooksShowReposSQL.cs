using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DTO;
using Interfaces.Repository;

namespace DAL.Repository
{
    internal class BooksShowReposSQL: IBooksShowRepository
    {
        private Elib db;

        public BooksShowReposSQL(Elib dbcontext)
        {
            this.db = dbcontext;
        }
        public List<BookPreviewDto> GetAllBooksByGenreName(int genreID)
        {
            return db.books
                .Where(b => b.genreid == genreID)
                .Select(b => new BookPreviewDto
                {
                    id = b.id,
                    bookTitle = b.bookTitle,
                    coverLink = b.coverLink
                })
                .ToList();
        }
        public List<BookPreviewDto> GetTopBooksByGenreName(int genreID, int count)
        {
                return db.books
                    .Where(b => b.genreid == genreID)
                    .OrderByDescending(b => b.rating ?? 0)
                    .Take(count)
                    .Select(b => new BookPreviewDto
                    {
                        id = b.id,
                        bookTitle = b.bookTitle,
                        coverLink = b.coverLink
                    })
                    .ToList();
        }
        public List<BookPreviewDto> GetAllBooksByAuthorName(int authorID)
        {
            return db.books
                .Where(b => b.authors.Any(a => a.id == authorID))
                .Select(b => new BookPreviewDto
                {
                    id = b.id,
                    bookTitle = b.bookTitle,
                    coverLink = b.coverLink
                })
                .ToList();
        }
        public List<BookPreviewDto> GetTopBooksByAuthorName(int authorID, int count)
        {
            return db.books
                .Where(b => b.authors.Any(a => a.id == authorID))
                .OrderByDescending(b => b.rating ?? 0)
                .Take(count)
                .Select(b => new BookPreviewDto
                {
                    id = b.id,
                    bookTitle = b.bookTitle,
                    coverLink = b.coverLink
                })
                .ToList();
        }
        public List<BookPreviewDto> GetRecomendationList(int usId)
        {
            var userReadBooks = db.reading_book.Where(rb => rb.user_id == usId).Select(rb => rb.book_id).ToList();

            var favoriteGenres = db.books
                .Where(b => userReadBooks.Contains(b.id))
                .GroupBy(b => b.genreid)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => g.Key)
                .ToList();

            var recommendedBooks = db.books
                .Where(b => favoriteGenres.Contains(b.genreid) && !userReadBooks.Contains(b.id))
                .OrderByDescending(b => b.rating ?? 0)
                .Take(10)
                .Select(b => new BookPreviewDto 
                {
                    id = b.id,
                    bookTitle = b.bookTitle,
                    coverLink = b.coverLink
                })
                .ToList();
            return recommendedBooks;
        }
        public List<SearchResultDto> GetSearchResults(string query)
        {
            query = query.ToLower();
            var results = db.books
                .Where(b => b.bookTitle.ToLower().Contains(query)
                         || b.genres.genre_name.ToLower().Contains(query)
                         || b.authors.Any(a => a.name.ToLower().Contains(query)))
                .Select(b => new SearchResultDto
                {
                    id = b.id,
                    display_name = b.bookTitle,
                    type = 1
                })
                .Union(
                    db.authors
                        .Where(a => a.name.ToLower().Contains(query))
                        .Select(a => new SearchResultDto
                        {
                            id=a.id,
                            display_name = a.name,
                            type = 3
                        })
                        )
                .Union(
                    db.genres
                        .Where(g => g.genre_name.ToLower().Contains(query))
                        .Select(g => new SearchResultDto
                        {
                            id = g.id,
                            display_name = g.genre_name,
                            type = 2
                        })
                         )
                .ToList();
            return results;
        }
        public List<BookPreviewDto> GetTopBooks()
        {
            var topBooks = db.books
                .OrderByDescending(b => b.rating ?? 0)
                .Take(10)
                .Select(b => new BookPreviewDto
                {
                    id = b.id,
                    bookTitle = b.bookTitle,
                    coverLink = b.coverLink
                })
                .ToList();
            return topBooks;
        }
        public List<BookPreviewDto> GetReadBooks(int usId)
        {
            var userReadBooks = db.reading_book
                .Where(rb => rb.user_id == usId)
                .OrderByDescending(rb => rb.last_date)
                .Take(10)
                .Select(rb => new BookPreviewDto
                {
                    id = rb.book_id,
                    bookTitle = rb.books.bookTitle,
                    coverLink = rb.books.coverLink
                })
                .ToList();
            return userReadBooks;
        }

    }
}
