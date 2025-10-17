export interface Review {
  userName: string;
  createdAt: string | Date;
  rating: number;
  comment: string;
}

export interface Book {
  id: number;
  title: string;
  author: string;
  description: string;
  isbn: string;
  publishedDate: string;
  category: string;
  coverImageUrl: string;
  pageCount: number;
  averageRating: number;
  reviewCount: number;
  createdAt: string;
}

export interface BookDetail extends Book {
  reviews: Review[];
}

export interface CreateBook {
  title: string;
  author: string;
  description: string;
  isbn: string;
  publishedDate: string;
  category: string;
  coverImageUrl: string;
  pageCount: number;
}

export interface CreateReview {
  bookId: number;
  rating: number;
  comment: string;
}
