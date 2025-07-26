using System;
using lab_4.Domain;

namespace lab_4.Infrastructure.Factories;


public class FileSystemFactory : IFileSystemFactory
{
    public IFileSystem CreateFileSystem(string mode)
    {
        return mode.ToLower(System.Globalization.CultureInfo.CurrentCulture) switch
        {
            "local" => new LocalFileSystem(),
            _ => throw new NotSupportedException($"Файловая система режима {mode} не поддерживается."),
        };
    }
}