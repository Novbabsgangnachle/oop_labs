using System;
using static Crayon.Output;

namespace lab_3.integration.display.colorStrategy;

public class RedColorStrategy : IColorStrategy
{
    public void Write(string text) => Console.WriteLine(Red(text));
}