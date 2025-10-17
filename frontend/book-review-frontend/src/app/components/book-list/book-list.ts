import { Component, OnInit } from '@angular/core';
import { Book } from '../../models/book.model';
import { BookService } from '../../services/book.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.html',
  styleUrls: ['./book-list.css'],
  standalone: true,
    imports: [
      FormsModule,
      CommonModule
    ],
})
export class BookListComponent implements OnInit {
  books: Book[] = [];
  filteredBooks: Book[] = [];
  searchTerm: string = '';
  selectedCategory: string = '';
  categories: string[] = [
    'Ficción', 'No Ficción', 'Ciencia Ficción', 'Fantasía',
    'Romance', 'Misterio', 'Biografía', 'Historia', 'Ciencia'
  ];

  constructor(
    private bookService: BookService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.bookService.getBooks().subscribe({
      next: (books) => {
        this.books = books;
        this.filteredBooks = books;
      },
      error: (error) => {
        console.error('Error loading books:', error);
      }
    });
  }

  onSearch(): void {
    if (this.searchTerm.trim()) {
      this.bookService.searchBooks(this.searchTerm).subscribe({
        next: (books) => {
          this.filteredBooks = books;
        },
        error: (error) => {
          console.error('Error searching books:', error);
        }
      });
    } else {
      this.filteredBooks = this.books;
    }
  }

  filterByCategory(category: string): void {
    this.selectedCategory = category;
    this.bookService.getBooksByCategory(category).subscribe({
      next: (books) => {
        this.filteredBooks = books;
      },
      error: (error) => {
        console.error('Error filtering by category:', error);
      }
    });
  }

  clearFilters(): void {
    this.selectedCategory = '';
    this.searchTerm = '';
    this.filteredBooks = this.books;
  }

  viewBook(bookId: number): void {
    this.router.navigate(['/book', bookId]);
  }

  getStars(rating: number): number[] {
    const fullStars = Math.floor(rating);
    return Array(fullStars).fill(0);
  }
}
