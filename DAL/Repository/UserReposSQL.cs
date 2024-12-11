using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repository;
using DomainModel;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
namespace DAL.Repository
{
    public class UserReposSQL: IRepository<users>
    {
        private ELibrary db;

        public UserReposSQL(ELibrary dbcontext)
        {
            this.db = dbcontext;
        }
        public IQueryable<users> GetAll()
        {
            return db.users;
        }
        public users GetItem(int id)
        {
            return db.users.Find(id);
        }

        public void Create(users us)
        {
            db.users.Add(us);
        }

        public void Update(users us)
        {
            db.Entry(us).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            users us = db.users.Find(id);
            if (us != null)
                db.users.Remove(us);
        }
    }
}
