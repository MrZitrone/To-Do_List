using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Buffers;

namespace To_Do_List
{

    public class Iteam
    {
        public int Id { get; set; }
        public bool isDone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        
        public Iteam(string name, string description)
        {
            Name = name;
            Description = description;
            isDone = false;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        public string ToFileString()
        {
            return $"{Id}|{isDone}|{Name}|{Description}|{Created}|{Updated}";
        }
        
        public static int GetNextId()
        {
            string path = "items.txt";

            if (!File.Exists(path))
                return 1; // erste ID

            string[] lines = File.ReadAllLines(path);
            int maxId = 0;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split('|');

                if (parts.Length == 0)
                    continue;

                if (int.TryParse(parts[0], out int id))
                {
                    if (id > maxId)
                        maxId = id;
                }
            }

            return maxId + 1;
        }
    }
    
    

    partial class main
    {
        static void AddIteam(Iteam team)
        {
            Console.Clear();

            // ➜ ID zuerst setzen
            team.Id = Iteam.GetNextId();

            // dann anzeigen
            Console.WriteLine($"{team.Id} - {team.isDone} - {team.Name} - {team.Description} - {team.Created}");
            
            // in Datei schreiben
            File.AppendAllText("items.txt", team.ToFileString() + Environment.NewLine);
            
            Console.WriteLine(new string('=',80));
            Console.WriteLine("File is saved!");
            Console.ReadLine();
            Menu();
        }




    }
    
    
}