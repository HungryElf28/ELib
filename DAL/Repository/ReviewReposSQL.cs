using DomainModel;
using DTO;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ReviewReposSQL : IReviewsRepository
    {
        private Elib db;

        public ReviewReposSQL(Elib dbcontext)
        {
            this.db = dbcontext;
        }
        public void CreateReview(review rev)
        {
            db.review.Add(rev);
        }
        public void UpdateReview(review review)
        {
            db.Entry(review).State = EntityState.Modified;
        }
        public review GetReview(int usId, int bkId)
        {
            return db.review.Find(usId, bkId);
        }
        public List<ReviewDto> GetBookReviews(int bkId)
        {
            return db.review.
                Where(r => r.book_id == bkId).
                Select(r => new ReviewDto
                {
                    mark = r.mark,
                    reviewText = r.reviewText,
                    user_id = r.user_id,
                    userLogin = r.users.login,
                    book_id = r.book_id
                }).ToList();
        }
        public void Delete(int usId, int bkId)
        {
            review rv = db.review.Find(usId, bkId);
            if (rv != null)
                db.review.Remove(rv);
        }
    }
}
