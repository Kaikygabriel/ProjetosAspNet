using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Application.Dtos.User;

public record UserLogin([Required]string Name,[Required]string Password);