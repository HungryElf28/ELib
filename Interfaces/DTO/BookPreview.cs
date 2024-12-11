using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BookPreviewDto
    {
        public int id { get; set; }

        [Required]
        [StringLength(8000)]
        public string bookTitle { get; set; }
        [Required]
        [StringLength(8000)]
        public string coverLink { get; set; }
        public BookPreviewDto() { }
        public BookPreviewDto(books bk)
        {
            id = bk.id;
            bookTitle = bk.bookTitle;
            coverLink = bk.coverLink;
        }
    }
}
