namespace Workflow.Model
{
    public class Part
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }
        public string InventoryItemCode { get; set; }
        public string InventoryItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        //public decimal ComputedPrice { get; set; }
        public string Notes { get; set; }
        public bool IsInstalled { get; set; }
    }
}
