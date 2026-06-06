using System.Net;
using System.Net.Mail;

namespace OptimalVisionAPI.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        var smtp = new SmtpClient(_config["Smtp:Host"])
        {
            Port = int.Parse(_config["Smtp:Port"]),
            Credentials = new NetworkCredential(
                _config["Smtp:Username"],
                _config["Smtp:Password"]
            ),
            EnableSsl = true
        };

        var message = new MailMessage(_config["Smtp:From"], to, subject, body);
        await smtp.SendMailAsync(message);
    }
}
