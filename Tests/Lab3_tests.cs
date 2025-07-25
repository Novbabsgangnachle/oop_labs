namespace DefaultNamespace;

public class Lab3_tests
{
    [Fact]
    public void User_Should_Save_Message_As_Unread_When_Received()
    {
        var loggerMock = new Mock<ILogger>();
        var user = new User("TestUser", loggerMock.Object);
        var message = new Message("TestTitle", "TestBody", ImportanceLevel.Medium);

        user.ReceiveMessage(message);

        Assert.Single(user.Messages);
        User.UserMessageStatus userMessageStatus = user.Messages.First();
        Assert.Equal(message, userMessageStatus.Message);
        Assert.False(userMessageStatus.IsRead);
    }

    [Fact]
    public void User_Should_Mark_Message_As_Read()
    {
        var loggerMock = new Mock<ILogger>();
        var user = new User("TestUser", loggerMock.Object);
        var message = new Message("TestTitle", "TestBody", ImportanceLevel.Medium);
        user.ReceiveMessage(message);

        user.MarkMessageAsRead(message);

        User.UserMessageStatus userMessageStatus = user.Messages.First();
        Assert.True(userMessageStatus.IsRead);
    }

    [Fact]
    public void User_Should_Throw_When_Marking_Already_Read_Message_As_Read()
    {
        var loggerMock = new Mock<ILogger>();
        var user = new User("TestUser", loggerMock.Object);
        var message = new Message("TestTitle", "TestBody", ImportanceLevel.Medium);
        user.ReceiveMessage(message);
        user.MarkMessageAsRead(message);

        Assert.Throws<InvalidOperationException>(() => user.MarkMessageAsRead(message));
    }

    [Fact]
    public void Recipient_Should_Filter_Message_By_Importance()
    {
        var loggerMock = new Mock<ILogger>();
        var userMock = new Mock<IUser>();
        var filter = new ImportanceFilter(ImportanceLevel.High);
        var recipient = new UserRecipient(userMock.Object, loggerMock.Object, filter);
        var message = new Message("TestTitle", "TestBody", ImportanceLevel.Medium);

        recipient.ReceiveMessage(message);

        userMock.Verify(u => u.ReceiveMessage(It.IsAny<IMessage>()), Times.Never);
        loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("filtered out"))));
    }

    [Fact]
    public void Recipient_Should_Log_When_Receiving_Message()
    {
        var loggerMock = new Mock<ILogger>();
        var recipientMock = new Mock<IRecipient>();
        var loggingRecipient = new LoggingRecipientDecorator(recipientMock.Object, loggerMock.Object);
        var message = new Message("TestTitle", "TestBody", ImportanceLevel.Medium);

        loggingRecipient.ReceiveMessage(message);

        loggerMock.Verify(l => l.Log($"Получение сообщения '{message.Title}' декоратором."), Times.Once);
        recipientMock.Verify(r => r.ReceiveMessage(message), Times.Once);
        loggerMock.Verify(l => l.Log($"Сообщение '{message.Title}' обработано декоратором."), Times.Once);
    }

    [Fact]
    public void MessengerRecipient_Should_Send_Message_Via_Messenger()
    {
        var loggerMock = new Mock<ILogger>();
        var messengerMock = new Mock<IMessenger>();
        var recipient = new MessengerRecipient(messengerMock.Object, loggerMock.Object);
        var message = new Message("TestTitle", "TestBody", ImportanceLevel.Medium);

        recipient.ReceiveMessage(message);

        messengerMock.Verify(m => m.Send(message.Body), Times.Once);
        loggerMock.Verify(l => l.Log($"Message '{message.Title}' sent via messenger."), Times.Once);
    }

    [Fact]
    public void GroupRecipient_Should_Deliver_Message_To_Unfiltered_User_Only()
    {
        var loggerMock = new Mock<ILogger>();
        var user = new User("TestUser", loggerMock.Object);

        var userRecipient1 = new UserRecipient(user, loggerMock.Object);
        var filter = new ImportanceFilter(ImportanceLevel.High);
        var userRecipient2 = new UserRecipient(user, loggerMock.Object, filter);

        var groupRecipient = new GroupRecipient("TestGroup", loggerMock.Object);
        groupRecipient.AddRecipient(userRecipient1);
        groupRecipient.AddRecipient(userRecipient2);

        var message = new Message("TestTitle", "TestBody", ImportanceLevel.Medium);

        groupRecipient.ReceiveMessage(message);

        Assert.Single(user.Messages);
        loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("filtered out"))), Times.Once);
        loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("delivered to user"))), Times.Once);
    }
}