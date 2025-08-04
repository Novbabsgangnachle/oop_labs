namespace DefaultNamespace;

public static class AccountNumberGenerator
{
    public static string Generate()
    {
        int number = RandomNumberGenerator.GetInt32(100000000, 999999999);
        return number.ToString();
    }
}