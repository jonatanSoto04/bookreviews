using Microsoft.AspNetCore.Identity;

namespace BookReviewsAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation property
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}