using AutoMapper;
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
    public class ReviewController
    {
        private readonly ILogger _logger;
        private IReviewService ReviewService { get; }
        private IMapper Mapper { get; }

        public ReviewController(ILoggerFactory loggerFactory, IReviewService reviewService, IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<ReviewController>();
            ReviewService = reviewService;
            Mapper = mapper;
        }

        [Function("CreateReview")]
        [OpenApiOperation(operationId: "CreateReview", tags: new[] { "review" }, Summary = "Create a review")]
        [OpenApiRequestBody("application/json", typeof(ReviewDTO), Required = true)]
        public async Task<HttpResponseData> CreateUser([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "review")] HttpRequestData req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                ReviewDTO reviewCreateDTO = JsonConvert.DeserializeObject<ReviewDTO>(requestBody);

                Review createdReview = Mapper.Map<Review>(reviewCreateDTO);

                createdReview = ReviewService.CreateReview(createdReview);

                var response = req.CreateResponse();
                await response.WriteAsJsonAsync(reviewCreateDTO);
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


        [Function("UpdateReview")]
        [OpenApiOperation(operationId: "UpdateReview", tags: new[] { "review" }, Summary = "Update a review")]
        [OpenApiParameter(name: "reviewId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The review id")]
        [OpenApiRequestBody("application/json", typeof(UserDTO), Required = true)]
        public async Task<HttpResponseData> UpdateReview([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "review/{reviewId}")] HttpRequestData req, int reviewId)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                ReviewDTO reviewUpdateDTO = JsonConvert.DeserializeObject<ReviewDTO>(requestBody);

                Review reviewToUpdate = ReviewService.GetReview(reviewId);
                reviewToUpdate.ProductId = reviewUpdateDTO.ProductId;
                reviewToUpdate.UserId = reviewUpdateDTO.UserId;
                reviewToUpdate.ReviewText = reviewUpdateDTO.reviewText;
                reviewToUpdate = ReviewService.UpdateReview(reviewToUpdate);

                var response = req.CreateResponse();
                await response.WriteAsJsonAsync(reviewUpdateDTO);
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

        [Function("GetReviewById")]
        [OpenApiOperation(operationId: "GetReviewById", tags: new[] { "review" }, Summary = "Get a review by ID")]
        [OpenApiParameter(name: "reviewId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The revies id")]
        public async Task<HttpResponseData> GetReviewById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "review/{reviewId}")] HttpRequestData req, int reviewId)
        {

            try
            {
                var response = req.CreateResponse();
                Review review = ReviewService.GetReview(reviewId);
                if (review == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                ReviewDTO responseReview = Mapper.Map<ReviewDTO>(review);
                await response.WriteAsJsonAsync(responseReview);
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



        [Function("GetAllReviews")]
        [OpenApiOperation(operationId: "GetAllReviews", tags: new[] { "review" }, Summary = "Get all Reviews")]
        public async Task<HttpResponseData> GetAllReviews([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "review")] HttpRequestData req)
        {

            try
            {
                var response = req.CreateResponse();
                List<Review> reviews = ReviewService.GetAllReviews();
                List<ReviewDTO> reviewResponse = new List<ReviewDTO>();

                foreach (Review r in reviews)
                {
                    ReviewDTO responseUser = Mapper.Map<ReviewDTO>(r);
                    reviewResponse.Add(responseUser);
                }
                await response.WriteAsJsonAsync(reviewResponse);
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




        [Function("DeleteReview")]
        [OpenApiOperation(operationId: "DeleteReview", tags: new[] { "review" }, Summary = "Delete a review")]
        [OpenApiParameter(name: "reviewId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The review id")]
        public async Task<HttpResponseData> DeleteReview([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "review/{reviewId}")] HttpRequestData req, int reviewId)
        {

            try
            {
                var response = req.CreateResponse();
                Review review = ReviewService.GetReview(reviewId);
                if (review == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                ReviewService.DeleteReview(review);
                await response.WriteAsJsonAsync("User deleted succesfully");
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
