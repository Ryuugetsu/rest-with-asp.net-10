using Scalar.AspNetCore;

namespace RestWithASPNET10.Configurations
{
    public static class ScalarConfig
    {
        private static readonly string _apiName = "REST with ASP.NET 10";

        public static WebApplication AddScalarSpecification(this WebApplication app)
        {
            app.MapScalarApiReference("/scalar", options =>
            {
                options
                    .WithTitle($"{_apiName} Scalar API Reference")
                    .WithOpenApiRoutePattern("/swagger/v1/swagger.json");
                    //.WithOpenApiRoutePattern("/scalar/openapi.json");
            });
            return app;
        }
    }
}
