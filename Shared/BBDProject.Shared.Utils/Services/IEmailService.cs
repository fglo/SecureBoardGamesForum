using System.Threading;
using System.Threading.Tasks;
using BBDProject.Shared.Models.Email;
using BBDProject.Shared.Models.User;

namespace BBDProject.Shared.Utils.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(CancellationToken cancellationToken, params EmailDefinition[] emailDefinitions);
        Task SendConfirmAccountEmail(CancellationToken cancellationToken, string token, BaseUserInfo user);
        Task SendResetPasswordEmail(CancellationToken cancellationToken, string token, BaseUserInfo user);
    }
}
