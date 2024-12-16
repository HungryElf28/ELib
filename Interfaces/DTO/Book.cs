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
        public int genreid { get; set; }
        public string genreName { get; set; }
        public ICollection<users> users_chose { get; set; }
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
            genreid = bk.genreid;
            authors = bk.authors.Select(a => new AuthorDto(a)).ToList();
            genreName = bk.genres.genre_name;
            users_chose = bk.users;
        }
    }
}
