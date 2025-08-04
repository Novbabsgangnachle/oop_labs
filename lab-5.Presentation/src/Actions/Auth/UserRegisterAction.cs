namespace DefaultNamespace;

public sealed class UserRegisterAction
{
    public void Execute(MenuContext ctx)
    {
        while (true)
        {
            var username = ctx.UI.AskString("Введите [green]имя пользователя[/]:");
            var password = ctx.UI.AskSecret("Введите [green]пароль[/]:");

            var result = ctx.UserService.Register(username, password);
            if (result is OperationResult.Success)
            {
                ctx.UI.Success("Регистрация прошла успешно");
                return;
            }

            if (result is OperationResult.Failure f)
            {
                ctx.UI.Error($"Ошибка: {f.Message}");
                if (!ctx.UI.Confirm("Хотите попробовать снова?"))
                {
                    ctx.UI.Info("Вы вернулись в главное меню");
                    return;
                }
            }
        }
    }
}