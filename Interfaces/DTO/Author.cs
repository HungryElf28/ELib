using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DTO
{
    public class AuthorDto
    {
        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [StringLength(20)]
        public string surname { get; set; }

        public string bio { get; set; }
        public AuthorDto() { }
        public AuthorDto(authors aut)
        {
            id = aut.id;
            name = aut.name;
            surname = aut.surname;
            bio = aut.bio;
        }
    }
}
