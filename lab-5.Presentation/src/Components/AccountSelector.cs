namespace DefaultNamespace;

public static class AccountSelector
{
    public static Account? SelectUserAccount(MenuContext ctx)
    {
        if (ctx.CurrentUser is null)
        {
            ctx.UI.Error("Пользователь не аутентифицирован");
            return null;
        }

        var accounts = ctx.AccountService.GetAllAccountsByUserId(ctx.CurrentUser.Id);
        if (accounts is null || accounts.Count == 0)
        {
            ctx.UI.Error("У вас нет счетов");
            return null;
        }

        var choices = accounts.Select(a => $"{a.Number} (Баланс: {a.Balance})").ToList();
        var selected = ctx.UI.Select("Выберите счет:", choices);
        var number = selected.Split(' ')[0];

        return accounts.FirstOrDefault(a => a.Number == number);
    }
}