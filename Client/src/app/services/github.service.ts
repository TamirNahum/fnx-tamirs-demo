import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';
import {
  GitHubRepository,
  GitHubRepositoryResponse,
} from '../models/github.model';

@Injectable({
  providedIn: 'root',
})
export class GitHubService {
  private http: HttpClient = inject(HttpClient);
  private apiUrl = 'https://localhost:7270';
  constructor() {}
  private toggling = new Set<number>(); // Track repo IDs being toggled

  toggleBookmark$(repo: GitHubRepository) {
    let repoId = repo.id;
    if (this.toggling.has(repoId)) return; // Already in progress

    this.toggling.add(repoId);

    //req tap do the same operation
    var req$ = !repo.bookmarked
      ? this.addBookmark(repo)
      : this.removeBookmark(repoId);
    return req$.pipe(
      tap((res) => {
        this.toggling.delete(repoId);
        repo.bookmarked = !repo.bookmarked;
        return res;
      }),
      catchError((err) => {
        this.toggling.delete(repoId);
        console.log('error on search');
        return of(null);
      })
    );
  }
  searchRepositories(repoName: string): Observable<GitHubRepositoryResponse> {
    return this.http.get<GitHubRepositoryResponse>(
      `${this.apiUrl}/searchrepo`,
      {
        params: { repoName },
      }
    );
  }

  addBookmark(repository: GitHubRepository): Observable<any> {
    return this.http.post(`${this.apiUrl}/bookmark`, repository);
  }

  getBookmarks(): Observable<GitHubRepository[]> {
    return this.http.get<GitHubRepository[]>(`${this.apiUrl}/bookmarks`);
  }

  removeBookmark(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/bookmark`, {
      params: { id },
    });
  }
}
