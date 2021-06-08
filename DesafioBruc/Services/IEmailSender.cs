using System;
using System.Threading.Tasks;

namespace DesafioBruc.Services
{
    public interface IEmailSender
    {

        Task SendEmailAsync(string email, string subject, string message);

    }
}
