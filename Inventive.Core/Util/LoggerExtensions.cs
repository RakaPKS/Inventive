#pragma warning disable S2629 // Don't use string interpolation in logging message templates
#pragma warning disable CA2254 // Template should be a static expression
#pragma warning disable CA1848 // Use LoggerMessage delegates for performance

using Microsoft.Extensions.Logging;

namespace Inventive.Core.Util;

/// <summary>
///     Extension methods for ILogger that automatically prepend class context.
/// </summary>
public static class LoggerExtensions
{
#pragma warning disable S1144
    private static object?[] PrependClassName(string className, object?[] args)
    {
        var allArgs = new object?[args.Length + 1];
        allArgs[0] = className;
        Array.Copy(args, 0, allArgs, 1, args.Length);
        return allArgs;
    }
#pragma warning restore S1144

    extension<T>(ILogger<T> logger)
    {
        public void LogInformationWithContext(string message, params object?[] args)
        {
            var className = typeof(T).Name;
            logger.LogInformation($"{{ClassName}}: {message}", PrependClassName(className, args));
        }

        public void LogWarningWithContext(string message, params object?[] args)
        {
            var className = typeof(T).Name;
            logger.LogWarning($"{{ClassName}}: {message}", PrependClassName(className, args));
        }

        public void LogErrorWithContext(string message, params object?[] args)
        {
            var className = typeof(T).Name;
            logger.LogError($"{{ClassName}}: {message}", PrependClassName(className, args));
        }

        public void LogCriticalWithContext(string message, params object?[] args)
        {
            var className = typeof(T).Name;
            logger.LogCritical($"{{ClassName}}: {message}", PrependClassName(className, args));
        }

        public void LogDebugWithContext(string message, params object?[] args)
        {
            var className = typeof(T).Name;
            logger.LogDebug($"{{ClassName}}: {message}", PrependClassName(className, args));
        }

        public void LogTraceWithContext(string message, params object?[] args)
        {
            var className = typeof(T).Name;
            logger.LogTrace($"{{ClassName}}: {message}", PrependClassName(className, args));
        }
    }
}
