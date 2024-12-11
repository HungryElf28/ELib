using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repository;
using Interfaces.Services;
using DTO;
using System.Windows.Input;

namespace BLL.Services
{
    public class ReadingService : IReadingService
    {
        private IDbRepos db;
        public ReadingDto CurrentReading { get; set; }
        public ReadingService(IDbRepos db)
        {
            this.db = db;
        }
        public void SetCurrentReading(int usId, int bkId)
        {
            CurrentReading = new ReadingDto(db.reading.GetReadingStatus(usId, bkId));
        }
        public void SaveCurrentPage(int page)
        {
            CurrentReading.current_page = page;

            var readingBook = db.reading.GetReadingStatus(CurrentReading.user_id, CurrentReading.book_id);
            readingBook.current_page = page;
            db.Save();
        }

    }
}
