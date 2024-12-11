using DTO;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ReviewReposSQL : IReviewsRepository
    {
        private ELibrary db;

        public ReviewReposSQL(ELibrary dbcontext)
        {
            this.db = dbcontext;
        }
        public List<ReviewDto> GetBookReviews(int bkId)
        {
            return db.review.
                Where(r => r.bookId == bkId).
                Select(r => new ReviewDto
                {
                    id = r.id,
                    mark = r.mark,
                    reviewText = r.reviewText,
                    userId = r.userId,
                    userLogin = r.users.login,
                    bookId = r.bookId
                }).ToList();
        }
    }
}
