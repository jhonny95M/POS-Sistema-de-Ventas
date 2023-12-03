using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infraestructure.Persistences.Contexts.Configurations
{
    internal sealed class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> entity)
        {
            entity.ToTable("Business");

            entity.Property(e => e.Address).IsUnicode(false);

            entity.Property(e => e.BusinessName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Logo).IsUnicode(false);

            entity.Property(e => e.Mision).IsUnicode(false);

            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Ruc)
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.Property(e => e.Vision).IsUnicode(false);

            entity.HasOne(d => d.District)
                .WithMany(p => p.Businesses)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Business__Distri__6FE99F9F");
        }
    }
}
