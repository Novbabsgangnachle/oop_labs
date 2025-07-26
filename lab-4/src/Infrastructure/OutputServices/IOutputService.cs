using lab_4.Domain;

namespace lab_4.Infrastructure.OutputServices;

public interface IOutputService
{
    void DisplayDirectory(IDirectory directory, int depth);

    void DisplayFile(IFile file, string mode);
}