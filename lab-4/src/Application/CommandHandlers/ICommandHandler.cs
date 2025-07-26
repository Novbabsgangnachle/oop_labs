using lab_4.Application.Commands;

namespace lab_4.Application.CommandHandlers;

public interface ICommandHandler
{
    bool CanHandle(string[] args);

    ICommand Handle(string[] args);
}