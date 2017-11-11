using Spring.Logging;
//using Spring.Logging.Simple;

namespace Spring.Aspects.Exceptions
{
//    public class CaptureOutputLogger : TraceLogger
//    {
//        public static readonly string NAME = "capturingLogger";
//        public CaptureOutputLogger()
//          : base(false, NAME, LoggingLevel.All, true, false, false, null)
//        { }
//
////        private LoggingLevel _currentLogLevel = LoggingLevel.All;
////
////        private IList logMessages = new ArrayList();
////
////        public IList LogMessages
////        {
////            get { return logMessages; }
////            set { logMessages = value; }
////        }
////
////        /// <summary>
////        /// Do the actual logging by constructing the log message using a <see cref="StringBuilder" /> then
////        /// sending the output to <see cref="Console.Out" />.
////        /// </summary>
////        /// <param name="level">The <see cref="LoggingLevel" /> of the message.</param>
////        /// <param name="message">The log message.</param>
////        /// <param name="e">An optional <see cref="Exception" /> associated with the message.</param>
////        private void Write(LoggingLevel level, object message, Exception e)
////        {
////            // Use a StringBuilder for better performance
////            StringBuilder sb = new StringBuilder();
////            // Append date-time if so configured
////            // Append a readable representation of the log level
////            sb.Append(("[" + level.ToString().ToUpper() + "]").PadRight(8));
////
////            // Append the message
////            sb.Append(message);
////
////            // Append stack trace if not null
////            if (e != null)
////            {
////                sb.Append(Environment.NewLine).Append(e.ToString());
////            }
////
////            // Print to the appropriate destination
////            logMessages.Add(sb.ToString());
////        }
////
////        /// <summary>
////        /// Determines if the given log level is currently enabled.
////        /// </summary>
////        /// <param name="level"></param>
////        /// <returns></returns>
////        private bool IsLevelEnabled(LoggingLevel level)
////        {
////            int iLevel = (int)level;
////            int iCurrentLogLevel = (int)_currentLogLevel;
////
////            // return iLevel.CompareTo(iCurrentLogLevel); better ???
////            return (iLevel >= iCurrentLogLevel);
////        }
////
////        #region ILogger Members
////
////        public void Trace(object message)
////        {
////            Trace(message, null);
////        }
////
////        public void Trace(object message, Exception e)
////        {
////            if (IsLevelEnabled(LoggingLevel.Trace))
////            {
////                Write(LoggingLevel.Trace, message, e);
////            }
////        }
////
////        public void Debug(object message)
////        {
////            Debug(message, null);
////        }
////
////        public void Debug(object message, Exception e)
////        {
////            if (IsLevelEnabled(LoggingLevel.Debug))
////            {
////                Write(LoggingLevel.Debug, message, e);
////            }
////        }
////
////        public void Error(object message)
////        {
////            Error(message, null);
////        }
////
////        public void Error(object message, Exception e)
////        {
////            if (IsLevelEnabled(LoggingLevel.Error))
////            {
////                Write(LoggingLevel.Error, message, e);
////            }
////        }
////
////        public void Fatal(object message)
////        {
////            Fatal(message, null);
////        }
////
////        public void Fatal(object message, Exception e)
////        {
////            if (IsLevelEnabled(LoggingLevel.Fatal))
////            {
////                Write(LoggingLevel.Fatal, message, e);
////            }
////        }
////
////        public void Info(object message)
////        {
////            Info(message, null);
////        }
////
////        public void Info(object message, Exception e)
////        {
////            if (IsLevelEnabled(LoggingLevel.Info))
////            {
////                Write(LoggingLevel.Info, message, e);
////            }
////        }
////
////        public void Warn(object message)
////        {
////            Warn(message, null);
////        }
////
////        public void Warn(object message, Exception e)
////        {
////            if (IsLevelEnabled(LoggingLevel.Warn))
////            {
////                Write(LoggingLevel.Warn, message, e);
////            }
////        }
////
////        /// <summary>
////        /// Returns <see langword="true" /> if the current <see cref="LoggingLevel" /> is greater than or
////        /// equal to <see cref="LoggingLevel.Trace" />. If it is, all messages will be sent to <see cref="Console.Out" />.
////        /// </summary>
////        public bool IsTraceEnabled
////        {
////            get { return IsLevelEnabled(LoggingLevel.Trace); }
////        }
////
////        /// <summary>
////        /// Returns <see langword="true" /> if the current <see cref="LoggingLevel" /> is greater than or
////        /// equal to <see cref="LoggingLevel.Debug" />. If it is, all messages will be sent to <see cref="Console.Out" />.
////        /// </summary>
////        public bool IsDebugEnabled
////        {
////            get { return IsLevelEnabled(LoggingLevel.Debug); }
////        }
////
////        /// <summary>
////        /// Returns <see langword="true" /> if the current <see cref="LoggingLevel" /> is greater than or
////        /// equal to <see cref="LoggingLevel.Error" />. If it is, only messages with a <see cref="LoggingLevel" /> of
////        /// <see cref="LoggingLevel.Error" /> and <see cref="LoggingLevel.Fatal" /> will be sent to <see cref="Console.Out" />.
////        /// </summary>
////        public bool IsErrorEnabled
////        {
////            get { return IsLevelEnabled(LoggingLevel.Error); }
////        }
////
////        /// <summary>
////        /// Returns <see langword="true" /> if the current <see cref="LoggingLevel" /> is greater than or
////        /// equal to <see cref="LoggingLevel.Fatal" />. If it is, only messages with a <see cref="LoggingLevel" /> of
////        /// <see cref="LoggingLevel.Fatal" /> will be sent to <see cref="Console.Out" />.
////        /// </summary>
////        public bool IsFatalEnabled
////        {
////            get { return IsLevelEnabled(LoggingLevel.Fatal); }
////        }
////
////        /// <summary>
////        /// Returns <see langword="true" /> if the current <see cref="LoggingLevel" /> is greater than or
////        /// equal to <see cref="LoggingLevel.Info" />. If it is, only messages with a <see cref="LoggingLevel" /> of
////        /// <see cref="LoggingLevel.Info" />, <see cref="LoggingLevel.Warn" />, <see cref="LoggingLevel.Error" />, and 
////        /// <see cref="LoggingLevel.Fatal" /> will be sent to <see cref="Console.Out" />.
////        /// </summary>
////        public bool IsInfoEnabled
////        {
////            get { return IsLevelEnabled(LoggingLevel.Info); }
////        }
////
////
////        /// <summary>
////        /// Returns <see langword="true" /> if the current <see cref="LoggingLevel" /> is greater than or
////        /// equal to <see cref="LoggingLevel.Warn" />. If it is, only messages with a <see cref="LoggingLevel" /> of
////        /// <see cref="LoggingLevel.Warn" />, <see cref="LoggingLevel.Error" />, and <see cref="LoggingLevel.Fatal" /> 
////        /// will be sent to <see cref="Console.Out" />.
////        /// </summary>
////        public bool IsWarnEnabled
////        {
////            get { return IsLevelEnabled(LoggingLevel.Warn); }
////        }
////
////        #endregion
//    }
}