using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DTO
{
    public class UserDto
    {
        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string login { get; set; }

        [Required]
        [StringLength(20)]
        public string password { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [StringLength(20)]
        public string surname { get; set; }

        public DateTime reg_date { get; set; }

        [Required]
        [StringLength(8000)]
        public string e_mail { get; set; }

        public int bonuses { get; set; }
        public ICollection<books> chosen_books { get; set; }
        public UserDto() { }
        public UserDto(users us)
        {
            id = us.id;
            login = us.login;
            password = us.password;
            name = us.name;
            surname = us.surname;
            e_mail = us.e_mail;
            bonuses = us.bonuses;
            reg_date = us.reg_date;
            chosen_books = us.books;
        }
    }
}
