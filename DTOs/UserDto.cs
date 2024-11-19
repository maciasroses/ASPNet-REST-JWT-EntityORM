namespace api_rest_cs.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<BookDto>? Books { get; set; }
}
