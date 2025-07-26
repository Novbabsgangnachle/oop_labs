using lab_4.Domain;

namespace lab_4.Application;

public class CommandContext
{
    public IFileSystem? FileSystem { get; set; } = null;
}