namespace DefaultNamespace;

public static class AccountsTableRenderer
{
    public static void Render(MenuContext ctx)
    {
        if (ctx.CurrentUser is null)
        {
            ctx.UI.Error("Пользователь не аутентифицирован");
            return;
        }

        var accounts = ctx.AccountService.GetAllAccountsByUserId(ctx.CurrentUser.Id);
        if (accounts is null || accounts.Count == 0)
        {
            ctx.UI.Error("У вас нет счетов");
            return;
        }

        var rows = accounts.Select(a => (a.Number, a.Balance));
        ctx.UI.RenderAccountsTable(rows);
    }
}