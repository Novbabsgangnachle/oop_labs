namespace DefaultNamespace;

public sealed class AdminAuthenticateAction
{
    public void Execute(MenuContext ctx)
    {
        var password = ctx.UI.AskSecret("Введите [red]системный пароль[/]:");
        var auth = ctx.AdminAuthService.Authenticate(password);

        if (auth is OperationResult.Success)
        {
            ctx.UI.Success("Успешная аутентификация администратора");

            var adminMenu = new Menu(
                "Администратор: Выберите действие:",
                new[] { new MenuItem("Выход", Menu.Back) });
            adminMenu.Run(ctx);
        }
        else if (auth is OperationResult.Failure f)
        {
            ctx.UI.Error($"Ошибка: {f.Message}");
            Environment.Exit(0);
        }
    }
}

