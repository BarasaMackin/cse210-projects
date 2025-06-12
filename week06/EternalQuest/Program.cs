using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    abstract class Goal
    {
        private string _name;
        private string _description;
        private int _points;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        public string Name { get { return _name; } }
        public string Description { get { return _description; } }
        public int Points { get { return _points; } }

        public abstract bool IsComplete();
        public abstract int RecordEvent();
        public abstract string GetStatus();
        public abstract string Serialize();
        public static Goal Deserialize(string data)
        {
            // Format: Type|Name|Description|Points|ExtraData
            string[] parts = data.Split('|');
            string type = parts[0];
            string name = parts[1];
            string description = parts[2];
            int points = int.Parse(parts[3]);

            switch (type)
            {
                case "SimpleGoal":
                    bool completed = bool.Parse(parts[4]);
                    return new SimpleGoal(name, description, points, completed);
                case "EternalGoal":
                    return new EternalGoal(name, description, points);
                case "ChecklistGoal":
                    int targetCount = int.Parse(parts[4]);
                    int currentCount = int.Parse(parts[5]);
                    int bonusPoints = int.Parse(parts[6]);
                    return new ChecklistGoal(name, description, points, targetCount, bonusPoints, currentCount);
                default:
                    throw new Exception("Unknown goal type");
            }
        }
    }

    class SimpleGoal : Goal
    {
        private bool _completed;

        public SimpleGoal(string name, string description, int points, bool completed = false)
            : base(name, description, points)
        {
            _completed = completed;
        }

        public override bool IsComplete()
        {
            return _completed;
        }

        public override int RecordEvent()
        {
            if (!_completed)
            {
                _completed = true;
                return Points;
            }
            return 0;
        }

        public override string GetStatus()
        {
            return _completed ? "[X]" : "[ ]";
        }

        public override string Serialize()
        {
            return $"SimpleGoal|{Name}|{Description}|{Points}|{_completed}";
        }
    }

    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public override bool IsComplete()
        {
            return false; // Never complete
        }

        public override int RecordEvent()
        {
            return Points;
        }

        public override string GetStatus()
        {
            return "[∞]";
        }

        public override string Serialize()
        {
            return $"EternalGoal|{Name}|{Description}|{Points}";
        }
    }

    class ChecklistGoal : Goal
    {
        private int _targetCount;
        private int _currentCount;
        private int _bonusPoints;

        public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints, int currentCount = 0)
            : base(name, description, points)
        {
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
            _currentCount = currentCount;
        }

        public override bool IsComplete()
        {
            return _currentCount >= _targetCount;
        }

        public override int RecordEvent()
        {
            if (!IsComplete())
            {
                _currentCount++;
                if (IsComplete())
                {
                    return Points + _bonusPoints;
                }
                else
                {
                    return Points;
                }
            }
            return 0;
        }

        public override string GetStatus()
        {
            string status = IsComplete() ? "[X]" : "[ ]";
            return $"{status} Completed {_currentCount}/{_targetCount} times";
        }

        public override string Serialize()
        {
            return $"ChecklistGoal|{Name}|{Description}|{Points}|{_targetCount}|{_currentCount}|{_bonusPoints}";
        }
    }

    class GoalManager
    {
        private List<Goal> _goals = new List<Goal>();
        private int _score = 0;

        public int Score { get { return _score; } }

        public void AddGoal(Goal goal)
        {
            _goals.Add(goal);
        }

        public void RecordEvent(int index)
        {
            if (index >= 0 && index < _goals.Count)
            {
                int pointsEarned = _goals[index].RecordEvent();
                _score += pointsEarned;
                Console.WriteLine($"You earned {pointsEarned} points!");
            }
            else
            {
                Console.WriteLine("Invalid goal index.");
            }
        }

        public void DisplayGoals()
        {
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()} {_goals[i].Name} ({_goals[i].Description})");
            }
        }

        public void Save(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_score);
                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.Serialize());
                }
            }
            Console.WriteLine("Goals and score saved.");
        }

        public void Load(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Save file not found.");
                return;
            }

            _goals.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                string scoreLine = reader.ReadLine();
                _score = int.Parse(scoreLine);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Goal goal = Goal.Deserialize(line);
                    _goals.Add(goal);
                }
            }
            Console.WriteLine("Goals and score loaded.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();
            string saveFile = "goals.txt";

            while (true)
            {
                Console.WriteLine("\nEternal Quest - Main Menu");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. Record Event");
                Console.WriteLine("3. Display Goals");
                Console.WriteLine("4. Display Score");
                Console.WriteLine("5. Save Goals");
                Console.WriteLine("6. Load Goals");
                Console.WriteLine("7. Quit");
                Console.Write("Select an option: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        CreateGoal(manager);
                        break;
                    case "2":
                        RecordEvent(manager);
                        break;
                    case "3":
                        manager.DisplayGoals();
                        break;
                    case "4":
                        Console.WriteLine($"Current Score: {manager.Score}");
                        break;
                    case "5":
                        manager.Save(saveFile);
                        break;
                    case "6":
                        manager.Load(saveFile);
                        break;
                    case "7":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CreateGoal(GoalManager manager)
        {
            Console.WriteLine("Select Goal Type:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            Console.Write("Enter goal name: ");
            string name = Console.ReadLine();
            Console.Write("Enter goal description: ");
            string description = Console.ReadLine();
            Console.Write("Enter points for this goal: ");
            int points = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case "1":
                    manager.AddGoal(new SimpleGoal(name, description, points));
                    break;
                case "2":
                    manager.AddGoal(new EternalGoal(name, description, points));
                    break;
                case "3":
                    Console.Write("Enter number of times to complete the goal: ");
                    int targetCount = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points for completing the goal: ");
                    int bonusPoints = int.Parse(Console.ReadLine());
                    manager.AddGoal(new ChecklistGoal(name, description, points, targetCount, bonusPoints));
                    break;
                default:
                    Console.WriteLine("Invalid goal type.");
                    break;
            }
        }

        static void RecordEvent(GoalManager manager)
        {
            Console.WriteLine("Select a goal to record an event:");
            manager.DisplayGoals();
            Console.Write("Choice: ");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                manager.RecordEvent(choice - 1);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}
