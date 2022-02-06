using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDestroyer.Utility;

public static class Logger
{

    public static void LogInformation(string message) => Log(LogLevel.Info, message);
    public static void LogWarning(string message) => Log(LogLevel.Warn, message);
    public static void LogError(string message) => Log(LogLevel.Error, message);
    public static void LogDebug(string message) => Log(LogLevel.Debug, message);

    private static void Log(LogLevel level, string message)
    {
        if (level == LogLevel.Info)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[INF] ");
        }
        else if (level == LogLevel.Warn)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[WRN] ");
        }
        else if (level == LogLevel.Debug)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("[DBG] ");
        }
        else if (level == LogLevel.Error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERR] ");
        }
        Console.ResetColor();
        Console.Write(message + "\n");
    }

    public enum LogLevel
    {
        Info,
        Warn,
        Error,
        Debug
    }
}
