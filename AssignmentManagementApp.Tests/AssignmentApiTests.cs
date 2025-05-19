using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using System.Net;
using AssignmentManagement.Core;
using AssignmentManagement.API;
using System.Text.Json;
using System.Text;
using AssignmentManagement.Core.Models;

namespace AssignmentManagement.Tests
{
    public class AssignmentAPITests : IClassFixture<WebApplicationFactory<AssignmentManagement.API.Program>>
    {
        private readonly HttpClient _client;

        public AssignmentAPITests(WebApplicationFactory<AssignmentManagement.API.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_ShouldGetAllAssignments()
        {
            string assignmentTitle = "Software Engineering";
            string assignmentDescription = "Make integration tests";

            var assignmentJson = new StringContent(JsonSerializer.Serialize(new Assignment(
                    assignmentTitle,
                    assignmentDescription)
                ), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Assignment", assignmentJson);
            response.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync("/api/Assignment");
            getResponse.EnsureSuccessStatusCode();

            var assignment = await getResponse.Content.ReadAsStringAsync();

            Assert.Contains(assignmentTitle, assignment);
            Assert.Contains(assignmentDescription, assignment);
        }

        [Fact]
        public async Task Create_ShouldCreateAssignment()
        {
            string assignmentTitle = "Software Engineering";
            string assignmentDescription = "Make integration tests";

            var assignmentJson = new StringContent(JsonSerializer.Serialize(new Assignment(
                    assignmentTitle,
                    assignmentDescription)
            ), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Assignment", assignmentJson);
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task Delete_ShouldDeleteAssignment()
        {
            string assignmentTitle = "Software Engineering";
            string assignmentDescription = "Make integration tests";

            var assignmentJson = new StringContent(JsonSerializer.Serialize(new Assignment(
                    assignmentTitle,
                    assignmentDescription)
            ), Encoding.UTF8, "application/json");


            var response = await _client.PostAsync("/api/Assignment", assignmentJson);
            response.EnsureSuccessStatusCode();

            var deleteResponse = await _client.DeleteAsync($"/api/Assignment/{assignmentTitle}");
            deleteResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync($"/api/Assignment/{assignmentTitle}");

            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);

        }

    }
}
