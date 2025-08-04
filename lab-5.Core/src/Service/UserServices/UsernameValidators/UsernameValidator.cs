namespace DefaultNamespace;

public static class UsernameValidator
{
    private const int MinLength = 3;

    private const int MaxLength = 20;

    private static readonly Regex Pattern =
        new Regex(@"^[A-Za-zА-Яа-яёЁ_][A-Za-zА-Яа-яёЁ0-9_]{2,19}$", RegexOptions.CultureInvariant);

    public static OperationResult IsUsernameValid(string username)
    {
        if (username.Length < MinLength || username.Length > 20)
        {
            return new OperationResult.Failure(
                $"The length of the username should be from {MinLength} to {MaxLength} characters");
        }

        if (!Pattern.IsMatch(username))
            return new OperationResult.Failure("Username contains invalid characters or starts with a number");

        return new OperationResult.Success();
    }
}