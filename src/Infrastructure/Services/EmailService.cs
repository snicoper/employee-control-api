﻿using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Infrastructure.Exceptions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EmployeeControl.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSenderSettings _emailSenderSettings;
    private readonly IHostEnvironment _environment;
    private readonly ILogger<EmailService> _logger;
    private readonly IRazorViewToStringRendererService _razorViewToStringRendererService;

    public EmailService(
        IOptions<EmailSenderSettings> options,
        ILogger<EmailService> logger,
        IHostEnvironment environment,
        IRazorViewToStringRendererService razorViewToStringRendererService)
    {
        _logger = logger;
        _environment = environment;
        _razorViewToStringRendererService = razorViewToStringRendererService;
        _emailSenderSettings = options.Value;

        MailPriority = MailPriority.High;
        From = _emailSenderSettings.DefaultFrom;
    }

    public MailPriority MailPriority { get; set; }

    public string? From { get; set; }

    public ICollection<string> To { get; set; } = new List<string>();

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public bool IsBodyHtml { get; set; }

    public async Task SendMailAsync()
    {
        await SendAsync();
    }

    public async Task SendMailWithViewAsync<TModel>(string viewName, TModel model)
        where TModel : class
    {
        Body = await _razorViewToStringRendererService.RenderViewToStringAsync(
            viewName,
            model,
            new Dictionary<string, object?>());

        await SendAsync();
    }

    private async Task SendAsync()
    {
        ValidateEmail();

        using var client = new SmtpClient(_emailSenderSettings.Host, _emailSenderSettings.Port);
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(_emailSenderSettings.Username, _emailSenderSettings.Password);
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = _emailSenderSettings.UseSsl;

        var mailMessage = new MailMessage();
        mailMessage.Priority = MailPriority;

        mailMessage.From = new MailAddress(From.NotNull());

        foreach (var to in To)
        {
            mailMessage.To.Add(new MailAddress(to));
        }

        mailMessage.Subject = Subject;
        mailMessage.Body = Body;
        mailMessage.IsBodyHtml = IsBodyHtml;

        LoggerMessage();

        // Solo en Prod se envían los mensajes por SMTP.
        if (!_environment.IsProduction())
        {
            return;
        }

        await client.SendMailAsync(mailMessage);
    }

    private void ValidateEmail()
    {
        if (To.Count == 0)
        {
            throw new EmailSenderException("The value cannot be an empty string. (Parameter 'To')");
        }

        if (string.IsNullOrEmpty(Subject))
        {
            throw new EmailSenderException("The value cannot be an empty string. (Parameter 'Subject')");
        }

        if (string.IsNullOrEmpty(Body))
        {
            throw new EmailSenderException("The value cannot be an empty string. (Parameter 'Body')");
        }
    }

    private void LoggerMessage()
    {
        var to = string.Join(", ", To);
        var body = !_environment.IsProduction() ? Body : "Body here.....";

        var stringBuilder = new StringBuilder();
        stringBuilder.Append("=========================================================\n");
        stringBuilder.Append($"From: {From}\n");
        stringBuilder.Append($"To: {to}\n");
        stringBuilder.Append($"Subject: {Subject}\n");
        stringBuilder.Append("=========================================================\n");
        stringBuilder.Append($"Body: {body}\n");
        stringBuilder.Append("=========================================================\n");

        _logger.LogDebug("{log}", stringBuilder.ToString());
    }
}
