// Journal.cs
using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };
    
    private Random _random = new Random();

    public void AddEntry(string response)
    {
        string prompt = GetRandomPrompt();
        _entries.Add(new Entry(prompt, response));
    }

    public void DisplayEntries()
    {
        foreach (var entry in _entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine("Prompt,Response,Date");
            foreach (var entry in _entries)
            {
                writer.WriteLine($"{entry.Prompt},{entry.Response},{entry.Date}");
            }
        }
    }

    public void LoadFromFile(string fileName)
    {
        _entries.Clear();
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            reader.ReadLine(); // skip header
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    var entry = new Entry(parts[0], parts[1]) 
                    {
                        Date = DateTime.Parse(parts[2])
                    };
                    _entries.Add(entry);
                }
            }
        }
    }

    private string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}