using System;
using System.Threading.Channels;

namespace msdn_delegate
{
    public static class LoggingMethods
    {
        public static void LogToConsole(string message)
        {
            Console.Error.WriteLine(message);
        }

        public static void LogLine(string messgage)
        {
            Console.WriteLine("a line" + messgage);
        }
    }
    class Program
    {
        public static Action<string> WriteMessage;

        public static void LogMessage(string msg)
        {
            WriteMessage(msg);
        }
        static void Main(string[] args)
        {
            string amsg = "simple Action";
            WriteMessage += LoggingMethods.LogToConsole;
            WriteMessage += LoggingMethods.LogLine;
            LogMessage(amsg);
        }
    }
}