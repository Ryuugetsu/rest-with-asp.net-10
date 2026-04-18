namespace RestWithASPNET10.Configurations
{
    public static class CorsConfig
    {
        public static string[] GetAllowedOrigins(IConfiguration configuration)
        {
            return configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();
        }

        public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string[] origins = GetAllowedOrigins(configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("LocalPolicy",
                                  policy => policy.WithOrigins("http://localhost:3000")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod()
                                                  .AllowCredentials());

                options.AddPolicy("MultipleLocalPolicy",
                                 policy => policy.WithOrigins("http://localhost:3000",
                                                              "http://localhost:8080")
                                                 .AllowAnyHeader()
                                                 .AllowAnyMethod()
                                                 .AllowCredentials());

                options.AddPolicy("DefaultPolicy",
                                policy => policy.WithOrigins(origins)
                                                .AllowAnyHeader()
                                                .AllowAnyMethod()
                                                .AllowCredentials());
            });
        }

        public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            string[] origins = GetAllowedOrigins(configuration);

            app.Use(async (context, next) =>
            {
                string origin = context.Request.Headers["Origin"].ToString();
                if (!string.IsNullOrEmpty(origin) && 
                    !origins.Contains(origin, StringComparer.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("CORS origin not allowed.");
                    return;
                }
                await next();
            });

            app.UseCors();
            return app;
        }
    }
}
