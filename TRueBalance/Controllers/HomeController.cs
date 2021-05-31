using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TRueBalance.Models;
using Microsoft.AspNetCore.Authorization;
using SendGrid;
using SendGrid.Helpers.Mail;
using TRueBalance.Services;

namespace TRueBalance.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        //TODO: Search where is this code
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

        public IActionResult Index()
        {
            return View();
        }

        //TODO: Search where is this code
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
