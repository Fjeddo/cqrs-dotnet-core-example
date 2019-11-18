using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Parking.Api.Commands.Handlers
{
    public class CommandRouter
    {
        private static readonly Dictionary<Type, Type> CommandHandlers = new Dictionary<Type, Type>();
        private static readonly Type CommandHandlerInterface = typeof(ICommandHandler<ICommand>);
        private static readonly List<Type> CommandHandlerTypes = CommandHandlerInterface.Assembly.GetTypes().Where(x => x.GetInterfaces().Any(y => y.IsAssignableFrom(CommandHandlerInterface))).ToList();

        private readonly IServiceProvider _serviceProvider;

        public CommandRouter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public static void RegisterHandlers(IServiceCollection serviceCollection /*, CommandStoreService commandStoreService*/)
        {
            serviceCollection.AddSingleton<CommandRouter>();

            CommandHandlerTypes.ForEach(handlerType =>
            {
                var commandType = handlerType.GetInterfaces().Single().GetGenericArguments().First();

                RegisterHandler(commandType, handlerType);
                serviceCollection.AddScoped(handlerType);
            });
        }

        public void Handle<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlerType = CommandHandlers[typeof(TCommand)];

            using var scope = _serviceProvider.CreateScope(); // C# 8

            if (!(scope.ServiceProvider.GetService(handlerType) is ICommandHandler<TCommand> handler)) // Type pattern matching
            {
                throw new NotSupportedException($"Handler for Command type {typeof(TCommand)} not registered");
            }

            handler.Handle(command);

            // Here we could go for the command store service push
            //_commandStoreService.Push(command);
        }

        private static void RegisterHandler(Type commandType, Type handlerType)
        {
            CommandHandlers.Add(commandType, handlerType);
        }
    }
}