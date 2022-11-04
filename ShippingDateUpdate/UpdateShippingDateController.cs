using System;
using AutoMapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WidgetAndCo.Domain;
using WidgetAndCo.Service.Interface;

namespace WidgetAndCo.ShippingDateUpdate
{
    public class UpdateShippingDateController
    {
        private readonly ILogger _logger;
        private IOrderService OrderService { get; }
        private IMapper Mapper { get; }

        public UpdateShippingDateController(ILoggerFactory loggerFactory, IMapper mapper, IOrderService orderService)
        {
            _logger = loggerFactory.CreateLogger<UpdateShippingDateController>();
            Mapper = mapper;
            OrderService = orderService;
        }

        [Function("UpdateShippingDate")]
        public void Run([TimerTrigger("0 */1 * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            List<Order> allOrdersWithoutShippingDate = OrderService.GetAllOrders();
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
