namespace DefaultNamespace;

public sealed class SpectreConsoleUI : IConsoleUI
{
    public string AskString(string prompt) => AnsiConsole.Ask<string>(prompt);
    public string AskSecret(string prompt) =>
        AnsiConsole.Prompt(new TextPrompt<string>(prompt).PromptStyle("red").Secret());
    public decimal AskDecimal(string prompt) => AnsiConsole.Ask<decimal>(prompt);
    public bool Confirm(string question) => AnsiConsole.Confirm(question);

    public string Select(string title, IList<string> choices) =>
        AnsiConsole.Prompt(new SelectionPrompt<string>().Title(title).AddChoices(choices));

    public void Info(string text) => AnsiConsole.MarkupLine($"[yellow]{text}[/]");
    public void Success(string text) => AnsiConsole.MarkupLine($"[bold green]{text}[/]");
    public void Error(string text) => AnsiConsole.MarkupLine($"[bold red]{text}[/]");

    public void RenderAccountsTable(IEnumerable<(string Number, decimal Balance)> rows)
    {
        var table = new Table { Border = TableBorder.Rounded };
        table.AddColumn("Номер счета");
        table.AddColumn("Баланс");
        foreach (var r in rows)
            table.AddRow(r.Number, $"{r.Balance}$");
        AnsiConsole.Write(table);
    }

    public void RenderTransactionsTable(IEnumerable<(Guid Id, string AccountNumber, string Type, decimal Amount, DateTime Date)> rows)
    {
        var table = new Table { Border = TableBorder.Rounded };
        table.AddColumn("Id операции");
        table.AddColumn("Номер счета");
        table.AddColumn("Тип операции");
        table.AddColumn("Сумма");
        table.AddColumn("Дата");

        foreach (var r in rows)
            table.AddRow(r.Id.ToString(), r.AccountNumber, r.Type, r.Amount.ToString(CultureInfo.InvariantCulture),
                r.Date.ToString(CultureInfo.InvariantCulture));
        AnsiConsole.Write(table);
    }
}