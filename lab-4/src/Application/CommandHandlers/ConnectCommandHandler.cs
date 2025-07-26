using System;
using lab_4.Application.Commands;
using lab_4.Domain.Exceptions;
using lab_4.Infrastructure.Factories;

namespace lab_4.Application.CommandHandlers;

public class ConnectCommandHandler(IFileSystemFactory factory, CommandContext context) : ICommandHandler
{
    public bool CanHandle(string[] args)
    {
        return args.Length > 0 && args[0].Equals("connect", StringComparison.CurrentCultureIgnoreCase);
    }

    public ICommand Handle(string[] args)
    {
        if (args.Length < 2)
            throw new InvalidCommandException("Недостаточно аргументов для команды connect.");

        string address = args[1];
        string mode = args.Length > 3 && args[2] == "-m" ? args[3] : "local";

        return new ConnectCommand(factory, address, mode, context);
    }
}