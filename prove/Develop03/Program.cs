using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizationProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize a scripture with the reference and text
            Scripture scripture = new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

            // Display the complete scripture
            scripture.Display();

            // Prompt the user to press enter to hide words or type "quit" to end the program
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to end the program.");
            string userInput = Console.ReadLine();

            // Continue hiding words until all words are hidden or the user types "quit"
            while (!scripture.AllWordsHidden() && userInput.ToLower() != "quit")
            {
                Console.Clear();
                scripture.HideWords();
                scripture.Display();

                Console.WriteLine("\nPress Enter to hide more words or type 'quit' to end the program.");
                userInput = Console.ReadLine();
            }

            // End of program
            Console.WriteLine("\nProgram ended.");
        }
    }

    // Class to represent a word in the scripture
    class Word
    {
        public string Text { get; set; }
        public bool IsHidden { get; set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }
    }

    // Class to represent the reference of the scripture
    class Reference
    {
        public string Verse { get; set; }
        public int StartVerse { get; set; }
        public int EndVerse { get; set; }

        public Reference(string verse)
        {
            Verse = verse;
            string[] parts = verse.Split(':');
            if (parts.Length > 1 && parts[1].Contains("-"))
            {
                string[] verses = parts[1].Split('-');
                StartVerse = int.Parse(verses[0]);
                EndVerse = int.Parse(verses[1]);
            }
            else if (parts.Length > 1)
            {
                StartVerse = int.Parse(parts[1]);
                EndVerse = StartVerse;
            }
        }
    }

    // Class to represent the scripture
    class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        public Scripture(string reference, string text)
        {
            _reference = new Reference(reference);
            _words = text.Split(' ').Select(word => new Word(word)).ToList();
        }

        // Display the complete scripture, including the reference and the text
        public void Display()
        {
            Console.WriteLine($"Scripture Reference: {_reference.Verse}");
            foreach (Word word in _words)
            {
                Console.Write(word.IsHidden ? "____ " : $"{word.Text} ");
            }
        }

        // Hide a few random words in the scripture
        public void HideWords()
        {
            Random random = new Random();
            int wordsToHide = random.Next(1, _words.Count(word => !word.IsHidden));
            int wordsHidden = 0;

            while (wordsHidden < wordsToHide)
            {
                int index = random.Next(_words.Count);
                if (!_words[index].IsHidden)
                {
                    _words[index].IsHidden = true;
                    wordsHidden++;
                }
            }
        }

        // Check if all words in the scripture are hidden
        public bool AllWordsHidden()
        {
            return _words.All(word => word.IsHidden);
        }
    }
}
