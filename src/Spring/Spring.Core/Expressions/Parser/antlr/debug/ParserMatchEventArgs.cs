namespace Spring.Expressions.Parser.antlr.debug
{
    public class MatchEventArgs : GuessingEventArgs
    {
        // NOTE: for a mismatch on type STRING, the "text" is used as the lookahead
        //       value.  Normally "value" is this
        public enum ParserMatchEnums
        {
            TOKEN = 0,
            BITSET = 1,
            CHAR = 2,
            CHAR_BITSET = 3,
            STRING = 4,
            CHAR_RANGE = 5
        }

        public static int TOKEN = 0;
        public static int BITSET = 1;
        public static int CHAR = 2;
        public static int CHAR_BITSET = 3;
        public static int STRING = 4;
        public static int CHAR_RANGE = 5;

        private bool inverse_;
        private bool matched_;

        public MatchEventArgs()
        {
        }

        public MatchEventArgs(int type, int val, object target, string text, int guessing, bool inverse, bool matched)
        {
            setValues(type, val, target, text, guessing, inverse, matched);
        }

        public virtual object Target { get; set; }

        public virtual string Text { get; set; }

        public virtual int Value { get; set; }

        internal bool Inverse
        {
            set { inverse_ = value; }
        }

        internal bool Matched
        {
            set { matched_ = value; }
        }


        public virtual bool isInverse()
        {
            return inverse_;
        }

        public virtual bool isMatched()
        {
            return matched_;
        }

        /// <summary>
        ///     This should NOT be called from anyone other than ParserEventSupport!
        /// </summary>
        internal void setValues(int type, int val, object target, string text, int guessing, bool inverse, bool matched)
        {
            base.setValues(type, guessing);
            Value = val;
            Target = target;
            Inverse = inverse;
            Matched = matched;
            Text = text;
        }

        public override string ToString()
        {
            return "ParserMatchEvent [" + (isMatched() ? "ok," : "bad,") + (isInverse() ? "NOT " : "") +
                   (Type == TOKEN ? "token," : "bitset,") + Value + "," + Target + "," + Guessing + "]";
        }
    }
}