using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num == 0)
                break;

            numbers.Add(num);
        }

        if (numbers.Count == 0)
        {
            Console.WriteLine("You didn't enter any numbers.");
            return;
        }

        // Compute sum
        int sum = numbers.Sum();

        // Compute average
        double average = numbers.Average();

        // Find maximum
        int max = numbers.Max();

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        // Stretch challenge: Find smallest positive number
        int smallestPositive = numbers.Where(n => n > 0).DefaultIfEmpty(int.MaxValue).Min();
        Console.WriteLine($"The smallest positive number is: {smallestPositive}");

        // Stretch challenge: Sort the numbers
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}