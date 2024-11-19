namespace api_rest_cs.DTOs;

public class UpdateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int UserId { get; set; }
}
