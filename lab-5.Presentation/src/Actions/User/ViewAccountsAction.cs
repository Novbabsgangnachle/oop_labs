namespace DefaultNamespace;

public sealed class ViewAccountsAction
{
    public void Execute(MenuContext ctx) => AccountsTableRenderer.Render(ctx);
}