namespace Tryitter.Web.Models
{
    public class Post
    {
        public Guid PostId { get; set; }
        public string Content { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}