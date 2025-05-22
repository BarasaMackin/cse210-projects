using System;

class TestFraction
{
    static void Main(string[] args)
    {
        // Test default constructor
        Fraction f1 = new Fraction();
        Console.WriteLine($"Default constructor: {f1.GetFractionString()} = {f1.GetDecimalValue()}");

        // Test constructor with numerator only
        Fraction f2 = new Fraction(3);
        Console.WriteLine($"Numerator only constructor: {f2.GetFractionString()} = {f2.GetDecimalValue()}");

        // Test constructor with numerator and denominator
        Fraction f3 = new Fraction(3, 4);
        Console.WriteLine($"Numerator and denominator constructor: {f3.GetFractionString()} = {f3.GetDecimalValue()}");

        // Test getters and setters
        f3.Numerator = 5;
        f3.Denominator = 6;
        Console.WriteLine($"After setters: {f3.GetFractionString()} = {f3.GetDecimalValue()}");

        // Test denominator zero exception
        try
        {
            Fraction f4 = new Fraction(1, 0);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Caught expected exception: {e.Message}");
        }

        try
        {
            f3.Denominator = 0;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Caught expected exception: {e.Message}");
        }
    }
}
