import { Component, inject, input } from '@angular/core';
import { NgClass } from '@angular/common';
import { GitHubRepository } from '../models/github.model';
import { GitHubService } from '../services/github.service';

@Component({
  selector: 'repo-list',
  imports: [NgClass],
  templateUrl: './repo-list.component.html',
  styleUrl: './repo-list.component.scss',
})
export class RepoListComponent {
  openRepo(repo: GitHubRepository) {
    window.open(repo.html_url, '_blank');
  }
  private gitHubSrv = inject(GitHubService);
  public repositories = input<GitHubRepository[] | null>();

  toggleBookmark(repo: GitHubRepository) {
    this.gitHubSrv.toggleBookmark$(repo)?.subscribe((res) => {
    });
  }
}
