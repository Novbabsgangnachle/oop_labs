namespace DefaultNamespace;

public static class PinHasher
{
    public static string HashPin(string pin)
    {
        if (string.IsNullOrEmpty(pin))
            throw new ArgumentException("Пароль не может быть пустым.", nameof(pin));

        using var sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(pin);
        byte[] hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}