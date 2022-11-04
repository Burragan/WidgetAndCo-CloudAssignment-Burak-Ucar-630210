using WidgetAndCo.DAL;
using WidgetAndCo.Domain;
using WidgetAndCo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WidgetAndCo.Repository.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private WidgetAndCoContext Context;
        public ReviewRepository(WidgetAndCoContext context)
        {
            Context = context;
        }
        public Review Add(Review entity)
        {
            Context.Review.Add(entity);
            Commit();
            return entity;
        }

        public IEnumerable<Review> AllIncluding(params Expression<Func<Review, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(Review entity)
        {
            Context.Remove(entity);
            Commit();
        }

        public void DeleteWhere(Expression<Func<Review, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> FindBy(Func<Review, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetAll()
        {
            throw new NotImplementedException();
        }

        public Review GetSingle(int id)
        {
            Review review = Context.Review.Where(r => r.Id == id).FirstOrDefault();
            return review;
        }

        public Review GetSingle(Expression<Func<Review, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Review GetSingle(Expression<Func<Review, bool>> predicate, params Expression<Func<Review, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Review Update(Review entity)
        {
            Context.Review.Update(entity);
            Commit();
            return entity;
        }
        public List<Review> GetAllReviews()
        {
            List<Review> allReviews = new List<Review>();
            allReviews = Context.Set<Review>().ToList();
            return allReviews;
        }
    }
}
