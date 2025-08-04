namespace DefaultNamespace;

[Migration(1, "Initial")]
public class InitialMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return """
               CREATE TABLE users
               (
                   user_id UUID PRIMARY KEY,
                   username TEXT NOT NULL,
                   password TEXT NOT NULL,
                   user_role TEXT NOT NULL
               );
                
               CREATE TABLE accounts
               (
                   account_id UUID PRIMARY KEY,
                   user_id UUID REFERENCES users(user_id),
                   number TEXT NOT NULL,
                   pin TEXT NOT NULL,
                   balance DECIMAL NOT NULL DEFAULT 0
               );

               CREATE TABLE transactions
               (
                   transaction_id UUID PRIMARY KEY,
                   account_id UUID REFERENCES accounts(account_id),
                   transaction_type TEXT NOT NULL,
                   amount DECIMAL NOT NULL,
                   transaction_date TIMESTAMP NOT NULL
               );
               """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return """
               DROP TABLE transactions;
               DROP TABLE users;
               DROP TABLE accounts;
               """;
    }
}