using System;

namespace Spring.Expressions.Parser.antlr.debug
{
    public class InputBufferReporter : InputBufferListenerBase, InputBufferListener
    {
        /// <summary>
        ///     charBufferConsume method comment.
        /// </summary>
        public override void inputBufferConsume(object source, InputBufferEventArgs e)
        {
            Console.Out.WriteLine(e);
        }

        /// <summary>
        ///     charBufferLA method comment.
        /// </summary>
        public override void inputBufferLA(object source, InputBufferEventArgs e)
        {
            Console.Out.WriteLine(e);
        }

        public override void inputBufferMark(object source, InputBufferEventArgs e)
        {
            Console.Out.WriteLine(e);
        }

        public override void inputBufferRewind(object source, InputBufferEventArgs e)
        {
            Console.Out.WriteLine(e);
        }

        public virtual void inputBufferChanged(object source, InputBufferEventArgs e)
        {
            Console.Out.WriteLine(e);
        }
    }
}