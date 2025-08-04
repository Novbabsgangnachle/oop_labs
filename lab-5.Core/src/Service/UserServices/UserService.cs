namespace DefaultNamespace;

public class UserService : IUserService
{
    public IUserRepository CurrentUserRepository { get; }

    public UserService(IUserRepository userRepository)
    {
        CurrentUserRepository = userRepository;
    }

    public OperationResult Login(string username, string password)
    {
        string passHash = PasswordHasher.HashPassword(password);
        Task<User?> temp = CurrentUserRepository.GetUserByUsernameAndPassword(username, passHash);

        User? user = temp.Result;
        if (user is null)
            return new OperationResult.Failure("Not found");

        CurrentUserRepository.CurrentUser = user;
        return new OperationResult.Success();
    }

    public OperationResult Register(string username, string password)
    {
        Task<User?> temp = CurrentUserRepository.GetUserByUsername(username);

        if (temp.Result is not null)
        {
            return new OperationResult.Failure("Already exists");
        }

        if (PasswordValidator.IsPasswordValid(password) is OperationResult.Failure passRes)
        {
            return new OperationResult.Failure(passRes.Message);
        }

        if (UsernameValidator.IsUsernameValid(username) is OperationResult.Failure usernameValid)
        {
            return new OperationResult.Failure(usernameValid.Message);
        }

        string passHash = PasswordHasher.HashPassword(password);

        User user = User.Builder
            .WithUsername(username)
            .WithPassword(passHash)
            .Build();

        CurrentUserRepository.CurrentUser = user;

        return CurrentUserRepository.AddUser(user).Result;
    }
}