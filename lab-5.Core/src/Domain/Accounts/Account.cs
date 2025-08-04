namespace DefaultNamespace;

public class Account
{
    public Account(Guid id, Guid userId, string number, string pin, decimal balance = 0)
    {
        Id = id;
        UserId = userId;
        Number = number;
        Pin = pin;
        Balance = balance;
    }

    public string Number { get; private set; }

    public string Pin { get; private set; }

    public decimal Balance { get; private set; }

    public Guid Id { get; private set; }

    public Guid UserId { get; }

    public void Replenishment(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Value should be positive");

        Balance += value;
    }

    public void Withdrawal(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Value should be positive");

        if (value > Balance)
            throw new ArgumentException("Value should be less than balance");

        Balance -= value;
    }
}