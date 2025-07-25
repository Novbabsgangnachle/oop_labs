using System;
using lab_3.core.interfaces;

namespace lab_3.integration.messenger;

public class ConsoleMessenger : IMessenger
{
    public void Send(string message)
    {
        Console.WriteLine($"[Messenger] {message}");
    }
}