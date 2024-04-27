using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();

        do
        {
            int magicNumber = random.Next(1, 101); // Generate a random number between 1 and 100
            int guess;
            int attempts = 0;

            Console.WriteLine("Welcome to Guess My Number Game!");

            // Game loop
            while (true)
            {
                Console.Write("What is your guess? ");
                guess = Convert.ToInt32(Console.ReadLine());
                attempts++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                    break; // Exit the game loop if the guess is correct
                }
            }

            // Inform the user of the number of attempts
            Console.WriteLine($"You made {attempts} attempts.");

            // Ask the user if they want to play again
            Console.Write("Do you want to play again? (yes/no): ");
        } while (Console.ReadLine().ToLower() == "yes");
    }
}