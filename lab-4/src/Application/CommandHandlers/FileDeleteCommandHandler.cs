using System;
using lab_4.Application.Commands;
using lab_4.Domain.Exceptions;

namespace lab_4.Application.CommandHandlers;

public class FileDeleteCommandHandler(CommandContext context) : ICommandHandler
{
    public bool CanHandle(string[] args)
    {
        return args.Length > 1 && args[0].Equals("file", StringComparison.CurrentCultureIgnoreCase) && args[1].Equals("delete", StringComparison.CurrentCultureIgnoreCase);
    }

    public ICommand Handle(string[] args)
    {
        if (args.Length < 3)
            throw new InvalidCommandException("Недостаточно аргументов для команды file delete.");

        var path = args[2];

        return new DeleteFileCommand(context.FileSystem ?? throw new InvalidOperationException(), path);
    }
}