
using FnxTamirsServer.Models;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace FnxTamirsServer.Services
{
  public class GitHubRepoService: IGitHubRepoService
  {
    private readonly HttpClient _httpClient;
    private readonly SessionService _sessionService;

    public GitHubRepoService(HttpClient httpClient)
    {
      _httpClient = httpClient;
      _httpClient.BaseAddress = new Uri("https://api.github.com/");

      // using Microsoft.Net.Http.Headers;
      // The GitHub API requires two headers.
      _httpClient.DefaultRequestHeaders.Add(
          HeaderNames.Accept, "application/vnd.github.v3+json");
      _httpClient.DefaultRequestHeaders.Add(
          HeaderNames.UserAgent, "HttpRequestsSample");
    }

    public async Task<GitHubSearchResponse> GetGitHubRepoAsync(string repoName)
    {

      var response = await _httpClient.GetAsync("https://api.github.com/search/repositories?q=" + repoName);
      response.EnsureSuccessStatusCode();
      var json = await response.Content.ReadAsStringAsync();
      var searchResult = JsonSerializer.Deserialize<GitHubSearchResponse>(json);
     
      return searchResult;
    }
  }
}
