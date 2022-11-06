using System.ComponentModel.DataAnnotations;

namespace Tryitter.Web.Controllers
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string? Nickname { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email")]
        [Required]
        public string? Login { get; set; } = null!;

        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Invalid login length")]
        public string? Password { get; set; } = null!;
        public byte[]? ProfileImage { get; set; }
    }
    public class LoginData
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}