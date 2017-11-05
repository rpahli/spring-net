namespace Spring.Expressions.Parser.antlr.debug
{
    public class MessageEventArgs : ANTLREventArgs
    {
        public static int WARNING = 0;
        public static int ERROR = 1;

        public MessageEventArgs()
        {
        }

        public MessageEventArgs(int type, string text)
        {
            setValues(type, text);
        }

        public virtual string Text { get; set; }


        /// <summary>
        ///     This should NOT be called from anyone other than ParserEventSupport!
        /// </summary>
        internal void setValues(int type, string text)
        {
            setValues(type);
            Text = text;
        }

        public override string ToString()
        {
            return "ParserMessageEvent [" + (Type == WARNING ? "warning," : "error,") + Text + "]";
        }
    }
}