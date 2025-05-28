using FnxTamirsServer.Models;

namespace FnxTamirsServer.Services
{
  public interface ISessionService
  {
    Task<IEnumerable<GitHubRepository>> GetBookmarks();
    bool AddBookmark(GitHubRepository repository);
    bool RemoveBookmark(long repositoryId);
  }
}
