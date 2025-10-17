namespace BookReviewsAPI.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int BookId { get; set; }
    }

    public class CreateReviewDto
    {
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
        public int BookId { get; set; }
    }
}