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
        BookDto GetBook(int id);
        void CreateBook(BookDto bk);
        void UpdateBook(BookDto bk);
        void DeleteBook(int id);
        void SetCurrentBook(int id);
        List<BookPreviewDto> GetTopList(int GenreId, int count);
        List<GenreDto> GetGenres();
        List<AuthorDto> GetAuthors();
        List<ReviewDto> GetReviews(int bkId);


    }
}
