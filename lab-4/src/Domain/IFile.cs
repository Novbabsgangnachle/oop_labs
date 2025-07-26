namespace lab_4.Domain;

public interface IFile
{
    string Path { get; }

    string Content { get; }
}