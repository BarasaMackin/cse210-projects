// Job.cs
using System;

public class Job
{
    private string _jobTitle;
    private string _company;
    private string _startDate;
    private string _endDate;

    // Constructor
    public Job(string jobTitle, string company, string startDate, string endDate)
    {
        _jobTitle = jobTitle;
        _company = company;
        _startDate = startDate;
        _endDate = endDate;
    }

    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startDate}-{_endDate}");
    }
}