using lab_3.core.filters;
using lab_3.core.interfaces;

namespace lab_3.core.factories.recipientConfigs;

public class UserRecipientConfig : RecipientConfig
{
    public IUser User { get; }

    public ImportanceFilter? Filter { get; }

    public UserRecipientConfig(IUser user, ILogger logger, ImportanceFilter? filter = null)
        : base(logger)
    {
        User = user;
        Filter = filter;
    }
}