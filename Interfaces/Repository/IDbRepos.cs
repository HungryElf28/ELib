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
        IRepository<tariff> tariff { get; }
        IBooksShowRepository showBooks {  get; }
        IReviewsRepository bookReviews { get; }
        IReadingRepository reading {  get; }
        IUserTariffRepository userTariff {  get; }
        IChosenRepository chosen {  get; }
        IOfflineRepository offline {  get; }
        int Save();
    }
 }
