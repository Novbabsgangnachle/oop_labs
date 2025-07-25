using lab_3.core.utils;

namespace lab_3.core.interfaces;

public interface IMessage
{
    public string Title { get; }

    public string Body { get; }

    ImportanceLevel Importance { get; }
}
