namespace Spring.Logging
{
    public interface ILoggerFactory
    {
        /// <summary>
        /// Returns an appropriate <see cref="ILogger"/> instance as specified by the name parameter.
        /// </summary>
        /// <param name="name">The name of the logger to return.</param>
        ILogger GetLogger(string name);
    }
}