namespace Tryitter.Web.Controllers
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string? Nickname { get; set; } = null!;
        public string? Login { get; set; } = null!;
        public string? Password { get; set; } = null!;
    }
}