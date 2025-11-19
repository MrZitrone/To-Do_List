using System;

namespace To_Do_List
{
    partial class main
    {
        private static readonly ItemService _service = 
            new ItemService(new ItemRepository());
    }
}