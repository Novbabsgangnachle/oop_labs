namespace DefaultNamespace;

public class TransactionBuilder
{
    private DateTime _date = DateTime.Now;

    private Guid _id = Guid.NewGuid();

    private Guid _accountId;

    private decimal _amount = 0;

    private TransactionType? _transactionType;

    public TransactionBuilder WithAccountId(Guid id)
    {
        _accountId = id;
        return this;
    }

    public TransactionBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public TransactionBuilder WithAmount(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");

        _amount = amount;
        return this;
    }

    public TransactionBuilder WithTransactionType(TransactionType transactionType)
    {
        _transactionType = transactionType;
        return this;
    }

    public TransactionBuilder WithDate(DateTime dateTime)
    {
        _date = dateTime;
        return this;
    }

    public Transaction Build()
    {
        if (_transactionType is null)
            throw new InvalidOperationException("TransactionType cannot be null");
        return new Transaction(
            _id,
            _accountId,
            (TransactionType)_transactionType,
            _amount,
            _date);
    }
}