using System;
using lab_4.Application;
using lab_4.Application.Commands;

namespace lab_4.Presentation;

public class ConsoleInterface(ICommandParser parser)
{
    public void Start()
    {
        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                continue;

            try
            {
                var command = parser.Parse(input);
                command.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}