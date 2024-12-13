using Interfaces.Repository;
using Interfaces.Services;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Metadata.Edm;

namespace BLL.Services
{
    public class TariffService: ITariffService
    {
        private IDbRepos db;
        public TariffService(IDbRepos db)
        {
            this.db = db;
        }
        public bool CheckTariff(int bkId, int usId)
        {
            var gnId = db.books.GetItem(bkId).genreid;

            if (db.userTariff.FindTariff(usId, gnId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SetTariff(int usId, int trId)
        {
            db.userTariff.SetUserTariff(usId, trId);
        }
        public List<TariffPreview> GetTariffPreviews(int usId)
        {
            var allTariffs = db.tariff.GetAll().ToList();

            var userTariffs = db.userTariff
                .GetAll()
                .Where(ut => ut.user_id == usId)
                .ToList();

            var tariffPreviews = allTariffs.Select(tariff =>
            {
                var userTariff = userTariffs.FirstOrDefault(ut => ut.tariff_id == tariff.id);

                return new TariffPreview
                {
                    id = tariff.id,
                    name = tariff.tariffName,
                    price = tariff.price,
                    isActive = userTariff != null && userTariff.endDate > DateTime.Now,
                    endDate = userTariff != null ? (DateTime?)userTariff.endDate : null
                };
            }).ToList();

            return tariffPreviews;
        }
    }
}
