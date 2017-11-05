namespace Spring.Expressions.Parser.antlr.debug
{
    public abstract class GuessingEventArgs : ANTLREventArgs
    {
        public GuessingEventArgs()
        {
        }

        public GuessingEventArgs(int type) : base(type)
        {
        }

        public virtual int Guessing { get; set; }


        /// <summary>
        ///     This should NOT be called from anyone other than ParserEventSupport!
        /// </summary>
        public virtual void setValues(int type, int guessing)
        {
            setValues(type);
            Guessing = guessing;
        }
    }
}