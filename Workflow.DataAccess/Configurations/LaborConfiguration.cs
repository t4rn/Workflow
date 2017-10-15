using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Workflow.Model;

namespace Workflow.DataAccess.Configurations
{
    public class LaborConfiguration : EntityTypeConfiguration<Labor>
    {
        public LaborConfiguration()
        {
            Property(l => l.ServiceItemCode)
                .HasMaxLength(15)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("AK_Labor", 2) { IsUnique = true }));

            Property(l => l.WorkOrderId)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("AK_Labor", 1) { IsUnique = true }));

            Property(l => l.ServiceItemName)
                .HasMaxLength(80)
                .IsRequired();

            Property(l => l.LaborHours).HasPrecision(18, 2);

            Property(l => l.Rate).HasPrecision(18, 2);

            Property(l => l.ComputedPrice)
                .HasPrecision(18, 2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(l => l.Notes).HasMaxLength(140).IsOptional();
        }
    }
}
