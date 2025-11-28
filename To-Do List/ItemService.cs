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

        public Item Add(string tag, string description)
        {
            var items = _repository.LoadItems();
            int nextId = _repository.GetNextId();

            Item newItem = new Item(tag, description);
            newItem.Id = nextId;

            items.Add(newItem);
            _repository.SaveAll(items);

            return newItem;
        }

        public bool Delete(int id)
        {
            var items = _repository.LoadItems();
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
                return false;
    
            items.Remove(item);
            _repository.SaveAll(items);
            return true;
        }

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
    }
}
