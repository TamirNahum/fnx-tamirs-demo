import { Component, inject, OnInit } from '@angular/core';
import { GitHubService } from '../services/github.service';
import { Observable } from 'rxjs';
import { GitHubRepository } from '../models/github.model';
import { AsyncPipe } from '@angular/common';
import { RepoListComponent } from '../repo-list/repo-list.component';

@Component({
  selector: 'app-bookmarks-managment',
  imports: [RepoListComponent,AsyncPipe],
  templateUrl: './bookmarks-managment.component.html',
  styleUrl: './bookmarks-managment.component.scss',
})
export class BookmarksManagmentComponent implements OnInit {
  private gitHubSrv = inject(GitHubService);
  bookmarks$!: Observable<GitHubRepository[]>;
  ngOnInit(): void {
    this.bookmarks$ = this.gitHubSrv.getBookmarks();
  }
}
