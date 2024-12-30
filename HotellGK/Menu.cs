using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK
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
    }
}
