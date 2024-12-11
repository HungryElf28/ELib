using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repository;
using DomainModel;
using System.Data.Entity;
using Interfaces.Services;

namespace DAL.Repository
{
    public class DbReposSQL: IDbRepos
    {
        private ELibrary db;
        private UserReposSQL usersRepos;
        private BookReposSQL booksRepos;
        private GenreReposSQL genreRepos;
        private AuthorReposSQL authorRepos;
        private MainWindowShowReposSQL mainShowRepos;
        private ReviewReposSQL reviewRepos;
        private ReadingReposSQL readingRepos;
        public DbReposSQL()
        {
            db = new ELibrary();
        }
        public IRepository<users> users
        {
            get
            {
                if (usersRepos == null)
                    usersRepos = new UserReposSQL(db);
                return usersRepos;
            }
        }
        public IRepository<books> books
        {
            get
            {
                if (booksRepos == null)
                    booksRepos = new BookReposSQL(db);
                return booksRepos;
            }
        }
        public IRepository<genres> genres
        {
            get
            {
                if (genreRepos == null)
                    genreRepos = new GenreReposSQL(db);
                return genreRepos;
            }
        }
        public IRepository<authors> authors
        {
            get
            {
                if (authorRepos == null)
                    authorRepos = new AuthorReposSQL(db);
                return authorRepos;
            }
        }
        public IMainBooksShowRepository showBooks
        {
            get
            {
                if (mainShowRepos == null)
                    mainShowRepos = new MainWindowShowReposSQL(db);
                return mainShowRepos;
            }
        }
        public IReviewsRepository bookReviews
        {
            get
            {
                if (reviewRepos == null)
                    reviewRepos = new ReviewReposSQL(db);
                return reviewRepos;
            }
        }
        public IReadingRepository reading
        {
            get
            {
                if (readingRepos == null)
                    readingRepos = new ReadingReposSQL(db);
                return readingRepos;
            }
        }
        public int Save()
        {
            return db.SaveChanges();
        }

    }
}
