using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MainBooksListDto
    {
        public GenreDto ListGenre { get; set; } = new GenreDto();
        public List<BookPreviewDto> ListBookPreviews { get; set; } = new List<BookPreviewDto>();
        public MainBooksListDto() { }
        public MainBooksListDto(GenreDto listGenre, List<BookPreviewDto> listBookPreviews)
        {
            ListGenre = listGenre;
            ListBookPreviews = listBookPreviews;
        }
    }
}
