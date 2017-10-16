﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Workflow.Model;

namespace Workflow.DataAccess.Configurations
{
    public class PartConfiguration : EntityTypeConfiguration<Part>
    {
        public PartConfiguration()
        {
            Property(p => p.InventoryItemCode)
                .HasMaxLength(15)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("AK_Part", 2) { IsUnique = true }));

            Property(p => p.WorkOrderId)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("AK_Part", 1) { IsUnique = true }));

            Property(p => p.InventoryItemName)
                .HasMaxLength(80)
                .IsRequired();

            Property(p => p.UnitPrice).HasPrecision(18, 2);

            Property(p => p.ComputedPrice).HasPrecision(18, 2).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(p => p.Notes)
                .HasMaxLength(140)
                .IsOptional();
        }
    }
}
