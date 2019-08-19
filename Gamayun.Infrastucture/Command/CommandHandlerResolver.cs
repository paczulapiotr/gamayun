using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gamayun.Infrastucture.Command
{
    public interface ICommandHandlerResolver
    {
        ICommandHandler<T> Resolve<T>() where T : ICommand;
    }

    public class CommandHandlerResolver : ICommandHandlerResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandHandlerResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandHandler<T> Resolve<T>() where T : ICommand
        {
            var commandHandlerType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommandHandler)))
                .Where(t => t.GetGenericArguments().Contains(typeof(T)))
                .FirstOrDefault();
            
            return _serviceProvider.GetService(commandHandlerType) as ICommandHandler<T>;
        }
    }
}
