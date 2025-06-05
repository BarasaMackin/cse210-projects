using System;
using Homework;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Homework Project.");

        Assignment assignment = new Assignment("John Doe", "Math");
        string summary = assignment.GetSummary();
        Console.WriteLine(summary);

        MathAssignment mathAssignment = new MathAssignment("John Doe", "Math", "Section 5.2", "1-10");
        string mathSummary = mathAssignment.GetSummary();
        string homeworkList = mathAssignment.GetHomeworkList();
        Console.WriteLine(mathSummary);
        Console.WriteLine(homeworkList);

        WritingAssignment writingAssignment = new WritingAssignment("Jane Smith", "English", "Essay on Shakespeare");
        string writingSummary = writingAssignment.GetSummary();
        string writingInfo = writingAssignment.GetWritingInformation();
        Console.WriteLine(writingSummary);
        Console.WriteLine(writingInfo);
    }
}