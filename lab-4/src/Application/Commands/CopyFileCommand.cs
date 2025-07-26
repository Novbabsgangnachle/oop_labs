using System;
using lab_4.Domain;

namespace lab_4.Application.Commands;

public class CopyFileCommand(IFileSystem fileSystem, string sourcePath, string destinationPath)
    : ICommand
{
    public void Execute()
    {
        fileSystem.CopyFile(sourcePath, destinationPath);
        Console.WriteLine($"Файл скопирован из {sourcePath} в {destinationPath}.");
    }
}