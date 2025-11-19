using System;

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

        public Iteam(string name, string description)
        {
            Name = name;
            Description = description;
            isDone = false;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        public string ToFileString()
        {
            return $"{Id}|{isDone}|{Name}|{Description}|{Created}|{Updated}";
        }
    }
}