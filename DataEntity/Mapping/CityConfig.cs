using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEntity.Mapping
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasIndex(d => d.Name).IsUnique();

            builder.Property(d => d.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(20);

            builder.HasMany(d => d.Tutors)
                .WithOne(t => t.City)
                .HasForeignKey(t => t.CityId);

            builder.HasData(
                new City { Id = 1, Name = "Могилёв" },
                new City { Id = 2, Name = "Минск" },
                new City { Id = 3, Name = "Витебск" },
                new City { Id = 4, Name = "Гомель" },
                new City { Id = 5, Name = "Гродно" },
                new City { Id = 6, Name = "Брест" }
                );
        }
    }
}
