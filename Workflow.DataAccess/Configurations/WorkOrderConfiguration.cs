using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Workflow.Model;

namespace Workflow.DataAccess.Configurations
{
    public class WorkOrderConfiguration  :EntityTypeConfiguration<WorkOrder>
    {
        public WorkOrderConfiguration()
        {
            Property(wo => wo.OrderDateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(wo => wo.Description).HasMaxLength(256).IsOptional();

            Property(wo => wo.Total).HasPrecision(18, 2).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(wo => wo.CertificationRequirements).HasMaxLength(120).IsOptional();
        }
    }
}
