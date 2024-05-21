using System;
using System.Threading;

public abstract class Activity
{
    protected int duration;
    protected string name;
    protected string description;
    private static int activityCount = 0;

    public Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void Start()
    {
        activityCount++;
        Console.Clear();
        Console.WriteLine($"Starting {name} Activity");
        Console.WriteLine(description);
        Console.Write("Enter the duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        PauseWithSpinner(5);

        RunActivity();

        End();
    }

    protected abstract void RunActivity();

    protected void End()
    {
        Console.WriteLine("Good job!");
        Console.WriteLine($"You have completed the {name} activity for {duration} seconds.");
        PauseWithSpinner(5);
        SaveLog();
    }

    protected void PauseWithSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    private void SaveLog()
    {
        string log = $"{DateTime.Now}: Completed {name} activity for {duration} seconds.\n";
        System.IO.File.AppendAllText("activity_log.txt", log);
    }

    public static int GetActivityCount()
    {
        return activityCount;
    }
}

// Breathing activity 

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void RunActivity()
    {
        int cycles = duration / 6;
        for (int i = 0; i < cycles; i++)
        {
            Console.WriteLine("Breathe in...");
            Countdown(3);
            Console.WriteLine("Breathe out...");
            Countdown(3);
        }
    }

    private void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}


// Adding Reflection activities

public class ReflectionActivity : Activity
{
    private static readonly string[] Prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly string[] Questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    private List<int> usedPrompts = new List<int>();
    private List<int> usedQuestions = new List<int>();

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    protected override void RunActivity()
    {
        Random random = new Random();
        int promptIndex = GetRandomIndex(Prompts.Length, usedPrompts, random);
        string prompt = Prompts[promptIndex];
        Console.WriteLine(prompt);

        int remainingTime = duration;
        while (remainingTime > 0)
        {
            int questionIndex = GetRandomIndex(Questions.Length, usedQuestions, random);
            string question = Questions[questionIndex];
            Console.WriteLine(question);
            PauseWithSpinner(5);
            remainingTime -= 5;
        }
    }

    private int GetRandomIndex(int length, List<int> usedIndices, Random random)
    {
        if (usedIndices.Count >= length)
        {
            usedIndices.Clear();
        }
        int index;
        do
        {
            index = random.Next(length);
        } while (usedIndices.Contains(index));
        usedIndices.Add(index);
        return index;
    }
}

// Adding Listing activities
public class ListingActivity : Activity
{
    private static readonly string[] Prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private List<int> usedPrompts = new List<int>();

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void RunActivity()
    {
        Random random = new Random();
        int promptIndex = GetRandomIndex(Prompts.Length, usedPrompts, random);
        string prompt = Prompts[promptIndex];
        Console.WriteLine(prompt);
        PauseWithSpinner(5);

        Console.WriteLine("Start listing items...");
        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            string item = Console.ReadLine();
            if (!string.IsNullOrEmpty(item))
            {
                items.Add(item);
            }
        }

        Console.WriteLine($"You listed {items.Count} items.");
    }

    private int GetRandomIndex(int length, List<int> usedIndices, Random random)
    {
        if (usedIndices.Count >= length)
        {
            usedIndices.Clear();
        }
        int index;
        do
        {
            index = random.Next(length);
        } while (usedIndices.Contains(index));
        usedIndices.Add(index);
        return index;
    }
}

// Program calls
class Program
{
    static void Main(string[] args)
    {
        LoadLog();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. View Log");
            Console.WriteLine("5. Quit");

            string choice = Console.ReadLine();

            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    ViewLog();
                    continue;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    continue;
            }

            activity.Start();
        }
    }

    static void LoadLog()
    {
        if (File.Exists("activity_log.txt"))
        {
            string[] logs = File.ReadAllLines("activity_log.txt");
            foreach (var log in logs)
            {
                Console.WriteLine(log);
            }
        }
    }

    static void ViewLog()
    {
        Console.Clear();
        if (File.Exists("activity_log.txt"))
        {
            string[] logs = File.ReadAllLines("activity_log.txt");
            foreach (var log in logs)
            {
                Console.WriteLine(log);
            }
        }
        else
        {
            Console.WriteLine("No logs found.");
        }
        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
    }
}
