using System;

class TestScriptureMemorizer
{
    static void Main(string[] args)
    {
        // Test Reference constructors
        Reference ref1 = new Reference("John", 3, 16);
        Console.WriteLine($"Reference single verse: {ref1.GetBook()} {ref1.GetChapter()}:{ref1.GetVerseStart()}");

        Reference ref2 = new Reference("Proverbs", 3, 5, 6);
        Console.WriteLine($"Reference verse range: {ref2.GetBook()} {ref2.GetChapter()}:{ref2.GetVerseStart()}-{ref2.GetVerseEnd()}");

        // Test Word class
        Word word = new Word("example");
        Console.WriteLine($"Word display (not hidden): {word.Display()}");
        word.Hide();
        Console.WriteLine($"Word display (hidden): {word.Display()}");

        // Test Scripture class
        string text = "Trust in the Lord with all your heart";
        Scripture scripture = new Scripture(ref2, text);
        scripture.Display();

        scripture.HideRandomWords(2);
        scripture.Display();

        Console.WriteLine($"All words hidden? {scripture.AllWordsHidden()}");

        // Hide all words
        scripture.HideRandomWords(scripture.GetWords().Count);
        scripture.Display();

        Console.WriteLine($"All words hidden? {scripture.AllWordsHidden()}");
    }
}
