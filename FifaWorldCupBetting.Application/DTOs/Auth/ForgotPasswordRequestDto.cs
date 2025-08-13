using System.ComponentModel.DataAnnotations;

namespace FifaWorldCupBetting.Application.DTOs.Auth;

public class ForgotPasswordRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string ResetUrl { get; set; } = string.Empty;
}
