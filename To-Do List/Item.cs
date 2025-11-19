using System;

namespace To_Do_List
{
    public class Item
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
            IsDone = false;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        public string ToFileString()
        {
            return $"{Id}|{IsDone}|{Name}|{Description}|{Created}|{Updated}";
        }
    }
}