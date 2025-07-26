using System;
using lab_4.Domain;

namespace lab_4.Application.Commands;

public class RenameFileCommand(IFileSystem fileSystem, string path, string newName) : ICommand
{
    public void Execute()
    {
        fileSystem.RenameFile(path, newName);
        Console.WriteLine($"Файл переименован: {path} -> {newName}.");
    }
}