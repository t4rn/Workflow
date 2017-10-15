using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Workflow.Model;

namespace Workflow.DataAccess.Configurations
{
    public class InventoryItemConfiguration : EntityTypeConfiguration<InventoryItem>
    {
        public InventoryItemConfiguration()
        {
            Property(x => x.Code)
                .HasMaxLength(15)
                .IsRequired()
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("AK_InventoryItem_Code") { IsUnique = true }));

            Property(x => x.Name)
                .HasMaxLength(80)
                .IsRequired()
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("AK_InventoryItem_Name") { IsUnique = true }));

            Property(si => si.UnitPrice).HasPrecision(18, 2);
        }
    }
}
