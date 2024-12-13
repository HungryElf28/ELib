using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Interfaces.Repository;

namespace BLL.Services
{
    public class ReviewService
    {
        private IDbRepos db;
        public ReviewService (IDbRepos db)
        {
            this.db = db;
        }
        public ReviewDto GetReview(int usId, int bkId)
        {
            var review = db.bookReviews.GetReview(usId, bkId);
            if (review != null)
            {
                return new ReviewDto(review);
            }
            else
            {
                return new ReviewDto()
                {
                    user_id = usId,
                    book_id = bkId,
                    mark = null,
                    reviewText = null,
                    userLogin = db.users.GetItem(usId).login
                };
            }
        }
    }
}
