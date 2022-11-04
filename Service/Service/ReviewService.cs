using WidgetAndCo.Repository.Interface;
using WidgetAndCo.Repository.Repository;
using WidgetAndCo.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.Domain;

namespace WidgetAndCo.Service.Service
{
    public class ReviewService : IReviewService
    {
        private IReviewRepository ReviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            ReviewRepository = reviewRepository;
        }

        public Review CreateReview(Review review)
        {
            return ReviewRepository.Add(review);
        }

        public void DeleteReview(Review review)
        {
            ReviewRepository.Delete(review);
        }

        public List<Review> GetAllReviews()
        {
            return ReviewRepository.GetAllReviews();
        }

        public Review GetReview(int id)
        {
            return ReviewRepository.GetSingle(id);
        }

        public Review UpdateReview(Review review)
        {
            return ReviewRepository.Update(review);
        }
    }
}
