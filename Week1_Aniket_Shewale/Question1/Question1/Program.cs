// 1. Write a program to take the user's name as input and display a greeting message with their name.

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter Your Name");
        string userName = Console.ReadLine();
        Console.WriteLine("Hello, " + userName);
    }
}