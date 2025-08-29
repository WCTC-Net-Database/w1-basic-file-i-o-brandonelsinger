class Program
{
    static void Main()
    {
        //--------------------------------------------------------------
        // Display options and get user choices
        //--------------------------------------------------------------
        Console.WriteLine("Welcome!");
        Console.WriteLine("\nSelect from the following menu choices:");
        Console.WriteLine("\n1. Display Characters");
        Console.WriteLine("2. Add Character");
        Console.WriteLine("3. Level Up Character");
        Console.Write("\n> ");

        var userInput = Console.ReadLine();

        Console.WriteLine($"\nYou chose: {userInput}");

        //--------------------------------------------------------------
        // Option 1 - Display all characters
        // Read CSV file and show all the character details
        //--------------------------------------------------------------
        if (userInput == "1")
        {
            var lines = File.ReadAllLines("input.csv");

            foreach (var line in lines)
            {
                var cols = line.Split(",");
                var name = cols[0];
                var @class = cols[1];
                var level = cols[2];
                var hp = cols[3];
                var equip = cols[4];

                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Class: {@class}");
                Console.WriteLine($"Level: {level}");
                Console.WriteLine($"HP: {hp}");
                
                var equipment = equip.Split("|");
                Console.WriteLine("Equipment: ");
                foreach (var eq in equipment)
                {
                    Console.WriteLine($"\t{eq}");  
                }

                Console.WriteLine("----------------------------");

            }
        }

        //--------------------------------------------------------------
        // Option 2 - Add new character
        // Get user input and append new character to CSV file
        //--------------------------------------------------------------
        else if (userInput == "2")
        {
            Console.Write("Enter your character's name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your character's class: ");
            string characterClass = Console.ReadLine();

            Console.Write("Enter your character's level: ");
            int level = int.Parse(Console.ReadLine());

            Console.Write("Enter your character's equipment (separate items with a '|'): ");
            string[] equipment = Console.ReadLine().Split('|');

            var userEnteredData = ($"{name},{characterClass},{level},0,{string.Join("|", equipment)}");

            using (StreamWriter writer = new StreamWriter("input.csv", true))
            {
                writer.WriteLine(userEnteredData);
            }

            Console.WriteLine($"\nWelcome, {name} the {characterClass}! You are level {level} and your equipment includes: {string.Join(", ", equipment)}.");
        }

        //--------------------------------------------------------------
        // Option 3 - Level up character
        // Show all characters, user picks one, increases their level
        // Overwrites old level in CSV file
        //--------------------------------------------------------------
        else if (userInput == "3") 
        {
            Console.WriteLine("Current Characters");
            Console.WriteLine("----------------------------");
            var lines = File.ReadAllLines("input.csv");

            foreach (var line in lines)
            {
                var cols = line.Split(",");
                Console.WriteLine($"{cols[0]} - Level {cols[2]}");
            }

            Console.Write("\nEnter the character name you want to level up: ");
            var name = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter("input.csv"))
            {
                foreach (var line in lines)
                {
                    var cols = line.Split(",");

                    if (cols[0] == name)
                    {
                        int currentLevel = Convert.ToInt32(cols[2]);
                        int newLevel = currentLevel + 1;

                        writer.WriteLine($"{cols[0]},{cols[1]},{newLevel},{cols[3]},{cols[4]}");
                        Console.WriteLine($"{cols[0]} leveled up from {currentLevel} to {newLevel}!");
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }
    }
}