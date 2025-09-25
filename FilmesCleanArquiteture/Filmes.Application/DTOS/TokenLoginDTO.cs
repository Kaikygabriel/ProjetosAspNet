namespace Filmes.Application.DTOS;

public class TokenLoginDTO
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}