using System;
using System.Collections.Generic;
using lab_3.core.filters;
using lab_3.core.interfaces;

namespace lab_3.core.recipients;

public class GroupRecipient : IRecipient
{
    private readonly List<IRecipient> _recipients;
    private readonly ILogger _logger;
    private readonly ImportanceFilter? _filter;

    private string GroupName { get; }

    public GroupRecipient(string groupName, ILogger logger, ImportanceFilter? filter = null)
    {
        GroupName = groupName ?? throw new ArgumentNullException(nameof(groupName));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _recipients = [];
        _filter = filter;
    }

    public void AddRecipient(IRecipient recipient)
    {
        ArgumentNullException.ThrowIfNull(recipient);
        _recipients.Add(recipient);
        _logger.Log($"Recipient added to group '{GroupName}'.");
    }

    public void RemoveRecipient(IRecipient recipient)
    {
        ArgumentNullException.ThrowIfNull(recipient);
        _recipients.Remove(recipient);
        _logger.Log($"Recipient removed from group '{GroupName}'.");
    }

    public void ReceiveMessage(IMessage message)
    {
        if (_filter != null && _filter.ShouldFilter(message))
        {
            _logger.Log($"Message '{message.Title}' filtered out for group '{GroupName}' due to importance level.");
            return;
        }

        _logger.Log($"Group '{GroupName}' is distributing message '{message.Title}'.");
        foreach (IRecipient recipient in _recipients)
        {
            recipient.ReceiveMessage(message);
        }
    }
}