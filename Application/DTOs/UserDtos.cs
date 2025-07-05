using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class CreateUserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "The username cannot exceed 50 characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "The email cannot exceed 50 characters")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(255, ErrorMessage = "The password cannot exceed 255 characters")]
        public string Password { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "The firstname cannot exceed 50 characters")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "The lastname cannot exceed 50 characters")]
        public string? LastName { get; set; }
    }

    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "The username cannot exceed 50 characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "The email cannot exceed 50 characters")]
        public string Email { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "The password cannot exceed 255 characters")]
        public string? PasswordHash { get; set; }

        [StringLength(50, ErrorMessage = "The firstname cannot exceed 50 characters")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "The lastname cannot exceed 50 characters")]
        public string? LastName { get; set; }
    }

    public class LoginUserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;
    }
}