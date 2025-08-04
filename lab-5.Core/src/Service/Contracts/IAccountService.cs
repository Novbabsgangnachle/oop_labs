namespace DefaultNamespace;

public interface IAccountService
{
    IAccountRepository CurrentAccountRepository { get; }

    ITransactionService CurrentTransactionService { get; }

    Account? CreateAccount(string pinCode, Guid userId);

    OperationResult ReplenishBalance(Guid accountId, decimal amount);

    OperationResult WithdrawBalance(Guid accountId, decimal amount);

    Collection<Account>? GetAllAccountsByUserId(Guid userId);

    decimal? ShowBalance(Guid accountId);
}