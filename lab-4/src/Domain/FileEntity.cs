using System.IO;

namespace lab_4.Domain;

public class FileEntity(FileInfo fileInfo) : IFile
{
    public string Path { get; } = fileInfo.FullName;

    public string Content => File.ReadAllText(Path);
}