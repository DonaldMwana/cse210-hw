using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===================================");
        Console.WriteLine("   FITNESS ACTIVITY TRACKER APP");
        Console.WriteLine("   Object-Oriented Programming");
        Console.WriteLine("===================================\n");

        List<Activity> activities = new List<Activity>();

        activities.Add(new Running("03 Nov 2022", 30, 3.0));   // miles
        activities.Add(new Cycling("03 Nov 2022", 45, 12.0));   // mph
        activities.Add(new Swimming("03 Nov 2022", 60, 20));    // laps

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

// ================= BASE CLASS =================
abstract class Activity
{
    private string _date;
    private int _minutes;

    public Activity(string date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    protected string GetDate()
    {
        return _date;
    }

    protected int GetMinutes()
    {
        return _minutes;
    }

    // Polymorphism methods
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Professional summary output
    public virtual string GetSummary()
    {
        return
$"""
----------------------------------------
🏋️ FITNESS ACTIVITY REPORT
----------------------------------------
📅 Date: {_date}
🏃 Activity: {this.GetType().Name}
⏱ Duration: {_minutes} minutes
📏 Distance: {GetDistance():0.00}
🚀 Speed: {GetSpeed():0.00}
📊 Pace: {GetPace():0.00}
----------------------------------------
""";
    }
}

// ================= RUNNING =================
class Running : Activity
{
    private double _distance; // miles

    public Running(string date, int minutes, double distance)
        : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return (_distance / GetMinutes()) * 60;
    }

    public override double GetPace()
    {
        return GetMinutes() / _distance;
    }
}

// ================= CYCLING =================
class Cycling : Activity
{
    private double _speed; // mph

    public Cycling(string date, int minutes, double speed)
        : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return (_speed * GetMinutes()) / 60;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        return 60 / _speed;
    }
}

// ================= SWIMMING =================
class Swimming : Activity
{
    private int _laps;
    private const double LapMeters = 50;

    public Swimming(string date, int minutes, int laps)
        : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        // Convert laps → km → miles (as required option)
        return (_laps * LapMeters / 1000) * 0.62;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / GetMinutes()) * 60;
    }

    public override double GetPace()
    {
        return GetMinutes() / GetDistance();
    }
}