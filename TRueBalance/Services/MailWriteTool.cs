using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRueBalance.Services
{
    public class MailWriteTool
    {
        static async Task Mail(string ToMail, string ToName)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@TRueBalance.com", "TRueBalance Team"),
                Subject = "Hello World from the SendGrid CSharp SDK!",
                PlainTextContent = "Hello, Email!",
                HtmlContent = "<strong>Hello, Email!</strong>"
            };
            msg.AddTo(new EmailAddress(ToMail, ToName));
            var response = await client.SendEmailAsync(msg);
        }
    }
}
