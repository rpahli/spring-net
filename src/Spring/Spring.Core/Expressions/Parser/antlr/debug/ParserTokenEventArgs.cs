namespace Spring.Expressions.Parser.antlr.debug
{
    public class TokenEventArgs : ANTLREventArgs
    {
        public static int LA = 0;
        public static int CONSUME = 1;

        public TokenEventArgs()
        {
        }

        public TokenEventArgs(int type, int amount, int val)
        {
            setValues(type, amount, val);
        }

        public virtual int Amount { get; set; }

        public virtual int Value { get; set; }


        /// <summary>
        ///     This should NOT be called from anyone other than ParserEventSupport!
        /// </summary>
        internal void setValues(int type, int amount, int val)
        {
            base.setValues(type);
            Amount = amount;
            Value = val;
        }

        public override string ToString()
        {
            if (Type == LA)
            {
                return "ParserTokenEvent [LA," + Amount + "," + Value + "]";
            }
            return "ParserTokenEvent [consume,1," + Value + "]";
        }
    }
}