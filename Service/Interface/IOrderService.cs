using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.Domain;

namespace WidgetAndCo.Service.Interface
{
    public interface IOrderService
    {
        Order CreateOrder(Order order);
        List<Order> GetAllOrders();
        List<Order> GetAllOrdersWithoutShippingDate();
        Order GetOrder(int id);
        Order UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
