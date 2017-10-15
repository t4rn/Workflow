﻿using System;

namespace Workflow.Model
{
    public class WorkOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime? TargetDateTime { get; set; }
        public DateTime? DropDeadDateTime { get; set; }
        public string Description { get; set; }
        public WorkOrderStatus WorkOrderStatus { get; set; }
        public decimal Total { get; set; }
        public string CertificationRequirements { get; set; }
    }

    public enum WorkOrderStatus
    {
        Created = 0,
        InProcess = 10,
        Rework = 15,
        Submitted = 20,
        Approved = 30,
        Canceled = -10,
        Rejected = -20
    }
}
