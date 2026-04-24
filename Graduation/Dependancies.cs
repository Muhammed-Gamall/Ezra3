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
            services.AddAuth(configuration);
            services.AddScoped<IAuthService, AuthService>();



            services.AddScoped<IPlantService, PlantService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IFarmerService, FarmerService>();
            //services.AddScoped<IUserService, UserService>();
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

        private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJwtProvider, JwtProvider>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

            //OPTION PATTERN
            services.Configure<OptionPattern>(configuration.GetSection(OptionPattern.SectionName));
            var jwtSettings = configuration.GetSection(OptionPattern.SectionName).Get<OptionPattern>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings!.issuer,
                        ValidAudience = jwtSettings.audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.key)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.Configure<IdentityOptions>(options =>
            {
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            });


            return services;
        }
    }
}
