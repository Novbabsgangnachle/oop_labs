using System;
using System.IO;

namespace lab_4.Domain;

public class LocalFileSystem : IFileSystem
{
    public string? CurrentPath { get; private set; } = null;

    public void Connect(string adress, string mode)
    {
        if (!mode.Equals("local", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new NotSupportedException("Только локальная файловая система поддерживается.");
        }

        if (!Directory.Exists(adress))
        {
            throw new DirectoryNotFoundException($"Директория не найдена: {adress}");
        }

        CurrentPath = Path.GetFullPath(adress);
    }

    public void Disconnect()
    {
        CurrentPath = null;
    }

    public IDirectory GetDirectory(string? path)
    {
        var fullPath = GetFullPath(path);
        if (!Directory.Exists(fullPath))
            throw new DirectoryNotFoundException($"Директория не найдена: {fullPath}");
        var directoryInfo = new DirectoryInfo(fullPath);
        return new DirectoryEntity(directoryInfo);
    }

    public IFile GetFile(string? path)
    {
        var fullPath = GetFullPath(path);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"Файл не найден {fullPath}");
        var fileInfo = new FileInfo(fullPath);
        return new FileEntity(fileInfo);
    }

    public void MoveFile(string? sourcePath, string? destinationPath)
    {
        var srcFullPath = GetFullPath(sourcePath);
        var destFullPath = GetFullPath(destinationPath);

        if (!File.Exists(srcFullPath))
            throw new FileNotFoundException($"Файл не найден {srcFullPath}");

        if (!Directory.Exists(destFullPath))
            throw new DirectoryNotFoundException($"Директория не найдена: {destFullPath}");
        if (File.Exists(destFullPath))
            throw new IOException($"Файл уже существует: {destFullPath}");
        File.Move(srcFullPath, destFullPath);
    }

    public void CopyFile(string? sourcePath, string? destinationPath)
    {
        var srcFullPath = GetFullPath(sourcePath);
        var destFullPath = GetFullPath(destinationPath);

        if (!File.Exists(srcFullPath))
            throw new FileNotFoundException($"Файл не найден {srcFullPath}");

        if (!Directory.Exists(destFullPath))
            throw new DirectoryNotFoundException($"Директория не найдена: {destFullPath}");
        if (File.Exists(destFullPath))
            throw new IOException($"Файл уже существует: {destFullPath}");
        File.Copy(srcFullPath, destFullPath);
    }

    public void DeleteFile(string? path)
    {
        var fullPath = GetFullPath(path);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"Файл не найден: {fullPath}");
        File.Delete(fullPath);
    }

    public void RenameFile(string? path, string newName)
    {
        var fullPath = GetFullPath(path);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"Файл не найден: {fullPath}");

        var directory = Path.GetDirectoryName(fullPath) ?? throw new InvalidOperationException();
        var newFullPath = Path.Combine(directory, newName);

        if (File.Exists(newFullPath))
            throw new IOException($"Файл с именем {newName} уже существует в директории.");
        File.Move(fullPath, newFullPath);
    }

    private string GetFullPath(string? path)
    {
        if (Path.IsPathRooted(path))
            return Path.GetFullPath(path);
        if (string.IsNullOrEmpty(CurrentPath))
            throw new InvalidOperationException("Не подключена файловая система.");
        return Path.GetFullPath(Path.Combine(CurrentPath, path ?? throw new ArgumentNullException(nameof(path))));
    }
}