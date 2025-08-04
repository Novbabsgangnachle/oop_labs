namespace DefaultNamespace;

public class Transaction
{
    public Guid Id { get; private set; }

    public Guid AccountId { get; private set; }

    public TransactionType TransactionType { get; private set; }

    public decimal Amount { get; private set; }

    public DateTime TransactionDate { get; private set; }

    public Transaction(Guid id, Guid accountId, TransactionType transactionType, decimal amount, DateTime transactionDate)
    {
        Id = id;
        AccountId = accountId;
        TransactionType = transactionType;
        Amount = amount;
        TransactionDate = transactionDate;
    }
}