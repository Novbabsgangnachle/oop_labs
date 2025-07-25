using lab_3.core.filters;
using lab_3.core.interfaces;

namespace lab_3.core.factories.recipientConfigs;

public class DisplayRecipientConfig : RecipientConfig
{
    public IDisplay Display { get; }

    public ImportanceFilter? Filter { get; }

    public DisplayRecipientConfig(IDisplay display, ILogger logger, ImportanceFilter? filter = null)
        : base(logger)
    {
        Display = display;
        Filter = filter;
    }
}