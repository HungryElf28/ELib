using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DTO
{
    public class SearchResultDto
    {
        public int id { get; set; }
        public string display_name { get; set; }
        public int type { get; set; }
        public SearchResultDto() { }
        public SearchResultDto(authors aut)
        {
            id = aut.id;
            display_name = aut.name;
            type = 3;
        }
        public SearchResultDto(books bk)
        {
            id = bk.id;
            display_name = bk.bookTitle;
            type = 1;
        }
        public SearchResultDto(genres gn)
        {
            id = gn.id;
            display_name = gn.genre_name;
            type = 2;
        }
    }
}
