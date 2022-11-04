using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.DAL;
using WidgetAndCo.Domain;
using WidgetAndCo.Repository.Interface;

namespace WidgetAndCo.Repository.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private WidgetAndCoContext Context;
        public ProductImageRepository(WidgetAndCoContext context)
        {
            Context = context;
        }

        public ProductImage Add(ProductImage entity)
        {
            Context.Add(entity);
            Commit();
            return entity;
        }

        public IEnumerable<ProductImage> AllIncluding(params Expression<Func<ProductImage, object>>[] includeProperties)
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

        public void Delete(ProductImage entity)
        {
            Context.Remove(entity);
            Commit();
        }

        public void DeleteWhere(Expression<Func<ProductImage, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductImage> FindBy(Func<ProductImage, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductImage> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ProductImage> getProductImageByProductId(int productId)
        {
            List<ProductImage> allProduct= new List<ProductImage>();
            Context.Set<ProductImage>().ToList();
            var result = allProduct.Where(p => p.ProductId == productId).ToList();
            return result;
        }

        public ProductImage GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public ProductImage GetSingle(Expression<Func<ProductImage, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ProductImage GetSingle(Expression<Func<ProductImage, bool>> predicate, params Expression<Func<ProductImage, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public ProductImage Update(ProductImage entity)
        {
            throw new NotImplementedException();
        }
    }
}
