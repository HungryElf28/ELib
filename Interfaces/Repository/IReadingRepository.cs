using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repository
{
    public interface IReadingRepository
    {
        reading_book GetReadingStatus(int usId, int bkId);
        IQueryable<reading_book> GetAll();
    }
}
