using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstraction.Services
{
    public interface IMailService 
    {
        Task SendMailAsync(string to,string mailBody,string subject);
        Task SendMailAsync(string[] to, string mailBody, string subject);
        Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
    }
}
