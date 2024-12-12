using DomainModel;
using System;
using Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserTariffReposSQL: IUserTariffRepository
    {
        private Elib db;
        public UserTariffReposSQL(Elib Context)
        {
            db = Context;
        }
        public IQueryable<user_tariff> GetAll()
        {
            return db.user_tariff;
        }
        public bool FindTariff(int usId, int gnId)
        {
            var userTariffs = db.user_tariff
                .Where(ut => ut.user_id == usId)
                .Select(ut => ut.tariff)
                .ToList();

            bool hasAccessToGenre = userTariffs
                .Any(t => t.genres.Any(g => g.id == gnId));
            return hasAccessToGenre;
        }
        public void SetUserTariff(int usId, int trId)
        {
            user_tariff ut = new user_tariff()
            {
                user_id = usId,
                tariff_id = trId,
                active = true,
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddMonths(1),
                users = db.users.Find(usId),
                tariff = db.tariff.Find(trId)
            };
            db.user_tariff.Add(ut);
            db.SaveChanges();
        }
        public void RemoveExpiredTariffs(int usId)
        {
            var expiredTariffs = db.user_tariff
                .Where(ut => ut.user_id == usId && ut.endDate < DateTime.Now)
                .ToList();
            if (expiredTariffs.Any())
            {
                db.user_tariff.RemoveRange(expiredTariffs);
                db.SaveChanges();
            }
        }
    }
}
