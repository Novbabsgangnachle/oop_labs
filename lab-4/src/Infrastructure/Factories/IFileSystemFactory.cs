using lab_4.Domain;

namespace lab_4.Infrastructure.Factories;

public interface IFileSystemFactory
{
    IFileSystem CreateFileSystem(string mode);
}