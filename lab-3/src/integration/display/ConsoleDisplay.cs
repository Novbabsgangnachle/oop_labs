using lab_3.core.interfaces;

namespace lab_3.integration.display;

public class ConsoleDisplay : IDisplay
{
    private readonly DisplayDriver _driver;
    private readonly ILogger _logger;

    public ConsoleDisplay(DisplayDriver driver, ILogger logger)
    {
        _driver = driver;
        _logger = logger;
    }

    public void Show(string text)
    {
        _driver.Clear();
        _driver.Write(text);
        _logger.Log("Отображено сообщение на консоли.");
    }
}