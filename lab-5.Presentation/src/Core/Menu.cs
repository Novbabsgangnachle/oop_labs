namespace DefaultNamespace;

public sealed class Menu
{
    private readonly string _title;
    private readonly IList<MenuItem> _items;
    private bool _isRunning;

    public static void Exit() => throw new OperationCanceledException("exit");
    public static void Back() => throw new OperationCanceledException("back");

    public Menu(string title, IEnumerable<MenuItem> items)
    {
        _title = title;
        _items = items.ToList();
    }

    public void Run(MenuContext ctx)
    {
        _isRunning = true;

        while (_isRunning)
        {
            try
            {
                var choice = ctx.UI.Select(_title, _items.Select(i => i.Title).ToList());
                var item = _items.First(i => i.Title == choice);
                item.Action.Invoke();
            }
            catch (OperationCanceledException oce) when (oce.Message == "back")
            {
                return;
            }
            catch (OperationCanceledException oce) when (oce.Message == "exit")
            {
                _isRunning = false;
                return;
            }
        }
    }
}