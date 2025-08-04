namespace DefaultNamespace;

public class PostgresAccountRepository : IAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public PostgresAccountRepository(
        IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<Collection<Account>?> GetAllAccountsByUserId(Guid userId)
    {
        const string query = @"
                SELECT account_id, user_id,  number, pin, balance
                FROM accounts
                WHERE user_id = @UserId;";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider
                .GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserId", userId);

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var collection = new Collection<Account>();
                while (await reader.ReadAsync())
                {
                    collection.Add(new Account(
                        reader.GetGuid(0),
                        reader.GetGuid(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetDecimal(4)));
                }

                return collection;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<OperationResult> AddAccount(Account account)
    {
        const string query = @"
                INSERT INTO accounts (account_id, user_id, number, pin, balance)
                VALUES (@Id, @UserId, @Number, @Pin, @Balance);";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", account.Id);
            command.Parameters.AddWithValue("@UserId", account.UserId);
            command.Parameters.AddWithValue("@Number", account.Number);
            command.Parameters.AddWithValue("@Pin", account.Pin);
            command.Parameters.AddWithValue("@Balance", account.Balance);

            int rowAffected = await command.ExecuteNonQueryAsync();
            if (rowAffected > 0)
            {
                return new OperationResult.Success();
            }

            return new OperationResult.Failure("Failed to add account");
        }
        catch (Exception ex)
        {
            return new OperationResult.Failure("Failed to add account" + ex.Message);
        }
    }

    public async Task<OperationResult> DeleteAccount(Account account)
    {
        const string query = @"
                DELETE FROM accounts
                WHERE account_id = @Id;";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", account.Id);

            int rowAffected = await command.ExecuteNonQueryAsync();
            if (rowAffected > 0)
            {
                return new OperationResult.Success();
            }

            return new OperationResult.Failure("Failed to delete account");
        }
        catch (Exception ex)
        {
            return new OperationResult.Failure("Failed to delete account" + ex.Message);
        }
    }

    public async Task<OperationResult> UpdateAccount(Guid id, decimal amount)
    {
        const string query = @"
                UPDATE accounts
                SET balance = @Balance
                WHERE account_id = @Id;";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Balance", amount);

            int rowAffected = await command.ExecuteNonQueryAsync();
            if (rowAffected > 0)
            {
                return new OperationResult.Success();
            }

            return new OperationResult.Failure("Failed to update account");
        }
        catch (Exception ex)
        {
            return new OperationResult.Failure("Failed to update account" + ex.Message);
        }
    }

    public async Task<Account?> GetAccountById(Guid id)
    {
        const string query = @"
                SELECT account_id, user_id, number, pin, balance
                FROM accounts
                WHERE account_id = @Id;";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new AccountBuilder()
                    .WithId(reader.GetGuid(0))
                    .WithUserId(reader.GetGuid(1))
                    .WithNumber(reader.GetString(2))
                    .WithPin(reader.GetString(3))
                    .WithBalance(reader.GetDecimal(4))
                    .Build();
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}