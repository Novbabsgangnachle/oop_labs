namespace DefaultNamespace;

public sealed class LogoutAction
{
    public void Execute(MenuContext ctx)
    {
        ctx.CurrentUser = null;
        ctx.UI.Info("Вы вышли из системы");
        Menu.Back();
    }
}