using System.ComponentModel.DataAnnotations;

namespace api_rest_cs.DTOs;

public class CreateBookDto
{
    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 3)]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "The field {0} is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a value greater than {1}")]
    public int UserId { get; set; }
}
