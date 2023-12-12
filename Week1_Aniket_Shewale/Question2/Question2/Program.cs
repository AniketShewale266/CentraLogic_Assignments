// 2. Write a program to prompt the user for two numbers and then print the square of sum of numbers.
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter 1st Number");
        int n1 = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter 2nd Number");
        int n2 = Convert.ToInt32(Console.ReadLine());
        int sum = n1 + n2;
        Console.WriteLine("Square is " + sum * sum);
    }
}