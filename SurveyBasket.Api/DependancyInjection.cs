﻿using FluentValidation.AspNetCore;
using MapsterMapper;
using SurveyBasket.Api.Persistence;
using System.Reflection;

namespace SurveyBasket.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllers();
			var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("connectionString 'DefaultConnection' not found.");
			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));

			services
				.AddSwaggerServices()
                .AddFluentValidationsConfig()
                .AddMapster();
            services.AddScoped<IPollService, PollService>();
			return services;
        }
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        public static IServiceCollection AddMapsterConfigurations(this IServiceCollection services)
        {
                // Add Mapster
                var mappingConfig = TypeAdapterConfig.GlobalSettings;
                mappingConfig.Scan(Assembly.GetExecutingAssembly());
                services.AddSingleton<IMapper>(new Mapper(mappingConfig));
            return services;
        }
        public static IServiceCollection AddFluentValidationsConfig(this IServiceCollection services)
        {
            // Fluent Validations
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}