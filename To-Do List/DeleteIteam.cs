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
        public int Id { get; set; }

        static void DeleteIteam()
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

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id} - {item.Name} - {item.Description} - Done: {item.isDone}");
            }

            Console.WriteLine(new string('=', 80));
            Console.Write("Choose which item to delete by ID: ");

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int chosenId))
            {
                Console.WriteLine("Invalid ID!");
                Console.ReadLine();
                Menu();
                return;
            }

            Iteam itemToDelete = null;

            foreach (var it in items)
            {
                if (it.Id == chosenId)
                {
                    itemToDelete = it;
                    break;
                }
            }

            if (itemToDelete == null)
            {
                Console.WriteLine("No item with that ID exists!");
                Console.ReadLine();
                Menu();
                return;
            }

            items.Remove(itemToDelete);

            RewriteItemsFile(items);

            Console.WriteLine($"Item {chosenId} deleted.");
            Console.ReadLine();
            Menu();
        }
        static void RewriteItemsFile(List<Iteam> items)
        {
            string path = "items.txt";

            // overwrite file
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var item in items)
                {
                    writer.WriteLine(item.ToFileString());
                }
            }
        }
    }
}