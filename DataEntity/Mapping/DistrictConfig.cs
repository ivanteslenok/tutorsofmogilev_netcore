using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEntity.Mapping
{
    public class DistrictConfig : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasIndex(d => d.Name).IsUnique();

            builder.Property(d => d.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            builder.HasMany(d => d.Tutors)
                .WithOne(t => t.District)
                .HasForeignKey(t => t.DistrictId);

            builder.HasData(
                new District { Id = 1, Name = "Ленинский" },
                new District { Id = 2, Name = "Октябрьский" }
                );
        }
    }
}