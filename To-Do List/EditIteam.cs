using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace To_Do_List
{

    partial class main
    {

        static void EditIteams()
        {
            Console.Clear();
            List<Iteam> items = _service.GetAll();

            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                Console.ReadLine();
                Menu();
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id} - {item.Name} - {item.Description} - Done: {item.isDone}");
            }

            Console.WriteLine(new string('=', 80));
            Console.WriteLine("To go back just press Enter (without input)!");
            Console.Write("Choose which item to be edited by ID: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Menu();
                return;
            }

            if (!int.TryParse(input, out int chosenId))
            {
                Console.WriteLine("Invalid ID.");
                Console.ReadLine();
                Menu();
                return;
            }

            Console.Clear();
            Console.WriteLine("What do you want to edit?");
            Console.WriteLine("1 - Name");
            Console.WriteLine("2 - Description");
            Console.WriteLine("3 - Done status (isDone)");
            Console.Write("Choice: ");

            string choiceInput = Console.ReadLine();
            if (!int.TryParse(choiceInput, out int editChoice))
            {
                Console.WriteLine("Invalid choice.");
                Console.ReadLine();
                Menu();
                return;
            }

            bool result = false;

            switch (editChoice)
            {
                case 1:
                    Console.Write("Change Name: ");
                    string newName = Console.ReadLine();
                    result = _service.UpdateName(chosenId, newName);
                    break;

                case 2:
                    Console.Write("New description: ");
                    string newDesc = Console.ReadLine();
                    result = _service.UpdateDescription(chosenId, newDesc);
                    break;

                case 3:
                    Console.Write("Is it done (y/n): ");
                    string doneInput = Console.ReadLine().Trim().ToLower();
                    bool? isDone = null;

                    if (doneInput == "y" || doneInput == "yes" || doneInput == "j" || doneInput == "ja")
                        isDone = true;
                    else if (doneInput == "n" || doneInput == "no" || doneInput == "nein")
                        isDone = false;
                    else
                    {
                        Console.WriteLine("Invalid input for done status.");
                        Console.ReadLine();
                        Menu();
                        return;
                    }

                    result = _service.UpdateDoneStatus(chosenId, isDone.Value);
                    break;

                default:
                    Console.WriteLine("Unknown option.");
                    Console.ReadLine();
                    Menu();
                    return;
            }

            if (!result)
            {
                Console.WriteLine("No item with that ID exists.");
            }
            else
            {
                Console.WriteLine("Item updated successfully.");
            }

            Console.ReadLine();
            Menu();
        }
    }
}
