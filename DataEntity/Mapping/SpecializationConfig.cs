using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEntity.Mapping
{
    public class SpecializationConfig : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.Name).IsUnique();

            builder.Property(s => s.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            builder.HasData(
                new Specialization { Id = 1, Name = "Репетитор для школьника" },
                new Specialization { Id = 2, Name = "Репетитор для студента" },
                new Specialization { Id = 3, Name = "Подготовка к ЦТ" }
                );
        }
    }
}