using FnxTamirsServer.Models;

namespace FnxTamirsServer.Services
{
  public interface IGitHubRepoService
  {
    public Task<GitHubSearchResponse> GetGitHubRepoAsync(string repoName);
  }
}
