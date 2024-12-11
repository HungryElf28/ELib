using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DTO
{
    public class GenreDto
    {
        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string genre_name { get; set; }
        public GenreDto() { }
        public GenreDto(genres genr)
        {
            this.id = genr.id;
            this.genre_name = genr.genre_name;
        }
    }
}
