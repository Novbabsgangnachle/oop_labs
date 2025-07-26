using System;
using lab_4.Domain;

namespace lab_4.Application.Commands;


public class MoveFileCommand(IFileSystem fileSystem, string sourcePath, string destinationPath)
    : ICommand
{
    public void Execute()
    {
        fileSystem.MoveFile(sourcePath, destinationPath);
        Console.WriteLine($"Файл перемещен из {sourcePath} в {destinationPath}.");
    }
}