using System;  
using System.Collections.Generic; 
using System.IO; 

public class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }
}

public class JournalManager
{
    private List<JournalEntry> journalEntries = new List<JournalEntry>();
    private string[] prompts = new string[]
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public void AddNewEntry()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);
        string response = Console.ReadLine();
        JournalEntry entry = new JournalEntry
        {
            Prompt = prompt,
            Response = response,
            Date = DateTime.Now.ToShortDateString()
        };
        journalEntries.Add(entry);
    }

    public void DisplayJournal()
    {
        foreach (JournalEntry entry in journalEntries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine();
        }
    }

    public void SaveJournal(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (JournalEntry entry in journalEntries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
    }

    public void LoadJournal(string filename)
    {
        journalEntries.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            JournalEntry entry = new JournalEntry
            {
                Date = parts[0],
                Prompt = parts[1],
                Response = parts[2]
            };
            journalEntries.Add(entry);
        }
    }
}
 
class Program 
{
    static void Main(string[] args) 
    {
        JournalManager manager = new JournalManager();
        while (true)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal");
            Console.WriteLine("4. Load journal");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option: ");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    manager.AddNewEntry();
                    break;
                case 2:
                    manager.DisplayJournal();
                    break;
                case 3:
                    Console.Write("Enter filename: ");
                    string saveFilename = Console.ReadLine();
                    manager.SaveJournal(saveFilename);
                    break;
                case 4:
                    Console.Write("Enter filename: ");
                    string loadFilename = Console.ReadLine();
                    manager.LoadJournal(loadFilename);
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }  
}    