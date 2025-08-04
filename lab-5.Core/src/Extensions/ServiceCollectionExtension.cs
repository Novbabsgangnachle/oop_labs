namespace DefaultNamespace;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<ITransactionService, TransactionService>();

        collection.AddScoped<IAccountService, AccountService>();

        collection.AddSingleton<AdminAuthenticationService>(sp =>
        {
            string? administatorPassword = configuration["AdminPassword"];
            return new AdminAuthenticationService(administatorPassword);
        });
        return collection;
    }
}