using lab_3.core.filters;
using lab_3.core.interfaces;

namespace lab_3.core.factories.recipientConfigs;

public class MessengerRecipientConfig : RecipientConfig
{
    public IMessenger Messenger { get; }

    public ImportanceFilter? Filter { get; }

    public MessengerRecipientConfig(IMessenger messenger, ILogger logger, ImportanceFilter? filter = null)
        : base(logger)
    {
        Messenger = messenger;
        Filter = filter;
    }
}