/**
 * Created by TruongNBN on 12/10/2021
 */

using System;
using exception;

namespace number {
	public class Fraction {
		// Constructor
		public Fraction() => init(0,1);
		public Fraction(long numerator = 0, long denominator = 1) => init(numerator, denominator);
		public Fraction(long value) => init(value,1);
		public Fraction(double doubleValue) {
			Fraction temp = ToFraction(doubleValue);
			init(temp.Numerator, temp.Denominator);
		}
		public Fraction(string stringValue) {
			Fraction temp = ToFraction(stringValue);
			init(temp.Numerator, temp.Denominator);
		}

		// initialize: use this in contructor
		public void init(long numerator, long denominator) {
			Numerator = numerator;
			Denominator = denominator;
			ReduceFraction(this);
		}

		// Properties
		public long Numerator { get; set; } = 0;
		public long Denominator { get; set; } = 1;
		public long Value {
			set {
				Numerator = value;
				Denominator = 1;
			}
		}

		// Check method
		public bool IsInfinity() => IsNaN() && Numerator > 0;
		public bool IsNegativeInfinity() => IsNaN() && Numerator < 0;
		public bool IsNaN() => Denominator == 0;

		// Parse Method
		public static Fraction ToFraction(double doubleValue) {
			try {
				checked {
					Fraction frac;
					if (doubleValue % 1 == 0) frac = new Fraction((long) doubleValue);
					else {
						double dTemp = doubleValue;
						string strTemp = doubleValue.ToString();
						long multiple = 1;
						while(strTemp.IndexOf("E") > 0) {
							dTemp *= 10;
							multiple *= 10;
							strTemp = dTemp.ToString();
						}
						int index = 0;
						while(strTemp[index]!='.') {
							index++;
						}
						int digitsAfterDecimal = strTemp.Length - index - 1;
						while (digitsAfterDecimal > 0) {
							dTemp *= 10;
							multiple *= 10;
							digitsAfterDecimal--;
						}
						frac = new Fraction((int)Math.Round(dTemp), multiple);
					}
					return frac;
				}
			}
            catch(OverflowException) {
				throw new FractionException("Conversion not possible due to overflow");
			}
			catch(Exception) {
				throw new FractionException("Conversion not possible");
			}
		}
		public static Fraction ToFraction(string stringValue) {
			int index;
			int length = stringValue.Length;
			for (index = 0; index < length; index ++) {
				if (stringValue[index] == '/') break;
			}
			if (index == length) return ToFraction(Convert.ToDouble(stringValue));
			long numerator = Convert.ToInt64(stringValue.Substring(0,index));
			long denominator = Convert.ToInt64(stringValue.Substring(index + 1));
			return new Fraction(numerator, denominator);
		}

		// Convert methor
		public double ToDouble() => (double)Numerator / Denominator;
		public override string ToString() => $"{Numerator}/{Denominator}";
		public override int GetHashCode() => Convert.ToInt32((Numerator ^ Denominator)& 0xFFFFFFFF);

		// Operator: includes -(unary), and binary opertors such as +,-,*,/
		public static Fraction operator +(Fraction operand) => operand;
		public static Fraction operator -(Fraction operand) => Negate(operand);
		public static Fraction operator +(Fraction lhs, Fraction rhs) => Add(lhs, rhs);
		public static Fraction operator +(int lhs, Fraction rhs) => Add(lhs, rhs);
		public static Fraction operator +(Fraction lhs, int rhs) => Add(lhs, rhs);
		public static Fraction operator +(double lhs, Fraction rhs) => Add(lhs, rhs);
		public static Fraction operator +(Fraction lhs, double rhs) => Add(lhs, rhs);
		public static Fraction operator -(Fraction lhs, Fraction rhs) => Add(lhs, -rhs);
		public static Fraction operator -(int lhs, Fraction rhs) => Add(lhs, -rhs);
		public static Fraction operator -(Fraction lhs, int rhs) => Add(lhs, -rhs);
		public static Fraction operator -(double lhs, Fraction rhs) => Add(lhs, -rhs);
		public static Fraction operator -(Fraction lhs, double rhs) => Add(lhs, -rhs);
		public static Fraction operator *(Fraction lhs, Fraction rhs) => Multiply(lhs, rhs);
		public static Fraction operator *(int lhs, Fraction rhs) => Multiply(lhs, rhs);
		public static Fraction operator *(Fraction lhs, int rhs) => Multiply(lhs, rhs);
		public static Fraction operator *(double lhs, Fraction rhs) => Multiply(lhs, rhs);
		public static Fraction operator *(Fraction lhs, double rhs) => Multiply(lhs, rhs);
		public static Fraction operator /(Fraction lhs, Fraction rhs) => Multiply(lhs, Inverse(rhs));
		public static Fraction operator /(int lhs, Fraction rhs) => Multiply(lhs, Inverse(rhs));
		public static Fraction operator /(Fraction lhs, int rhs) => Multiply(lhs, Inverse(rhs));
		public static Fraction operator /(double lhs, Fraction rhs) => Multiply(lhs, Inverse(rhs));
		public static Fraction operator /(Fraction lhs, double rhs) => Multiply(lhs, Inverse(rhs));

