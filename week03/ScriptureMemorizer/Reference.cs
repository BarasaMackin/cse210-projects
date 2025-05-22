using System;

public class Reference
{
    private string book;
    private int chapter;
    private int verseStart;
    private int verseEnd;

    // Constructor for single verse
    public Reference(string book, int chapter, int verse)
    {
        this.book = book;
        this.chapter = chapter;
        this.verseStart = verse;
        this.verseEnd = verse;
    }

    // Constructor for verse range
    public Reference(string book, int chapter, int verseStart, int verseEnd)
    {
        this.book = book;
        this.chapter = chapter;
        this.verseStart = verseStart;
        this.verseEnd = verseEnd;
    }

    public string GetBook()
    {
        return book;
    }

    public int GetChapter()
    {
        return chapter;
    }

    public int GetVerseStart()
    {
        return verseStart;
    }

    public int GetVerseEnd()
    {
        return verseEnd;
    }
}
