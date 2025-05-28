using FnxTamirsServer.Models;
using System.Collections.Concurrent;

namespace FnxTamirsServer.Services
{
  //custom session implementation
  public class SessionService : ISessionService
  {
    private readonly HashSet<GitHubRepository> _bookmarks;

    public SessionService()
    {
      _bookmarks = new  HashSet<GitHubRepository>();
    }

    public bool AddBookmark(GitHubRepository repository)
    {
      
      lock (_bookmarks)
      {
        var existsRepo = _bookmarks.FirstOrDefault(r => r.Id == repository.Id);
        if (existsRepo == null)
        {
          repository.Bookmarked = true;
          return _bookmarks.Add(repository);
        }
      }
      return false;
    }

    public async Task<IEnumerable<GitHubRepository>> GetBookmarks()
    {
        return _bookmarks.ToList();
    }

    public bool RemoveBookmark( long repositoryId)
    {
      
        lock (_bookmarks)
        {
          var repository = _bookmarks.FirstOrDefault(r => r.Id == repositoryId);
          if (repository != null)
          {
          repository.Bookmarked = false;
            return _bookmarks.Remove(repository);
          }
        }
      
      return false;
    }
  }
}
