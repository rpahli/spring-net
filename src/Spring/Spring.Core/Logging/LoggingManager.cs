using System;

namespace Spring.Logging
{
    public class LoggingManager
    {
        [Serializable]
        protected class ConsoleLogger : ILogger
        {
            public string Name
            {
                get { return "Console"; }
            }

            public void Trace(object message)
            {
                Console.WriteLine(message);
            }

            public void Trace(object message, Exception exception)
            {
                Console.WriteLine(message);
            }

            public void TraceFormat(string format, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void TraceFormat(string format, Exception exception, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
                Console.WriteLine(string.Format(formatProvider, format, args));
            }

            public void TraceFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
            {
                Console.WriteLine(string.Format(formatProvider, format, args));
            }

            public void Debug(object message)
            {
                Console.WriteLine(message);
            }

            public void Debug(object message, Exception exception)
            {
                Console.WriteLine(message);
            }

            public void DebugFormat(string format, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void DebugFormat(string format, Exception exception, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
                Console.WriteLine(string.Format(formatProvider, format, args));
            }

            public void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
            {
                Console.WriteLine(string.Format(formatProvider, format, args));
            }

            public void Info(object message)
            {
                Console.WriteLine(message);
            }

            public void Info(object message, Exception exception)
            {
                Console.WriteLine(message);
            }

            public void InfoFormat(string format, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void InfoFormat(string format, Exception exception, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
                Console.WriteLine(string.Format(formatProvider, format, args));
            }

            public void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
            {
                Console.WriteLine(string.Format(formatProvider, format, args));
            }

            public void Warn(object message)
            {
                Console.WriteLine(message);
            }

            public void Warn(object message, Exception exception)
            {
                Console.WriteLine(message);
            }

            public void WarnFormat(string format, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void WarnFormat(string format, Exception exception, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
                Console.WriteLine(string.Format(formatProvider, format, args));
            }

            public void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
            {
                Console.WriteLine(string.Format(formatProvider, format, args));
            }

            public void Error(object message)
            {
                Console.WriteLine(message);
            }

            public void Error(object message, Exception exception)
            {
                Console.WriteLine(message);
            }

            public void ErrorFormat(string format, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void ErrorFormat(string format, Exception exception, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
                Console.WriteLine(string.Format(formatProvider, format, args));
            }

            public void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
            {
                Console.WriteLine(String.Format(formatProvider, format, args));
            }

            public void Fatal(object message)
            {
                Console.WriteLine(message);
            }

            public void Fatal(object message, Exception exception)
            {
                Console.WriteLine(message);
            }

            public void FatalFormat(string format, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void FatalFormat(string format, Exception exception, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
                Console.WriteLine(string.Format(formatProvider, format, args));
            }

            public void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
            {
                Console.WriteLine(string.Format(formatProvider, format, args));
            }

            public bool IsTraceEnabled { get; } = true;

            public bool IsDebugEnabled { get; } = true;

            public bool IsErrorEnabled { get; } = true;

            public bool IsFatalEnabled { get; } = true;

            public bool IsInfoEnabled { get; } = true;

            public bool IsWarnEnabled { get; } = true;
        }

        public static ILogger GetLogger<T>()
        {
            return new ConsoleLogger();
        }

        public static ILogger GetLogger(Type type)
        {
            return new ConsoleLogger();
        }

        public static ILogger GetCurrentClassLogger()
        {
            return new ConsoleLogger();
        }

        public static ILogger GetLogger(string fullName)
        {
            return new ConsoleLogger();
        }
    }
}