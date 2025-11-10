using System;
using System.Collections.Generic;
using System.Linq;

public class ScriptureReference
{
    public string Book { get; set; }
    public int Chapter { get; set; }
    public int StartVerse { get; set; }
    public int? EndVerse { get; set; }

    public ScriptureReference(string book, int chapter, int startVerse, int? endVerse = null)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        if (EndVerse.HasValue)
        {
            return $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
        }
        else
        {
            return $"{Book} {Chapter}:{StartVerse}";
        }
    }
}

public class ScriptureWord
{
    public string Text { get; set; }
    public bool IsHidden { get; set; }

    public ScriptureWord(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public string GetDisplayText()
    {
        if (IsHidden)
        {
            return new string('_', Text.Length);
        }
        else
        {
            return Text;
        }
    }
}

public class Scripture
{
    public ScriptureReference Reference { get; set; }
    public List<ScriptureWord> Words { get; set; }

    public Scripture(ScriptureReference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new ScriptureWord(word)).ToList();
    }

    public void HideRandomWords(int count)
    {
        Random random = new Random();
        for (int i = 0; i < count; i++)
        {
            var word = Words[random.Next(Words.Count)];
            if (!word.IsHidden)
            {
                word.IsHidden = true;
            }
            else
            {
                i--;
            }
        }
    }

    public string GetDisplayText()
    {
        return string.Join(" ", Words.Select(word => word.GetDisplayText()));
    }
}

class Program
{
    static void Main(string[] args)
    {
        ScriptureReference reference = new ScriptureReference("John", 3, 16);
        Scripture scripture = new Scripture(reference, "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

        Console.WriteLine($"{scripture.Reference} - {scripture.GetDisplayText()}");
        Console.WriteLine("Press enter to hide words, type 'quit' to exit.");

        while (true)
        {
            var input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }

            Console.Clear();
            scripture.HideRandomWords(3);
            Console.WriteLine($"{scripture.Reference} - {scripture.GetDisplayText()}");
            Console.WriteLine("Press enter to hide words, type 'quit' to exit.");

            if (scripture.Words.All(word => word.IsHidden))
            {
                Console.WriteLine("All words are hidden. Exiting...");
                break;
            }
        }
    }
}
