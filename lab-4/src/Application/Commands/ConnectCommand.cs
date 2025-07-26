using System;
using lab_4.Domain;
using lab_4.Infrastructure.Factories;

namespace lab_4.Application.Commands;

public class ConnectCommand(IFileSystemFactory factory, string address, string mode, CommandContext context)
    : ICommand
{
    public void Execute()
    {
        var fileSystem = factory.CreateFileSystem(mode);
        fileSystem.Connect(address, mode);
        context.FileSystem = fileSystem;
        Console.WriteLine($"Подключено к {address} в режиме {mode}.");
    }
}