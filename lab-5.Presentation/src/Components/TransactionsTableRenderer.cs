namespace DefaultNamespace;

public static class TransactionsTableRenderer
{
    public static void Render(MenuContext ctx)
    {
        if (ctx.CurrentUser is null)
        {
            ctx.UI.Error("Пользователь не аутентифицирован");
            return;
        }

        var transactions = ctx.TransactionService.GetAllTransactionsByUserId(ctx.CurrentUser.Id);
        if (transactions == null || transactions.Count == 0)
        {
            ctx.UI.Error("У вас нет счетов");
            return;
        }

        var rows = new List<(Guid, string, string, decimal, DateTime)>();
        foreach (var t in transactions)
        {
            var acc = ctx.AccountService.CurrentAccountRepository.GetAccountById(t.AccountId).Result;
            var accNumber = acc?.Number ?? string.Empty;
            rows.Add((t.Id, accNumber, t.TransactionType.ToString(), t.Amount, t.TransactionDate));
        }

        ctx.UI.RenderTransactionsTable(rows);
    }
}