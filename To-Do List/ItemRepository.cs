using System;
using System.Collections.Generic;
using System.IO;

namespace To_Do_List
{
    public class ItemRepository
    {
        private readonly string path;

        public ItemRepository(string path = "items.txt")
        {
            this.path = path;
        }

        public List<Item> LoadItems()
        {
            List<Item> items = new List<Item>();

            if (!File.Exists(path))
                return items;

            try
            {
                string[] lines = File.ReadAllLines(path);
                int currentId = 1; // Start IDs from 1

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    Item item = DeserializeItem(line);
                    if (item != null)
                    {
                        item.Id = currentId++; // Assign ID in memory NEW
                        items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading items: {ex.Message}");
            }

            return items;
        }

        public void SaveAll(List<Item> items)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    foreach (var item in items)
                    {
                        writer.WriteLine(SerializeItem(item));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving items: {ex.Message}");
            }
        }

        public void Append(Item item)
        {
            try
            {
                File.AppendAllText(path, SerializeItem(item) + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending item: {ex.Message}");
            }
        }

        public int GetNextId()
        {
            // Count the number of items in the file to determine next ID
            if (!File.Exists(path))
                return 1;

            try
            {
                string[] lines = File.ReadAllLines(path);
                int count = 0;

                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        count++;
                }

                return count + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private string SerializeItem(Item item)
        {
            // ID is not saved to file anymore
            return $"{item.IsDone}|{item.Tag}|{item.Description}|{item.Created}|{item.Updated}";
        }

        private Item DeserializeItem(string line)
        {
            try
            {
                string[] parts = line.Split('|');

                if (parts.Length < 3)
                    return null;

                bool.TryParse(parts[0], out bool IsDone);

                string tag = parts[1];
                string desc = parts[2];

                DateTime created = DateTime.Now;
                DateTime updated = DateTime.Now;

                if (parts.Length > 3)
                    DateTime.TryParse(parts[3], out created);

                if (parts.Length > 4)
                    DateTime.TryParse(parts[4], out updated);

                Item item = new Item(tag, desc);
                // ID will be assigned in LoadItems()
                item.IsDone = IsDone;
                item.Created = created;
                item.Updated = updated;

                return item;
            }
            catch
            {
                return null;
            }
        }
    }
}
