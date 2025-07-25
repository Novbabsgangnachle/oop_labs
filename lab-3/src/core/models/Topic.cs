using lab_3.core.interfaces;

namespace lab_3.core.models;

public class Topic
{
    private readonly ILogger _logger;

    private string Name { get; }

    private readonly IRecipient _recipient;

    public Topic(string name, ILogger logger, IRecipient recipient)
    {
        Name = name;
        _logger = logger;
        _recipient = recipient;
    }

    public void SendMessage(IMessage message)
    {
        _logger.Log($"Sending message '{message.Title}' to topic '{Name}'.");
        _recipient.ReceiveMessage(message);
    }
}