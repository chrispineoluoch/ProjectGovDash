using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration configuration;

    public EmailSender(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        // Validate user input to ensure email, subject, and message are not null or empty
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email address is required.", nameof(email));
        }

        if (string.IsNullOrEmpty(subject))
        {
            throw new ArgumentException("Email subject is required.", nameof(subject));
        }

        if (string.IsNullOrEmpty(message))
        {
            throw new ArgumentException("Email message is required.", nameof(message));
        }

        // Retrieve email credentials from configuration
        var emailUsername = configuration.GetValue<string>("EmailSettings:Username");
        var emailPassword = configuration.GetValue<string>("EmailSettings:Password");

        // Instantiate SmtpClient object
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(emailUsername, emailPassword)
        };

        // Send email using client object
        return client.SendMailAsync(
            new MailMessage(from: $"ICT Authority <{emailUsername}>",
                            to: email,
                            subject,
                            message
                            ));
    }
}
