using lab_3.core.filters;
using lab_3.core.interfaces;

namespace lab_3.core.recipients;

public class DisplayRecipient : IRecipient
{
    private readonly IDisplay _display;
    private readonly ILogger _logger;
    private readonly ImportanceFilter? _filter;

    public DisplayRecipient(IDisplay display, ILogger logger, ImportanceFilter? filter = null)
    {
        _display = display;
        _logger = logger;
        _filter = filter;
    }

    public void ReceiveMessage(IMessage message)
    {
        if (_filter != null && _filter.ShouldFilter(message))
        {
            _logger.Log($"Message '{message.Title}' filtered out for display due to importance level.");
            return;
        }

        _display.Show(message.Body);
        _logger.Log($"Message '{message.Title}' displayed.");
    }
}