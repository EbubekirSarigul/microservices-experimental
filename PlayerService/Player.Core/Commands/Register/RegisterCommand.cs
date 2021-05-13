using MicroserviceTraining.Framework.Commands;

namespace Player.Core.Commands.Register
{
    public class RegisterCommand : BaseCommand<RegisterResult>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public int Rating { get; set; }
    }
}
