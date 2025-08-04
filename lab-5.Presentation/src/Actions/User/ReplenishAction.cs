namespace DefaultNamespace;

public sealed class ReplenishAction
{
    public void Execute(MenuContext ctx)
    {
        var account = AccountSelector.SelectUserAccount(ctx);
        if (account is null) return;

        var amount = ctx.UI.AskDecimal("Введите [green]сумму для пополнения[/]:");
        var result = ctx.AccountService.ReplenishBalance(account.Id, amount);

        if (result is OperationResult.Success) ctx.UI.Success("Счет успешно пополнен");
        else if (result is OperationResult.Failure f) ctx.UI.Error($"Ошибка: {f.Message}");
    }
}