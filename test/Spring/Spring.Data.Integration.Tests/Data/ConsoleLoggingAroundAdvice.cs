using AopAlliance.Intercept;
using Spring.Logging;

namespace Spring.Data
{
    public class ConsoleLoggingAroundAdvice : IMethodInterceptor
    {
        private static readonly ILogger LOG = LoggingManager.GetLogger(typeof(ConsoleLoggingAroundAdvice));
        public object Invoke(IMethodInvocation invocation)
        {
            LOG.Debug("Advice executing; calling the advised method [" + invocation.Method.Name + "]");
            object returnValue = invocation.Proceed();
            LOG.Debug("Advice executed; advised method [" + invocation.Method.Name + "] returned " + returnValue);
            return returnValue;
        }
    }
}
