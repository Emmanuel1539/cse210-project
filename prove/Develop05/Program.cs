using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

// Goal Management Application
namespace EternalQuest
{
    // Base Goal Class
    public abstract class Goal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; protected set; }
        public bool IsCompleted { get; protected set; }

        public Goal(string name, string description, int points)
        {
            Name = name;
            Description = description;
            Points = points;
            IsCompleted = false;
        }

        public abstract void RecordEvent();

        public override string ToString()
        {
            return $"{Name}: {Description} - Points: {Points} - Completed: {IsCompleted}";
        }
    }

    // Simple Goal Class
    public class SimpleGoal : Goal
    {
        public SimpleGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override void RecordEvent()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                // Logic to add points, typically handled by GoalManager
            }
        }
    }

    // Eternal Goal Class
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override void RecordEvent()
        {
            // Logic to add points each time event is recorded, typically handled by GoalManager
        }
    }

    // Checklist Goal Class
    public class ChecklistGoal : Goal
    {
        public int TargetCount { get; private set; }
        public int CurrentCount { get; private set; }
        public int BonusPoints { get; private set; }

        public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
            : base(name, description, points)
        {
            TargetCount = targetCount;
            CurrentCount = 0;
            BonusPoints = bonusPoints;
        }

        public override void RecordEvent()
        {
            if (!IsCompleted)
            {
                CurrentCount++;
                if (CurrentCount >= TargetCount)
                {
                    IsCompleted = true;
                    // Logic to add bonus points, typically handled by GoalManager
                }
                // Logic to add points, typically handled by GoalManager
            }
        }

        public override string ToString()
        {
            return base.ToString() + $" - Progress: {CurrentCount}/{TargetCount}";
        }
    }

    // Goal Manager Class
    public class GoalManager
    {
        private List<Goal> goals;
        private int totalScore;

        public GoalManager()
        {
            goals = new List<Goal>();
            totalScore = 0;
        }

        public void AddGoal(Goal goal)
        {
            goals.Add(goal);
        }

        public void RecordEvent(string goalName)
        {
            var goal = goals.FirstOrDefault(g => g.Name == goalName);
            if (goal != null)
            {
                goal.RecordEvent();
                totalScore += goal.Points;
                if (goal is ChecklistGoal checklistGoal && checklistGoal.IsCompleted)
                {
                    totalScore += checklistGoal.BonusPoints;
                }
            }
        }

        public void DisplayGoals()
        {
            foreach (var goal in goals)
            {
                Console.WriteLine(goal);
            }
        }

        public int GetTotalScore()
        {
            return totalScore;
        }

        public void SaveGoals(string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var data = new
            {
                Goals = goals,
                TotalScore = totalScore
            };
            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
        }

        public void LoadGoals(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);
            var goalsJson = data["Goals"].ToString();
            goals = JsonSerializer.Deserialize<List<Goal>>(goalsJson, options);
            totalScore = Convert.ToInt32(data["TotalScore"]);
        }
    }

    // Main Program Class
    class Program
    {
        static void Main(string[] args)
        {
            GoalManager goalManager = new GoalManager();

            // Add some sample goals
            goalManager.AddGoal(new SimpleGoal("Run a marathon", "Complete a full marathon", 1000));
            goalManager.AddGoal(new EternalGoal("Read Scriptures", "Read scriptures daily", 100));
            goalManager.AddGoal(new ChecklistGoal("Attend Temple", "Attend the temple 10 times", 50, 10, 500));

            while (true)
            {
                Console.WriteLine("\nEternal Quest Program");
                Console.WriteLine("1. Display Goals");
                Console.WriteLine("2. Add Goal");
                Console.WriteLine("3. Record Event");
                Console.WriteLine("4. Display Score");
                Console.WriteLine("5. Save Goals");
                Console.WriteLine("6. Load Goals");
                Console.WriteLine("7. Exit");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        goalManager.DisplayGoals();
                        break;
                    case "2":
                        AddGoal(goalManager);
                        break;
                    case "3":
                        RecordEvent(goalManager);
                        break;
                    case "4":
                        Console.WriteLine($"Total Score: {goalManager.GetTotalScore()}");
                        break;
                    case "5":
                        goalManager.SaveGoals("goals.json");
                        break;
                    case "6":
                        goalManager.LoadGoals("goals.json");
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddGoal(GoalManager goalManager)
        {
            Console.Write("Enter goal type (Simple/Eternal/Checklist): ");
            string type = Console.ReadLine();

            Console.Write("Enter goal name: ");
            string name = Console.ReadLine();

            Console.Write("Enter goal description: ");
            string description = Console.ReadLine();

            Console.Write("Enter goal points: ");
            int points = int.Parse(Console.ReadLine());

            if (type.Equals("Simple", StringComparison.OrdinalIgnoreCase))
            {
                goalManager.AddGoal(new SimpleGoal(name, description, points));
            }
            else if (type.Equals("Eternal", StringComparison.OrdinalIgnoreCase))
            {
                goalManager.AddGoal(new EternalGoal(name, description, points));
            }
            else if (type.Equals("Checklist", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter target count: ");
                int targetCount = int.Parse(Console.ReadLine());

                Console.Write("Enter bonus points: ");
                int bonusPoints = int.Parse(Console.ReadLine());

                goalManager.AddGoal(new ChecklistGoal(name, description, points, targetCount, bonusPoints));
            }
            else
            {
                Console.WriteLine("Invalid goal type.");
            }
        }

        static void RecordEvent(GoalManager goalManager)
        {
            Console.Write("Enter goal name to record event: ");
            string name = Console.ReadLine();

            goalManager.RecordEvent(name);
        }
    }
}
