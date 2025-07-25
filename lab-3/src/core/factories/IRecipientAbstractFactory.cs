using lab_3.core.factories.recipientConfigs;
using lab_3.core.interfaces;

namespace lab_3.core.factories;

public interface IRecipientAbstractFactory
{
    public IRecipient CreateRecipient(RecipientConfig config);
}


