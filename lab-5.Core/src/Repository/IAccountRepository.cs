namespace DefaultNamespace;

public interface IAccountRepository
{
    Task<Collection<Account>?> GetAllAccountsByUserId(Guid userId);

    Task<Account?> GetAccountById(Guid id);

    Task<OperationResult> AddAccount(Account account);

    Task<OperationResult> DeleteAccount(Account account);

    Task<OperationResult> UpdateAccount(Guid id, decimal amount);
}