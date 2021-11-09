using System;

namespace exception {
    public class FractionException : Exception {
        public FractionException() : base() {}

        public FractionException(string Message) : base($"Fraction Error: {Message}") {}

        public FractionException(string Message, Exception InnerException) : base($"Fraction Error: {Message}", InnerException) {}
    }
}