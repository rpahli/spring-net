using System;

namespace Spring.Logging
{
    public class LogManager
    {
        public static ILogger GetLogger<T>()
        {
            throw new NotImplementedException();
        }

        public static ILogger GetLogger(Type type)
        {
            throw new NotImplementedException();
        }

        public static ILogger GetCurrentClassLogger()
        {
            throw new NotImplementedException();
        }

        public static ILogger GetLogger(string fullName)
        {
            throw new NotImplementedException();
        }
    }
}