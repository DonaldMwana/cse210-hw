using System;
using System.Collections.Generic;

// Comment class
class Comment
{
    private string commenterName;
    private string commentText;

    public Comment(string name, string text)
    {
        commenterName = name;
        commentText = text;
    }

    public string GetCommenterName()
    {
        return commenterName;
    }

    public string GetCommentText()
    {
        return commentText;
    }
}

// Video class
class Video
{
    private string title;
    private string author;
    private int lengthInSeconds;
    private List<Comment> comments;

    public Video(string title, string author, int length)
    {
        this.title = title;
        this.author = author;
        lengthInSeconds = length;
        comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    public List<Comment> GetComments()
    {
        return comments;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetAuthor()
    {
        return author;
    }

    public int GetLength()
    {
        return lengthInSeconds;
    }
}

// Program
class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("C# Basics", "Alice", 600);
        video1.AddComment(new Comment("John", "Great video!"));
        video1.AddComment(new Comment("Sara", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Mike", "Loved the examples."));

        Video video2 = new Video("OOP Concepts", "Bob", 800);
        video2.AddComment(new Comment("Tom", "Clear explanation."));
        video2.AddComment(new Comment("Anna", "Good job!"));
        video2.AddComment(new Comment("Lisa", "This helped me a lot."));

        Video video3 = new Video("YouTube API Overview", "Carol", 720);
        video3.AddComment(new Comment("Steve", "Interesting content."));
        video3.AddComment(new Comment("Nancy", "Thanks for sharing."));
        video3.AddComment(new Comment("David", "Well explained."));

        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video info
        foreach (Video v in videos)
        {
            Console.WriteLine($"Title: {v.GetTitle()}");
            Console.WriteLine($"Author: {v.GetAuthor()}");
            Console.WriteLine($"Length (seconds): {v.GetLength()}");
            Console.WriteLine($"Number of comments: {v.GetNumberOfComments()}");
            Console.WriteLine("Comments:");
            foreach (Comment c in v.GetComments())
            {
                Console.WriteLine($"- {c.GetCommenterName()}: {c.GetCommentText()}");
            }
            Console.WriteLine(new string('-', 40));
        }
    }
}