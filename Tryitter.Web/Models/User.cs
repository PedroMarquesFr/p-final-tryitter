
using System.ComponentModel.DataAnnotations;

namespace Tryitter.Web.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Invalid Nickname length")]
        public string Nickname { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email")]
        [Required]
        public string Login { get; set; } = null!;

        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Invalid password length")]
        public string Password { get; set; } = null!;

        public byte[]? ProfileImage { get; set; }

        public ICollection<Post>? Posts { get; set; }
    }
}