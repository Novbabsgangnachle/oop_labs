namespace DefaultNamespace;

public class PostgresTransactionRepository : ITransactionRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public PostgresTransactionRepository(
        IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<ReadOnlyCollection<Transaction>?> GetAllTransactionsByUserId(Guid userId)
    {
        const string query = @"
                SELECT t.transaction_id, t.account_id, t.transaction_type, t.amount, t.transaction_date
                FROM transactions t
                INNER JOIN accounts a ON t.account_id = a.account_id
                WHERE a.user_id = @UserId
                ORDER BY t.transaction_date DESC;";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider
                .GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserId", userId);

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            var collection = new Collection<Transaction>();
            while (await reader.ReadAsync())
            {
                TransactionBuilder temp1 = new TransactionBuilder().WithId(reader.GetGuid(0));
                temp1.WithAccountId(reader.GetGuid(1));
                temp1.WithTransactionType(Enum.Parse<TransactionType>(reader.GetString(2), ignoreCase: true));
                temp1.WithAmount(reader.GetDecimal(3));
                temp1.WithDate(reader.GetDateTime(4));

                Transaction transaction = temp1
                    .Build();
                collection.Add(transaction);
            }

            return collection.AsReadOnly();
        }
        catch
        {
            return null;
        }
    }

    public async Task<Collection<Transaction>?> GetAllTransactionsByAccountId(Guid accountId)
    {
        const string query = @"
                SELECT transaction_id, account_id, transaction_type, amount, transaction_date
                FROM transactions
                WHERE account_id = @AccountId
                ORDER BY transaction_date DESC;";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider
                .GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@AccountId", accountId);

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            var collection = new Collection<Transaction>();
            while (await reader.ReadAsync())
            {
                collection.Add(new TransactionBuilder()
                    .WithId(reader.GetGuid(0))
                    .WithAccountId(reader.GetGuid(1))
                    .WithTransactionType(Enum.Parse<TransactionType>(reader.GetString(2), ignoreCase: true))
                    .WithAmount(reader.GetDecimal(3))
                    .WithDate(reader.GetDateTime(4))
                    .Build());
            }

            return collection;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Transaction?> GetTransactionById(Guid id)
    {
        const string query = @"
                SELECT transaction_id, account_id, transaction_type, amount, transaction_date
                FROM transactions
                WHERE transaction_id = @Id;";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider
                .GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new TransactionBuilder()
                    .WithId(reader.GetGuid(0))
                    .WithAccountId(reader.GetGuid(1))
                    .WithTransactionType(Enum.Parse<TransactionType>(reader.GetString(2)))
                    .WithAmount(reader.GetDecimal(3))
                    .WithDate(reader.GetDateTime(4))
                    .Build();
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<OperationResult> AddTransaction(Transaction transaction)
    {
        const string query = @"
                INSERT INTO transactions (transaction_id, account_id, transaction_type, amount, transaction_date)
                VALUES (@Id, @AccountId, @TransactionType, @Amount, @TransactionDate);";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider
                .GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", transaction.Id);
            command.Parameters.AddWithValue("@AccountId", transaction.AccountId);
            command.Parameters.AddWithValue(
                "@TransactionType",
                transaction.TransactionType.ToString().ToLower(System.Globalization.CultureInfo.CurrentCulture));
            command.Parameters.AddWithValue("@Amount", transaction.Amount);
            command.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);

            int rows = await command.ExecuteNonQueryAsync();
            if (rows > 0)
            {
                return new OperationResult.Success();
            }

            return new OperationResult.Failure("Failed to add transaction");
        }
        catch (Exception ex)
        {
            return new OperationResult.Failure("Failed to add transaction" + ex.Message);
        }
    }

    public async Task<OperationResult> RemoveTransaction(Transaction transaction)
    {
        const string query = @"
                DELETE FROM transactions
                WHERE transaction_id = @Id;";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider
                .GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", transaction.Id);

            int rows = await command.ExecuteNonQueryAsync();
            if (rows > 0)
            {
                return new OperationResult.Success();
            }

            return new OperationResult.Failure("Failed to delete transaction");
        }
        catch (Exception ex)
        {
            return new OperationResult.Failure("Failed to delete transaction" + ex.Message);
        }
    }

    public async Task<OperationResult> UpdateTransaction(Guid id, Transaction transaction)
    {
        const string query = @"
                UPDATE transactions
                SET account_id = @AccountId,
                    transaction_type = @TransactionType,
                    amount = @Amount,
                    transaction_date = @TransactionDate
                WHERE transaction_id = @Id;";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider
                .GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@account_id", transaction.AccountId);
            command.Parameters.AddWithValue(
                "@transaction_type",
                transaction.TransactionType.ToString().ToLower(System.Globalization.CultureInfo.CurrentCulture));
            command.Parameters.AddWithValue("@amount", transaction.Amount);
            command.Parameters.AddWithValue("@transaction_date", transaction.TransactionDate);

            int rows = await command.ExecuteNonQueryAsync();
            if (rows > 0)
            {
                return new OperationResult.Success();
            }

            return new OperationResult.Failure("Failed to update transaction");
        }
        catch (Exception ex)
        {
            return new OperationResult.Failure("Failed to update transaction" + ex.Message);
        }
    }
}