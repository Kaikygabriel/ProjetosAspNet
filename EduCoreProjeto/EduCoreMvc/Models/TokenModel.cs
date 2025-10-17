namespace EduCoreMvc.Models;

public class TokenModel
{
    public string? token  { get; set; } 
    public string? refreshToken  { get; set; } 

    public DateTime? expiredRefreshToken  { get; set; } 

}