using System;
using static Crayon.Output;
namespace lab_3.integration.display.colorStrategy;

public class BlueColorStrategy : IColorStrategy
{
    public void Write(string text) => Console.WriteLine(Blue(text));
}