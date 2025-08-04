namespace DefaultNamespace;


public interface ITransactionService
{
    ITransactionRepository CurrentTransactionRepository { get; }

    ReadOnlyCollection<Transaction>? GetAllTransactionsByUserId(Guid userId);

    OperationResult AddTransaction(Transaction transaction);

    OperationResult UpdateTransaction(Guid id, Transaction transaction);
}