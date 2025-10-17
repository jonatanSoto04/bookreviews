export interface Review {
  id: number;
  comment: string;
  rating: number;
  createdAt: string;
  userName: string;
  userId: string;
  bookId: number;
}

export interface CreateReview {
  comment: string;
  rating: number;
  bookId: number;
}
