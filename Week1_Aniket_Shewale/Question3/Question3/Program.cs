//3.Write a program to Make simple calculator which which takes two a numbers as an input. (add, subtract, multiply, divide, modulus) 
//  Note: Print modulus value in decimals.

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("***Simple Calculator***");
        Console.WriteLine("---------------------------");
        do
        {
            Console.WriteLine("Enter 1 for Addition");
            Console.WriteLine("Enter 2 for Subtraction");
            Console.WriteLine("Enter 3 for Multiplication");
            Console.WriteLine("Enter 4 for Division");
            Console.WriteLine("Enter 5 for Modulus");
            Console.WriteLine("Enter 6 for Exit");

            Console.Write("Enter Your Choice: ");
            int userChoice = Convert.ToInt32(Console.ReadLine());

            if (userChoice == 6)
            {
                break;
            }

            if(userChoice > 6 || userChoice <= 0)
            {
                Console.WriteLine("Invalid Choice, Try Again...");
                Console.WriteLine("---------------------------");
                continue;
            }

            Console.WriteLine("Enter 1st Number");
            int n1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter 2nd Number");
            int n2 = Convert.ToInt32(Console.ReadLine());

            if (userChoice == 1)
            {
                Console.WriteLine("Addition is " + (n1 + n2));
                Console.WriteLine("---------------------------");
            }
            else if (userChoice == 2)
            {
                Console.WriteLine("Subtraction is " + (n1 - n2));
                Console.WriteLine("---------------------------");
            }
            else if (userChoice == 3)
            {
                Console.WriteLine("Multiplication is " + (n1 * n2));
                Console.WriteLine("---------------------------");
            }
            else if (userChoice == 4)
            {
                Console.WriteLine("Division is " + (n1 / n2));
                Console.WriteLine("---------------------------");
            }
            else 
            {
                float ans = n1 % n2;
                Console.WriteLine("Modulus is " + ans);
                Console.WriteLine("---------------------------");
            }
        }
        while (true);
    }
}