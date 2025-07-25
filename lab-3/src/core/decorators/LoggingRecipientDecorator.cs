using lab_3.core.interfaces;

namespace lab_3.core.decorators;


public class LoggingRecipientDecorator : RecipientDecoratorBase
{
    public LoggingRecipientDecorator(IRecipient recipient, ILogger logger)
        : base(recipient, logger) { }

    public new void ReceiveMessage(IMessage message)
    {
        Logger.Log($"Получение сообщения '{message.Title}' декоратором.");
        base.ReceiveMessage(message);
        Logger.Log($"Сообщение '{message.Title}' обработано декоратором.");
    }
}