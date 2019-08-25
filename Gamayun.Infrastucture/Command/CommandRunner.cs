using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gamayun.Infrastucture.Command
{
    public interface ICommandRunner
    {
        ICommandResult Run<TCommand>(TCommand command) where TCommand : ICommand;
    }

    public class CommandRunner : ICommandRunner
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandRunner(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand
            => _serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;

        public ICommandResult Run<TCommand>(TCommand command) where TCommand : ICommand
            => Resolve<TCommand>().Handle(command);
    }
}
