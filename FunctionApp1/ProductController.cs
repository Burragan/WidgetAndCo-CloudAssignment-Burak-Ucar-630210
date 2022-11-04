using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using WidgetAndCo.Domain.DTO;
using WidgetAndCo.Service.Interface;
using WidgetAndCo.Domain;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Domain.DTO;
using Domain;
using WidgetAndCo.Service.Service;

namespace WidgetAndCo.FunctionApp1
{
    public class ProductController
    {
        private readonly ILogger _logger;
        private IProductService ProductService { get; }
        private IMapper Mapper { get; }

        public ProductController(ILoggerFactory loggerFactory, IProductService productService, IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<ProductController>();
            ProductService = productService;
            Mapper = mapper;
        }

        [Function("CreateProduct")]
        [OpenApiOperation(operationId: "CreateProduct", tags: new[] { "product" }, Summary = "Create a product")]
        [OpenApiRequestBody("application/json", typeof(ProductDTO), Required = true)]
        public async Task<HttpResponseData> CreateProduct([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")] HttpRequestData req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                ProductDTO productCreateDTO = JsonConvert.DeserializeObject<ProductDTO>(requestBody);
                List<ProductImage> productImages = new List<ProductImage>();
                foreach (ProductImageDTO i in productCreateDTO.Images)
                {
                    ProductImage productImage = Mapper.Map<ProductImage>(i);
                    productImages.Add(productImage);
                }
                Product product = Mapper.Map<Product>(productCreateDTO);
                product.Images = productImages;
                product = ProductService.CreateProduct(product);

                var response = req.CreateResponse();
                await response.WriteAsJsonAsync(productCreateDTO);
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

        [Function("GetAllProducts")]
        [OpenApiOperation(operationId: "GetAllProducts", tags: new[] { "product" }, Summary = "Get all Products")]
        public async Task<HttpResponseData> GetAllUsers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product")] HttpRequestData req)
        {

            try
            {
                var response = req.CreateResponse();
                List<Product> products = ProductService.GetAllProducts();
                List<ProductDTO> productResponse = new List<ProductDTO>();

                foreach (Product p in products)
                {
                    ProductDTO responseProduct = Mapper.Map<ProductDTO>(p);
                    productResponse.Add(responseProduct);
                }
                await response.WriteAsJsonAsync(productResponse);
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

        [Function("GetProductById")]
        [OpenApiOperation(operationId: "GetProductById", tags: new[] { "product" }, Summary = "Get a product by ID")]
        [OpenApiParameter(name: "productId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The product id")]
        public async Task<HttpResponseData> GetProductById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/{productId}")] HttpRequestData req, int productId)
        {

            try
            {
                var response = req.CreateResponse();
                Product product = ProductService.GetProduct(productId);
                if (product == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                ProductDTO responseProduct = Mapper.Map<ProductDTO>(product);
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

        [Function("UpdateProduct")]
        [OpenApiOperation(operationId: "UpdateProduct", tags: new[] { "product" }, Summary = "Update a Product")]
        [OpenApiParameter(name: "productId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The Product id")]
        [OpenApiRequestBody("application/json", typeof(ProductDTO), Required = true)]
        public async Task<HttpResponseData> UpdateProduct([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "product/{productId}")] HttpRequestData req, int productId)
        {
            try
            {
                var response = req.CreateResponse();
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                ProductDTO ProductUpdateDTO = JsonConvert.DeserializeObject<ProductDTO>(requestBody);
                Product productToUpdate = ProductService.GetProduct(productId);
                if (productToUpdate == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                productToUpdate.Name = ProductUpdateDTO.Name;
                productToUpdate.Price = ProductUpdateDTO.Price;
                productToUpdate.Specification = ProductUpdateDTO.Specification;

                List<ProductImage> productImages = new List<ProductImage>();
                foreach (ProductImageDTO i in ProductUpdateDTO.Images)
                {
                    ProductImage productImage = Mapper.Map<ProductImage>(i);
                    productImages.Add(productImage);
                }
                productToUpdate.Images = productImages;
                productToUpdate = ProductService.UpdateProduct(productToUpdate);



                await response.WriteAsJsonAsync(ProductUpdateDTO);
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

        [Function("DeleteProduct")]
        [OpenApiOperation(operationId: "DeleteProduct", tags: new[] { "product" }, Summary = "Delete a Product")]
        [OpenApiParameter(name: "productId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The product id")]
        public async Task<HttpResponseData> DeleteProduct([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "product/{productId}")] HttpRequestData req, int productId)
        {

            try
            {
                var response = req.CreateResponse();
                Product product = ProductService.GetProduct(productId);
                if (product == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                ProductService.DeleteProduct(product);
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





    }
}
