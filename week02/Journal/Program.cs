using System;

public class Program
{
    public static void Main(string[] args)
    {
        Journal journal = new Journal();
        int choice;

        do
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal entries");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            choice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;

            switch (choice)
            {
                case 1:
                    Console.WriteLine("New Entry:");
                    Console.WriteLine("Enter your response:");
                    string response = Console.ReadLine();
                    journal.AddEntry(response);
                    Console.WriteLine("Entry added.");
                    break;

                case 2:
                    Console.WriteLine("Journal Entries:");
                    journal.DisplayEntries();
                    break;

                case 3:
                    Console.WriteLine("Enter filename to save journal:");
                    string saveFileName = Console.ReadLine();
                    journal.SaveToFile(saveFileName);
                    Console.WriteLine("Journal saved.");
                    break;

                case 4:
                    Console.WriteLine("Enter filename to load journal:");
                    string loadFileName = Console.ReadLine();
                    journal.LoadFromFile(loadFileName);
                    Console.WriteLine("Journal loaded.");
                    break;

                case 5:
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }

        } while (choice != 5);
    }
}