using System;
using lab_3.core.interfaces;

namespace lab_3.logging;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}