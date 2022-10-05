namespace Tryitter.Web.Models
{
    public class Post
    {
        public Guid PostId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;                                                                        
        public Guid UserId { get; set; }

        public User? User { get; set; }
    }
}