using DomainModel;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IReviewsRepository
    {
        void CreateReview(review rev);
        void UpdateReview(review review);
        review GetReview(int usId, int bkId);
        List<ReviewDto> GetBookReviews(int bkId);
        void Delete(int usId, int bkId);
    }
}
