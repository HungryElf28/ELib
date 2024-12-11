using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DTO
{
    public class BookDto
    {
        public int id { get; set; }

        [Required]
        [StringLength(8000)]
        public string bookTitle { get; set; }

        [Column(TypeName = "date")]
        public DateTime? releaseDate { get; set; }

        [StringLength(8000)]
        public string description { get; set; }

        [Required]
        [StringLength(8000)]
        public string fileLink { get; set; }

        [Required]
        [StringLength(8000)]
        public string coverLink { get; set; }
        public double? rating { get; set; }
        public List<AuthorDto> authors { get; set; } = new List<AuthorDto>();
        public List<GenreDto> genres { get; set; } = new List<GenreDto>();
        public BookDto() { }
        public BookDto(books bk)
        {
            id = bk.id;
            bookTitle = bk.bookTitle;
            releaseDate = bk.releaseDate;
            description = bk.description;
            fileLink = bk.fileLink;
            coverLink = bk.coverLink;
            rating = bk.rating;
            authors = bk.authors.Select(a => new AuthorDto(a)).ToList();
            genres = bk.genres.Select(g => new GenreDto(g)).ToList();
        }
    }
}
