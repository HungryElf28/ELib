using DTO;
using Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthorService
    {
        private IDbRepos db;
        public AuthorDto CurrentAuthor { get; set; }
        public AuthorService(IDbRepos db)
        {
            this.db = db;
        }
        public void SetCurrentAuthor(int id)
        {
            CurrentAuthor = new AuthorDto(db.authors.GetItem(id));
        }
    }
}
