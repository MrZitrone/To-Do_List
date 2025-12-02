namespace To_Do_List
{
    public class Item
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Item(string tag, string description)
        {
            Tag = tag;
            Description = description;
            IsDone = false;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}