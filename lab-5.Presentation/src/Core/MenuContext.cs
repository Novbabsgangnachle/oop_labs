namespace DefaultNamespace;

public sealed class MenuContext
{
    public IConsoleUI UI { get; }
    public IUserService UserService { get; }
    public IAccountService AccountService { get; }
    public AdminAuthenticationService AdminAuthService { get; }
    public ITransactionService TransactionService { get; }

    public User? CurrentUser { get; set; }

    public MenuContext(
        IConsoleUI ui,
        IUserService userService,
        IAccountService accountService,
        AdminAuthenticationService adminAuthService,
        ITransactionService transactionService)
    {
        UI = ui;
        UserService = userService;
        AccountService = accountService;
        AdminAuthService = adminAuthService;
        TransactionService = transactionService;
    }
}