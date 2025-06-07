using System;
using System.Collections.Generic;
using System.Threading;

namespace Mindfulness
{
    abstract class Activity
    {
        private string _name;
        private string _description;
        private int _duration; // in seconds

        public Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine($"Starting {_name} Activity");
            Console.WriteLine();
            Console.WriteLine(_description);
            Console.WriteLine();
            _duration = GetDurationFromUser();
            Console.WriteLine("Get ready to begin...");
            PauseWithAnimation(5);
        }

        public void End()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            PauseWithAnimation(3);
            Console.WriteLine($"You have completed the {_name} activity for {_duration} seconds.");
            PauseWithAnimation(5);
        }

        protected int GetDurationFromUser()
        {
            int duration = 0;
            while (true)
            {
                Console.Write("Enter the duration of the activity in seconds: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out duration) && duration > 0)
                {
                    break;
                }
                Console.WriteLine("Please enter a valid positive integer.");
            }
            return duration;
        }

        protected void PauseWithAnimation(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"\rStarting in {i} second{(i > 1 ? "s" : " ")}... ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        protected void Spinner(int durationSeconds)
        {
            string[] spinner = { "|", "/", "-", "\\" };
            int spinnerIndex = 0;
            DateTime endTime = DateTime.Now.AddSeconds(durationSeconds);
            while (DateTime.Now < endTime)
            {
                Console.Write(spinner[spinnerIndex]);
                spinnerIndex = (spinnerIndex + 1) % spinner.Length;
                Thread.Sleep(250);
                Console.Write("\b");
            }
        }

        public abstract void RunActivity();
    }

    class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        public override void RunActivity()
        {
            Start();
            int elapsed = 0;
            while (elapsed < GetDuration())
            {
                Console.WriteLine("Breathe in...");
                Countdown(4);
                elapsed += 4;
                if (elapsed >= GetDuration()) break;

                Console.WriteLine("Breathe out...");
                Countdown(6);
                elapsed += 6;
            }
            End();
        }

        private void Countdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"\r{i} ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        private int GetDuration()
        {
            // Accessing private _duration via reflection or redesign is needed.
            // To keep encapsulation, we can store duration in a protected property in base class.
            // For now, workaround by re-prompting duration (not ideal).
            // Instead, store duration in a protected property in base class.
            return typeof(Activity).GetField("_duration", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) is int d ? d : 0;
        }
    }

    class ReflectionActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new List<string>
        {
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

        public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
        }

        public override void RunActivity()
        {
            Start();
            Random rand = new Random();
            string prompt = _prompts[rand.Next(_prompts.Count)];
            Console.WriteLine();
            Console.WriteLine(prompt);
            Console.WriteLine();

            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            while (DateTime.Now < endTime)
            {
                string question = _questions[rand.Next(_questions.Count)];
                Console.WriteLine(question);
                Spinner(5);
                Console.WriteLine();
            }
            End();
        }

        private int GetDuration()
        {
            return typeof(Activity).GetField("_duration", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) is int d ? d : 0;
        }
    }

    class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
        }

        public override void RunActivity()
        {
            Start();
            Random rand = new Random();
            string prompt = _prompts[rand.Next(_prompts.Count)];
            Console.WriteLine();
            Console.WriteLine(prompt);
            Console.WriteLine();
            Console.WriteLine("You may begin in:");
            Countdown(5);

            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            List<string> items = new List<string>();
            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    items.Add(input.Trim());
                }
            }

            Console.WriteLine();
            Console.WriteLine($"You listed {items.Count} items.");
            End();
        }

        private void Countdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"\r{i} ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        private int GetDuration()
        {
            return typeof(Activity).GetField("_duration", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) is int d ? d : 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            while (!quit)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Quit");
                Console.Write("Select an option (1-4): ");

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
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }

                if (activity != null)
                {
                    activity.RunActivity();
                }
            }
        }
    }
}
