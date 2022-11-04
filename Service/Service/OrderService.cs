using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.Domain;
using WidgetAndCo.Repository.Interface;
using WidgetAndCo.Service.Interface;

namespace WidgetAndCo.Service.Service
{
    public class OrderService : IOrderService
    {
        private IOrderRepository OrderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            OrderRepository = orderRepository;
        }

        public Order CreateOrder(Order order)
        {
            return OrderRepository.Add(order);
        }

        public void DeleteOrder(Order order)
        {
            OrderRepository.Delete(order);
        }

        public List<Order> GetAllOrders()
        {
            return OrderRepository.GetAllOrders();
        }

        public List<Order> GetAllOrdersWithoutShippingDate()
        {
            return OrderRepository.GetAllOrdersWithoutShippingDate();
        }

        public Order GetOrder(int id)
        {
            return OrderRepository.GetSingle(id);
        }

        public Order UpdateOrder(Order order)
        {
            return OrderRepository.Update(order);
        }
    }
}
