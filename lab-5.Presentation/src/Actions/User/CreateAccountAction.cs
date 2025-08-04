namespace DefaultNamespace;

public sealed class CreateAccountAction
{
    public void Execute(MenuContext ctx)
    {
        while (true)
        {
            var pin = ctx.UI.AskSecret("Введите [green]пин-код[/]:");

            if (ctx.CurrentUser is null)
            {
                ctx.UI.Error("Пользователь не аутентифицирован");
                return;
            }

            try
            {
                var account = ctx.AccountService.CreateAccount(pin, ctx.CurrentUser.Id);
                if (account is not null)
                {
                    ctx.UI.Success("Счет успешно создан");
                    ctx.UI.Info($"{account.Number}: {account.Balance}$");
                    return;
                }
            }
            catch (Exception e)
            {
                ctx.UI.Error("Ошибка: " + e.Message);
                if (!ctx.UI.Confirm("Хотите попробовать снова?"))
                {
                    ctx.UI.Info("Вы вернулись в главное меню");
                    return;
                }
            }
        }
    }
}