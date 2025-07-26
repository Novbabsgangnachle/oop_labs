using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab_4.Domain;

public class DirectoryEntity : IDirectory
{
    public string Path { get; }

    public IEnumerable<IDirectory> SubDirectories { get; }

    public IEnumerable<IFile> Files { get; }

    public DirectoryEntity(DirectoryInfo directoryInfo)
    {
        Path = directoryInfo.FullName;
        SubDirectories = directoryInfo.GetDirectories().Select(d => new DirectoryEntity(d));
        Files = directoryInfo.GetFiles().Select<FileInfo, IFile>(f => new FileEntity(f));
    }
}