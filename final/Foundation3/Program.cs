using System;
using System.Collections.Generic;

public class Address
{
    private string streetAddress;
    private string city;
    private string state;
    private string country;

    public Address(string streetAddress, string city, string state, string country)
    {
        this.streetAddress = streetAddress;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public override string ToString()
    {
        return $"{streetAddress}, {city}, {state}, {country}";
    }
}

public abstract class Event
{
    private string title;
    private string description;
    private string date;
    private string time;
    private Address address;

    protected string Title => title;
    protected string Description => description;
    protected string Date => date;
    protected string Time => time;
    protected Address EventAddress => address;

    public Event(string title, string description, string date, string time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public string GetStandardDetails()
    {
        return $"Title: {title}\nDescription: {description}\nDate: {date}\nTime: {time}\nAddress: {address}";
    }

    public abstract string GetFullDetails();
    public abstract string GetShortDescription();
}

public class Lecture : Event
{
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, string date, string time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Lecture\nSpeaker: {speaker}\nCapacity: {capacity}";
    }

    public override string GetShortDescription()
    {
        return $"Type: Lecture\nTitle: {Title}\nDate: {Date}";
    }
}

public class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, string date, string time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Reception\nRSVP Email: {rsvpEmail}";
    }

    public override string GetShortDescription()
    {
        return $"Type: Reception\nTitle: {Title}\nDate: {Date}";
    }
}

public class OutdoorGathering : Event
{
    private string weatherForecast;

    public OutdoorGathering(string title, string description, string date, string time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Outdoor Gathering\nWeather Forecast: {weatherForecast}";
    }

    public override string GetShortDescription()
    {
        return $"Type: Outdoor Gathering\nTitle: {Title}\nDate: {Date}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create some addresses
        var address1 = new Address("123 Main St", "Springfield", "IL", "USA");
        var address2 = new Address("456 Elm St", "Vancouver", "BC", "Canada");
        var address3 = new Address("789 Oak St", "Los Angeles", "CA", "USA");

        // Create some events
        var lecture = new Lecture("Advanced C# Programming", "Learn advanced concepts in C#", "2024-07-01", "10:00 AM", address1, "John Doe", 100);
        var reception = new Reception("Company Annual Meetup", "Meet and greet with the company staff", "2024-07-02", "6:00 PM", address2, "rsvp@company.com");
        var outdoorGathering = new OutdoorGathering("Summer Picnic", "Enjoy a fun day in the sun", "2024-07-03", "12:00 PM", address3, "Sunny and warm");

        // Put events in a list
        var events = new List<Event> { lecture, reception, outdoorGathering };

        // Display event details
        foreach (var ev in events)
        {
            Console.WriteLine(ev.GetStandardDetails());
            Console.WriteLine();
            Console.WriteLine(ev.GetFullDetails());
            Console.WriteLine();
            Console.WriteLine(ev.GetShortDescription());
            Console.WriteLine("--------------------------------------------------");
        }
    }
}
