using DomainModel;
using Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BookReposSQL: IRepository<books>
    {
        private Elib db;

        public BookReposSQL(Elib dbcontext)
        {
            this.db = dbcontext;
        }
        public IQueryable<books> GetAll()
        {
            return db.books;
        }
        public books GetItem(int id)
        {
            return db.books.Find(id);
        }

        public void Create(books bk)
        {
            db.books.Add(bk);
        }

        public void Update(books bk)
        {
            db.Entry(bk).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            books bk = db.books.Find(id);
            if (bk != null)
                db.books.Remove(bk);
        }
    }
}
