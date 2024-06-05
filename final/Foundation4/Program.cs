using System;
using System.Collections.Generic;

public abstract class Activity
{
    private string date;
    private int length; // Length in minutes

    public Activity(string date, int length)
    {
        this.date = date;
        this.length = length;
    }

    public string Date => date;
    public int Length => length;

    public abstract double GetDistance(); // In kilometers or miles
    public abstract double GetSpeed();    // In kph or mph
    public abstract double GetPace();     // In min per km or min per mile

    public virtual string GetSummary()
    {
        return $"{date} {GetType().Name} ({length} min): Distance {GetDistance():0.0} km, Speed {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km";
    }
}

public class Running : Activity
{
    private double distance; // Distance in kilometers

    public Running(string date, int length, double distance) : base(date, length)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return (distance / Length) * 60; // Speed in kph
    }

    public override double GetPace()
    {
        return Length / distance; // Pace in min per km
    }
}

public class Cycling : Activity
{
    private double speed; // Speed in kph

    public Cycling(string date, int length, double speed) : base(date, length)
    {
        this.speed = speed;
    }

    public override double GetDistance()
    {
        return (speed * Length) / 60; // Distance in kilometers
    }

    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetPace()
    {
        return 60 / speed; // Pace in min per km
    }
}

public class Swimming : Activity
{
    private int laps;

    public Swimming(string date, int length, int laps) : base(date, length)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return (laps * 50) / 1000.0; // Distance in kilometers
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Length) * 60; // Speed in kph
    }

    public override double GetPace()
    {
        return Length / GetDistance(); // Pace in min per km
    }

    public override string GetSummary()
    {
        double distanceMiles = GetDistance() * 0.62; // Convert kilometers to miles
        double speedMph = GetSpeed() * 0.62;        // Convert kph to mph
        double paceMile = Length / distanceMiles;    // Pace in min per mile

        return $"{Date} Swimming ({Length} min): Distance {GetDistance():0.0} km ({distanceMiles:0.0} miles), Speed {GetSpeed():0.0} kph ({speedMph:0.0} mph), Pace: {GetPace():0.0} min per km ({paceMile:0.0} min per mile)";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create some activities
        var running = new Running("03 Nov 2022", 30, 4.8); // 4.8 km in 30 minutes
        var cycling = new Cycling("03 Nov 2022", 45, 20.0); // 20 kph for 45 minutes
        var swimming = new Swimming("03 Nov 2022", 60, 30); // 30 laps in 60 minutes

        // Put activities in a list
        var activities = new List<Activity> { running, cycling, swimming };

        // Display summaries for each activity
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
            Console.WriteLine("--------------------------------------------------");
        }
    }
}
