namespace Workflow.Model
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
