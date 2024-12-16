using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Interfaces.Repository;
using DomainModel;
using Interfaces.Services;

namespace BLL.Services
{
    public class ReviewService : IReviewService
    {
        private IDbRepos db;
        public ReviewDto CurrentReview { get; set; }
        public ReviewService (IDbRepos db)
        {
            this.db = db;
        }
        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }
        public ReviewDto GetReview(int usId, int bkId)
        {
            var review = db.bookReviews.GetReview(usId, bkId);
            if (review != null)
            {
                CurrentReview = new ReviewDto(review);
                return CurrentReview;
            }
            else
            {
                CurrentReview = new ReviewDto()
                {
                    user_id = usId,
                    book_id = bkId,
                    mark = null,
                    reviewText = null,
                    userLogin = db.users.GetItem(usId).login
                };
                return CurrentReview;
            }
        }
        public void CreateOrUpdateReview(ReviewDto rev)
        {
            review rv = db.bookReviews.GetReview(rev.user_id, rev.book_id);
            if (rv != null)
            {
                rv.mark = rev.mark;
                rv.reviewText = rev.reviewText;
                Save();
            }
            else
            {
                review r = new review
                {
                    user_id = rev.user_id,
                    book_id = rev.book_id,
                    mark = rev.mark,
                    reviewText = rev.reviewText,
                    books = db.books.GetItem(rev.book_id),
                    users = db.users.GetItem(rev.user_id)
                };
                db.bookReviews.CreateReview(r);
                users us = db.users.GetItem(r.user_id);
                us.bonuses += 15;
                db.users.Update(us);
                Save();
            }
        }
        public void DeleteReview(int usId, int bkId)
        {
            review rv = db.bookReviews.GetReview(usId, bkId);
            if (rv != null)
            {
                rv.mark = null;
                rv.reviewText = null;
                db.bookReviews.UpdateReview(rv);
                //db.bookReviews.Delete(rv.user_id, rv.book_id);
                Save();
            }
        }
    }
}
