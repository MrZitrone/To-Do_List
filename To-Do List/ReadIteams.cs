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

        static void ReadIteams()
        {
            Console.Clear();
            List<Iteam> items = _service.GetAll();

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id} - {item.Name} - {item.Description} - Done:{item.isDone} -- {item.Created}");
            }

            Console.WriteLine(new string('=', 80));
            Console.WriteLine("Press Enter to go back:");
            Console.ReadLine();
            Menu();
        }

    }
}