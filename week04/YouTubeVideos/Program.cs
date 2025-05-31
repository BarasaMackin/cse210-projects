using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    // Class to represent a comment on a video
    public class Comment
    {
        public string CommenterName { get; set; }
        public string Text { get; set; }

        public Comment(string commenterName, string text)
        {
            CommenterName = commenterName;
            Text = text;
        }
    }

    // Class to represent a YouTube video
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int LengthInSeconds { get; set; }
        private List<Comment> Comments { get; set; }

        public Video(string title, string author, int lengthInSeconds)
        {
            Title = title;
            Author = author;
            LengthInSeconds = lengthInSeconds;
            Comments = new List<Comment>();
        }

        // Add a comment to the video
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        // Get the number of comments
        public int GetNumberOfComments()
        {
            return Comments.Count;
        }

        // Get the list of comments
        public List<Comment> GetComments()
        {
            return Comments;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a list to hold videos
            List<Video> videos = new List<Video>();

            // Create videos and add comments
            Video video1 = new Video("C# Tutorial for Beginners", "John Doe", 600);
            video1.AddComment(new Comment("Alice", "Great tutorial!"));
            video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
            video1.AddComment(new Comment("Charlie", "Can you do an advanced tutorial?"));

            Video video2 = new Video("Learn ASP.NET Core", "Jane Smith", 900);
            video2.AddComment(new Comment("Dave", "This helped me a lot."));
            video2.AddComment(new Comment("Eve", "Clear explanations."));
            video2.AddComment(new Comment("Frank", "Looking forward to more videos."));

            Video video3 = new Video("LINQ in C#", "Mike Johnson", 450);
            video3.AddComment(new Comment("Grace", "LINQ is so powerful!"));
            video3.AddComment(new Comment("Heidi", "Thanks for the examples."));
            video3.AddComment(new Comment("Ivan", "Can you cover async LINQ?"));

            Video video4 = new Video("Entity Framework Core Basics", "Sara Lee", 750);
            video4.AddComment(new Comment("Judy", "EF Core makes data access easy."));
            video4.AddComment(new Comment("Karl", "Good introduction."));
            video4.AddComment(new Comment("Liam", "Please do a tutorial on migrations."));

            // Add videos to the list
            videos.Add(video1);
            videos.Add(video2);
            videos.Add(video3);
            videos.Add(video4);

            // Display video information and comments
            foreach (Video video in videos)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length (seconds): {video.LengthInSeconds}");
                Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
                Console.WriteLine("Comments:");
                foreach (Comment comment in video.GetComments())
                {
                    Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
                }
                Console.WriteLine(); // Blank line between videos
            }
        }
    }
}
