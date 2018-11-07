using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEntity.Mapping
{
    public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.TutorId).IsRequired();

            builder.Property(c => c.Name)
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(c => c.Value)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            builder.HasOne(c => c.ContactType)
                .WithMany(ct => ct.Contacts)
                .HasForeignKey(c => c.ContactTypeId);

            builder.HasOne(c => c.Tutor)
                .WithMany(t => t.Contacts)
                .HasForeignKey(c => c.TutorId);
        }
    }
}