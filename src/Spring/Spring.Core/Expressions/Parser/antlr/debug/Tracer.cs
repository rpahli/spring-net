using System;

namespace Spring.Expressions.Parser.antlr.debug
{
    public class Tracer : TraceListenerBase, TraceListener
    {
        protected string indentString = "";

        public override void enterRule(object source, TraceEventArgs e)
        {
            Console.Out.WriteLine(indentString + e);
            indent();
        }

        public override void exitRule(object source, TraceEventArgs e)
        {
            dedent();
            Console.Out.WriteLine(indentString + e);
        }
        // TBD: should be StringBuffer


        protected internal virtual void dedent()
        {
            if (indentString.Length < 2)
            {
                indentString = "";
            }
            else
            {
                indentString = indentString.Substring(2);
            }
        }

        protected internal virtual void indent()
        {
            indentString += "  ";
        }
    }
}