using AutoMapper;
using Domain.DTO;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.Domain.DTO;
using WidgetAndCo.Domain;
using WidgetAndCo.Service.Interface;

namespace WidgetAndCo
{
    public class OrderController
    {
        private readonly ILogger _logger;
        private IOrderService OrderService { get; }
        private IMapper Mapper { get; }

        public OrderController(ILoggerFactory loggerFactory, IMapper mapper, IOrderService orderService)
        {
            _logger = loggerFactory.CreateLogger<OrderController>();
            Mapper = mapper;
            OrderService = orderService;
        }

        [Function("CreateOrder")]
        [OpenApiOperation(operationId: "CreateOrder", tags: new[] { "order" }, Summary = "Create an Order")]
        [OpenApiRequestBody("application/json", typeof(OrderDTO), Required = true)]
        public async Task<HttpResponseData> CreateOrder([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order")] HttpRequestData req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                OrderDTO orderCreateDTO = JsonConvert.DeserializeObject<OrderDTO>(requestBody);

                Order order = Mapper.Map<Order>(orderCreateDTO);
                order = OrderService.CreateOrder(order);
                var response = req.CreateResponse();
                await response.WriteAsJsonAsync(orderCreateDTO);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch
            {
                var response = req.CreateResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

        }


        [Function("GetAllOrders")]
        [OpenApiOperation(operationId: "GetAllOrders", tags: new[] { "order" }, Summary = "Get all Orders")]
        public async Task<HttpResponseData> GetAllOrders([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "order")] HttpRequestData req)
        {

            try
            {
                var response = req.CreateResponse();
                List<Order> orders = OrderService.GetAllOrders();
                List<OrderDTO> orderResponse = new List<OrderDTO>();

                foreach (Order o in orders)
                {
                    OrderDTO responseProduct = Mapper.Map<OrderDTO>(o);
                    orderResponse.Add(responseProduct);
                }
                await response.WriteAsJsonAsync(orderResponse);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch
            {
                var response = req.CreateResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

        }


        [Function("GetOrderById")]
        [OpenApiOperation(operationId: "GetOrderById", tags: new[] { "order" }, Summary = "Get an order by ID")]
        [OpenApiParameter(name: "orderId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The order id")]
        public async Task<HttpResponseData> GetProductById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "order/{orderId}")] HttpRequestData req, int orderId)
        {

            try
            {
                var response = req.CreateResponse();
                Order order = OrderService.GetOrder(orderId);

                if (order == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                OrderDTO responseProduct = Mapper.Map<OrderDTO>(order);
                await response.WriteAsJsonAsync(responseProduct);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch
            {
                var response = req.CreateResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

        }



        [Function("UpdateOrder")]
        [OpenApiOperation(operationId: "UpdateOrder", tags: new[] { "order" }, Summary = "Update an order")]
        [OpenApiParameter(name: "orderId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The order id")]
        [OpenApiRequestBody("application/json", typeof(ProductDTO), Required = true)]
        public async Task<HttpResponseData> UpdateProduct([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "order/{orderId}")] HttpRequestData req, int orderId)
        {
            try
            {
                var response = req.CreateResponse();
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();


                OrderDTO orderUpdateDTO = JsonConvert.DeserializeObject<OrderDTO>(requestBody);

                Order orderToUpdate = OrderService.GetOrder(orderId);
                if (orderToUpdate == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                orderToUpdate.ProductId = orderUpdateDTO.ProductId;
                orderToUpdate.UserId = orderUpdateDTO.UserId;
                orderToUpdate.ShippingAddress = orderUpdateDTO.ShippingAddress;
                orderToUpdate.OrderDate = orderUpdateDTO.OrderDate;
                orderToUpdate.ShippingDate = orderUpdateDTO.ShippingDate;

                orderToUpdate = OrderService.UpdateOrder(orderToUpdate);

                await response.WriteAsJsonAsync(orderUpdateDTO);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch
            {
                var response = req.CreateResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

        }

        [Function("DeleteOrder")]
        [OpenApiOperation(operationId: "DeleteOrder", tags: new[] { "order" }, Summary = "Delete an Order")]
        [OpenApiParameter(name: "orderId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The order id")]
        public async Task<HttpResponseData> DeleteProduct([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "order/{orderId}")] HttpRequestData req, int orderId)
        {

            try
            {
                var response = req.CreateResponse();
                Order order = OrderService.GetOrder(orderId);
                if (order == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                OrderService.DeleteOrder(order);
                await response.WriteAsJsonAsync("Product deleted succesfully");
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch
            {
                var response = req.CreateResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

        }

        //Shipping only happens at 9 in the morning, 12 midday and at 16 before closing (only in workdays mon/fri)
        [Function("UpdateShippingDate")]
        public void Run([TimerTrigger("0 0 9,12,16 * * 1-5")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            List<Order> allOrdersWithoutShippingDate = OrderService.GetAllOrders();
            foreach (Order o in allOrdersWithoutShippingDate)
            {
                o.ShippingDate = DateTime.Now;
                OrderService.UpdateOrder(o);
            }
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
