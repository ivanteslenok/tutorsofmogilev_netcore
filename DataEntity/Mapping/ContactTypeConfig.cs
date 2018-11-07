using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEntity.Mapping
{
    public class ContactTypeConfig : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> builder)
        {
            builder.HasKey(ct => ct.Id);

            builder.HasIndex(ct => ct.Name).IsUnique();

            builder.Property(ct => ct.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.HasMany(ct => ct.Contacts)
                .WithOne(c => c.ContactType)
                .HasForeignKey(c => c.ContactTypeId);

            builder.HasData(
                new ContactType { Id = 1, Name = "Skype" },
                new ContactType { Id = 2, Name = "Viber" },
                new ContactType { Id = 3, Name = "Email" }
                );
        }
    }
}