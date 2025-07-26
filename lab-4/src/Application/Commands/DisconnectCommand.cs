using System;
using lab_4.Domain;

namespace lab_4.Application.Commands;

public class DisconnectCommand(CommandContext context) : ICommand
{
    public void Execute()
    {
        var fileSystem = context.FileSystem;
        if (fileSystem == null)
        {
            Console.WriteLine("Нет активного подключения.");
            return;
        }

        fileSystem.Disconnect();
        context.FileSystem = null;
        Console.WriteLine("Отключено от файловой системы.");
    }
}