using System;

namespace exception {
    public class MatrixException : Exception {
        public MatrixException() : base() {}

        public MatrixException(string Message) : base($"Matrix Error: {Message}") {}

        public MatrixException(string Message, Exception InnerException) : base($"Matrix Error: {Message}", InnerException) {}
    }
}