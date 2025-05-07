using System.Net;
using System.Text;
using System.Text.Json;

namespace Lab7AutomatedSolution.Test;

public class ReqResIntegrationTests
{
    readonly HttpClient _client;

    public ReqResIntegrationTests()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(Constants.REQ_RES_BASE_URL)
        };
        _client.DefaultRequestHeaders.Add(Constants.REQ_RES_API_KEY, Constants.REQ_RES_API_VALUE);
    }

    [Fact]
    public async Task GetUsers_ShouldReturnOkResult()
    {
        // arrange
        var request = "users?page=2";

        // act
        var response = await _client.GetAsync(request);

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetUsers_StatusCodeShouldBeSuccess()
    {
        // arrange
        var request = "users?page=2";

        // act
        var response = await _client.GetAsync(request);

        // assert
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetUsers_ShouldReturnContent()
    {
        // arrange
        var request = "users?page=2";
        var expectedKey = "data";

        // act
        var response = await _client.GetAsync(request);

        // assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains(expectedKey, content);
    }

    [Fact]
    public async Task PostCreateUser_ReturnsCreated()
    {
        // arrange
        var request = "users";

        var payload = new StringContent(
            JsonSerializer.Serialize(new
            {
                Name = "morpheus",
                Job = "leader"
            }),
            Encoding.UTF8, "application/json");

        // act
        var response = await _client.PostAsync(request, payload);

        // assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PostCreateUser_ReturnsExpectedContent()
    {
        // arrange
        var request = "users";
        var expectedIdKey = "id";
        var expectedCreatedAtKey = "createdAt";

        var payload = new StringContent(
            JsonSerializer.Serialize(new
            {
                Name = "morpheus",
                Job = "leader"
            }),
            Encoding.UTF8, "application/json");

        // act
        var response = await _client.PostAsync(request, payload);

        // assert
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains(expectedIdKey, content);
        Assert.Contains(expectedCreatedAtKey, content);
    }

    [Fact]
    public async Task PutUpdateUser_ReturnsSuccess()
    {
        // arrange
        var expectedContent = "updatedAt";
        var request = "users/2";

        var payload = new StringContent(
            JsonSerializer.Serialize(new
            {
                Name = "morpheus",
                Job = "leader"
            }),
            Encoding.UTF8, "application/json");

        // act
        var response = await _client.PutAsync(request, payload);

        // assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains(expectedContent, content);
    }

    [Fact]
    public async Task PatchUpdateUser_ReturnsSuccess()
    {
        // arrange
        var expectedContent = "updatedAt";

        var payload = new StringContent(
            JsonSerializer.Serialize(new
            {
                Job = "zionleader"
            }),
            Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Patch, "users/2") { Content = payload };

        // act
        var response = await _client.SendAsync(request);

        // assert
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains(expectedContent, content);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNoContent()
    {
        // arrange
        var request = "users/2";

        // act
        var response = await _client.DeleteAsync(request);

        // assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}