using System.Threading.Tasks;
using RecipeApp.Core.Models.Mail;

namespace RecipeApp.Service.Contracts
{
    public interface IMailService
    {
        Task SendAsync(MailDTO mail);
    }
}