using System.Collections.Generic;

namespace Workflow.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<InventoryItem> InventoryItems { get; set; }
    }
}
