using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.Domain;

namespace WidgetAndCo.Service.Interface
{
    public interface IReviewService
    {
        Review CreateReview(Review review);
        Review GetReview(int id);
        Review UpdateReview(Review review);
        List<Review> GetAllReviews();
        void DeleteReview(Review review);
    }
}
