namespace DefaultNamespace;

public static class Program
{
    public static void Main()
    {
        IUserService userService
        IAccountService accountService
        AdminAuthenticationService adminAuthService
        ITransactionService transactionService

        var ui = new SpectreConsoleUI();
        var context = new MenuContext(ui, userService, accountService, adminAuthService, transactionService);

        // Меню
        var mainMenu = new Menu(
            "Выберите режим работы:",
            new[]
            {
                new MenuItem("Пользователь", () =>
                {
                    var userAuthMenu = new Menu(
                        "Выберите действие:",
                        new[]
                        {
                            new MenuItem("Войти",    new UserLoginAction().Execute),
                            new MenuItem("Зарегистрироваться", new UserRegisterAction().Execute),
                            new MenuItem("Назад",    Menu.Back)
                        });
                    userAuthMenu.Run(context);
                }),
                new MenuItem("Администратор", new AdminAuthenticateAction().Execute),
                new MenuItem("Выход", Menu.Exit)
            });

        mainMenu.Run(context);
    }
}