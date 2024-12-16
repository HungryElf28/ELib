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
        private Elib db;
        private UserReposSQL usersRepos;
        private BookReposSQL booksRepos;
        private GenreReposSQL genreRepos;
        private AuthorReposSQL authorRepos;
        private BooksShowReposSQL mainShowRepos;
        private ReviewReposSQL reviewRepos;
        private ReadingReposSQL readingRepos;
        private TariffReposSQL tariffRepos;
        private UserTariffReposSQL userTariffRepos;
        private ChosenReposSQL chosenRepos;
        private OfflineReposSQL offlineRepos;
        public DbReposSQL()
        {
            db = new Elib();
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
        public IRepository<tariff> tariff
        {
            get
            {
                if (tariffRepos == null)
                    tariffRepos = new TariffReposSQL(db);
                return tariffRepos;
            }
        }
        public IBooksShowRepository showBooks
        {
            get
            {
                if (mainShowRepos == null)
                    mainShowRepos = new BooksShowReposSQL(db);
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
        public IUserTariffRepository userTariff
        {
            get
            {
                if (userTariffRepos == null)
                    userTariffRepos = new UserTariffReposSQL(db);
                return userTariffRepos;
            }
        }
        public IChosenRepository chosen
        {
            get
            {
                if (chosenRepos == null)
                    chosenRepos = new ChosenReposSQL(db);
                return chosenRepos;
            }
        }
        public IOfflineRepository offline
        {
            get
            {
                if (offlineRepos == null)
                    offlineRepos = new OfflineReposSQL(db);
                return offlineRepos;
            }
        }
        public int Save()
        {
            return db.SaveChanges();
        }

    }
}
