using EPharma.Application.Requests.Mail;
using System.Threading.Tasks;

namespace EPharma.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}