using RestWithASPNET10.Configurations;
using Testcontainers.MsSql;

namespace xUnit.Tests.IntegrationTest.Tools
{
    public class SqlServerFixture : IAsyncLifetime
    {
        public MsSqlContainer Container { get; }

        //- propriedade para acessar a string de conexão do container SQL Server
        public string ConnectionString => Container.GetConnectionString();
      
        public SqlServerFixture()
        {
            //- criando o container image oficial do SQL Server, com a senha definida para "@Admin123"
            Container = new MsSqlBuilder().WithPassword("@Admin123").WithPortBinding(0, 1433).Build();
        }

        public async Task InitializeAsync()
        {
            //- iniciando o container SQL Server e executando as migrações usando a string de conexão do container
            await Container.StartAsync();
            EvolveConfig.ExecuteMigration(ConnectionString);
        }

        public async Task DisposeAsync()
        {
            //- parando e descartando o container SQL Server para liberar os recursos
            await Container.DisposeAsync();
        }
    }
}
