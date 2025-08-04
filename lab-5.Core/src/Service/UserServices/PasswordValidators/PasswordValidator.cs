namespace DefaultNamespace;

public static class PasswordValidator
{
    private const int MinLength = 8;

    private static readonly Regex UpperCaseRegex = new Regex(@"[A-Z]", RegexOptions.CultureInvariant);
    private static readonly Regex LowerCaseRegex = new Regex(@"[a-z]", RegexOptions.CultureInvariant);
    private static readonly Regex DigitRegex = new Regex(@"\d", RegexOptions.CultureInvariant);
    private static readonly Regex SpecialCharRegex = new Regex(@"[!@#$()%^&*]", RegexOptions.CultureInvariant);

    public static OperationResult IsPasswordValid(string password)
    {
        if (password.Length < MinLength)
            return new OperationResult.Failure($"Password must be {MinLength} chars length");

        if (!UpperCaseRegex.IsMatch(password))
            return new OperationResult.Failure("The password must contain at least one capital letter");

        if (!LowerCaseRegex.IsMatch(password))
            return new OperationResult.Failure("The password must contain at least one lowercase letter");

        if (!DigitRegex.IsMatch(password))
            return new OperationResult.Failure("The password must contain at least one number");

        if (!SpecialCharRegex.IsMatch(password))
            return new OperationResult.Failure("The password must contain at least one special symbol (!@#$()%^&*)");

        return new OperationResult.Success();
    }
}