namespace DefaultNamespace;

public sealed class ShowBalanceAction
{
    public void Execute(MenuContext ctx)
    {
        var account = AccountSelector.SelectUserAccount(ctx);
        if (account is null) return;

        var balance = ctx.AccountService.ShowBalance(account.Id);
        if (balance.HasValue) ctx.UI.Success($"Текущий баланс: {balance.Value}");
        else ctx.UI.Error("Ошибка: Счет не найден");
    }
}