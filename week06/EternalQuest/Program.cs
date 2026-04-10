using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Program
{
    static List<Goal> goals = new List<Goal>();

    static int totalScore = 0;
    static int level = 1;

    static int streak = 0;
    static string lastActionDate = "";

    static int comboMultiplier = 1;
    static int comboCounter = 0;

    static List<string> badges = new List<string>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n=== ETERNAL QUEST ULTIMATE ===");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Dashboard");
            Console.WriteLine("5. Save");
            Console.WriteLine("6. Load");
            Console.WriteLine("7. Exit");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            if (choice == "1") CreateGoal();
            else if (choice == "2") ListGoals();
            else if (choice == "3") RecordEvent();
            else if (choice == "4") Dashboard();
            else if (choice == "5") Save();
            else if (choice == "6") Load();
            else if (choice == "7") break;
        }
    }

    // ---------------- CREATE GOAL ----------------
    static void CreateGoal()
    {
        Console.WriteLine("\n1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("4. Negative Goal (lose points)");
        Console.Write("Choose: ");

        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
            goals.Add(new SimpleGoal(name, points));

        else if (type == "2")
            goals.Add(new EternalGoal(name, points));

        else if (type == "3")
        {
            Console.Write("Target times: ");
            int target = int.Parse(Console.ReadLine());

            Console.Write("Bonus: ");
            int bonus = int.Parse(Console.ReadLine());

            goals.Add(new ChecklistGoal(name, points, target, bonus));
        }
        else if (type == "4")
        {
            goals.Add(new NegativeGoal(name, points));
        }
    }

    // ---------------- LIST GOALS ----------------
    static void ListGoals()
    {
        Console.WriteLine("\nYOUR GOALS:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
        }
    }

    // ---------------- RECORD EVENT ----------------
    static void RecordEvent()
    {
        ListGoals();
        Console.Write("\nChoose goal: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index < 0 || index >= goals.Count) return;

        ShowLoading();

        int basePoints = goals[index].RecordEvent();

        // COMBO SYSTEM
        comboCounter++;
        if (comboCounter >= 3)
            comboMultiplier = Math.Min(comboMultiplier + 1, 5);

        int finalPoints = basePoints * comboMultiplier;
        totalScore += finalPoints;

        UpdateLevel();
        UpdateStreak();
        CheckBadges();

        Console.WriteLine($"Earned: {finalPoints} points (x{comboMultiplier} combo)");
    }

    // ---------------- DASHBOARD ----------------
    static void Dashboard()
    {
        Console.WriteLine("\n===== DASHBOARD =====");
        Console.WriteLine($"Score: {totalScore}");
        Console.WriteLine($"Level: {level}");
        Console.WriteLine($"Streak: {streak}");
        Console.WriteLine($"Combo: x{comboMultiplier}");
        Console.WriteLine($"Badges: {(badges.Count == 0 ? "None" : string.Join(", ", badges))}");
    }

    // ---------------- LEVEL SYSTEM ----------------
    static void UpdateLevel()
    {
        level = (totalScore / 1000) + 1;
    }

    // ---------------- STREAK SYSTEM ----------------
    static void UpdateStreak()
    {
        string today = DateTime.Now.ToString("yyyyMMdd");

        if (lastActionDate == "")
        {
            streak = 1;
        }
        else if (lastActionDate == today)
        {
            return;
        }
        else if (DateTime.Parse(lastActionDate).AddDays(1).ToString("yyyyMMdd") == today)
        {
            streak++;
        }
        else
        {
            streak = 1;
        }

        lastActionDate = today;
    }

    // ---------------- BADGES ----------------
    static void CheckBadges()
    {
        if (totalScore >= 1000 && !badges.Contains("Starter Hero"))
            badges.Add("Starter Hero");

        if (totalScore >= 5000 && !badges.Contains("Elite Champion"))
            badges.Add("Elite Champion");

        if (streak >= 3 && !badges.Contains("Consistency Maker"))
            badges.Add("Consistency Maker");

        if (comboMultiplier >= 3 && !badges.Contains("Combo Master"))
            badges.Add("Combo Master");
    }

    // ---------------- LOADING ANIMATION ----------------
    static void ShowLoading()
    {
        Console.Write("Processing");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(300);
            Console.Write(".");
        }
        Console.WriteLine();
    }

    // ---------------- SAVE ----------------
    static void Save()
    {
        using (StreamWriter w = new StreamWriter("goals.txt"))
        {
            w.WriteLine(totalScore);
            w.WriteLine(level);
            w.WriteLine(streak);
            w.WriteLine(lastActionDate);
            w.WriteLine(comboMultiplier);
            w.WriteLine(comboCounter);
            w.WriteLine(string.Join(",", badges));

            foreach (Goal g in goals)
                w.WriteLine(g.SaveString());
        }

        Console.WriteLine("Saved!");
    }

    // ---------------- LOAD ----------------
    static void Load()
    {
        if (!File.Exists("goals.txt"))
        {
            Console.WriteLine("No save file.");
            return;
        }

        goals.Clear();

        string[] lines = File.ReadAllLines("goals.txt");

        totalScore = int.Parse(lines[0]);
        level = int.Parse(lines[1]);
        streak = int.Parse(lines[2]);
        lastActionDate = lines[3];
        comboMultiplier = int.Parse(lines[4]);
        comboCounter = int.Parse(lines[5]);

        badges = new List<string>();
        if (lines[6] != "")
            badges.AddRange(lines[6].Split(','));

        for (int i = 7; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(':');
            string type = parts[0];
            string data = parts[1];

            if (type == "Simple")
            {
                string[] p = data.Split(',');
                goals.Add(new SimpleGoal(p[0], int.Parse(p[1])));
            }
            else if (type == "Eternal")
            {
                string[] p = data.Split(',');
                goals.Add(new EternalGoal(p[0], int.Parse(p[1])));
            }
            else if (type == "Checklist")
            {
                string[] p = data.Split(',');
                goals.Add(new ChecklistGoal(p[0], int.Parse(p[1]), int.Parse(p[3]), int.Parse(p[4])));
            }
            else if (type == "Negative")
            {
                string[] p = data.Split(',');
                goals.Add(new NegativeGoal(p[0], int.Parse(p[1])));
            }
        }

        Console.WriteLine("Loaded!");
    }
}

