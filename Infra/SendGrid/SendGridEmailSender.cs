using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace w_escolas.Infra.SendGrid;

public class SendGridEmailSender : IEmailSender
{
    public SendGridEmailSender(
        //IOptions<SendGridAuth> optionsAccessor,
        IConfiguration configuration)
    {
        //Options = optionsAccessor.Value;
        this.configuration = configuration;
    }
    //public SendGridAuth Options { get; }
    private readonly IConfiguration configuration;
    public Task SendEmailAsync(string email, string subject, string message)
    {
        //return Execute(Options.SendGridKey, subject, message, email);
        var sendGridKey = configuration["SENDGRID_API_KEY"];
        return Execute(sendGridKey, subject, message, email);
    }
    public Task Execute(string apiKey, string subject, string message, string email)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("dev.kgsh@gmail.com", "Sistema Escolar"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(email));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);

        return client.SendEmailAsync(msg);
    }
}
