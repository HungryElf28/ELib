using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Interfaces.Repository;

namespace DAL.Repository
{
    internal class MainWindowShowReposSQL: IMainBooksShowRepository
    {
        private ELibrary db;

        public MainWindowShowReposSQL(ELibrary dbcontext)
        {
            this.db = dbcontext;
        }
        public List<BookPreviewDto> GetTopBooksByGenreName(int genreID, int count)
        {
                return db.books
                    .Where(b => b.genres.Any(g => g.id == genreID))
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

    }
}
