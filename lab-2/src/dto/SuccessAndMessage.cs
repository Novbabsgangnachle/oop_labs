namespace lab_2.dto;

public class SuccessAndMessage
{
    public bool Success { get; private set; }

    public string? Message { get; private set; }

    public SuccessAndMessage(bool success, string? message)
    {
        Success = success;
        Message = message;
    }
}