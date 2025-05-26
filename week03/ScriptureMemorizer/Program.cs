using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a Reference object for a scripture with verse range
        Reference reference = new Reference("Proverbs", 3, 5, 6);

        // Scripture text for the reference
        string scriptureText = "Trust in the Lord with all your heart and lean not on your own understanding";

        // Create a Scripture object
        Scripture scripture = new Scripture(reference, scriptureText);

        // Display the full scripture initially
        scripture.Display();

        while (true)
        {
            Console.WriteLine();
            Console.Write("Press Enter to hide words or type 'quit' to exit: ");
string input = Console.ReadLine();

if (input != null && input.Trim().ToLower() == "quit")
{
    break;
}

            // Hide 2 random words
            scripture.HideRandomWords(2);

            // Display the scripture again
            scripture.Display();

            // If all words are hidden, end the program
            if (scripture.AllWordsHidden())
            {
                Console.WriteLine("All words are hidden. Program will exit.");
                break;
            }
        }
    }
}
