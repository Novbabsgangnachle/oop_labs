using System;
using lab_4.Application.Commands;
using lab_4.Domain.Exceptions;
using lab_4.Infrastructure.OutputServices;

namespace lab_4.Application.CommandHandlers;

public class FileShowCommandHandler(CommandContext context, IOutputService outputService) : ICommandHandler
{
    public bool CanHandle(string[] args)
    {
        return args.Length > 1 && args[0].Equals("file", StringComparison.CurrentCultureIgnoreCase) && args[1].Equals("show", StringComparison.CurrentCultureIgnoreCase);
    }

    public ICommand Handle(string[] args)
    {
        if (args.Length < 3)
            throw new InvalidCommandException("Недостаточно аргументов для команды file show.");

        var path = args[2];
        var mode = args.Length > 4 && args[3] == "-m" ? args[4] : "console";

        return new ShowFileCommand(context.FileSystem ?? throw new InvalidOperationException(), outputService, path, mode);
    }
}