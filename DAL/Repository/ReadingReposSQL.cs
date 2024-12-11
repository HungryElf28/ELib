using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repository;
using DomainModel;

namespace DAL.Repository
{
    public class ReadingReposSQL: IReadingRepository
    {
        private ELibrary db;
        public ReadingReposSQL(ELibrary Context)
        {
            db = Context;
        }
        public reading_book GetReadingStatus(int usId, int bkId)
        {
            reading_book rb = db.reading_book.Find(usId, bkId);
            if (rb == null)
            {
                rb = new reading_book();
                rb.user_id = usId;
                rb.book_id = bkId;
                rb.current_page = 0;
                rb.users = db.users.Find(usId);
                rb.books= db.books.Find(bkId);
                db.reading_book.Add(rb);
                db.SaveChanges();
            }
            return db.reading_book.Find(usId, bkId);
        }
        public void Create(reading_book rb)
        {
            db.reading_book.Add(rb);
        }

    }
}
