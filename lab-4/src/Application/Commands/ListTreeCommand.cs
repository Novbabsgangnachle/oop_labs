using System;
using lab_4.Domain;
using lab_4.Infrastructure.OutputServices;

namespace lab_4.Application.Commands;


public class ListTreeCommand(IFileSystem fileSystem, IOutputService outputService, int depth)
    : ICommand
{
    public void Execute()
    {
        var currentDirectory = fileSystem.GetDirectory(fileSystem is LocalFileSystem localFs ? localFs.CurrentPath : throw new InvalidOperationException("Unsupported FileSystem"));
        outputService.DisplayDirectory(currentDirectory, depth);
    }
}