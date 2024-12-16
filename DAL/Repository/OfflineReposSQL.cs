using DomainModel;
using Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL.Repository
{
    public class OfflineReposSQL : IOfflineRepository
    {
        private Elib db;
        public OfflineReposSQL(Elib Context)
        {
            db = Context;
        }
        public void DownloadBook(int usId, int bkId)
        {
            offline_book ob = new offline_book()
            {
                user_id = usId,
                book_id = bkId,
                download_date = DateTime.Now,
                delete_date = DateTime.Now.AddDays(7),
                books = db.books.Find(bkId),
                users = db.users.Find(usId)
            };
            db.offline_book.Add(ob);
            db.SaveChanges();
        }
        public List<BookPreviewDto> GetOfflineBooks(int usId)
        {
            return db.offline_book.Where(ob => ob.user_id==usId).Select(ob => new BookPreviewDto
            {
                id = ob.books.id,
                bookTitle = ob.books.bookTitle,
                coverLink = ob.books.coverLink,
            })
             .ToList();
        }
        public bool IsOffline(int usId, int bkId)
        {
            var ob = db.offline_book.Find(usId, bkId);
            if(ob != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DeleteExpiredBooks(int usId)
        {
            var expiredBooks = db.offline_book.Where(ob => ob.user_id == usId && ob.delete_date <= DateTime.Now).ToList();
            if (expiredBooks.Any())
            {
                db.offline_book.RemoveRange(expiredBooks);
                db.SaveChanges();
            }
        }
        public void DeleteBook(int usId, int bkId)
        {
            offline_book ob = db.offline_book.Find(usId, bkId);
            if(ob != null)
            {
                db.offline_book.Remove(ob);
                db.SaveChanges();
            }
        }
    }
}
