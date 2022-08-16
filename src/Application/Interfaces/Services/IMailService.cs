using DynaCore.Application.Requests.Mail;
using System.Threading.Tasks;

namespace DynaCore.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}