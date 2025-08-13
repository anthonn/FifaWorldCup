using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using FifaWorldCupBetting.Application.Interfaces;

namespace FifaWorldCupBetting.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;
    private readonly string _fromEmail;
    private readonly string _fromName;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        
        _smtpHost = _configuration["EmailSettings:SmtpHost"] ?? throw new ArgumentNullException("SMTP Host not configured");
        _smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587");
        _smtpUsername = _configuration["EmailSettings:SmtpUsername"] ?? throw new ArgumentNullException("SMTP Username not configured");
        _smtpPassword = _configuration["EmailSettings:SmtpPassword"] ?? throw new ArgumentNullException("SMTP Password not configured");
        _fromEmail = _configuration["EmailSettings:FromEmail"] ?? _smtpUsername;
        _fromName = _configuration["EmailSettings:FromName"] ?? "FIFA World Cup Betting";
    }

    public async Task SendPasswordResetEmailAsync(string email, string resetToken, string username)
    {
        var resetUrl = $"{_configuration["AppSettings:FrontendUrl"]}/reset-password?token={resetToken}&email={Uri.EscapeDataString(email)}";
        
        var subject = "Reset Your Password - FIFA World Cup Betting";
        var body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Password Reset</title>
</head>
<body style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
    <div style='background-color: #f8f9fa; padding: 20px; border-radius: 10px;'>
        <h2 style='color: #333; text-align: center;'>FIFA World Cup Betting</h2>
        <h3 style='color: #007bff;'>Password Reset Request</h3>
        
        <p>Hello {username},</p>
        
        <p>We received a request to reset your password for your FIFA World Cup Betting account.</p>
        
        <p>Click the button below to reset your password:</p>
        
        <div style='text-align: center; margin: 30px 0;'>
            <a href='{resetUrl}' style='background-color: #007bff; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block;'>Reset Password</a>
        </div>
        
        <p>Or copy and paste this link into your browser:</p>
        <p style='word-break: break-all; color: #666;'>{resetUrl}</p>
        
        <p><strong>This link will expire in 1 hour.</strong></p>
        
        <p>If you didn't request this password reset, please ignore this email.</p>
        
        <hr style='margin: 20px 0; border: none; border-top: 1px solid #eee;'>
        <p style='color: #666; font-size: 12px; text-align: center;'>
            This is an automated message from FIFA World Cup Betting. Please do not reply to this email.
        </p>
    </div>
</body>
</html>";

        await SendEmailAsync(email, subject, body);
    }

    public async Task SendWelcomeEmailAsync(string email, string username)
    {
        var subject = "Welcome to FIFA World Cup Betting!";
        var body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Welcome</title>
</head>
<body style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
    <div style='background-color: #f8f9fa; padding: 20px; border-radius: 10px;'>
        <h2 style='color: #333; text-align: center;'>FIFA World Cup Betting</h2>
        <h3 style='color: #28a745;'>Welcome!</h3>
        
        <p>Hello {username},</p>
        
        <p>Welcome to FIFA World Cup Betting! Your account has been successfully created.</p>
        
        <p>You can now:</p>
        <ul>
            <li>Make predictions for group stage matches</li>
            <li>Bet on knockout stage winners</li>
            <li>Compete with friends and other users</li>
            <li>Track your progress on the leaderboard</li>
        </ul>
        
        <div style='text-align: center; margin: 30px 0;'>
            <a href='{_configuration["AppSettings:FrontendUrl"]}/dashboard' style='background-color: #28a745; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block;'>Start Betting</a>
        </div>
        
        <p>Good luck with your predictions!</p>
        
        <hr style='margin: 20px 0; border: none; border-top: 1px solid #eee;'>
        <p style='color: #666; font-size: 12px; text-align: center;'>
            This is an automated message from FIFA World Cup Betting. Please do not reply to this email.
        </p>
    </div>
</body>
</html>";

        await SendEmailAsync(email, subject, body);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        try
        {
            using var client = new SmtpClient(_smtpHost, _smtpPort);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail, _fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);
            _logger.LogInformation("Email sent successfully to {Email}", toEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Email}", toEmail);
            throw;
        }
    }
}
