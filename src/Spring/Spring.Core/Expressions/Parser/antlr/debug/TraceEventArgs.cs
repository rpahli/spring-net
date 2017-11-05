namespace Spring.Expressions.Parser.antlr.debug
{
    public class TraceEventArgs : GuessingEventArgs
    {
        public static int ENTER = 0;
        public static int EXIT = 1;
        public static int DONE_PARSING = 2;

        public TraceEventArgs()
        {
        }

        public TraceEventArgs(int type, int ruleNum, int guessing, int data)
        {
            setValues(type, ruleNum, guessing, data);
        }

        public virtual int Data { get; set; }

        public virtual int RuleNum { get; set; }


        /// <summary>
        ///     This should NOT be called from anyone other than ParserEventSupport!
        /// </summary>
        internal void setValues(int type, int ruleNum, int guessing, int data)
        {
            base.setValues(type, guessing);
            RuleNum = ruleNum;
            Data = data;
        }

        public override string ToString()
        {
            return "ParserTraceEvent [" + (Type == ENTER ? "enter," : "exit,") + RuleNum + "," + Guessing + "]";
        }
    }
}