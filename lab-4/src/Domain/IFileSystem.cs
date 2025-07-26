namespace lab_4.Domain;

public interface IFileSystem
{
    void Connect(string adress, string mode);

    void Disconnect();

    IDirectory GetDirectory(string? path);

    IFile GetFile(string? path);

    void MoveFile(string? sourcePath, string? destinationPath);

    void CopyFile(string? sourcePath, string? destinationPath);

    void DeleteFile(string? path);

    void RenameFile(string? path, string newName);
}