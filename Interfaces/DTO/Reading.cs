using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ReadingDto
    {
        public int user_id { get; set; }

        public int book_id { get; set; }

        public int current_page { get; set; }

        public virtual books books { get; set; }

        public virtual users users { get; set; }
        public ReadingDto() { }
        public ReadingDto(reading_book rb)
        {
            this.user_id = rb.user_id;
            this.book_id = rb.book_id;
            this.current_page = rb.current_page;
            this.books = rb.books;
            this.users = rb.users;
        }
    }
}
