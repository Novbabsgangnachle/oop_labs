namespace DefaultNamespace;

public class User
{
    private User(
        Guid id,
        string username,
        Collection<Account> accounts,
        UserRole userRole,
        Collection<Transaction> transactions,
        string password)
    {
        Id = id;
        UserRole = userRole;
        Username = username;
        Accounts = accounts;
        Transactions = transactions;
        Password = password;
    }

    public static UserBuilder Builder => new UserBuilder();

    public Guid Id { get; private set; }

    public string Username { get; private set; }

    public string Password { get; private set; }

    public Collection<Account> Accounts { get; }

    public Collection<Transaction> Transactions { get; }

    public UserRole UserRole { get; private set; }

    public void AddAccount(Account account)
    {
        Accounts.Add(account);
    }

    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
    }

    public class UserBuilder
    {
        private readonly Collection<Account> _accounts = new();

        private readonly Collection<Transaction> _transactions = new();

        private Guid _id = Guid.NewGuid();

        private string _username = string.Empty;

        private UserRole _userRole = UserRole.User;

        private string _password = string.Empty;

        public UserBuilder WithUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username should not be null or empty");
            _username = username;
            return this;
        }

        public UserBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public UserBuilder AddAccount(Account account)
        {
            _accounts.Add(account);
            return this;
        }

        public UserBuilder AddTransactions(Transaction transaction)
        {
            _transactions.Add(transaction);
            return this;
        }

        public UserBuilder WithUserRole(UserRole userRole)
        {
            _userRole = userRole;
            return this;
        }

        public User Build()
        {
            if (string.IsNullOrWhiteSpace(_username))
                throw new ArgumentException("Username should not be null or empty");
            return new User(
                _id,
                _username,
                _accounts,
                _userRole,
                _transactions,
                _password);
        }
    }
}