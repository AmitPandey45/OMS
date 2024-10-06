using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OMS.Common.Api.Filters;
using OMS.Common.Api.Middlewares;
using OMS.Common.Api.ResponseFactories;
using OMS.Domain.User.Mapping;
using User.Api.Extensions;

namespace User.Api.AppStartup
{
    public static class Startup
    {
        public static (WebApplicationBuilder builder, WebApplication app) StartApp(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            AutoMapperConfig.Configure();
            IServiceCollection services = ConfigureServices(builder, args);
            WebApplication app = ConfigureMiddleware(builder, args);

            return (builder, app);
        }

        public static IServiceCollection ConfigureServices(WebApplicationBuilder builder, string[] args)
        {
            IServiceCollection services = builder.Services;

            services.RegisterServices();
            services.RegisterRepositories();

            //// Approach 1

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});
            //services.AddControllers();

            //// OR

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            //// Approach 2

            //services.AddControllers(options =>
            //{
            //    // Add custom validation response filter
            //    options.Filters.Add<CustomValidationResponseFilter>();
            //})
            //.ConfigureApiBehaviorOptions(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});

            //// Approach 3

            //services.AddControllers()
            //    .ConfigureApiBehaviorOptions(options =>
            //    {
            //        options.InvalidModelStateResponseFactory = context =>
            //        {
            //            options.SuppressModelStateInvalidFilter = true; // with / without works

            //            // Resolve the logger from the service provider
            //            var logger = context.HttpContext.RequestServices.GetService<ILogger<ModelStateResponseFactory>>();

            //            // Pass the entire context and logger to the response factory
            //            return ModelStateResponseFactory.CreateResponse(context, logger);
            //        };
            //    });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.DocumentFilter<LowercasePathsFilter>(); // Register the lowercase path filter
            });

            return services;
        }

        public static WebApplication ConfigureMiddleware(WebApplicationBuilder builder, string[] args)
        {
            WebApplication app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseMiddleware<CustomValidationMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<LowercaseRoutingMiddleware>();

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

            return app;
        }
    }
}
