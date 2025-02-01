using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();
    static string filePath = "tasks.txt";

    static void Main()
    {
        LoadTasks();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Checklist Application ===");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Remove Task");
            Console.WriteLine("5. Send Reminders");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    MarkTaskCompleted();
                    break;
                case "4":
                    RemoveTask();
                    break;
                case "5":
                    SendReminders();
                    break;
                case "6":
                    SaveTasks();
                    return;
                default:
                    Console.WriteLine("Invalid choice, press Enter to try again...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void AddTask()
    {
        Console.Write("Enter the task: ");
        string taskDescription = Console.ReadLine();

        Console.Write("Enter due date (YYYY-MM-DD): ");
        DateTime dueDate;
        while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
        {
            Console.Write("Invalid date format. Enter again (YYYY-MM-DD): ");
        }

        Console.Write("Enter priority (High, Medium, Low): ");
        string priority = Console.ReadLine().ToLower();
        while (priority != "high" && priority != "medium" && priority != "low")
        {
            Console.Write("Invalid priority. Choose High, Medium, or Low: ");
            priority = Console.ReadLine().ToLower();
        }

        tasks.Add(new TaskItem
        {
            Description = taskDescription,
            DueDate = dueDate,
            Priority = priority,
            Completed = false
        });

        SaveTasks();
        Console.WriteLine("Task added! Press Enter to continue...");
        Console.ReadLine();
    }

    static void ViewTasks()
    {
        Console.WriteLine("\n=== Checklist ===");
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                string status = task.Completed ? "[✓]" : "[ ]";
                Console.WriteLine($"{i + 1}. {status} {task.Description} - Due: {task.DueDate.ToShortDateString()} - Priority: {task.Priority}");
            }
        }
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }

    static void MarkTaskCompleted()
    {
        ViewTasks();
        Console.Write("Enter task number to mark as completed: ");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= tasks.Count)
        {
            tasks[index - 1].Completed = true;
            SaveTasks();
            Console.WriteLine("Task marked as completed! Press Enter to continue...");
        }
        else
        {
            Console.WriteLine("Invalid task number! Press Enter to try again...");
        }
        Console.ReadLine();
    }

    static void RemoveTask()
    {
        ViewTasks();
        Console.Write("Enter task number to remove: ");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= tasks.Count)
        {
            tasks.RemoveAt(index - 1);
            SaveTasks();
            Console.WriteLine("Task removed! Press Enter to continue...");
        }
        else
        {
            Console.WriteLine("Invalid task number! Press Enter to try again...");
        }
        Console.ReadLine();
    }

    static void SendReminders()
    {
        Console.WriteLine("\n=== Sending Reminders ===");
        DateTime today = DateTime.Today;
        foreach (var task in tasks)
        {
            if (!task.Completed && task.DueDate <= today)
            {
                Console.WriteLine($"Reminder: {task.Description} is due today or overdue! Priority: {task.Priority}");
                // Here, you would call an email/SMS function
            }
        }
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }

    static void SaveTasks()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var task in tasks)
            {
                writer.WriteLine($"{task.Description}|{task.DueDate}|{task.Priority}|{task.Completed}");
            }
        }
    }

    static void LoadTasks()
    {
        if (File.Exists(filePath))
        {
            tasks.Clear();
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 4)
                {
                    tasks.Add(new TaskItem
                    {
                        Description = parts[0],
                        DueDate = DateTime.Parse(parts[1]),
                        Priority = parts[2],
                        Completed = bool.Parse(parts[3])
                    });
                }
            }
        }
    }
}

class TaskItem
{
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; }
    public bool Completed { get; set; }
}
