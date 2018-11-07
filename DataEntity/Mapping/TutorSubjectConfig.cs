using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEntity.Mapping
{
    public class TutorSubjectConfig : IEntityTypeConfiguration<TutorSubject>
    {
        public void Configure(EntityTypeBuilder<TutorSubject> builder)
        {
            builder.HasKey(ts => new { ts.TutorId, ts.SubjectId });

            builder
                .HasOne(ts => ts.Tutor)
                .WithMany(t => t.TutorSubjects)
                .HasForeignKey(ts => ts.TutorId);

            builder
                .HasOne(ts => ts.Subject)
                .WithMany(s => s.TutorSubjects)
                .HasForeignKey(ts => ts.SubjectId);

            TutorSubject[] tutorSubjectsData = new TutorSubject[50];

            for (int i = 0; i < tutorSubjectsData.Length; i++)
            {
                tutorSubjectsData[i] = new TutorSubject
                {
                    TutorId = i + 1,
                    SubjectId = i % 6 + 1
                };
            }

            builder.HasData(tutorSubjectsData);
        }
    }
}