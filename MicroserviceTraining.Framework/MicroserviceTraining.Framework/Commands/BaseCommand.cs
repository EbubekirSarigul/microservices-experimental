using MediatR;

namespace MicroserviceTraining.Framework.Commands
{
    public class BaseCommand<T> : IRequest<T>
    {
    }
}
