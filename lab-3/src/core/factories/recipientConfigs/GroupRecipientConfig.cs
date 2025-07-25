using lab_3.core.interfaces;

namespace lab_3.core.factories.recipientConfigs;

public class GroupRecipientConfig : RecipientConfig
{
    public string GroupName { get; }

    public GroupRecipientConfig(string groupName, ILogger logger)
        : base(logger)
    {
        GroupName = groupName;
    }
}