using System;
using lab_4.Domain;

namespace lab_4.Application.Commands;

public class DeleteFileCommand(IFileSystem fileSystem, string path) : ICommand
{
    public void Execute()
    {
        fileSystem.DeleteFile(path);
        Console.WriteLine($"Файл удален: {path}.");
    }
}