import { Component, OnInit } from '@angular/core';
import { RouterModule, ActivatedRoute, Router} from '@angular/router';
import { BookDetail, CreateReview } from '../../models/book.model';
import { BookService } from '../../services/book.service';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { BookDetailComponent } from 'src/app/components/book-detail/book-detail';
import { ReviewFormComponent } from 'src/app/components/review-form/review-form';

@Component({
  selector: 'app-book-page',
  templateUrl: './book-page.html',
  styleUrls: ['./book-page.css'],
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    BookDetailComponent,
    ReviewFormComponent
  ],
})
export class BookPageComponent implements OnInit {
  book: BookDetail | null = null;
  isLoading: boolean = true;
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadBook();
  }

  loadBook(): void {
    const bookId = Number(this.route.snapshot.paramMap.get('id'));

    this.bookService.getBook(bookId).subscribe({
      next: (book) => {
        this.book = book;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar el libro';
        this.isLoading = false;
        console.error('Error loading book:', error);
      }
    });
  }

  onReviewSubmitted(review: CreateReview): void {
    this.bookService.createReview(review).subscribe({
      next: (newReview) => {
        if (this.book) {
          this.book.reviews.unshift(newReview);
          this.book.reviewCount = this.book.reviews.length;
          this.book.averageRating = this.book.reviews.reduce((sum, r) => sum + r.rating, 0) / this.book.reviews.length;
        }
      },
      error: (error) => {
        console.error('Error creating review:', error);
        alert('Error al crear la reseña');
      }
    });
  }

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  getStars(rating: number): number[] {
    return Array(rating).fill(0);
  }
}
