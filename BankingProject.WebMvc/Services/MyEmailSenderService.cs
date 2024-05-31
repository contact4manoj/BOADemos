using Microsoft.AspNetCore.Identity.UI.Services;

namespace BankingProject.WebMvc.Services;

// C# 12.0 : Primary Constructor
public class MyEmailSenderService(ILogger<MyEmailSenderService> _logger)
                : IEmailSender
{

    //private readonly ILogger<MyEmailSenderService> _logger;

    //public MyEmailSenderService(ILogger<MyEmailSenderService> logger)
    //{
    //    _logger = logger;
    //}

    #region IEmailSender members 

    public Task SendEmailAsync(
        string email, string subject, string htmlMessage)
    {
        //TODO: configure the SMTP email service object and send the email.

        _logger.LogInformation($"Email sent to {email}, Content: {htmlMessage}");

        return Task.CompletedTask;
    }

    #endregion

}
