using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace To_Do_List
{
    public class ConsoleUI
    {
        private readonly ItemService _service;

        public ConsoleUI()
        {
            _service = new ItemService(new ItemRepository());
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(new string('=', 20) + "To Do List" + new string('=', 20));
                Console.WriteLine("1) Add New Item");
                Console.WriteLine("2) Edit Item");
                Console.WriteLine("3) Delete Item");
                Console.WriteLine("4) Show All Items");
                Console.WriteLine("5) Search Items");
                Console.WriteLine("6) Exit");
                Console.WriteLine(new string('=', 50));

                int choice = GetValidInt("Choose an option: ", 1, 6);

                switch (choice)
                {
                    case 1:
                        AddItem();
                        break;
                    case 2:
                        EditItems();
                        break;
                    case 3:
                        DeleteItem();
                        break;
                    case 4:
                        ReadItems();
                        break;
                    case 5:
                        Search();
                        break;
                    case 6:
                        return;
                }
            }
        }

        private void AddItem()
        {
            Console.Clear();
            Console.Write("Tag: ");
            string tag = Console.ReadLine();

            Console.Write("Description: ");
            string desc = Console.ReadLine();

            Item newItem = _service.Add(tag, desc);

            Console.WriteLine();
            Console.WriteLine($"{newItem.Id} - {newItem.IsDone} - {newItem.Tag} - {newItem.Description} - {newItem.Created}");
            Console.WriteLine(new string('=', 80));
            Console.WriteLine("File is saved!");
            WaitForUser();
        }

        private void EditItems()
        {
            Console.Clear();
            List<Item> items = _service.GetAll();

            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                WaitForUser();
                return;
            }

            int? chosenId = ShowPagedList(items, allowSelection: true);

            if (chosenId == null) return; // User went back

            Console.Clear();
            Console.WriteLine($"Editing Item ID: {chosenId}");
            Console.WriteLine("What do you want to edit?");
            Console.WriteLine("1 - Tag");
            Console.WriteLine("2 - Description");
            Console.WriteLine("3 - Done status (IsDone)");
            
            int editChoice = GetValidInt("Choice: ", 1, 3);
            bool result = false;

            switch (editChoice)
            {
                case 1:
                    Console.Write("Change Tag: ");
                    string newTag = Console.ReadLine();
                    result = _service.UpdateTag(chosenId.Value, newTag);
                    break;

                case 2:
                    Console.Write("New description: ");
                    string newDesc = Console.ReadLine();
                    result = _service.UpdateDescription(chosenId.Value, newDesc);
                    break;

                case 3:
                    bool isDone = GetYesNo("Is it done (y/n): ");
                    result = _service.UpdateDoneStatus(chosenId.Value, isDone);
                    break;
            }

            if (!result)
            {
                Console.WriteLine("No item with that ID exists.");
            }
            else
            {
                Console.WriteLine("Item updated successfully.");
            }

            WaitForUser();
        }

        private void DeleteItem()
        {
            Console.Clear();
            List<Item> items = _service.GetAll();

            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                WaitForUser();
                return;
            }

            int? chosenId = ShowPagedList(items, allowSelection: true);

            if (chosenId == null) return; // User went back

            bool success = _service.Delete(chosenId.Value);

            if (!success)
            {
                Console.WriteLine("No item with that ID exists!");
            }
            else
            {
                Console.WriteLine($"Item {chosenId} deleted.");
            }

            WaitForUser();
        }

        private void ReadItems()
        {
            List<Item> items = _service.GetAll();
            if (items.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No items found.");
                WaitForUser();
                return;
            }
            ShowPagedList(items, allowSelection: false);
        }

        private void Search()
        {

            Console.WriteLine("1 - Tag");
            Console.WriteLine("2 - Description");
            Console.WriteLine("3 - Done status (IsDone)");
            
            int getChoice = GetValidInt("Choice: ", 0, 3);
            bool result = false;

            string searchTerm = "";
            
            switch (getChoice)
            {
                case 1:
                    Console.WriteLine("Enter search partition: ");
                    searchTerm = Console.ReadLine(); 
                    break;
                case 2:
                    Console.WriteLine("Enter search partition: ");
                    searchTerm = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Enter search partition: ");
                    searchTerm = Console.ReadLine();
                    break;
                default:
                    break;
            }

            List<Item> searchResults = _service.Search(searchTerm, getChoice);

            if (searchResults.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No items found.");
                WaitForUser();
                return;
            }
            ShowPagedList(searchResults, allowSelection: false);
            WaitForUser();
        }

        // --- Helper Methods ---


        //AI generated code Start
        private int? ShowPagedList(List<Item> items, bool allowSelection)
        {
            const int pageSize = 10;
            int currentPage = 1;

            while (true)
            {
                Console.Clear();
                int totalPages = (int)Math.Ceiling((double)items.Count / pageSize);
                if (totalPages == 0) totalPages = 1;

                var pageItems = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                Console.WriteLine($"Page {currentPage} of {totalPages}");
                Console.WriteLine(new string('-', 80));

                foreach (var item in pageItems)
                {
                    Console.WriteLine($"{item.Id} - {item.Tag} - {item.Description} - Done:{item.IsDone} -- {item.Created}");
                }

                Console.WriteLine(new string('=', 80));
                
                if (allowSelection)
                    Console.WriteLine("[N]ext | [P]revious | [B]ack | Enter ID to select");
                else
                    Console.WriteLine("[N]ext | [P]revious | [B]ack");
                
                Console.Write("Action: ");
                string input = Console.ReadLine()?.Trim().ToUpper();

                if (input == "N")
                {
                    if (currentPage < totalPages) currentPage++;
                }
                else if (input == "P")
                {
                    if (currentPage > 1) currentPage--;
                }
                else if (input == "B")
                {
                    return null;
                }
                else if (allowSelection)
                {
                    if (int.TryParse(input, out int id))
                    {
                        return id;
                    }
                }
            }
        }

        //AI generated code End

        private int GetValidInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result) && result >= min && result <= max)
                {
                    return result;
                }

                Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
            }
        }

        private bool GetYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y" || input == "yes" || input == "j" || input == "ja") return true;
                if (input == "n" || input == "no" || input == "nein") return false;

                Console.WriteLine("Invalid input. Please answer y or n.");
            }
        }
        
        private void WaitForUser()
        {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
