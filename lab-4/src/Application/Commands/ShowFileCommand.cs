using lab_4.Domain;
using lab_4.Infrastructure.OutputServices;

namespace lab_4.Application.Commands;

public class ShowFileCommand(IFileSystem fileSystem, IOutputService outputService, string path, string mode)
    : ICommand
{
    public void Execute()
    {
        var file = fileSystem.GetFile(path);
        outputService.DisplayFile(file, mode);
    }
}