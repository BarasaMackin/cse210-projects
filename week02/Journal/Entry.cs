// Entry.cs
using System;

public class Entry
{
    public string Prompt { get; private set; }
    public string Response { get; private set; }
    public DateTime Date { get; private set; }

    public Entry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now; // Set to current date and time
    }

    // New constructor overload to set Date explicitly
    public Entry(string prompt, string response, DateTime date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString()
    {
        return $"{Date}: {Prompt}\nResponse: {Response}\n";
    }
}
