using System.IO;
using lab_3.core.interfaces;

namespace lab_3.integration.display;

public class FileDisplay : IDisplay
{
    private readonly DisplayDriver _driver;
    private readonly string _filePath;
    private readonly ILogger _logger;

    public FileDisplay(DisplayDriver driver, string filePath, ILogger logger)
    {
        _driver = driver;
        _filePath = filePath;
        _logger = logger;
    }

    public void Show(string text)
    {
        _driver.Clear();
        _driver.SetColor("Default");
        _driver.Write(text);
        File.WriteAllText(_filePath, text);
        _logger.Log($"Displayed message in file '{_filePath}'.");
    }
}