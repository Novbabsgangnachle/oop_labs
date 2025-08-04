namespace DefaultNamespace;

public class TransactionService : ITransactionService
{
    public ITransactionRepository CurrentTransactionRepository { get; }

    public TransactionService(ITransactionRepository transactionRepository)
    {
        CurrentTransactionRepository = transactionRepository;
    }

    public ReadOnlyCollection<Transaction>? GetAllTransactionsByUserId(Guid userId)
    {
        return CurrentTransactionRepository.GetAllTransactionsByUserId(userId).Result;
    }

    public OperationResult AddTransaction(Transaction transaction)
    {
        return CurrentTransactionRepository.AddTransaction(transaction).Result;
    }

    public OperationResult UpdateTransaction(Guid id, Transaction transaction)
    {
        return CurrentTransactionRepository.UpdateTransaction(id, transaction).Result;
    }
}