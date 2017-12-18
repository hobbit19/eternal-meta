using System.ComponentModel.DataAnnotations;

namespace Eternal.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 30 characters in length.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 30 characters in length.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 30 characters in length.")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
        public bool Active { get; set; }
        public string Token { get; set; }
    }
}
