using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using xUnit.Tests.IntegrationTest.Tools;

namespace xUnit.Tests.IntegrationTest
{
    public class ScalarIntegrationTests : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;

        public ScalarIntegrationTests(SqlServerFixture sqlServerFixture)
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
        public async Task Scalar_ShouldReturnScalarUI()
        {
            //- Arrange
            var requestUri = "/scalar/";

            //- Act
            var response = await _httpClient.GetAsync(requestUri);

            //- Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            content.Should().NotBeNullOrEmpty();

            content.Should().Contain("script src");
        }
    }
}
