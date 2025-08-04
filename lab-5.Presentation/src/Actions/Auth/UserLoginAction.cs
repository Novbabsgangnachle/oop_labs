namespace DefaultNamespace;

public sealed class UserLoginAction
{
    public void Execute()
    {
        var ctx = _ctx ?? throw new InvalidOperationException();
    }

    private static MenuContext? _ctx;
    public void Execute(MenuContext ctx)
    {
        _ctx = ctx;
        var username = ctx.UI.AskString("Введите [green]имя пользователя[/]:");
        var password = ctx.UI.AskSecret("Введите [green]пароль[/]:");

        var result = ctx.UserService.Login(username, password);
        if (result is OperationResult.Success)
        {
            ctx.CurrentUser = ctx.UserService.CurrentUserRepository.CurrentUser;
            ctx.UI.Success("Успешный вход");

            var userMenu = MenusFactory.CreateUserMenu();
            userMenu.Run(ctx);
        }
        else if (result is OperationResult.Failure f)
        {
            ctx.UI.Error($"Ошибка: {f.Message}");
        }
    }
}