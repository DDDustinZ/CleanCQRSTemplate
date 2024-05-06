using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Common;

public static class MoqExtensions
{
    public static void VerifyLog<T>(this Mock<ILogger<T>> mock, LogLevel logLevel)
    {
        VerifyLog(mock, logLevel, _ => true);
    }

    public static void VerifyLog<T>(this Mock<ILogger<T>> mock, LogLevel logLevel, string message)
    {
        VerifyLog(mock, logLevel, message, Times.Once());
    }

    public static void VerifyLog<T>(this Mock<ILogger<T>> mock, LogLevel logLevel, string message, Times times)
    {
        VerifyLog(mock, logLevel, x => x == message, times);
    }

    public static void VerifyLog<T>(this Mock<ILogger<T>> mock, LogLevel logLevel, Func<string, bool> messagePredicate)
    {
        VerifyLog(mock, logLevel, messagePredicate, Times.Once());
    }

    public static void VerifyLog<T>(this Mock<ILogger<T>> mock, LogLevel logLevel, Func<string, bool> messagePredicate, Times times)
    {
        mock.Verify(x => x.Log(
                It.Is<LogLevel>(y => y == logLevel),
                It.Is<EventId>(eventId => eventId.Id == 0),
                It.Is<It.IsAnyType>((@object, type) => messagePredicate(@object.ToString()!) && type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            times);
    }

    public static string FormatLogDateTime(this DateTime dateTime) => dateTime.ToString("MM/dd/yyyy HH:mm:ss");
}