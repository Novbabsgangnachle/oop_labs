namespace DefaultNamespace;

public static class MenusFactory
{
    public static Menu CreateUserMenu()
    {
        return new Menu(
            "Выберите действие:",
            new[]
            {
                new MenuItem("Снять деньги",       () => _withdraw.Execute(_ctx!)),
                new MenuItem("Пополнить счет",     () => _replenish.Execute(_ctx!)),
                new MenuItem("Показать баланс",    () => _showBalance.Execute(_ctx!)),
                new MenuItem("Создать счет",       () => _createAccount.Execute(_ctx!)),
                new MenuItem("Показать все счета", () => _viewAccounts.Execute(_ctx!)),
                new MenuItem("Показать историю операций", () => _viewTransactions.Execute(_ctx!)),
                new MenuItem("Выйти",              () => _logout.Execute(_ctx!))
            });
    }

    private static readonly WithdrawAction _withdraw = new();
    private static readonly ReplenishAction _replenish = new();
    private static readonly ShowBalanceAction _showBalance = new();
    private static readonly CreateAccountAction _createAccount = new();
    private static readonly ViewAccountsAction _viewAccounts = new();
    private static readonly ViewTransactionsAction _viewTransactions = new();
    private static readonly LogoutAction _logout = new();

    private static MenuContext? _ctx;
    public static void Run(this Menu menu, MenuContext ctx)
    {
        _ctx = ctx;
        menu.Run(ctx);
    }
}