using MicroserviceTraining.Framework.Sms.Abstractions;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.Sms.Implementations
{
    public class MockSmsProvider : ISmsProvider
    {
        public async Task SendSms(string phoneNumber, string message)
        {
            await Task.Delay(100);
        }
    }
}
