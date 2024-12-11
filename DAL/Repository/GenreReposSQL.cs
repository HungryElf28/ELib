using DomainModel;
using Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenreReposSQL: IRepository<genres>
    {
        private ELibrary db;
        public GenreReposSQL(ELibrary dbcontext)
        {
            this.db = dbcontext;
        }
        public IQueryable<genres> GetAll()
        {
            return db.genres;
        }
        public genres GetItem(int id)
        {
            return db.genres.Find(id);
        }

        public void Create(genres gnr)
        {
            db.genres.Add(gnr);
        }

        public void Update(genres gnr)
        {
            db.Entry(gnr).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            genres gnr = db.genres.Find(id);
            if (gnr != null)
                db.genres.Remove(gnr);
        }
    }
}
