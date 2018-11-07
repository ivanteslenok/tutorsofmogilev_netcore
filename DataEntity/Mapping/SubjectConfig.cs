using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEntity.Mapping
{
    public class SubjectConfig : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.Name).IsUnique();

            builder.Property(s => s.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            builder.HasData(
                new Subject { Id = 1, Name = "Математика" },
                new Subject { Id = 2, Name = "Физика" },
                new Subject { Id = 3, Name = "История" },
                new Subject { Id = 4, Name = "Русский язык" },
                new Subject { Id = 5, Name = "Химия" },
                new Subject { Id = 6, Name = "Английский язык" }
                );
        }
    }
}