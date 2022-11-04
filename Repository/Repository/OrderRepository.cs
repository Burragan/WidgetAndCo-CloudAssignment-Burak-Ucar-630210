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
    public class OrderRepository : IOrderRepository
    {
        private WidgetAndCoContext Context;
        public OrderRepository(WidgetAndCoContext context)
        {
            Context = context;
        }
        public Order Add(Order entity)
        {
            Context.Order.Add(entity);
            Commit();
            return entity;
        }

        public IEnumerable<Order> AllIncluding(params Expression<Func<Order, object>>[] includeProperties)
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

        public void Delete(Order entity)
        {
            Context.Remove(entity);
            Commit();
        }

        public void DeleteWhere(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> FindBy(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            List<Order> allOrders = new List<Order>();
            allOrders = Context.Set<Order>().ToList();
            return allOrders;
        }

        public List<Order> GetAllOrdersWithoutShippingDate()
        {
            List<Order> allOrders = new List<Order>();
            Context.Set<Order>().ToList();
            var ordersWithoutShippingDate = allOrders.Where(o => o.ShippingDate == null).ToList();
            return ordersWithoutShippingDate;
        }

        public Order GetSingle(int id)
        {
            Order order = Context.Order.Where(u => u.Id == id).FirstOrDefault();
            return order;
        }

        public Order GetSingle(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Order GetSingle(Expression<Func<Order, bool>> predicate, params Expression<Func<Order, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order entity)
        {
            Context.Order.Update(entity);
            Commit();
            return entity;
        }
    }
}
