using lab_3.core.filters;
using lab_3.core.interfaces;

namespace lab_3.core.recipients;

public class UserRecipient : IRecipient
{
    private readonly IUser _user;
    private readonly ILogger _logger;
    private readonly ImportanceFilter? _filter;

    public UserRecipient(IUser user, ILogger logger, ImportanceFilter? filter = null)
    {
        _user = user;
        _logger = logger;
        _filter = filter;
    }

    public void ReceiveMessage(IMessage message)
    {
        if (_filter != null && _filter.ShouldFilter(message))
        {
            _logger.Log($"Message '{message.Title}' filtered out for user '{_user.UserName}' due to importance level.");
            return;
        }

        _user.ReceiveMessage(message);
        _logger.Log($"Message '{message.Title}' delivered to user '{_user.UserName}'.");
    }
}