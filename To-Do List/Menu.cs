using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Buffers;

namespace To_Do_List
{

    partial class main
    {
        static void Menu()
        {
            
            while (true)
            {
                Console.Clear();
                
                Console.WriteLine(new string('=', 20) + "To Do List" + new string('=', 20));    
                Console.WriteLine("1) Add New Item");
                Console.WriteLine("2) Edit Item");
                Console.WriteLine("3) Delete Item");
                Console.WriteLine("4) Show All Items");
                Console.WriteLine("5) Save Item");
                Console.WriteLine("6) Exit");
                Console.WriteLine(new string('=', 50));
                Console.Write("Choose an option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    Console.Clear();
                    Console.Write("Enter name: ");
                    string name = Console.ReadLine();

                    Console.Clear();
                    Console.Write("Enter description: ");
                    string desc = Console.ReadLine();
                    
                    Iteam item = new Iteam(name, desc);
                    AddIteam(item);
                }
                else if (choice == 2)
                {

                }
                else if (choice == 3)
                {

                }
                else if (choice == 4)
                {
                    ReadIteams();
                }
                else if (choice == 5)
                {
                    
                }
                else if (choice == 6)
                {
                    System.Environment.Exit(0);
                }
                else if (choice > 6)
                {
                    Console.Clear();
                    Console.WriteLine("Ungültige Eingabe");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Menu();
                }

            }
        }
    }
}

