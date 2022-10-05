namespace Tryitter.Web.Controllers
{
    public class PostDTO
    {
        public Guid PostId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }                                                                         
        public Guid UserId { get; set; }
    }
}