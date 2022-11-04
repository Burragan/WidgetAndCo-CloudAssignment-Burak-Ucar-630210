using WidgetAndCo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetAndCo.Repository.Interface
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public List<Order> GetAllOrders();
        public List<Order> GetAllOrdersWithoutShippingDate();
    }
}
