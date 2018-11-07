using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEntity.Mapping
{
    public class PhoneConfig : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TutorId).IsRequired();

            builder.Property(p => p.Operator)
                .IsUnicode()
                .HasMaxLength(30);;

            builder.Property(p => p.Number)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(30);

            builder.HasOne(p => p.Tutor)
                .WithMany(t => t.Phones)
                .HasForeignKey(p => p.TutorId);
        }
    }
}