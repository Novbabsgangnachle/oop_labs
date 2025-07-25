using System;
using lab_3.integration.display.colorStrategy;

namespace lab_3.integration.display;

public class DisplayDriver
{
    private IColorStrategy _colorStrategy;

    public DisplayDriver()
    {
        _colorStrategy = new DefaultColorStrategy();
    }

    public void Clear()
    {
        Console.Clear();
    }

    public void SetColor(string color)
    {
        _colorStrategy = color.ToLower(System.Globalization.CultureInfo.CurrentCulture) switch
        {
            "red" => new RedColorStrategy(),
            "green" => new GreenColorStrategy(),
            "blue" => new BlueColorStrategy(),
            _ => new DefaultColorStrategy(),
        };
    }

    public void Write(string text)
    {
        _colorStrategy.Write(text);
    }
}