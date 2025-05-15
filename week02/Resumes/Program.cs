// Program.cs
using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Create job instances
        Job job1 = new Job("Software Engineer", "Microsoft", "2019", "2022");
        Job job2 = new Job("Manager", "Apple", "2022", "2023");

        // Create resume instance
        Resume myResume = new Resume("Barasa Khatete");

        // Add jobs to resume
        myResume.AddJob(job1);
        myResume.AddJob(job2);

        // Display resume details
        myResume.Display();
    }
}