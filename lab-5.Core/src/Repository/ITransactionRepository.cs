namespace DefaultNamespace;

public interface ITransactionRepository
{
    Task<ReadOnlyCollection<Transaction>?> GetAllTransactionsByUserId(Guid userId);

    Task<Collection<Transaction>?> GetAllTransactionsByAccountId(Guid accountId);

    Task<Transaction?> GetTransactionById(Guid id);

    Task<OperationResult> AddTransaction(Transaction transaction);

    Task<OperationResult> RemoveTransaction(Transaction transaction);

    Task<OperationResult> UpdateTransaction(Guid id, Transaction transaction);
}