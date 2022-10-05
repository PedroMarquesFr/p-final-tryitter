
using System.ComponentModel.DataAnnotations;

namespace Tryitter.Web.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Nickname { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email")]
        [Required]
        public string Login { get; set; } = null!;

        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Invalid login length")]
        public string Password { get; set; } = null!;

        public ICollection<Post>? Posts { get; set; }
    }
}