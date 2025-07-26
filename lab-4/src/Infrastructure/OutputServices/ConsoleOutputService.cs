using System;
using System.IO;
using System.Linq;
using lab_4.Domain;

namespace lab_4.Infrastructure.OutputServices;

public class ConsoleOutputService(OutputConfig config) : IOutputService
{
    public void DisplayFile(IFile file, string mode)
    {
        if (mode.Equals("console", StringComparison.CurrentCultureIgnoreCase))
        {
            Console.WriteLine(file.Content);
        }
        else
        {
            throw new NotSupportedException($"Режим вывода {mode} не поддерживается.");
        }
    }

    public void DisplayDirectory(IDirectory directory, int depth)
    {
        DisplayDirectoryRecursive(directory, depth, 0);
    }

    private void DisplayDirectoryRecursive(IDirectory directory, int maxDepth, int currentDepth)
    {
        if (currentDepth > maxDepth) return;

        var indent = string.Concat(Enumerable.Repeat(config.Indentation, currentDepth));
        Console.WriteLine($"{indent}{config.DirectorySymbol} {Path.GetFileName(directory.Path)}");

        foreach (var subDir in directory.SubDirectories)
        {
            DisplayDirectoryRecursive(subDir, maxDepth, currentDepth + 1);
        }

        foreach (var file in directory.Files)
        {
            Console.WriteLine($"{indent}{config.FileSymbol} {Path.GetFileName(file.Path)}");
        }
    }
}