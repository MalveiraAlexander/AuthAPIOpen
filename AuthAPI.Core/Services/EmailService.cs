using AuthAPI.Commons.Interfaces.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string URL_MS_EMAIL;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            URL_MS_EMAIL = _configuration["EmailMsURL"];
        }

        public async Task SendRecoveryAsync(string emailTo, string name, string lastname, string url, string subject)
        {
            Email email = new Email()
            {
                To = emailTo,
                Name = name,
                LastName = lastname,
                Type = 0,
                URL = url,
                Subject = subject
            };
            var client = new RestClient(URL_MS_EMAIL);
            var request = new RestRequest($"api/email", Method.Post);
            request.AddBody(email);
            await client.ExecuteAsync(request);
        }
    }

    internal class Email
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Type { get; set; }
        public string? URL { get; set; }
    }
}
