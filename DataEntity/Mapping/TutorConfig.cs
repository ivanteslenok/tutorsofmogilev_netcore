using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEntity.Mapping
{
    public class TutorConfig : IEntityTypeConfiguration<Tutor>
    {
        public void Configure(EntityTypeBuilder<Tutor> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.FirstName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(t => t.LastName)
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(t => t.Patronymic)
                .IsUnicode()
                .HasMaxLength(50);

            //builder.Property(t => t.PhotoPath);

            builder.Property(t => t.Description)
                .IsUnicode()
                .HasMaxLength(1000);

            builder.Property(t => t.Education)
                .IsUnicode()
                .HasMaxLength(500);

            builder.Property(t => t.Job)
                .IsUnicode()
                .HasMaxLength(500);

            //builder.Property(t => t.Cost);

            builder.Property(t => t.Address)
                .IsUnicode()
                .HasMaxLength(300);

            builder.Property(t => t.IsVisible).IsRequired();

            builder.HasOne(t => t.District)
                .WithMany(d => d.Tutors)
                .HasForeignKey(t => t.DistrictId);

            builder.HasMany(t => t.Phones)
                .WithOne(p => p.Tutor)
                .HasForeignKey(p => p.TutorId);

            builder.HasMany(t => t.Contacts)
                .WithOne(c => c.Tutor)
                .HasForeignKey(c => c.TutorId);

            Tutor[] tutorsData = new Tutor[50];

            for (int i = 0; i < tutorsData.Length; i++)
            {
                tutorsData[i] = new Tutor
                {
                    Id = i + 1,
                    FirstName = "FirstName" + i,
                    LastName = "LastName" + i,
                    Patronymic = "Patronymic" + i,
                    Address = "Address" + i,
                    Cost = i,
                    Description = "Description" + i,
                    Education = i % 2 == 0 ? "High" : "Medium",
                    Experience = (byte)i,
                    Job = "Job" + i,
                    Rating = (i % 5) + 1,
                    IsVisible = true,
                    DistrictId = i % 2 + 1
                };
            }

            builder.HasData(tutorsData);
        }
    }
}