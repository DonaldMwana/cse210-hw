using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static int breathingCount = 0;
    static int reflectionCount = 0;
    static int listingCount = 0;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Mindfulness Program ===");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. View Session Report");
            Console.WriteLine("5. Quit");
            Console.Write("\nSelect an option: ");

            string choice = Console.ReadLine();

            Activity activity = null;

            switch (choice)
            {
                case "1":
                    breathingCount++;
                    activity = new BreathingActivity();
                    break;

                case "2":
                    reflectionCount++;
                    activity = new ReflectionActivity();
                    break;

                case "3":
                    listingCount++;
                    activity = new ListingActivity();
                    break;

                case "4":
                    ShowReport();
                    continue;

                case "5":
                    ShowReport();
                    return;
            }

            activity?.Run();
        }
    }

    static void ShowReport()
    {
        Console.Clear();
        Console.WriteLine("=== SESSION REPORT ===");
        Console.WriteLine($"Breathing Activity: {breathingCount} times");
        Console.WriteLine($"Reflection Activity: {reflectionCount} times");
        Console.WriteLine($"Listing Activity: {listingCount} times");

        int total = breathingCount + reflectionCount + listingCount;

        Console.WriteLine($"\nTotal Activities Completed: {total}");

        if (total > 0)
        {
            Console.WriteLine("\nGreat job staying mindful today!");
            Console.WriteLine("Keep building your mental wellness 💙");
        }

        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }
}

// ================= BASE CLASS =================
class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;

    public void Run()
    {
        DisplayStart();
        Prepare();
        PerformActivity();
        End();
    }

    protected void DisplayStart()
    {
        Console.Clear();
        Console.WriteLine($"--- {_name} ---");
        Console.WriteLine(_description);

        Console.Write("\nEnter duration (seconds): ");
        _duration = int.Parse(Console.ReadLine());
    }

    protected void Prepare()
    {
        Console.WriteLine("\nGet ready...");
        Spinner(3);
    }

    protected void End()
    {
        Console.WriteLine("\nWell done!");
        Spinner(2);

        Console.WriteLine($"You completed {_name} for {_duration} seconds.");
        Spinner(3);

        Console.WriteLine("\nBonus thought: Stay present and grateful today 🌿");
        Thread.Sleep(1500);
    }

    protected void Spinner(int seconds)
    {
        string[] spin = { "|", "/", "-", "\\" };
        DateTime end = DateTime.Now.AddSeconds(seconds);

        int i = 0;
        while (DateTime.Now < end)
        {
            Console.Write(spin[i % spin.Length]);
            Thread.Sleep(150);
            Console.Write("\b");
            i++;
        }
    }

    protected void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    protected virtual void PerformActivity() { }
}

// ================= BREATHING =================
class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        _name = "Breathing Activity";
        _description = "Relax by breathing in and out slowly.";
    }

    protected override void PerformActivity()
    {
        DateTime end = DateTime.Now.AddSeconds(_duration);
        bool inhale = true;

        while (DateTime.Now < end)
        {
            if (inhale)
                Console.Write("\nBreathe in... ");
            else
                Console.Write("\nBreathe out... ");

            Countdown(4);
            inhale = !inhale;
        }
    }
}

// ================= REFLECTION =================
class ReflectionActivity : Activity
{
    List<string> prompts = new List<string>()
    {
        "Think of a time you helped someone in need.",
        "Think of a time you overcame a challenge.",
        "Think of a time you stood strong for someone.",
        "Think of a time you did something meaningful."
    };

    List<string> questions = new List<string>()
    {
        "Why was this meaningful?",
        "What did you learn?",
        "How did you feel?",
        "What would you do differently next time?",
        "How has this shaped you?"
    };

    public ReflectionActivity()
    {
        _name = "Reflection Activity";
        _description = "Reflect on moments of strength and resilience.";
    }

    protected override void PerformActivity()
    {
        Random rand = new Random();

        Console.WriteLine("\nPrompt:");
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);

        Console.WriteLine("\nThink deeply...");
        Spinner(4);

        DateTime end = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < end)
        {
            Console.WriteLine("\n" + questions[rand.Next(questions.Count)]);
            Spinner(3);
        }
    }
}

// ================= LISTING =================
class ListingActivity : Activity
{
    List<string> prompts = new List<string>()
    {
        "List people you appreciate:",
        "List your strengths:",
        "List things you're grateful for:",
        "List people you've helped:",
        "List your personal heroes:"
    };

    public ListingActivity()
    {
        _name = "Listing Activity";
        _description = "List as many positive things as you can.";
    }

    protected override void PerformActivity()
    {
        Random rand = new Random();

        Console.WriteLine("\nPrompt:");
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);

        Console.WriteLine("\nGet ready...");
        Countdown(5);

        Console.WriteLine("\nStart listing (press Enter after each):");

        int count = 0;
        DateTime end = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < end)
        {
            Console.ReadLine();
            count++;
        }

        Console.WriteLine($"\nYou listed {count} items!");
    }
}