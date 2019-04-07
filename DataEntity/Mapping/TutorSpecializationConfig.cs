using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEntity.Mapping
{
    public class TutorSpecializationConfig : IEntityTypeConfiguration<TutorSpecialization>
    {
        public void Configure(EntityTypeBuilder<TutorSpecialization> builder)
        {
            builder.HasKey(ts => new { ts.TutorId, ts.SpecializationId });

            builder
                .HasOne(ts => ts.Tutor)
                .WithMany(t => t.TutorSpecializations)
                .HasForeignKey(ts => ts.TutorId);

            builder
                .HasOne(ts => ts.Specialization)
                .WithMany(s => s.TutorSpecializations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(ts => ts.SpecializationId);
        }
    }
}