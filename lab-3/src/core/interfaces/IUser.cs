namespace lab_3.core.interfaces;

public interface IUser
{
    string UserName { get; }

    void ReceiveMessage(IMessage message);

    void MarkMessageAsRead(IMessage message);
}