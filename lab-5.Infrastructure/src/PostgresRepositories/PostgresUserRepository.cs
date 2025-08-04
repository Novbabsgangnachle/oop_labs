namespace DefaultNamespace;

public class PostgresUserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public PostgresUserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public User? CurrentUser { get; set; }

    public async Task<User?> GetUserByUsernameAndPassword(string username, string passHash)
    {
        const string query = @"
                SELECT user_id, username, password, user_role
                FROM users
                WHERE username = @username AND password = @passHash";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@passHash", passHash);

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                User user = new User.UserBuilder()
                    .WithId(reader.GetGuid(0))
                    .WithUsername(reader.GetString(1))
                    .WithPassword(reader.GetString(2))
                    .WithUserRole(Enum.Parse<UserRole>(
                        reader.GetString(3),
                        ignoreCase: true))
                    .Build();

                return user;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        const string query = @"
                SELECT user_id, username, password, user_role
                FROM users
                WHERE username = @username";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                User user = new User.UserBuilder()
                    .WithId(reader.GetGuid(0))
                    .WithUsername(reader.GetString(1))
                    .WithPassword(reader.GetString(2))
                    .WithUserRole(Enum.Parse<UserRole>(
                        reader.GetString(3),
                        ignoreCase: true))
                    .Build();

                return user;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<OperationResult> UpdateUser(User? user)
    {
        if (user == null)
            return new OperationResult.Failure("User cannot be null");

        const string query = @"UPDATE users
                           SET username = @username, password = @password, user_role = @userRole
                           WHERE user_id = @id";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue(
                "@userRole",
                user.UserRole.ToString().ToLower(System.Globalization.CultureInfo.CurrentCulture));

            int rowAffected = await command.ExecuteNonQueryAsync();
            if (rowAffected > 0)
            {
                return new OperationResult.Success();
            }

            return new OperationResult.Failure("No rows were updated");
        }
        catch (Exception ex)
        {
            return new OperationResult.Failure(ex.Message);
        }
    }

    public async Task<OperationResult> AddUser(User? user)
    {
        if (user == null)
            return new OperationResult.Failure("User cannot be null");

        const string query = @"INSERT INTO users
                           (user_id, username, password, user_role) VALUES
                           (@Id, @Username, @Password, @UserRole)";

        try
        {
            using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue(
                "@UserRole",
                user.UserRole.ToString().ToLower(System.Globalization.CultureInfo.CurrentCulture));

            int rowAffected = await command.ExecuteNonQueryAsync();
            if (rowAffected > 0)
            {
                return new OperationResult.Success();
            }

            return new OperationResult.Failure("No rows were inserted");
        }
        catch (Exception ex)
        {
            return new OperationResult.Failure(ex.Message);
        }
    }
}