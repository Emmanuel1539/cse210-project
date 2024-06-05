using System;
using System.Collections.Generic;

class Comment
{
    public string Name { get; }
    public string Text { get; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }

    public override string ToString()
    {
        return $"{Name}: {Text}";
    }
}

class Video
{
    public string Title { get; }
    public string Author { get; }
    public int Length { get; }  // Length in seconds
    private List<Comment> Comments { get; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public override string ToString()
    {
        int minutes = Length / 60;
        int seconds = Length % 60;
        return $"Title: {Title}, Author: {Author}, Length: {minutes}m {seconds}s, Comments: {GetNumberOfComments()}";
    }

    public void DisplayComments()
    {
        foreach (var comment in Comments)
        {
            Console.WriteLine(comment);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create some videos
        var video1 = new Video("Learning Python", "John Doe", 300);
        var video2 = new Video("Cooking Pasta", "Jane Smith", 600);
        var video3 = new Video("Travel Vlog: Japan", "Alice Brown", 1200);
        var video4 = new Video("Guitar Tutorial", "Bob Johnson", 900);

        // Create some comments
        var comments1 = new List<Comment> {
            new Comment("User1", "Great video!"),
            new Comment("User2", "Very informative."),
            new Comment("User3", "Thanks for sharing!")
        };

        var comments2 = new List<Comment> {
            new Comment("UserA", "Love this recipe!"),
            new Comment("UserB", "Yummy!"),
            new Comment("UserC", "Can't wait to try it.")
        };

        var comments3 = new List<Comment> {
            new Comment("Traveler1", "Beautiful places!"),
            new Comment("Traveler2", "Awesome vlog!"),
            new Comment("Traveler3", "Well edited.")
        };

        var comments4 = new List<Comment> {
            new Comment("Musician1", "Helpful tutorial."),
            new Comment("Musician2", "Great tips!"),
            new Comment("Musician3", "I learned a lot.")
        };

        // Add comments to videos
        foreach (var comment in comments1)
        {
            video1.AddComment(comment);
        }

        foreach (var comment in comments2)
        {
            video2.AddComment(comment);
        }

        foreach (var comment in comments3)
        {
            video3.AddComment(comment);
        }

        foreach (var comment in comments4)
        {
            video4.AddComment(comment);
        }

        // Put videos in a list
        var videos = new List<Video> { video1, video2, video3, video4 };

        // Display video details and comments
        foreach (var video in videos)
        {
            Console.WriteLine(video);
            video.DisplayComments();
            Console.WriteLine();
        }
    }
}
