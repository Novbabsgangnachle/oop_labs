using lab_3.core.filters;
using lab_3.core.interfaces;

namespace lab_3.core.recipients;

public class MessengerRecipient : IRecipient
{
    private readonly IMessenger _messenger;
    private readonly ILogger _logger;
    private readonly ImportanceFilter? _filter;

    public MessengerRecipient(IMessenger messenger, ILogger logger, ImportanceFilter? filter = null)
    {
        _messenger = messenger;
        _logger = logger;
        _filter = filter;
    }

    public void ReceiveMessage(IMessage message)
    {
        if (_filter != null && _filter.ShouldFilter(message))
        {
            _logger.Log($"Message '{message.Title}' filtered out for messenger due to importance level.");
            return;
        }

        _messenger.Send(message.Body);
        _logger.Log($"Message '{message.Title}' sent via messenger.");
    }
}