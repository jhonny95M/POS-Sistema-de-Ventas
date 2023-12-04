using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infraestructure.Persistences.Contexts.Configurations
{
    internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasKey(c=>c.Id);
            entity.Property(c => c.Id)
                .HasColumnName("CategoryId");
            entity.Property(e => e.Name).HasMaxLength(100);
        }
    }
}
