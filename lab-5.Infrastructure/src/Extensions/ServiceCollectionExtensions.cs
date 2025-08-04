namespace DefaultNamespace;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatform();
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        collection.AddScoped<IUserRepository, PostgresUserRepository>();
        collection.AddScoped<ITransactionRepository, PostgresTransactionRepository>();
        collection.AddScoped<IAccountRepository, PostgresAccountRepository>();

        return collection;
    }
}