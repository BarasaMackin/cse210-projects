using System;

namespace Fractions
{
    public class FractionUpdated
    {
        private int numerator;
        private int denominator;

        // Default constructor (1/1)
        public FractionUpdated()
        {
            numerator = 1;
            denominator = 1;
        }

        // Constructor with numerator only (denominator = 1)
        public FractionUpdated(int numerator)
        {
            this.numerator = numerator;
            this.denominator = 1;
        }

        // Constructor with numerator and denominator
        public FractionUpdated(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.");
            }
            this.numerator = numerator;
            this.denominator = denominator;
        }

        // Getter and setter for numerator
        public int Numerator
        {
            get { return numerator; }
            set { numerator = value; }
        }

        // Getter and setter for denominator
        public int Denominator
        {
            get { return denominator; }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Denominator cannot be zero.");
                }
                denominator = value;
            }
        }

        // Method to return fractional representation as string
        public string GetFractionString()
        {
            return $"{numerator}/{denominator}";
        }

        // Method to return decimal representation as double
        public double GetDecimalValue()
        {
            return (double)numerator / denominator;
        }
    }
}
