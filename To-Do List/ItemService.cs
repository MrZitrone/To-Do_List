using System;
using System.Collections.Generic;
using System.Linq;

namespace To_Do_List
{
    public class ItemService
    {
        private readonly ItemRepository _repository;

        public ItemService(ItemRepository repository)
        {
            _repository = repository;
        }

        public List<Item> GetAll()
        {
            return _repository.LoadItems();
        }

        // Function of how the added Items are being saved. It's being used in ConsoleUI class like all the others 
        public Item Add(string tag, string description)
        {
            
            var items = _repository.LoadItems();
            int nextId = _repository.GetNextId(); // Reads last possible ID so it can assign a non given and next possible ID to the Item that has just been added

            Item newItem = new Item(tag, description);
            newItem.Id = nextId;

            items.Add(newItem);
            _repository.SaveAll(items); // Saves the file:) 

            return newItem;
        }

        
        // Function of Delete (also used in ConsoleUI)
        public bool Delete(int id)
        {
            var items = _repository.LoadItems();
            var item = items.FirstOrDefault(i => i.Id == id); // Sorts by ID (Lower to higher)

            if (item == null)
                return false;
    
            items.Remove(item); // deletes the selected iteam with the speicific given ID
            _repository.SaveAll(items);
            return true;
        }

        // for editing the tag
        public bool UpdateTag(int id, string newTag)
        {
            var items = _repository.LoadItems();
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
                return false;

            item.Tag = newTag;
            item.Updated = DateTime.Now;
            _repository.SaveAll(items);
            return true;
        }

        // FOr editin the description
        public bool UpdateDescription(int id, string newDescription)
        {
            var items = _repository.LoadItems();
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
                return false;

            item.Description = newDescription;
            item.Updated = DateTime.Now;
            _repository.SaveAll(items);
            return true;
        }

        // For editing the Done Status
        public bool UpdateDoneStatus(int id, bool IsDone)
        {
            var items = _repository.LoadItems();
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
                return false;

            item.IsDone = IsDone;
            item.Updated = DateTime.Now;
            _repository.SaveAll(items);
            return true;
        }

        
        // LOADS ALL THE ITEAMS IN SPECIFIC WAY FOR THE SEARCH
        public List<Item> Search(string searchTerm, int getChoice)
        {
            if (getChoice == 1)
            {
                var items = _repository.LoadItems();
                return items.Where(i => i.Tag.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if (getChoice == 2)
            {
                var items = _repository.LoadItems();
                return items.Where(i => i.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if (getChoice == 3)
            {
                var items = _repository.LoadItems();
                return items.Where(i => i.IsDone.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return null;
        }
    }
}
