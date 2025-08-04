namespace DefaultNamespace;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection services)
    {
        services.AddSingleton<IConsoleUI, SpectreConsoleUI>();

        services.AddTransient<UserLoginAction>();
        services.AddTransient<UserRegisterAction>();
        services.AddTransient<AdminAuthenticateAction>();

        services.AddTransient<WithdrawAction>();
        services.AddTransient<ReplenishAction>();
        services.AddTransient<ShowBalanceAction>();
        services.AddTransient<CreateAccountAction>();
        services.AddTransient<ViewAccountsAction>();
        services.AddTransient<ViewTransactionsAction>();
        services.AddTransient<LogoutAction>();

        services.AddScoped<App>();

        return services;
    }
}