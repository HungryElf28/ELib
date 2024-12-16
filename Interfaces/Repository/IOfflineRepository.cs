using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repository
{
    public interface IOfflineRepository
    {
        List<BookPreviewDto> GetOfflineBooks(int usId);
        void DeleteExpiredBooks(int usId);
        bool IsOffline(int usId, int bkId);
        void DownloadBook(int usId, int bkId);
        void DeleteBook(int usId, int bkId);
    }
}
