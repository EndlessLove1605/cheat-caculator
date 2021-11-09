using System;
using exception;
/**
 * Created by TruongNBN on 18/10/2021
 */

namespace numerics {
    public class Matrix<T> {
        /// <summary>
        /// Properties
        /// </summary>
        public int Row { get; set; }
        public int Col { get; set; }
        public T[,] Value { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Matrix() => init(0,0);
        public Matrix(int size) => init(size, size);
        public Matrix(int row, int col) => init(row, col);
        public Matrix(T[,] value) {
            Row = value.GetLength(0);
            Col = value.GetLength(1);
            Value = value;
        }
        public Matrix(string dataMatrix) {
            // Todo
        }

        /// <summary>
        /// Initialize
        /// using in contructor
        /// </summary>
        public void init(int row, int col) {
            Row = row;
            Col = col;
            Value = new T[row, col];
        }

        /// <summary>
        /// Check Matrix
        /// </summary>
        public bool isSquareMatrix() => Row == Col;

        
        /// <summary>
        /// Get data in this Matrix: row, col, Diagonal
        /// </summary>
        public T[] getRow(int dimension) {
            try {
                if (dimension >= 0 && dimension < Row) {
                    T[] result = new T[Col];
                    for (int i = 0; i < Col; i++) {
                        result[i] = Value[dimension, i];
                    }
                    return result;
                } else {
                    throw new MatrixException("Cannot get undefine array from Matrix");
                }
            }
            catch(Exception) {
                throw new MatrixException("Cannot get row from Matrix");
            }
        }
        public T[] getCol(int dimension) {
            try {
                if (dimension >= 0 && dimension < Row) {
                    T[] result = new T[Row];
                    for (int i = 0; i < Row; i++) {
                        result[i] = Value[i, dimension];
                    }
                    return result;
                } else {
                    throw new MatrixException("Cannot get undefine array from Matrix");
                }
            }
            catch(Exception) {
                throw new MatrixException("Cannot get col from Matrix");
            }
        }
        public T[] getMainDiagonal() {
            try {
                if (isSquareMatrix()) {
                    T[] result = new T[Row];
                    for( int i = 0; i < Row; i++) {
                        result[i] = Value[i, i];
                    }
                    return result;
                } else {
                    throw new MatrixException("Matrix is not Square Matrix.");
                }
            }
            catch(Exception) {
                throw new MatrixException("Cannot get Diagonal from Matrix.");
            }
        }

        /// <summary>
        /// Method
        /// </summary>
        public static Matrix<T> Add(Matrix<T> matrix1, Matrix<T> matrix2) {
            try {
                if (matrix1.Row == matrix2.Row
                    && matrix1.Col == matrix2.Col) {
                    Matrix<T> result = new Matrix<T>(matrix1.Row, matrix1.Col);
                    // Todo
                    return result;
                } 
                else {
                    throw new MatrixException("Two matrices of different sizes.");
                }
            }
            catch {
                throw new MatrixException("Cannot add 2 matrices");
            }
        }

        /// <summary>
        /// Parse and  converter
        /// </summary>

        /// <summary>
        /// Operator as : + - * !
        /// </summary>

        /// <summary>
        /// Action
        /// </summary>
    }
}