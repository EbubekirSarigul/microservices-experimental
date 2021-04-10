using Castle.Windsor;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MicroserviceTraining.Framework.IOC
{
    public sealed class IocFacility : WindsorContainer
    {
        public IConfiguration Configuration { get; }

        public IocFacility() : base()
        {
        }

        private static readonly Lazy<IocFacility> _container = new Lazy<IocFacility>(CreateInstance, LazyThreadSafetyMode.ExecutionAndPublication);
        private static IocFacility CreateInstance()
        {
            return new IocFacility();
        }


        public static IocFacility Container => _container.Value;
    }
}
