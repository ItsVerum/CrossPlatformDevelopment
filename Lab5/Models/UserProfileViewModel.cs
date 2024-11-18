using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class UserProfileViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(500)]
        public string FullName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$",
            ErrorMessage = "Password must be between 8 and 16 characters and contain at least one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^\+380\d{9}$",
            ErrorMessage = "Please enter a valid Ukrainian phone number (format: +380XXXXXXXXX)")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public string ProfileImage { get; set; }
    }
}
