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
        public bool IsDeleted { get; set; }
        public DateTime Deleted { get; set; }
        
        public Iteam(string name, string description)
        {
            Name = name;
            Description = description;
            isDone = false;
            Created = DateTime.Now;
            Updated = DateTime.Now;
            IsDeleted = false;
        }
    }

    partial class main
    {
        static void AddIteam(Iteam team)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{team.Id} - {team.Name} - {team.Description}");
            }

        }

    }
}