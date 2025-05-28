import { Component, inject, OnInit } from '@angular/core';
import { GitHubService } from '../services/github.service';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import {
  catchError,
  debounceTime,
  distinctUntilChanged,
  filter,
  map,
  Observable,
  of,
  Subject,
  switchMap,
  tap,
} from 'rxjs';
import { GitHubRepository } from '../models/github.model';
import { AsyncPipe } from '@angular/common';
import { RepoListComponent } from '../repo-list/repo-list.component';

@Component({
  selector: 'app-main-page',
  imports: [ReactiveFormsModule, AsyncPipe, RepoListComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss',
})
export class MainPageComponent implements OnInit {
  private gitHubSrv = inject(GitHubService);

  searchText$ = new Subject<string>();
  repositories$!: Observable<GitHubRepository[]>;
  searchControl = new FormControl('');
  totalCount: number = 0;
  isLoading: boolean = false;

  constructor() {
    //subscribing to text changes and pass it to the subject
    this.searchControl.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        filter((query) => !!query && query.length > 2)
      )
      .subscribe((query) => this.searchText$.next(query ?? ''));
  }
  ngOnInit(): void {
    this.repositories$ = this.searchText$.pipe(
      tap((searchTerm) => (this.isLoading = true)),
      switchMap((searchTerm) => this.searchRepositories(searchTerm))//returning new observable, that canels previous requests
    );
  }
  //
  private searchRepositories(query: string): Observable<GitHubRepository[]> {
    return this.gitHubSrv.searchRepositories(query).pipe(
      tap((res) => {
        this.isLoading = false;
        this.totalCount = res.total_count;
      }),
      map((res) => res.items),//get only the items back
      catchError((err) => {
        this.isLoading = false;
        console.log('error on search');
        return of([]);
      })
    );
  }
}
