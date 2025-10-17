using BookReviewsAPI.Models;
using BookReviewsAPI.DTOs;

namespace BookReviewsAPI.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(string category);
        Task<Book> AddBookAsync(Book book);
        Task<Review> AddReviewAsync(Review review);
        Task<bool> BookExistsAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}