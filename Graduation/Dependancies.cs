

namespace Graduation
{
    public static class Dependancies 
    {
        public static IServiceCollection AddDependancies (this IServiceCollection services , IConfiguration configuration)
        {
            services.AddControllers();
            services.AddOpenApi();
            services.AddCloudinary(configuration);
            services.AddDataBase(configuration);

            services.AddScoped<IPlantService, PlantService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IFarmerService, FarmerService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionstring = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionstring));
            return services;
        }
        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
  );
            var cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);
            return services;
        }
    }
}
