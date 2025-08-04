namespace DefaultNamespace;

public interface IConsoleUI
{
    string AskString(string prompt);
    string AskSecret(string prompt);
    decimal AskDecimal(string prompt);
    bool Confirm(string question);
    string Select(string title, IList<string> choices);

    void Info(string text);
    void Success(string text);
    void Error(string text);

    void RenderAccountsTable(IEnumerable<(string Number, decimal Balance)> rows);
    void RenderTransactionsTable(IEnumerable<(Guid Id, string AccountNumber, string Type, decimal Amount, DateTime Date)> rows);
}