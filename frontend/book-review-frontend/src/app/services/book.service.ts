import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book, BookDetail, CreateBook } from '../models/book.model';
import { Review, CreateReview } from '../models/review.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://localhost:5001/api';

  constructor(private http: HttpClient) { }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.apiUrl}/books`);
  }

  getBook(id: number): Observable<BookDetail> {
    return this.http.get<BookDetail>(`${this.apiUrl}/books/${id}`);
  }

  searchBooks(searchTerm: string): Observable<Book[]> {
    const params = new HttpParams().set('q', searchTerm);
    return this.http.get<Book[]>(`${this.apiUrl}/books/search`, { params });
  }

  getBooksByCategory(category: string): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.apiUrl}/books/category/${category}`);
  }

  createBook(book: CreateBook): Observable<Book> {
    return this.http.post<Book>(`${this.apiUrl}/books`, book);
  }

  createReview(review: CreateReview): Observable<Review> {
    return this.http.post<Review>(`${this.apiUrl}/reviews`, review);
  }
}
