namespace DefaultNamespace;

public class AccountService : IAccountService
{
    public IAccountRepository CurrentAccountRepository { get; }

    public ITransactionService CurrentTransactionService { get; }

    public AccountService(
        IAccountRepository accountRepository,
        ITransactionService transactionService)
    {
        CurrentAccountRepository = accountRepository;
        CurrentTransactionService = transactionService;
    }

    public Account? CreateAccount(string pinCode, Guid userId)
    {
        if (pinCode.Length != 4 || !pinCode.All(char.IsDigit))
            throw new ArgumentException("Invalid pinCode");

        string number = AccountNumberGenerator.Generate();

        string pinHash = PinHasher.HashPin(pinCode);

        Account newAccount = new AccountBuilder()
            .WithNumber(number)
            .WithUserId(userId)
            .WithPin(pinHash)
            .Build();
        if (CurrentAccountRepository.AddAccount(newAccount).Result is OperationResult.Failure)
            throw new Exception("Cannot add account to repo");
        return newAccount;
    }

    public OperationResult ReplenishBalance(Guid accountId, decimal amount)
    {
        if (amount < 0)
            return new OperationResult.Failure("Amount cannot be negative");

        Task<Account?> accountTask = CurrentAccountRepository.GetAccountById(accountId);
        Account? account = accountTask.Result;

        if (account == null)
            return new OperationResult.Failure("Account not found");

        account.Replenishment(amount);

        Transaction newTransaction = new TransactionBuilder()
            .WithTransactionType(TransactionType.Replenishment)
            .WithAccountId(account.Id)
            .WithAmount(amount)
            .Build();

        OperationResult result = CurrentTransactionService.AddTransaction(newTransaction);
        if (result is OperationResult.Failure)
            return result;

        return CurrentAccountRepository.UpdateAccount(account.Id, account.Balance).Result;
    }

    public OperationResult WithdrawBalance(Guid accountId, decimal amount)
    {
        if (amount < 0)
            return new OperationResult.Failure("Amount cannot be negative");

        Task<Account?> accountTask = CurrentAccountRepository.GetAccountById(accountId);
        Account? account = accountTask.Result;

        if (account == null)
            return new OperationResult.Failure("Account not found");

        if (amount > account.Balance)
            return new OperationResult.Failure("Amount cannot be greater than current account");

        account.Withdrawal(amount);

        Transaction newTransaction = new TransactionBuilder()
            .WithTransactionType(TransactionType.Withdrawal)
            .WithAccountId(account.Id)
            .WithAmount(amount)
            .Build();

        OperationResult result = CurrentTransactionService.AddTransaction(newTransaction);
        if (result is OperationResult.Failure)
            return result;

        return CurrentAccountRepository.UpdateAccount(account.Id, account.Balance).Result;
    }

    public Collection<Account>? GetAllAccountsByUserId(Guid userId)
    {
        return CurrentAccountRepository.GetAllAccountsByUserId(userId).Result;
    }

    public decimal? ShowBalance(Guid accountId)
    {
        Task<Account?> accountTask = CurrentAccountRepository.GetAccountById(accountId);
        Account? account = accountTask.Result;
        if (account != null)
            return account.Balance;
        return null;
    }
}