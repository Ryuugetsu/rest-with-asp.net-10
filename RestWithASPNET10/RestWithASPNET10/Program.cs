using Core;
using Core.Interfaces.Repositories;
using Infrastructure;
using Infrastructure.JsonSerializer;
using Infrastructure.Repositories;
using RestWithASPNET10.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSerilogLogging();

builder.Services.AddControllers()
                .AddJsonOptions(option =>
                {
                    option.JsonSerializerOptions.Converters.Add(new DateSerializer());
                })
                .AddContentNegotiation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenAPIConfig();
builder.Services.AddSwaggerConfig();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddEvolveMigration(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});

builder.Services.AddCorsConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseCorsConfiguration(builder.Configuration);

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerSpecification();
app.AddScalarSpecification();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://*:{port}");
