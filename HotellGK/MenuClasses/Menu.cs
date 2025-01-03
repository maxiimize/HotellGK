using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK.MenuClasses
{
    public class Menu
    {
        public static void DrawMenu(string[] options, int selectedOption)
        {
            int boxWidth = 30;
            int boxHeight = options.Length + 4;

            // Ritar topp kanten
            Console.WriteLine(new string(' ', (Console.WindowWidth - boxWidth) / 2) + new string('=', boxWidth));

            for (int i = 0; i < boxHeight; i++)
            {
                if (i == 1 || i == boxHeight - 2)
                {
                    Console.WriteLine(new string(' ', (Console.WindowWidth - boxWidth) / 2) + "=" + new string(' ', boxWidth - 2) + "=");
                }
                else if (i >= 2 && i <= options.Length + 1)
                {
                    int optionIndex = i - 2;
                    string optionText = options[optionIndex];

                    if (optionIndex == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(new string(' ', (Console.WindowWidth - boxWidth) / 2) + $"= {optionText.PadRight(boxWidth - 4)} =");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(new string(' ', (Console.WindowWidth - boxWidth) / 2) + $"= {optionText.PadRight(boxWidth - 4)} =");
                    }
                }
                else
                {
                    Console.WriteLine(new string(' ', (Console.WindowWidth - boxWidth) / 2) + "=" + new string(' ', boxWidth - 2) + "=");
                }
            }

            // Ritar botten kanten
            Console.WriteLine(new string(' ', (Console.WindowWidth - boxWidth) / 2) + new string('=', boxWidth));
        }

        public static void HandleMenuSelection(string selectedOption)
        {
            Console.Clear();
            Console.WriteLine($"You selected: {selectedOption}");

            if (selectedOption == "Exit")
            {
                Environment.Exit(0);
            }

            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey(true);
        }

        public string DrawMenuController(string[] options, string prompt)
        {
            int selectedOption = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(prompt);

                // Visa alla alternativ
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // Markera det valda alternativet
                        Console.WriteLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {options[i]}");
                    }
                }

                var key = Console.ReadKey(true).Key;

                // Navigera med piltangenter
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption == 0) ? options.Length - 1 : selectedOption - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption == options.Length - 1) ? 0 : selectedOption + 1;
                        break;
                    case ConsoleKey.Enter:
                        return options[selectedOption]; // Returnera det valda alternativet när Enter trycks
                }
            }
        }
    }
}
