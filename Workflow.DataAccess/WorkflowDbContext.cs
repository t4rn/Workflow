using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Workflow.DataAccess.Configurations;
using Workflow.DataAccess.Migrations;
using Workflow.Model;

namespace Workflow.DataAccess
{
    public class WorkflowDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Labor> Labors { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }

        public WorkflowDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static WorkflowDbContext Create()
        {
            return new WorkflowDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new InventoryItemConfiguration());
            modelBuilder.Configurations.Add(new LaborConfiguration());
            modelBuilder.Configurations.Add(new PartConfiguration());
            modelBuilder.Configurations.Add(new ServiceItemConfiguration());
            modelBuilder.Configurations.Add(new WorkOrderConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