		// Operator: includes relational and logical operators such as ==,!=,<,>,<=,>=
		public static bool operator ==(Fraction lhs, Fraction rhs) => lhs.Equals(rhs);
		public static bool operator !=(Fraction lhs, Fraction rhs) => !lhs.Equals(rhs);
		public static bool operator ==(Fraction lhs, int rhs) => lhs.Equals(rhs);
		public static bool operator !=(Fraction lhs, int rhs) => !lhs.Equals(rhs);
		public static bool operator ==(Fraction lhs, double rhs) => lhs.Equals(rhs);
		public static bool operator !=(Fraction lhs, double rhs) => !lhs.Equals(rhs);
		public static bool operator <(Fraction lhs, Fraction rhs)
			=> lhs.Numerator * rhs.Denominator < rhs.Numerator * lhs.Denominator;
		public static bool operator >(Fraction lhs, Fraction rhs)
			=> lhs.Numerator * rhs.Denominator > rhs.Numerator * lhs.Denominator;
		public static bool operator <=(Fraction lhs, Fraction rhs)
			=> lhs.Numerator * rhs.Denominator <= rhs.Numerator * lhs.Denominator;
		public static bool operator >=(Fraction lhs, Fraction rhs)
			=> lhs.Numerator * rhs.Denominator >= rhs.Numerator * lhs.Denominator;

		// Action method
		public Fraction Clone() {
			return new Fraction(Numerator, Denominator);
		}
        public override bool Equals(object obj) {
			Fraction frac = (Fraction)obj;
			return (Numerator == frac.Numerator && Denominator == frac.Denominator);
		}
		public static Fraction Negate(Fraction frac) => new Fraction(-frac.Numerator, frac.Denominator);
		public static Fraction Add(Fraction frac1, Fraction frac2) {
            try { 
				checked {
					long numerator = frac1.Numerator * frac2.Denominator + frac2.Numerator * frac1.Denominator;
					long denominator = frac1.Denominator * frac2.Denominator;
					return new Fraction(numerator, denominator);
				}
			}
			catch (OverflowException) {
				throw new FractionException("Overflow occurred while performing arithemetic operation");
			}
			catch (Exception) {
				throw new FractionException("An error occurred while performing arithemetic operation");
			}
		}
		public static Fraction Multiply(Fraction frac1, Fraction frac2) {
            try {
                checked {
					long numerator = frac1.Numerator * frac2.Numerator;
					long denominator = frac1.Denominator * frac2.Denominator;
					return new Fraction(numerator, denominator);
				}
			}
			catch (OverflowException) {
				throw new FractionException("Overflow occurred while performing arithemetic operation");
			}
			catch (Exception) {
				throw new FractionException("An error occurred while performing arithemetic operation");
			}
		}
        public static Fraction Inverse(Fraction frac) { 
			if (frac.Numerator == 0) {
				throw new FractionException("Operation not possible (Denominator cannot be assigned a ZERO Value)");
			}
			return new Fraction(frac.Denominator, frac.Numerator);
		}
		public Fraction getSimpFraction() {
			long num = Math.Abs(Numerator);
			long den = Math.Abs(Denominator);
			while(num != den && num != 0 && den != 0) {
				if (num > den) num = num % den;
				else den = den % num;
			}
			long divNumber = Denominator < 0 ? -(Math.Max(num, den)) : (Math.Max(num, den));
			return new Fraction(Numerator / divNumber, Denominator / divNumber);
		}

		// Overload operator
		public static implicit operator Fraction(long longValue) => new Fraction(longValue);
		public static implicit operator Fraction(double doubleValue) => new Fraction(doubleValue);
		public static implicit operator Fraction(string stringValue) => new Fraction(stringValue);
		public static explicit operator double(Fraction frac) => frac.ToDouble();
		public static explicit operator string(Fraction frac) => frac.ToString();

		// Reduce fraction
		public static void ReduceFraction(Fraction frac) {
			try {
				 if (frac.Numerator == 0) {
					 frac.Denominator = 1;
					 return;
				 }
				 long numAbs = Math.Abs(frac.Numerator);
				 long denAbs = Math.Abs(frac.Denominator);
				 while (numAbs != denAbs && numAbs != 0 && denAbs != 0) {
					 if (numAbs > denAbs) numAbs = numAbs % denAbs;
					 else denAbs = denAbs % numAbs;
				 }
				 long divNumber = frac.Denominator < 0 ? -Math.Max(numAbs,denAbs) : Math.Max(numAbs,denAbs);
				  frac.Numerator /= divNumber; 
				  frac.Denominator /= divNumber; 
			}
			catch (Exception exp) {
				throw new FractionException("Cannot reduce Fraction: " + exp.Message);;
			}
		}
	}
}