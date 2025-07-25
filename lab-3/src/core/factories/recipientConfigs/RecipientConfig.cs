using lab_3.core.interfaces;

namespace lab_3.core.factories.recipientConfigs;

public abstract class RecipientConfig
{
    public ILogger Logger { get; }

    protected RecipientConfig(ILogger logger)
    {
        Logger = logger;
    }
}