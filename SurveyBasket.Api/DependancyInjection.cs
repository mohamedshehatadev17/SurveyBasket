using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SurveyBasket.Api.Authentication;
using SurveyBasket.Api.Persistence;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace SurveyBasket.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllers();
            services.AddAuthConfig(configuration);



            _ = services.AddCors(Options =>
                Options.AddDefaultPolicy(builder =>
                    builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                //.WithOrigins(configuration.GetSection("AllowOrigins").Get<string[]>()!) // appsetting.dev
                )
            );

			var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("connectionString 'DefaultConnection' not found.");
			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));

			services
				.AddSwaggerServices()
                .AddFluentValidationsConfig()
                .AddMapster();
            services.AddScoped<IPollService, PollService>();
            services.AddScoped<IAuthService, AuthService>();
			return services;
        }
		private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
		private static IServiceCollection AddMapsterConfigurations(this IServiceCollection services)
        {
                // Add Mapster
                var mappingConfig = TypeAdapterConfig.GlobalSettings;
                mappingConfig.Scan(Assembly.GetExecutingAssembly());
                services.AddSingleton<IMapper>(new Mapper(mappingConfig));
            return services;
        }
        private static IServiceCollection AddFluentValidationsConfig(this IServiceCollection services)
        {
            // Fluent Validations
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
		private static IServiceCollection AddAuthConfig(this IServiceCollection services,
            IConfiguration configuration)
		{
            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>();
            // Fluent Validations
            
            services.AddSingleton<IJwtProvider,JwtProvider>();
            
            //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();
            var JwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
                .AddJwtBearer(opts =>
                {
                    opts.SaveToken = true;
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings?.Key!)),
                        ValidIssuer = JwtSettings?.Issuer,
                        ValidAudience = JwtSettings?.Audience
                    };
                });
         
			return services;
		}
	}
}
