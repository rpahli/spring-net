namespace Spring.Expressions.Parser.antlr.debug
{
    public class NewLineEventArgs : ANTLREventArgs
    {
        private int line_;

        public NewLineEventArgs()
        {
        }

        public NewLineEventArgs(int line)
        {
            Line = line;
        }

        public virtual int Line
        {
            get { return line_; }
            set { line_ = value; }
        }


        /// <summary>
        ///     This should NOT be called from anyone other than ParserEventSupport!
        /// </summary>
        public override string ToString()
        {
            return "NewLineEvent [" + line_ + "]";
        }
    }
}