using System;

namespace exception {
    public class FractionException : Exception {
        public FractionException() : base() {}

        public FractionException(string Message) : base(Message) {}

        public FractionException(string Message, Exception InnerException) : base(Message, InnerException) {}
    }
}