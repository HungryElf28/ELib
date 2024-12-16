using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IReviewService
    {
        ReviewDto CurrentReview { get; set; }
        ReviewDto GetReview(int usId, int bkId);
        void CreateOrUpdateReview(ReviewDto rev);
        void DeleteReview(int usId, int bkId);
    }
}
