namespace Identity_Application.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}