using WidgetAndCo.DAL;
using WidgetAndCo.Domain;
using WidgetAndCo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WidgetAndCo.Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private WidgetAndCoContext Context;
        public ProductRepository(WidgetAndCoContext context)
        {
            Context = context;
        }
        public Product Add(Product entity)
        {
            Context.Add(entity);
            Commit();
            return entity;
        }

        public IEnumerable<Product> AllIncluding(params Expression<Func<Product, object>>[] includeProperties)
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

        public void Delete(Product entity)
        {
            Context.Remove(entity);
            Commit();
        }

        public void DeleteWhere(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> FindBy(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> allProducts = new List<Product>();
            allProducts = Context.Set<Product>().Include(p => p.Images).ToList();
            return allProducts;
        }

        public Product GetSingle(int id)
        {
            Product product = Context.Product.Where(p => p.Id == id).Include(p => p.Images).FirstOrDefault();
            return product;
        }

        public Product GetSingle(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Product GetSingle(Expression<Func<Product, bool>> predicate, params Expression<Func<Product, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product entity)
        {
            {
                Context.Product.Update(entity);
                Commit();
                return entity;
            }
        }
    }
}