// ---------------- BASE CLASS ----------------
abstract class Goal
{
    protected string _name;
    protected int _points;

    public Goal(string name, int points)
    {
        _name = name;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract string SaveString();
}

// ---------------- SIMPLE ----------------
class SimpleGoal : Goal
{
    bool done = false;

    public SimpleGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent()
    {
        if (!done)
        {
            done = true;
            return _points;
        }
        return 0;
    }

    public override string GetStatus()
        => done ? "[X] " + _name : "[ ] " + _name;

    public override string SaveString()
        => $"Simple:{_name},{_points},{done}";
}

// ---------------- ETERNAL ----------------
class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent()
        => _points;

    public override string GetStatus()
        => "[∞] " + _name;

    public override string SaveString()
        => $"Eternal:{_name},{_points}";
}

// ---------------- CHECKLIST ----------------
class ChecklistGoal : Goal
{
    int count = 0;
    int target;
    int bonus;

    public ChecklistGoal(string name, int points, int target, int bonus)
        : base(name, points)
    {
        this.target = target;
        this.bonus = bonus;
    }

    public override int RecordEvent()
    {
        count++;
        if (count == target)
            return _points + bonus;

        return _points;
    }

    public override string GetStatus()
        => $"[ ] {_name} ({count}/{target})";

    public override string SaveString()
        => $"Checklist:{_name},{_points},{count},{target},{bonus}";
}

// ---------------- NEGATIVE GOAL ----------------
class NegativeGoal : Goal
{
    public NegativeGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent()
        => -_points;

    public override string GetStatus()
        => "[!] " + _name + " (Avoid!)";

    public override string SaveString()
        => $"Negative:{_name},{_points}";
}