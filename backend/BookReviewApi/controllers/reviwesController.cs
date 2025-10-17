using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BookReviewsAPI.Models;
using BookReviewsAPI.DTOs;
using BookReviewsAPI.Repository;

namespace BookReviewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public ReviewsController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] CreateReviewDto createReviewDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            // Validar que el rating esté entre 1 y 5
            if (createReviewDto.Rating < 1 || createReviewDto.Rating > 5)
                return BadRequest("El rating debe estar entre 1 y 5");

            // Verificar que el libro existe
            var bookExists = await _bookRepository.BookExistsAsync(createReviewDto.BookId);
            if (!bookExists)
                return NotFound("Libro no encontrado");

            var review = new Review
            {
                Comment = createReviewDto.Comment,
                Rating = createReviewDto.Rating,
                BookId = createReviewDto.BookId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            var createdReview = await _bookRepository.AddReviewAsync(review);

            var reviewDto = new ReviewDto
            {
                Id = createdReview.Id,
                Comment = createdReview.Comment,
                Rating = createdReview.Rating,
                CreatedAt = createdReview.CreatedAt,
                UserName = userEmail!,
                UserId = userId,
                BookId = createdReview.BookId
            };

            return CreatedAtAction(nameof(GetReview), new { id = createdReview.Id }, reviewDto);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            var review = book.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
                return NotFound();

            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                Comment = review.Comment,
                Rating = review.Rating,
                CreatedAt = review.CreatedAt,
                UserName = review.User.UserName!,
                UserId = review.UserId,
                BookId = review.BookId
            };

            return Ok(reviewDto);
        }
    }
}