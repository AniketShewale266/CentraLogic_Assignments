/*
mini-project that involves CRUD (Create, Read, Update, Delete) operations using a list, for and foreach loops, if-else statements, and a switch case in C#. In this project, we'll create a basic task list application.

Project Description: Simple Task List Application

Features:

Create a task: Add a new task with a title 
Read tasks: Display the list of tasks with their titles and descriptions.
Update a task: Modify the title or description of an existing task.
Delete a task: Remove a task from the list.
Exit: Exit the application.

 */

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Simple Task List Application");
        Console.WriteLine("----------------------------");
        List<string> myList = new List<string>();
        int ch;
        do
        {
        Console.WriteLine("Enter 1: Add Task");
        Console.WriteLine("Enter 2: Read Tasks");
        Console.WriteLine("Enter 3: Update Task");
        Console.WriteLine("Enter 4: Delete Task");
        Console.WriteLine("Enter 5: Exit");
        Console.Write("Enter your Choice: ");
        ch = Convert.ToInt32(Console.ReadLine());
        if(ch == 5)
        {
           break;
        }
            switch (ch)
            {
                case 1:
                    // For Adding
                    Console.Write("Enter Task Name: ");
                    string taskName = Console.ReadLine();
                    myList.Add(taskName);
                    Console.WriteLine("***Successfullt Added!***");
                    Console.WriteLine("----------------------------");
                    break;
                case 2:
                    // For Reading
                    if (myList.Count == 0)
                    {
                        Console.WriteLine("List is Empty, Add Your Tasks");
                        Console.WriteLine("----------------------------");
                        break;
                    }
                    Console.WriteLine("****************************");
                    Console.WriteLine("Your Tasks:");
                    int cnt = 1;
                    foreach (string task in myList)
                    {
                        Console.WriteLine(cnt + " -> " + task);
                        cnt++;
                    }
                    Console.WriteLine("****************************");
                    Console.WriteLine("----------------------------");
                    break;
                case 3:
                    // For Update
                    Console.WriteLine("****************************");
                    Console.WriteLine("Your Tasks:");
                    int count = 1;
                    foreach (string task in myList)
                    {
                        Console.WriteLine(count + " -> " + task);
                        count++;
                    }
                    Console.WriteLine("****************************");

                    if (myList.Count == 0)
                    {
                        Console.WriteLine("List is Empty, So Cannot Update");
                        Console.WriteLine("----------------------------");
                        break;
                    }
                    if (myList.Count == 1)
                    {
                        Console.Write("Enter Task Name: ");
                        string taskNaam = Console.ReadLine();
                        myList.RemoveAt(0);
                        myList.Insert(0, taskNaam);
                        Console.WriteLine("***Successfullt Updated!***");
                        Console.WriteLine("----------------------------");
                        break;
                    }
                    Console.Write("Enter Task Number that you want to Edit: ");
                    int taskNum = Convert.ToInt32(Console.ReadLine());
                    if(taskNum > myList.Count)
                    {
                        Console.WriteLine("Invalid Task Number!!");
                        Console.WriteLine("----------------------------");
                        continue;
                    }
                    Console.Write("Enter Task Name: ");
                    string taskN = Console.ReadLine();
                    myList.RemoveAt(taskNum - 1);
                    myList.Insert(taskNum-1,taskN);
                    Console.WriteLine("***Successfullt Updated!***");
                    Console.WriteLine("----------------------------");
                    break;
                case 4:
                    // For Delete
                    Console.WriteLine("****************************");
                    Console.WriteLine("Your Tasks:");
                    int countD = 1;
                    foreach (string task in myList)
                    {
                        Console.WriteLine(countD + " -> " + task);
                        countD++;
                    }
                    Console.WriteLine("****************************");

                    if (myList.Count == 0)
                    {
                        Console.WriteLine("List is Empty, So Cannot Delete");
                        Console.WriteLine("----------------------------");
                        break;
                    }
                    if (myList.Count == 1)
                    {
                        myList.Clear();
                        Console.WriteLine("***Successfullt Deleted!***");
                        Console.WriteLine("----------------------------");
                        break;
                    }
                    Console.Write("Enter Task Number that you want to Delete: ");
                    int taskNumber = Convert.ToInt32(Console.ReadLine());
                    if (taskNumber > myList.Count)
                    {
                        Console.WriteLine("Invalid Task Number!!");
                        Console.WriteLine("----------------------------");
                        continue;
                    }
                    myList.RemoveAt(taskNumber - 1);
                    Console.WriteLine("***Successfullt Deleted!***");
                    Console.WriteLine("----------------------------");
                    break;
                default:
                    Console.WriteLine("Invalid Choice! Please try again...");
                    Console.WriteLine("----------------------------");
                    break;
            }
        } while (ch != 5);
    }
}