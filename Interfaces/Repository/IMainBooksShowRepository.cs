using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repository
{
    public interface IMainBooksShowRepository
    {
        List<BookPreviewDto> GetTopBooksByGenreName(int genreId, int count);
    }
}
