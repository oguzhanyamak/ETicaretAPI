using ETicaretAPI.Application.Abstraction.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration configuration;
        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task SendMailAsync(string to, string mailBody, string subject)
        {
            await SendMailAsync(new[] { to },mailBody,subject);
        }

        public async Task SendMailAsync(string[] to, string mailBody, string subject)
        {
            MailMessage message = new MailMessage();
            message.Subject = subject;
            message.Body = mailBody;
            foreach (string s in to)
            {
                message.To.Add(s);
            }
            message.From = new(configuration["Mail:Username"]!,"Name",System.Text.Encoding.UTF8);

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(configuration["Mail:Username"], configuration["Mail:Password"]);
            smtpClient.Port = int.Parse(configuration["Mail:Port"]!);
            smtpClient.EnableSsl = true;
            smtpClient.Host = configuration["Mail:Host"]!;
            await smtpClient.SendMailAsync(message);

        }

        public async Task SendPasswordResetMailAsync(string to,string userId,string resetToken)
        {
            StringBuilder sb = new();
            sb.AppendLine("Merhaba<br> Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><br> <a style=\"display: inline-block; padding: 10px; background-color: green; color: white; text-decoration: none; border: none; cursor: pointer;\" target=\"_blank\" href=\"");
            sb.AppendLine(configuration["WebClientUrl"]);
            sb.AppendLine("/update-password/");
            sb.AppendLine(userId); 
            sb.AppendLine("/");
            sb.AppendLine(resetToken);
            sb.AppendLine("\">Şifre Sıfırla</a>");

            await SendMailAsync(to, sb.ToString(), "Şifre Yenileme Talebi");
        }
    }
}
