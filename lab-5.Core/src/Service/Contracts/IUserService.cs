namespace DefaultNamespace;

public interface IUserService
{
    public IUserRepository CurrentUserRepository { get; }

    OperationResult Login(string username, string password);

    OperationResult Register(string username, string password);
}