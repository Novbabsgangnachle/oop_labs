using System;
using lab_4.Application.Commands;
using lab_4.Domain.Exceptions;
using lab_4.Infrastructure.OutputServices;

namespace lab_4.Application.CommandHandlers;

public class TreeListCommandHandler(CommandContext context, IOutputService outputService) : ICommandHandler
{
    public bool CanHandle(string[] args)
    {
        return args.Length > 0 && args[0].Equals("tree", StringComparison.CurrentCultureIgnoreCase) && args.Length > 1 && args[1].Equals("list", StringComparison.CurrentCultureIgnoreCase);
    }

    public ICommand Handle(string[] args)
    {
        var depth = 1;
        if (args.Length <= 3 || args[2] != "-d")
            return new ListTreeCommand(fileSystem: context.FileSystem ?? throw new InvalidOperationException(),
                outputService, depth);
        if (!int.TryParse(args[3], out depth))
            throw new InvalidCommandException("Неверный формат параметра depth.");

        return new ListTreeCommand(fileSystem: context.FileSystem ?? throw new InvalidOperationException(), outputService, depth);
    }
}