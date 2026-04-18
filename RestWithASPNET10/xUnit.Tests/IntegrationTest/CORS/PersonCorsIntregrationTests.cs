using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RestWithASPNET10.Data;
using System.Net;
using System.Net.Http.Json;
using xUnit.Tests.IntegrationTest.Tools;

namespace xUnit.Tests.IntegrationTest.CORS
{
    [TestCaseOrderer("xUnit.Tests.IntegrationTest.Tools.PriorityOrderer", "xUnit.Tests")]
    [Collection("Sequential Tests")]
    public class PersonCorsIntregrationTests : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;
        private static PersonDTO _person;

        public PersonCorsIntregrationTests(SqlServerFixture sqlServerFixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(sqlServerFixture.ConnectionString);
            _httpClient = factory.CreateClient(
                    new WebApplicationFactoryClientOptions
                    {
                        BaseAddress = new Uri("http://localhost")
                    }
            );
        }

        private void AddOriginHeader(string origin)
        {
            _httpClient.DefaultRequestHeaders.Remove("Origin");
            _httpClient.DefaultRequestHeaders.Add("Origin", origin);
        }

        [Fact(DisplayName = "01 - Create Person With Allowed Origin")]
        [TestPriority(1)]
        public async Task CreatePerson_WithAllowedOrigin_ShouldSucceed()
        {
            // Arrange
            AddOriginHeader("http://localhost:3000");
            PersonDTO person = new PersonDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Address = "address",
                Gender = "Male"
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/person", person);

            // Assert
            response.EnsureSuccessStatusCode();
            PersonDTO? created = await response.Content.ReadFromJsonAsync<PersonDTO>();

            created.Should().NotBeNull();
            created!.Id.Should().BeGreaterThan(0);

            _person = created;
        }

        [Fact(DisplayName = "02 - Create Person With Disallowed Origin")]
        [TestPriority(2)]
        public async Task CreatePerson_WithDisallowedOrigin_ShouldForbiden()
        {
            // Arrange
            AddOriginHeader("http://localhost:3003");
            PersonDTO person = new PersonDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Address = "address",
                Gender = "Male"
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/person", person);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            string? content = await response.Content.ReadAsStringAsync();

        }

        [Fact(DisplayName = "03 - Get Person By ID With Allowed Origin")]
        [TestPriority(3)]
        public async Task GetPerson_ById_WithAllowedOrigin_ShouldSucceed()
        {
            // Arrange
            AddOriginHeader("http://localhost:3000");

            // Act
            var response = await _httpClient.GetAsync($"/api/person/{_person.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            PersonDTO? retrieved = await response.Content.ReadFromJsonAsync<PersonDTO>();

            retrieved.Should().NotBeNull();
            retrieved!.Id.Should().Be(_person.Id);
            retrieved.FirstName.Should().Be(_person.FirstName);
            retrieved.LastName.Should().Be(_person.LastName);
            retrieved.Address.Should().Be(_person.Address);
            retrieved.Gender.Should().Be(_person.Gender);
        }

        [Fact(DisplayName = "04 - Get Person By ID With Disallowed Origin")]
        [TestPriority(4)]
        public async Task GetPerson_ById_WithDisallowedOrigin_ShouldForbiden()
        {
            // Arrange
            AddOriginHeader("http://localhost:3003");

            // Act
            var response = await _httpClient.GetAsync($"/api/person/{_person.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            string? content = await response.Content.ReadAsStringAsync();
        }
    }
}
