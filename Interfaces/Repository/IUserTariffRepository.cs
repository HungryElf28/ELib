using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repository
{
    public interface IUserTariffRepository
    {
        IQueryable<user_tariff> GetAll();
        void RemoveUserTariff(int usId, int trId);
        bool FindTariff(int usId, int gnId);
        void SetUserTariff(int usId, int trId);
        void RemoveExpiredTariffs(int usId);
    }
}
