using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace xUnit.Tests.IntegrationTest.Tools
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly string _connectionString;

        public CustomWebApplicationFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
            //- Aqui estamos interceptando a configuração do WebHost
            builder.ConfigureAppConfiguration((context, config) =>
                {
                    var inMemorySettings = new Dictionary<string, string>
                    {
                        //- aqui estamos interceptando a configuração da string de conexão para usar a do container SQL Server
                        ["ConnectionStrings:DefaultConnection"] = _connectionString
                    };
                    
                    config.AddInMemoryCollection(inMemorySettings!);
                });
        }
    }
}
