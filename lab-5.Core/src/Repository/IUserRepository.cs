namespace DefaultNamespace;

public interface IUserRepository
{
    public User? CurrentUser { get; set; }

    Task<User?> GetUserByUsernameAndPassword(string username, string passHash);

    Task<User?> GetUserByUsername(string username);

    Task<OperationResult> UpdateUser(User? user);

    Task<OperationResult> AddUser(User? user);
}