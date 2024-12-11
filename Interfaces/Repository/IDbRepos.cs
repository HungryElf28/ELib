using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Interfaces.Services;


namespace Interfaces.Repository
{
    public interface IDbRepos
    {
        IRepository<users> users { get; }
        IRepository<books> books { get; }
        IRepository<genres> genres { get; }
        IRepository<authors> authors { get; }
        IMainBooksShowRepository showBooks {  get; }
        IReviewsRepository bookReviews { get; }
        IReadingRepository reading {  get; }
        int Save();
    }
 }
