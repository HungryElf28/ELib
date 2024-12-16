using DomainModel;
using DTO;
using Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ChosenReposSQL : IChosenRepository
    {
        private Elib db;

        public ChosenReposSQL(Elib dbcontext)
        {
            this.db = dbcontext;
        }
        public void Add(int usId, int bkId)
        {
            var user = db.users.FirstOrDefault(u => u.id == usId);
            var book = db.books.FirstOrDefault(b => b.id == bkId);
            if (user != null && book != null)
            {
                if (!user.books.Contains(book))
                {
                    user.books.Add(book);
                }
                if (!book.users.Contains(user))
                {
                    book.users.Add(user);
                }
            }
            db.SaveChanges();
        }
        public void Delete(int usId, int bkId)
        {
            var user = db.users.FirstOrDefault(u => u.id == usId);
            var book = db.books.FirstOrDefault(b => b.id == bkId);
            if (user != null && book != null)
            {
                if (user.books.Contains(book))
                {
                    user.books.Remove(book);
                }
                if (book.users.Contains(user))
                {
                    book.users.Remove(user);
                }
            }
            db.SaveChanges();
        }
        public bool Check(int usId, int bkId)
        {
            var user = db.users.FirstOrDefault(u => u.id == usId);
            var book = db.books.FirstOrDefault(b => b.id == bkId);
            if (user.books.Contains(book) && book.users.Contains(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<BookPreviewDto> GetChosenBooks(int usId)
        {
            var user = db.users.FirstOrDefault(u => u.id == usId);
            return user.books.Select(b => new BookPreviewDto
            {
                id = b.id,
                bookTitle = b.bookTitle,
                coverLink = b.coverLink
            })
            .ToList();
        }
    }
}
