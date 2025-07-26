namespace DefaultNamespace;

public class ComandParserTests
{
    private readonly CommandParser _commandParser;
    private readonly Mock<ICommandHandler> _mockHandler;

    public ComandParserTests()
    {
        _mockHandler = new Mock<ICommandHandler>();
        _commandParser = new CommandParser(new List<ICommandHandler> { _mockHandler.Object });
    }

    [Fact]
    public void Parse_ValidCommand_ReturnsCommand()
    {
        string input = "test";
        string[] args = new string[] { "test" };
        ICommand mockCommand = new Mock<ICommand>().Object;

        _mockHandler.Setup(h => h.CanHandle(It.IsAny<string[]>())).Returns(true);
        _mockHandler.Setup(h => h.Handle(It.IsAny<string[]>())).Returns(mockCommand);

        ICommand result = _commandParser.Parse(input);

        Assert.Equal(mockCommand, result);
        _mockHandler.Verify(h => h.CanHandle(args), Times.Once);
        _mockHandler.Verify(h => h.Handle(args), Times.Once);
    }

    [Fact]
    public void Parse_InvalidCommand_ThrowsInvalidCommandException()
    {
        string input = "invalid";
        string[] args = new string[] { "invalid" };

        _mockHandler.Setup(h => h.CanHandle(It.IsAny<string[]>())).Returns(false);

        Assert.Throws<InvalidCommandException>(() => _commandParser.Parse(input));
        _mockHandler.Verify(h => h.CanHandle(args), Times.Once);
        _mockHandler.Verify(h => h.Handle(It.IsAny<string[]>()), Times.Never);
    }

    [Fact]
    public void Parse_EmptyInput_ThrowsInvalidCommandException()
    {
        string input = string.Empty;

        Assert.Throws<InvalidCommandException>(() => _commandParser.Parse(input));
    }
}