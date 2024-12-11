using DomainModel;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IReadingService
    {
        ReadingDto CurrentReading { get; set; }
        void SetCurrentReading(int usId, int bkId);
        void SaveCurrentPage(int page);
    }
}
