using System;
using System.Collections.Generic;
using lab_3.core.interfaces;

namespace lab_3.core.models;

public class User : IUser
{
    private readonly List<UserMessageStatus> _messages;
    private readonly ILogger _logger;

    public IReadOnlyList<UserMessageStatus> Messages => _messages;

    public string UserName { get; }

    public User(string userName, ILogger logger)
    {
        UserName = userName;
        _logger = logger;
        _messages = [];
    }

    public void ReceiveMessage(IMessage message)
    {
        _messages.Add(new UserMessageStatus(message));
        _logger.Log($"User '{UserName}' received message '{message.Title}'.");
    }

    public void MarkMessageAsRead(IMessage message)
    {
        var msgStatus = _messages.Find(m => m.Message?.Title == message.Title && !m.IsRead);
        if (msgStatus != null)
        {
            msgStatus.MarkAsRead();
            _logger.Log($"User '{UserName}' marked message '{message.Title}' as read.");
        }
        else
        {
            throw new InvalidOperationException("Message is already read or does not exist.");
        }
    }

    public class UserMessageStatus(IMessage message)
    {
        public IMessage? Message { get; private set; } = message;

        public bool IsRead { get; private set; } = false;

        public void MarkAsRead() => IsRead = true;
    }
}