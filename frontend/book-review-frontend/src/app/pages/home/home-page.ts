import { Component } from '@angular/core';
import { BookListComponent } from 'src/app/components/book-list/book-list';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [BookListComponent],
  templateUrl: './home-page.html',
  styleUrls: ['./home-page.css']
})
export class HomePage {}

