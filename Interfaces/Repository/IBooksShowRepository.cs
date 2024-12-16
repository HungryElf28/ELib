using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repository
{
    public interface IBooksShowRepository
    {
        List<BookPreviewDto> GetRecomendationList(int usId);
        List<BookPreviewDto> GetAllBooksByGenreName(int genreID);
        List<BookPreviewDto> GetTopBooksByGenreName(int genreId, int count);
        List<BookPreviewDto> GetAllBooksByAuthorName(int authorID);
        List<BookPreviewDto> GetTopBooksByAuthorName(int authorID, int count);
        List<SearchResultDto> GetSearchResults(string query);
    }
}
