namespace Workflow.Model
{
    public class Labor
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }
        public string ServiceItemCode { get; set; }
        public string ServiceItemName { get; set; }
        public decimal LaborHours { get; set; }
        public decimal Rate { get; set; }
        public decimal ComputedPrice { get; set; }
        public string Notes { get; set; }
    }
}
