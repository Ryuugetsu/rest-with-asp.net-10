using Microsoft.OpenApi;

namespace RestWithASPNET10.Configurations
{
    public static class OpenAPIConfig
    {
        private static readonly string _apiName = "REST with ASP.NET 10";
        private static readonly string _apiDescription = $"A simple API to learn {_apiName}.";

        public static IServiceCollection AddOpenAPIConfig(this IServiceCollection services)
        {
            services.AddSingleton(new OpenApiInfo
            {
                Title = _apiName,
                Description = _apiDescription,
                Version = "v1",
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

            return services;
        }
    }
}
