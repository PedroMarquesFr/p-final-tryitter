namespace Tryitter.Web.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Nickname { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public ICollection<Post>? Posts { get; set; }

    }
}