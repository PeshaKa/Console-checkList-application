using System;
using System.Collections.Generic;

class Program
{
    static List<string> tasks = new List<string>();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Checklist Application ===");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Remove Task");
            Console.WriteLine("5. Exit");
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
        string task = Console.ReadLine();
        tasks.Add("[ ] " + task);
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
                Console.WriteLine($"{i + 1}. {tasks[i]}");
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
            string taskText = tasks[index - 1].Substring(4); // Extracts text after "[ ] "
            tasks[index - 1] = "[✓] " + taskText; // Marks task as completed
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
            Console.WriteLine("Task removed! Press Enter to continue...");
        }
        else
        {
            Console.WriteLine("Invalid task number! Press Enter to try again...");
        }
        Console.ReadLine();
    }
}
