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

        public List<Iteam> LoadItems()
        {
            List<Iteam> items = new List<Iteam>();

            if (!File.Exists(path))
                return items;

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split('|');

                if (parts.Length < 4)
                    continue;

                int.TryParse(parts[0], out int id);
                bool.TryParse(parts[1], out bool isDone);

                string name = parts[2];
                string desc = parts[3];

                DateTime created = DateTime.Now;
                DateTime updated = DateTime.Now;

                if (parts.Length > 4)
                    DateTime.TryParse(parts[4], out created);

                if (parts.Length > 5)
                    DateTime.TryParse(parts[5], out updated);

                Iteam item = new Iteam(name, desc);
                item.Id = id;
                item.isDone = isDone;
                item.Created = created;
                item.Updated = updated;

                items.Add(item);
            }

            return items;
        }

        public void SaveAll(List<Iteam> items)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var item in items)
                {
                    writer.WriteLine(item.ToFileString());
                }
            }
        }

        public void Append(Iteam item)
        {
            File.AppendAllText(path, item.ToFileString() + Environment.NewLine);
        }

        public int GetNextId()
        {
            if (!File.Exists(path))
                return 1;

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
}
