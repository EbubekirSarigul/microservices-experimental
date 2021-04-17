using MediatR;

namespace MicroserviceTraining.Framework.Queries
{
    public class BaseQuery<T> : IRequest<T>
    {
    }
}
