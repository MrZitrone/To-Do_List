using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Buffers;

namespace To_Do_List
{
    
    partial class main{ 

        static void AddIteam()
        {
            Console.Clear();
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Description: ");
            string desc = Console.ReadLine();

            Iteam newItem = _service.Add(name, desc);

            Console.WriteLine();
            Console.WriteLine($"{newItem.Id} - {newItem.isDone} - {newItem.Name} - {newItem.Description} - {newItem.Created}");
            Console.WriteLine(new string('=', 80));
            Console.WriteLine("File is saved!");
            Console.ReadLine();
            Menu();
        }





    }
    
    
}