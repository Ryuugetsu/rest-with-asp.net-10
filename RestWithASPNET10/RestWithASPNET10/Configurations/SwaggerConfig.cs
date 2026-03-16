using Microsoft.OpenApi;

namespace RestWithASPNET10.Configurations
{
    public static class SwaggerConfig
    {
        private static readonly string _apiName = "REST with ASP.NET 10";
        private static readonly string _apiDescription = $"A simple API to learn {_apiName}.";

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = _apiName,
                    Version = "v1",
                    Description = _apiDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "Bruno Tavares",
                        Email = "bruno.200926@hotmail.com",
                        Url = new Uri("https://www.linkedin.com/in/bruno-tavares-marinho-de-morais-5ab3a2150"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });
                options.CustomSchemaIds(type => type.FullName);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerSpecification(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_apiName} v1");
                options.RoutePrefix = "swagger-ui";
                options.DocumentTitle = $"{_apiName} API Documentation";
            });
            return app;
        }
    }
}