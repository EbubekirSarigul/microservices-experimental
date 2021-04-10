using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.Sms.Abstractions
{
    public interface ISmsProvider
    {
        Task SendSms(string phoneNumber, string message);
    }
}
