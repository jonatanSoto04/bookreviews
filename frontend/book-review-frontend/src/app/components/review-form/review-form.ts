import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CreateReview } from '../../models/review.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-review-form',
  templateUrl: './review-form.html',
  styleUrls: ['./review-form.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class ReviewFormComponent {
  @Input() bookId!: number;
  @Output() reviewSubmitted = new EventEmitter<CreateReview>();

  reviewForm: FormGroup;
  selectedRating: number = 0;

  constructor(private fb: FormBuilder) {
    this.reviewForm = this.fb.group({
      comment: ['', [Validators.required, Validators.minLength(10)]]
    });
  }

  setRating(rating: number): void {
    this.selectedRating = rating;
  }

  onSubmit(): void {
    if (this.reviewForm.valid && this.selectedRating > 0) {
      const review: CreateReview = {
        comment: this.reviewForm.value.comment,
        rating: this.selectedRating,
        bookId: this.bookId
      };

      this.reviewSubmitted.emit(review);
      this.reviewForm.reset();
      this.selectedRating = 0;
    }
  }
}
