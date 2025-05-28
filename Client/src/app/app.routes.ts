import { Routes } from '@angular/router';
import { MainPageComponent } from './main-page/main-page.component';
import { BookmarksManagmentComponent } from './bookmarks-managment/bookmarks-managment.component';

export const routes: Routes = [
  {path: '', redirectTo: '/search', pathMatch: 'full'},
  { path: 'search', component: MainPageComponent },
  { path: 'bookmarks', component: BookmarksManagmentComponent },
];
