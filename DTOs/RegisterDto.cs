using System.ComponentModel.DataAnnotations;

namespace api_rest_cs.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 3)]
    public string Password { get; set; } = string.Empty;
}