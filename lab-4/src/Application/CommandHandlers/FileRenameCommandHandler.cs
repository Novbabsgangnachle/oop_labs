using System;
using lab_4.Application.Commands;
using lab_4.Domain.Exceptions;

namespace lab_4.Application.CommandHandlers;

public class FileRenameCommandHandler(CommandContext context) : ICommandHandler
{
    public bool CanHandle(string[] args)
    {
        return args.Length > 1 && args[0].Equals("file", StringComparison.CurrentCultureIgnoreCase) && args[1].Equals("rename", StringComparison.CurrentCultureIgnoreCase);
    }

    public ICommand Handle(string[] args)
    {
        if (args.Length < 4)
            throw new InvalidCommandException("Недостаточно аргументов для команды file rename.");

        var path = args[2];
        var newName = args[3];

        return new RenameFileCommand(context.FileSystem ?? throw new InvalidOperationException(), path, newName);
    }
}