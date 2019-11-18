using System;
using Microsoft.Extensions.DependencyInjection;
using Parking.Api.Commands.Handlers;

namespace Parking.Api
{
    public static class CqrsServicesExtensions
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection serviceCollection)
        {
            CommandRouter.RegisterHandlers(serviceCollection);

            return serviceCollection;
        }

        public static IServiceCollection AddQueryHandlers(this IServiceCollection serviceCollection)
        {
            throw new NotImplementedException();
        }
    }
}