using System;
using lab_4.Application.Commands;

namespace lab_4.Application.CommandHandlers;


public class DisconnectCommandHandler(CommandContext context) : ICommandHandler
{
    public bool CanHandle(string[] args)
    {
        return args.Length > 0 && args[0].Equals("disconnect", StringComparison.CurrentCultureIgnoreCase);
    }

    public ICommand Handle(string[] args)
    {
        return new DisconnectCommand(context);
    }
}