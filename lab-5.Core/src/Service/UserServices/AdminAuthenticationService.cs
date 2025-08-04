namespace DefaultNamespace;

public class AdminAuthenticationService
{
    private readonly string? _adminPassword;

    public AdminAuthenticationService(string? adminPassword)
    {
        _adminPassword = adminPassword;
    }

    public OperationResult Authenticate(string password)
    {
        if (password == _adminPassword)
            return new OperationResult.Success();
        return new OperationResult.Failure("Not found");
    }
}