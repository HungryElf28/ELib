using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DTO
{
    public class ReviewDto
    {
        public int user_id { get; set; }
        public int book_id { get; set; }
        public int mark { get; set; }

        [StringLength(8000)]
        public string reviewText { get; set; }

        public string userLogin { get; set; }

        public ReviewDto()
        {}
        public ReviewDto(review rv)
        {
            this.mark = rv.mark;
            this.reviewText = rv.reviewText;
            this.user_id = rv.user_id;
            this.book_id = rv.book_id;
            this.userLogin = rv.users.login;

        }
    }
}
