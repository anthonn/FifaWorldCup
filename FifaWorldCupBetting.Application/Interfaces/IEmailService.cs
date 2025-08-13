namespace FifaWorldCupBetting.Application.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetEmailAsync(string email, string resetToken, string username);
    Task SendWelcomeEmailAsync(string email, string username);
}
