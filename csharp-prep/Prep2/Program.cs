using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        int gradePercentage = Convert.ToInt32(Console.ReadLine());

        // Determine the letter grade
        char letter;
        if (gradePercentage >= 90)
        {
            letter = 'A';
        }
        else if (gradePercentage >= 80)
        {
            letter = 'B';
        }
        else if (gradePercentage >= 70)
        {
            letter = 'C';
        }
        else if (gradePercentage >= 60)
        {
            letter = 'D';
        }
        else
        {
            letter = 'F';
        }

        // Determine if the user passed the course
        bool passed = gradePercentage >= 70;

        // Display the letter grade
        Console.WriteLine($"Your letter grade is: {letter}");

        // Display a message based on whether the user passed or not
        if (passed)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Keep working hard! You'll get it next time.");
        }

        // Stretch Challenge: Determine the sign for the grade
        char sign = ' ';
        int lastDigit = gradePercentage % 10;
        if (letter == 'A' && lastDigit >= 7)
        {
            sign = '+';
        }
        else if (letter != 'F' && lastDigit < 3)
        {
            sign = '-';
        }

        // Handle exceptional cases
        if (letter == 'A' && sign == '+')
        {
            sign = ' ';
            letter = 'A';
        }
        else if (letter == 'F' && (sign == '+' || sign == '-'))
        {
            sign = ' ';
        }

        // Display the grade with sign
        Console.WriteLine($"Your grade with sign is: {letter}{sign}");

    }
}