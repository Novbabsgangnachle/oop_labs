using System.Collections.Generic;

namespace lab_4.Domain;

public interface IDirectory
{
    string Path { get; }

    IEnumerable<IDirectory> SubDirectories { get; }

    IEnumerable<IFile> Files { get; }
}