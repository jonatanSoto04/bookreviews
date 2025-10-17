using System.ComponentModel.DataAnnotations.Schema;

namespace BookReviewsAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; } // 1-5 stars
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign keys
        public int BookId { get; set; }
        public string UserId { get; set; } = string.Empty;
        
        // Navigation properties
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; } = null!;
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}