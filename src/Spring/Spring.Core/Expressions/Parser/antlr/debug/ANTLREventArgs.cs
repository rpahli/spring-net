using System;

namespace Spring.Expressions.Parser.antlr.debug
{
    public abstract class ANTLREventArgs : EventArgs
    {
        /// <summary>
        ///     Event type.
        /// </summary>
        private int type_;

        public ANTLREventArgs()
        {
        }

        public ANTLREventArgs(int type)
        {
            Type = type;
        }

        public virtual int Type
        {
            get { return type_; }
            set { type_ = value; }
        }

        internal void setValues(int type)
        {
            Type = type;
        }
    }
}