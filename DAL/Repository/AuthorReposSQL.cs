using Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using System.Data.Entity;

namespace DAL.Repository
{
    public class AuthorReposSQL: IRepository<authors>
    {
        private Elib db;
        public AuthorReposSQL(Elib dbcontext)
        {
            this.db = dbcontext;
        }
        public IQueryable<authors> GetAll()
        {
            return db.authors;
        }
        public authors GetItem(int id)
        {
            return db.authors.Find(id);
        }

        public void Create(authors aut)
        {
            db.authors.Add(aut);
        }

        public void Update(authors aut)
        {
            db.Entry(aut).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            authors aut = db.authors.Find(id);
            if (aut != null)
                db.authors.Remove(aut);
        }
    }
}
