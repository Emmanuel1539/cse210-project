using System;
using System.Collections.Generic;

public abstract class Activity
{
    public DateTime Date { get; set; }
    public int DurationMinutes { get; set; }  // Duration in minutes

    public Activity(DateTime date, int durationMinutes)
    {
        Date = date;
        DurationMinutes = durationMinutes;
    }

    public abstract double GetDistance();  // in miles or kilometers
    public abstract double GetSpeed();  // in mph or kph
    public abstract double GetPace();  // in minutes per mile or kilometer

    public virtual string GetSummary()
    {
        return $"{Date.ToString("dd MMM yyyy")} {this.GetType().Name} ({DurationMinutes} min): Distance {GetDistance():F2} units, Speed {GetSpeed():F2} units/hour, Pace: {GetPace():F2} min/unit";
    }
}

public class Running : Activity
{
    public double Distance { get; set; }  // in miles or kilometers

    public Running(DateTime date, int durationMinutes, double distance)
        : base(date, durationMinutes)
    {
        Distance = distance;
    }

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        return (Distance / DurationMinutes) * 60;
    }

    public override double GetPace()
    {
        return DurationMinutes / Distance;
    }
}

public class Cycling : Activity
{
    public double Speed { get; set; }  // in mph or kph

    public Cycling(DateTime date, int durationMinutes, double speed)
        : base(date, durationMinutes)
    {
        Speed = speed;
    }

    public override double GetDistance()
    {
        return (Speed * DurationMinutes) / 60;
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetPace()
    {
        return 60 / Speed;
    }
}

public class Swimming : Activity
{
    public int Laps { get; set; }
    private const double LapDistanceMeters = 50;
    private const double MetersToKilometers = 0.001;
    private const double KilometersToMiles = 0.621371;

    public Swimming(DateTime date, int durationMinutes, int laps)
        : base(date, durationMinutes)
    {
        Laps = laps;
    }

    public override double GetDistance()
    {
        return Laps * LapDistanceMeters * MetersToKilometers;  // distance in kilometers
    }

    public override double GetSpeed()
    {
        return (GetDistance() / DurationMinutes) * 60;
    }

    public override double GetPace()
    {
        return DurationMinutes / GetDistance();
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} (Laps: {Laps})";
    }
}

class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2024, 6, 3), 30, 3.0),
            new Cycling(new DateTime(2024, 6, 4), 45, 20.0),
            new Swimming(new DateTime(2024, 6, 5), 60, 40)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
