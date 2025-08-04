namespace DefaultNamespace;

public sealed class WithdrawAction
{
    public void Execute(MenuContext ctx)
    {
        var account = AccountSelector.SelectUserAccount(ctx);
        if (account is null) return;

        var amount = ctx.UI.AskDecimal("Введите [green]сумму для снятия[/]:");
        var result = ctx.AccountService.WithdrawBalance(account.Id, amount);

        if (result is OperationResult.Success) ctx.UI.Success("Средства успешно сняты");
        else if (result is OperationResult.Failure f) ctx.UI.Error($"Ошибка: {f.Message}");
    }
}