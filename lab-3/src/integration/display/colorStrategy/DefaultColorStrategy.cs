using System;

namespace lab_3.integration.display.colorStrategy;

public class DefaultColorStrategy : IColorStrategy
{
    public void Write(string text) => Console.WriteLine(text);
}