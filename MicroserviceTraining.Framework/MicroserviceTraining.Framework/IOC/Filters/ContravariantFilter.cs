using Castle.MicroKernel;
using System;
using System.Linq;

namespace MicroserviceTraining.Framework.IOC.Filters
{
    public class ContravariantFilter : IHandlersFilter
    {
        public bool HasOpinionAbout(Type service)
        {
            if (!service.IsGenericType)
                return false;

            var genericType = service.GetGenericTypeDefinition();
            var genericArguments = genericType.GetGenericArguments();

            return genericArguments.Length == 1 && genericArguments.Single().GenericParameterAttributes.HasFlag(System.Reflection.GenericParameterAttributes.Contravariant);
        }

        public IHandler[] SelectHandlers(Type service, IHandler[] handlers)
        {
            return handlers;
        }
    }
}
