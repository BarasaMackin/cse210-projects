using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference reference;
    private List<Word> words;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = new List<Word>();
        string[] splitWords = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in splitWords)
        {
            words.Add(new Word(word));
        }
    }

    public Reference GetReference()
    {
        return reference;
    }

    public List<Word> GetWords()
    {
        return words;
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine($"{reference.GetBook()} {reference.GetChapter()}:{reference.GetVerseStart()}" +
            (reference.GetVerseStart() != reference.GetVerseEnd() ? $"-{reference.GetVerseEnd()}" : ""));
        foreach (Word word in words)
        {
            Console.Write(word.Display() + " ");
        }
        Console.WriteLine();
    }

    public void HideRandomWords(int count)
    {
        Random rand = new Random();
        int hiddenCount = 0;
        int attempts = 0;
        int maxAttempts = words.Count * 5;

        while (hiddenCount < count && attempts < maxAttempts)
        {
            int index = rand.Next(words.Count);
            if (!words[index].IsHidden())
            {
                words[index].Hide();
                hiddenCount++;
            }
            attempts++;
        }
    }

    public bool AllWordsHidden()
    {
        foreach (Word word in words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }
}
