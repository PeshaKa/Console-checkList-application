using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<string> tasks = new List<string>();
    static string filePath = "tasks.txt";

    static void Main()
    {
        LoadTasks(); // Load tasks from file at startup

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
                    SaveTasks(); // Save tasks before exiting
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
        SaveTasks(); // Save immediately
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
            SaveTasks(); // Save changes
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
            SaveTasks(); // Save changes
            Console.WriteLine("Task removed! Press Enter to continue...");
        }
        else
        {
            Console.WriteLine("Invalid task number! Press Enter to try again...");
        }
        Console.ReadLine();
    }

    static void SaveTasks()
    {
        try
        {
            File.WriteAllLines(filePath, tasks);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving tasks: " + ex.Message);
        }
    }

    static void LoadTasks()
    {
        if (File.Exists(filePath))
        {
            try
            {
                tasks = new List<string>(File.ReadAllLines(filePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading tasks: " + ex.Message);
            }
        }
    }
}
