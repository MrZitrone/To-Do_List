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
            Console.Write("Choose which item to delete by ID: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Menu();
                return;
            }

            if (!int.TryParse(input, out int chosenId))
            {
                Console.WriteLine("Invalid ID!");
                Console.ReadLine();
                Menu();
                return;
            }

            bool success = _service.Delete(chosenId);

            if (!success)
            {
                Console.WriteLine("No item with that ID exists!");
            }
            else
            {
                Console.WriteLine($"Item {chosenId} deleted.");
            }

            Console.ReadLine();
            Menu();
        }

    }
}