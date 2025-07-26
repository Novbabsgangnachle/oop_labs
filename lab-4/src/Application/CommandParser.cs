using System;
using System.Collections.Generic;
using System.Linq;
using lab_4.Application.CommandHandlers;
using lab_4.Application.Commands;
using lab_4.Domain.Exceptions;

namespace lab_4.Application;

public class CommandParser(IEnumerable<ICommandHandler> handlers) : ICommandParser
{
    private readonly List<ICommandHandler> _handlers = handlers.ToList();

    public ICommand Parse(string input)
    {
        var args = SplitArguments(input);
        if (args.Length == 0)
            throw new InvalidCommandException("Пустая команда.");

        foreach (var handler in _handlers.Where(handler => handler.CanHandle(args)))
        {
            return handler.Handle(args);
        }

        throw new InvalidCommandException($"Неизвестная команда: {args[0]}");
    }

    private static string[] SplitArguments(string input)
    {
        return input.Split(' ');
    }
}