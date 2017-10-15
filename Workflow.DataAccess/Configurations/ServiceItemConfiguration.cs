using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Workflow.Model;

namespace Workflow.DataAccess.Configurations
{
    public class ServiceItemConfiguration : EntityTypeConfiguration<ServiceItem>
    {
        public ServiceItemConfiguration()
        {
            Property(x => x.Code)
             .HasMaxLength(15)
             .IsRequired()
             .HasColumnAnnotation("Index",
             new IndexAnnotation(new IndexAttribute("AK_ServiceItem_Code") { IsUnique = true }));

            Property(x => x.Name)
                .HasMaxLength(80)
                .IsRequired()
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("AK_ServiceItem_Name") { IsUnique = true }));

            Property(x => x.Rate).HasPrecision(18, 2);
        }

    }
}
