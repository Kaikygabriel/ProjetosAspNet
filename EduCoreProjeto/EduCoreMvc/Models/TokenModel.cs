namespace EduCoreMvc.Models;

public class TokenModel
{
    public string? Token { get; set; } 
    public string? RefreshToken { get; set; } 

    public DateTime? ExpiredRefreshToken { get; set; } 

}