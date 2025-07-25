using lab_3.core.interfaces;

namespace lab_3.core.decorators;

public abstract class RecipientDecoratorBase : IRecipient
{
    protected ILogger Logger { get; }

    private IRecipient Recipient { get; }

    protected RecipientDecoratorBase(IRecipient recipient, ILogger logger)
    {
        Recipient = recipient;
        Logger = logger;
    }

    public void ReceiveMessage(IMessage message)
    {
        Recipient.ReceiveMessage(message);
    }
}