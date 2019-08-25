namespace Gamayun.Infrastucture.Command
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        ICommandResult Handle(TCommand command);
    }
}
