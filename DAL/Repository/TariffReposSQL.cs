using Interfaces.Repository;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.Repository
{
    public class TariffReposSQL: IRepository<tariff>
    {
        private Elib db;

        public TariffReposSQL(Elib dbcontext)
        {
            this.db = dbcontext;
        }
        public IQueryable<tariff> GetAll()
        {
            return db.tariff;
        }
        public tariff GetItem(int id)
        {
            return db.tariff.Find(id);
        }

        public void Create(tariff tr)
        {
            db.tariff.Add(tr);
        }

        public void Update(tariff tr)
        {
            db.Entry(tr).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            tariff tr = db.tariff.Find(id);
            if (tr != null)
                db.tariff.Remove(tr);
        }
    }
}
