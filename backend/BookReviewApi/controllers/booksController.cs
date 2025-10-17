using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BookReviewsAPI.Models;
using BookReviewsAPI.DTOs;
using BookReviewsAPI.Repository;

namespace BookReviewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            var bookDtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                Category = b.Category,
                CoverImageUrl = b.CoverImageUrl,
                AverageRating = b.AverageRating,
                ReviewCount = b.ReviewCount,
                PublishedDate = b.PublishedDate,
                PageCount = b.PageCount
            });

            return Ok(bookDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailDto>> GetBook(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            
            if (book == null)
                return NotFound();

            var bookDto = new BookDetailDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Category = book.Category,
                CoverImageUrl = book.CoverImageUrl,
                AverageRating = book.AverageRating,
                ReviewCount = book.ReviewCount,
                PublishedDate = book.PublishedDate,
                PageCount = book.PageCount,
                Reviews = book.Reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    CreatedAt = r.CreatedAt,
                    UserName = r.User.UserName!,
                    UserId = r.UserId,
                    BookId = r.BookId
                }).OrderByDescending(r => r.CreatedAt).ToList()
            };

            return Ok(bookDto);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> SearchBooks([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("El término de búsqueda no puede estar vacío");

            var books = await _bookRepository.SearchBooksAsync(q);
            var bookDtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                Category = b.Category,
                CoverImageUrl = b.CoverImageUrl,
                AverageRating = b.AverageRating,
                ReviewCount = b.ReviewCount
            });

            return Ok(bookDtos);
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByCategory(string category)
        {
            var books = await _bookRepository.GetBooksByCategoryAsync(category);
            var bookDtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                Category = b.Category,
                CoverImageUrl = b.CoverImageUrl,
                AverageRating = b.AverageRating,
                ReviewCount = b.ReviewCount
            });

            return Ok(bookDtos);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookDto createBookDto)
        {
            var book = new Book
            {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                Description = createBookDto.Description,
                ISBN = createBookDto.ISBN,
                PublishedDate = createBookDto.PublishedDate,
                Category = createBookDto.Category,
                CoverImageUrl = createBookDto.CoverImageUrl,
                PageCount = createBookDto.PageCount
            };

            var createdBook = await _bookRepository.AddBookAsync(book);

            var bookDto = new BookDto
            {
                Id = createdBook.Id,
                Title = createdBook.Title,
                Author = createdBook.Author,
                Description = createdBook.Description,
                Category = createdBook.Category,
                CoverImageUrl = createdBook.CoverImageUrl,
                AverageRating = createdBook.AverageRating,
                ReviewCount = createdBook.ReviewCount
            };

            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, bookDto);
        }
    }
}