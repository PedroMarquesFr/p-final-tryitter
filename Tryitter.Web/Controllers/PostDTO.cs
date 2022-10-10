namespace Tryitter.Web.Controllers
{
    public class PostDTO
    {
        public Guid PostId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }
    }
}