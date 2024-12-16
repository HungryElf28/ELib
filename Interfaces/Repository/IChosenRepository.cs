using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repository
{
    public interface IChosenRepository
    {
        void Add(int usId, int bkId);
        void Delete(int usId, int bkId);
        bool Check(int usId, int bkId);
        List<BookPreviewDto> GetChosenBooks(int usId);
    }
}
