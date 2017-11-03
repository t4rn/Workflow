using System.Data.Entity.ModelConfiguration;
using Workflow.Model;

namespace Workflow.DataAccess.Configurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(x => x.FirstName).HasMaxLength(15).IsOptional();
            Property(au => au.LastName).HasMaxLength(15).IsOptional();
            Property(au => au.Address).HasMaxLength(30).IsOptional();
            Property(au => au.City).HasMaxLength(20).IsOptional();
            Property(au => au.State).HasMaxLength(2).IsOptional();
            Property(au => au.ZipCode).HasMaxLength(10).IsOptional();
        }
    }
}
