using lab_4.Application.Commands;

namespace lab_4.Application;

public interface ICommandParser
{
    ICommand Parse(string input);
}