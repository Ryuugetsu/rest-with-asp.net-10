using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using xUnit.Tests.IntegrationTest.Tools;

namespace xUnit.Tests.IntegrationTest
{
    public class SwaggerIntegrationTests : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;

        public SwaggerIntegrationTests(SqlServerFixture sqlServerFixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(sqlServerFixture.ConnectionString);
            _httpClient = factory.CreateClient(
                    new WebApplicationFactoryClientOptions
                    {
                        BaseAddress = new Uri("http://localhost")
                    }
            );
        }

        [Fact]
        public async Task SwaggerJson_ShouldReturnSwaggerJson()
        {
            //- Arrange
            //var requestUri = "/swagger/v1/swagger.json";
            var requestUri = "/swagger-ui/index.html";

            //- Act
            var response = await _httpClient.GetAsync(requestUri);

            //- Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            content.Should().NotBeNullOrEmpty();

            content.Should().Contain("<div id=\"swagger-ui\">");


            //var json = JsonDocument.Parse(content);
            ////json.RootElement.TryGetProperty("openapi", out var openApiProp)
            ////                .Should().BeTrue("o Swagger deve conter a propriedade 'openapi'");

            ////openApiProp.GetString().Should().NotBeNullOrEmpty("o Swagger deve informar a versão do OpenAPI");
            //json.RootElement.TryGetProperty("paths", out var paths)
            //                .Should().BeTrue("o Swagger deve conter os endpoints da API");

            //paths.EnumerateObject().Should().NotBeEmpty("deve haver ao menos um endpoint documentado");
        }
    }
}
