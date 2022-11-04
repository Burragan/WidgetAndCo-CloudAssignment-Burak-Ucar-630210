using WidgetAndCo.DAL;
using WidgetAndCo.Domain;
using WidgetAndCo.Domain.DTO;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using WidgetAndCo.Repository.Interface;
using WidgetAndCo.Repository.Repository;
using WidgetAndCo.Service.Interface;
using WidgetAndCo.Service.Service;
using Service.Service;
using Domain;
using Domain.DTO;

string? connectionString = Environment.GetEnvironmentVariable("connectionString");
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.Configure<JsonSerializerSettings>(options =>
        {
            options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
        services.AddDbContext<WidgetAndCoContext>(
            options =>
            {
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            });
        services.AddAutoMapper(cfg =>
        {
            cfg.CreateMap<User, UserDTO>().ReverseMap();
            cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            cfg.CreateMap<Order, OrderDTO>().ReverseMap();
            cfg.CreateMap<Review, ReviewDTO>().ReverseMap();
            cfg.CreateMap<ProductImage, ProductImageDTO>().ReverseMap();
        });
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IProductService, ProductService>();
        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<IReviewService, ReviewService>();
        services.AddSingleton<IProductImageService, ProductImageService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();

    })
    .ConfigureOpenApi()
    .Build();

host.Run();