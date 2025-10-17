import { Component, Input, OnInit } from '@angular/core';
import { BookDetail } from '../../models/book.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-detail',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './book-detail.html',
  styleUrls: ['./book-detail.css']
})
export class BookDetailComponent implements OnInit {
  @Input() book!: BookDetail;

  ngOnInit(): void {}
}
