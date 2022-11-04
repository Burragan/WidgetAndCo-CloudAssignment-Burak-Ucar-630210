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

namespace WidgetAndCo.FunctionApp1
{
    public class UserController
    {
        private readonly ILogger _logger;
        private IUserService UserService { get; }
        private IMapper Mapper { get; }

        public UserController(ILoggerFactory loggerFactory, IUserService userService, IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<UserController>();
            UserService = userService;
            Mapper = mapper;
        }

        [Function("CreateUser")]
        [OpenApiOperation(operationId: "CreateUser", tags: new[] { "user" }, Summary = "Create a User")]
        [OpenApiRequestBody("application/json", typeof(UserDTO), Required = true)]
        public async Task<HttpResponseData> CreateUser([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")] HttpRequestData req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                UserDTO userCreateDTO = JsonConvert.DeserializeObject<UserDTO>(requestBody);

                User createdUser = Mapper.Map<User>(userCreateDTO);

                createdUser = UserService.CreateUser(createdUser);

                var response = req.CreateResponse();
                await response.WriteAsJsonAsync(userCreateDTO);
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


        [Function("UpdateUser")]
        [OpenApiOperation(operationId: "UpdateUser", tags: new[] { "user" }, Summary = "Update a User")]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The user id")]
        [OpenApiRequestBody("application/json", typeof(UserDTO), Required = true)]
        public async Task<HttpResponseData> UpdateUser([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "user/{userId}")] HttpRequestData req, int userId)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                UserDTO userCreateDTO = JsonConvert.DeserializeObject<UserDTO>(requestBody);

                User userToUpdate = UserService.GetUser(userId);
                userToUpdate.Email = userCreateDTO.Email;
                userToUpdate.FirstName = userCreateDTO.FirstName;
                userToUpdate.LastName = userCreateDTO.LastName;
                userToUpdate = UserService.UpdateUser(userToUpdate);

                var response = req.CreateResponse();
                await response.WriteAsJsonAsync(userCreateDTO);
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


        [Function("GetUserById")]
        [OpenApiOperation(operationId: "GetUserById", tags: new[] { "user" }, Summary = "Get a User by ID")]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The user id")]
        public async Task<HttpResponseData> GetUserById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{userId}")] HttpRequestData req, int userId)
        {

            try
            {
                var response = req.CreateResponse();
                User user = UserService.GetUser(userId);
                if (user == null)
                {                    
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                UserDTO responseUser = Mapper.Map<UserDTO>(user);
                await response.WriteAsJsonAsync(responseUser);
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

        [Function("GetAllUsers")]
        [OpenApiOperation(operationId: "GetAllUsers", tags: new[] { "user" }, Summary = "Get all Users")]
        public async Task<HttpResponseData> GetAllUsers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user")] HttpRequestData req)
        {

            try
            {
                var response = req.CreateResponse();
                List<User> users = UserService.GetAllUsers();
                List<UserDTO> userResponse = new List<UserDTO>();

                foreach (User u in users)
                {
                    UserDTO responseUser = Mapper.Map<UserDTO>(u);
                    userResponse.Add(responseUser);
                }
                await response.WriteAsJsonAsync(userResponse);
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

        [Function("DeleteUser")]
        [OpenApiOperation(operationId: "DeleteUser", tags: new[] { "user" }, Summary = "Delete an User")]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The user id")]
        public async Task<HttpResponseData> DeleteUser([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "user/{userId}")] HttpRequestData req, int userId)
        {

            try
            {
                var response = req.CreateResponse();
                User user = UserService.GetUser(userId);
                if (user == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                UserService.DeleteUser(user);
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
