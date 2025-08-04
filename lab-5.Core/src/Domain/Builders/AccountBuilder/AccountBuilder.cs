namespace DefaultNamespace;

public class AccountBuilder
{
    private Guid _id = Guid.NewGuid();

    private Guid _userId;

    private decimal _balance;

    private string _number = string.Empty;

    private string _pin = string.Empty;

    public AccountBuilder WithNumber(string number)
    {
        if (string.IsNullOrEmpty(number))
            throw new ArgumentException("Number can`t be empty");

        if (!number.All(char.IsDigit))
            throw new ArgumentException("Number cant` contains not digits");

        _number = number;
        return this;
    }

    public AccountBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public AccountBuilder WithPin(string pin)
    {
        _pin = pin;

        return this;
    }

    public AccountBuilder WithUserId(Guid userId)
    {
        _userId = userId;
        return this;
    }

    public AccountBuilder WithBalance(decimal balance)
    {
        if (balance < 0)
            throw new ArgumentException("Balance can`t be negative");

        _balance = balance;
        return this;
    }

    public Account Build()
    {
        return new Account(
            _id,
            _userId,
            _number,
            _pin,
            _balance);
    }
}