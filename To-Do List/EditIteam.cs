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
        List<Iteam> items = LoadItems();

        if (items.Count == 0)
        {
            Console.WriteLine("No items found.");
            Console.ReadLine();
            Menu();
            return;
        }

        // Liste anzeigen
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Id} - {item.Name} - {item.Description} - Done: {item.isDone}");
        }

        Console.WriteLine(new string('=', 80));
        Console.Write("Choose which item to be edited by ID: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int chosenId))
        {
            Console.WriteLine("Invalid ID.");
            Console.ReadLine();
            Menu();
            return;
        }

        // Item mit dieser ID suchen
        Iteam itemToEdit = null;
        foreach (var it in items)
        {
            if (it.Id == chosenId)
            {
                itemToEdit = it;
                break;
            }
        }

        if (itemToEdit == null)
        {
            Console.WriteLine("No item with that ID exists.");
            Console.ReadLine();
            Menu();
            return;
        }

        // Auswahl, was geändert werden soll
        Console.Clear();
        Console.WriteLine($"Editing item: {itemToEdit.Id} - {itemToEdit.Name} - {itemToEdit.Description} - Done: {itemToEdit.isDone}");
        Console.WriteLine(new string('=', 80));
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

        switch (editChoice)
        {
            case 1:
                Console.Write("Change Name: ");
                string newName = Console.ReadLine();
                itemToEdit.Name = newName;
                break;

            case 2:
                Console.Write("New description: ");
                string newDesc = Console.ReadLine();
                itemToEdit.Description = newDesc;
                break;

            case 3:
                Console.Write("Is it done (y/n): ");
                string doneInput = Console.ReadLine().Trim().ToLower();

                if (doneInput == "y" || doneInput == "yes" || doneInput == "j" || doneInput == "ja")
                {
                    itemToEdit.isDone = true;
                }
                else if (doneInput == "n" || doneInput == "no" || doneInput == "nein")
                {
                    itemToEdit.isDone = false;
                }
                else
                {
                    Console.WriteLine("Invalid input for done status.");
                    Console.ReadLine();
                    Menu();
                    return;
                }
                break;

            default:
                Console.WriteLine("Unknown option.");
                Console.ReadLine();
                Menu();
                return;
        }

        // Updated-Datum setzen
        itemToEdit.Updated = DateTime.Now;

        // Datei neu schreiben
        RewriteItemsFile(items);

        Console.WriteLine("Item updated successfully.");
        Console.ReadLine();
        Menu();


        }
    }
}