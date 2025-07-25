using lab_3.core.interfaces;
using lab_3.core.utils;

namespace lab_3.core.models;

public record Message : IMessage
{
    public string Title { get; }

    public string Body { get; }

    public ImportanceLevel Importance { get; }

    public Message(string title, string body, ImportanceLevel importance)
    {
        Title = title;
        Body = body;
        Importance = importance;
    }
}