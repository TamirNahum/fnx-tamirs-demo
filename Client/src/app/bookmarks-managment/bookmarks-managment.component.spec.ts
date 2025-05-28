import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookmarksManagmentComponent } from './bookmarks-managment.component';

describe('BookmarksManagmentComponent', () => {
  let component: BookmarksManagmentComponent;
  let fixture: ComponentFixture<BookmarksManagmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookmarksManagmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookmarksManagmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
