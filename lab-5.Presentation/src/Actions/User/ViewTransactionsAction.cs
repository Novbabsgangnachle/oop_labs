namespace DefaultNamespace;


public sealed class ViewTransactionsAction
{
    public void Execute(MenuContext ctx) => TransactionsTableRenderer.Render(ctx);
}