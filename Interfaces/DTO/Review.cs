using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ReviewDto
    {
        public int id { get; set; }

        public int mark { get; set; }

        [StringLength(8000)]
        public string reviewText { get; set; }

        public int userId { get; set; }
        public string userLogin { get; set; }

        public int bookId { get; set; }
    }
}
