using DAL.Models;
using System.Threading.Tasks;
using static SmartClickCore.common;

namespace SmartClickCore.Interface{

    public interface IMailProvider
    {
        Task<bool> EnviarAsync(MailAPI mail, MailConfig config);
    }

    public interface IMailService
    {
        Task<bool> EnviarAsync(MailAPI mail);
    }
}
