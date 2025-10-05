namespace BibliotecaMVC.Models;

public class TokenViewModel
{
    public string? Token { get; set; } = string.Empty;
    public string? RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiredToken { get; set; }
    public bool Authenticated { get; set; }
}