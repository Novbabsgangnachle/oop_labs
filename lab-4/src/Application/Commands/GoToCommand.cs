using System;
using lab_4.Domain;

namespace lab_4.Application.Commands;

public class GotoCommand(IFileSystem fileSystem, string path) : ICommand
{
    public void Execute()
    {
        fileSystem.Connect(path, "local");
        Console.WriteLine($"Перейдено к каталогу: {path}");
    }
}