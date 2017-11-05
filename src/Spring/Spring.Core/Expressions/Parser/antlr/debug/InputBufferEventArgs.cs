namespace Spring.Expressions.Parser.antlr.debug
{
    public class InputBufferEventArgs : ANTLREventArgs
    {
        public const int CONSUME = 0;
        public const int LA = 1;
        public const int MARK = 2;
        public const int REWIND = 3;

        internal char c_;
        internal int lookaheadAmount_; // amount of lookahead

        public InputBufferEventArgs()
        {
        }

        public InputBufferEventArgs(int type, char c, int lookaheadAmount)
        {
            setValues(type, c, lookaheadAmount);
        }

        public virtual char Char
        {
            get { return c_; }
            set { c_ = value; }
        }

        public virtual int LookaheadAmount
        {
            get { return lookaheadAmount_; }
            set { lookaheadAmount_ = value; }
        }


        /// <summary>
        ///     This should NOT be called from anyone other than ParserEventSupport!
        /// </summary>
        internal void setValues(int type, char c, int la)
        {
            setValues(type);
            Char = c;
            LookaheadAmount = la;
        }

        public override string ToString()
        {
            return "CharBufferEvent [" + (Type == CONSUME ? "CONSUME, " : "LA, ") + Char + "," + LookaheadAmount + "]";
        }
    }
}