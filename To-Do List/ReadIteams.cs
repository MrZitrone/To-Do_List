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
        public static List<Iteam> LoadItems()
        {
            string path = "items.txt";
            List<Iteam> items = new List<Iteam>();

            if (!File.Exists(path))
                return items; // keine Datei -> leere Liste zurück

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split('|');

                // Wenn die Zeile nicht genug Teile hat -> überspringen
                if (parts.Length < 4)
                {
                    Console.WriteLine($"Überspringe ungültige Zeile: {line}");
                    continue;
                }

                // Minimal: Id, isDone, Name, Description
                int id = 0;
                bool isDone = false;
                DateTime created = DateTime.Now;
                DateTime updated = DateTime.Now;
                bool isDeleted = false;
                DateTime deleted = DateTime.MinValue;

                int.TryParse(parts[0], out id);
                bool.TryParse(parts[1], out isDone);

                string name = parts[2];
                string desc = parts[3];

                if (parts.Length > 4)
                    DateTime.TryParse(parts[4], out created);

                if (parts.Length > 5)
                    DateTime.TryParse(parts[5], out updated);

                if (parts.Length > 6)
                    bool.TryParse(parts[6], out isDeleted);

                if (parts.Length > 7)
                    DateTime.TryParse(parts[7], out deleted);

                Iteam item = new Iteam(name, desc);
                item.Id = id;
                item.isDone = isDone;
                item.Created = created;
                item.Updated = updated;


                items.Add(item);
            }

            return items;
        }

        static void ReadIteams()
        {
            Console.Clear();
            List<Iteam> items = LoadItems();

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id} - {item.Name} - {item.Description} - {item.Created} - Done: {item.isDone}");
            }

            Console.ReadLine();
            Menu();
        }
    }
}