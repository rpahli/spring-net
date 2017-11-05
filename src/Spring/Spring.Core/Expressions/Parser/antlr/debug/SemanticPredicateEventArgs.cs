namespace Spring.Expressions.Parser.antlr.debug
{
    public class SemanticPredicateEventArgs : GuessingEventArgs
    {
        public const int VALIDATING = 0;
        public const int PREDICTING = 1;

        public SemanticPredicateEventArgs()
        {
        }

        public SemanticPredicateEventArgs(int type) : base(type)
        {
        }

        public virtual int Condition { get; set; }

        public virtual bool Result { get; set; }


        /// <summary>
        ///     This should NOT be called from anyone other than ParserEventSupport!
        /// </summary>
        internal void setValues(int type, int condition, bool result, int guessing)
        {
            base.setValues(type, guessing);
            Condition = condition;
            Result = result;
        }

        public override string ToString()
        {
            return "SemanticPredicateEvent [" + Condition + "," + Result + "," + Guessing + "]";
        }
    }
}